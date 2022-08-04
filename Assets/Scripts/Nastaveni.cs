using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Nastaveni : MonoBehaviour
{
    [SerializeField]
    private Animator animace;
    [SerializeField]
    private Sprite[] zvukImg;
    [SerializeField]
    private Button zvukTlacitko;

    [SerializeField]
    private Sprite[] vibraceImg;
    [SerializeField]
    private Button vibraceTlacitko;

    [SerializeField]
    private Sprite[] hudbaImg;
    [SerializeField]
    private Button hudbaTlacitko;

    private bool otevrit = true;
    private bool zvuk = true;
    private bool vibrace = true;
    private bool hudba = true;
    private bool povoleno = true;

    [SerializeField]
    private AudioClip[] zvuky;
    [SerializeField]
    private AudioSource zdrojZvuku;
    [SerializeField]
    private AudioSource motory;
    [SerializeField]
    private AudioSource zvukPozadi;

    private float time = 5;

    private void Start()
    {
        AktualizujZvuk();
        AktualizujVibrace();
        AktualizujHudbu();
    }

    public void Otevrit()
    {
        if (povoleno)
        {
            if (otevrit)
            {
                animace.Play("SettingsOpen");
            }
            else
            {
                animace.Play("SettingsClose");
            }

            povoleno = false;
            otevrit = !otevrit;
            StartCoroutine(Osetreni());
        }
    }

    private IEnumerator Osetreni()
    {
        yield return new WaitForSeconds(0.3f);
        povoleno = true;
    }

    public void Zvuk()
    {
        zvuk = !zvuk;
        PlayerPrefs.SetInt("Zvuk", zvuk ? 1 : 0);
        AktualizujZvuk();
    }

    public void AktualizujZvuk()
    {
        if (PlayerPrefs.GetInt("Zvuk") == 1)
        {
            zvuk = true;
        }
        else
        {
            zvuk = false;
        }


        if (!zvuk)
        {
            zvukTlacitko.GetComponent<Image>().sprite = zvukImg[0];
        }
        else
        {
            zvukTlacitko.GetComponent<Image>().sprite = zvukImg[1];
        }

    }

    public void Vibrace()
    {
        vibrace = !vibrace;
        PlayerPrefs.SetInt("Vibrace", vibrace ? 1 : 0);
        AktualizujVibrace();
    }

    public void AktualizujVibrace()
    {
        if (PlayerPrefs.GetInt("Vibrace") == 1)
        {
            vibrace = true;
        }
        else
        {
            vibrace = false;
        }

        if (!vibrace)
        {
            vibraceTlacitko.GetComponent<Image>().sprite = vibraceImg[0];
        }
        else
        {
            vibraceTlacitko.GetComponent<Image>().sprite = vibraceImg[1];
        }
    }

    public void Hudba()
    {
        hudba = !hudba;
        PlayerPrefs.SetInt("Hudba", hudba ? 1 : 0);
        AktualizujHudbu();
    }

    public void AktualizujHudbu()
    {
        if (PlayerPrefs.GetInt("Hudba") == 1)
        {
            hudba = true;
        }
        else
        {
            hudba = false;
        }

        if (!hudba)
        {
            hudbaTlacitko.GetComponent<Image>().sprite = hudbaImg[0];
        }
        else
        {
            hudbaTlacitko.GetComponent<Image>().sprite = hudbaImg[1];
        }
    }

    public void StopniZvukovyEfektMotoru()
    {
        if (motory.isPlaying)
        {
            motory.Stop();
        }
    }

    public void ZvukovyEfektMotoru()
    {
        if (!zvuk)
        {
            motory.Play();
        }
    }

    public void ZvukovyEfekt(int typ)
    {
        if (!zvuk)
        {
            zdrojZvuku.clip = zvuky[typ];
            zdrojZvuku.Play();
        }
    }

    public void Vibruj()
    {
        if (!vibrace)
        {
            Handheld.Vibrate();
        }
    }

    public IEnumerator NastavZvuk(bool stav)
    {
        if (!hudba)
        {
            if (stav)
            {
                zvukPozadi.Play();
            }

            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                if (stav)
                {
                    zvukPozadi.volume = Mathf.Lerp(zvukPozadi.volume, 0.2f, (elapsedTime / time));
                }
                else
                {
                    zvukPozadi.volume = Mathf.Lerp(zvukPozadi.volume, 0, (elapsedTime / time));
                }

                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (zvukPozadi.volume == 0)
            {
                zvukPozadi.Stop();
            }
        }
    }

    public IEnumerator NastavPauzuZvuku(bool stav)
    {
        if (!hudba)
        {
            if (!stav)
            {
                zvukPozadi.UnPause();
            }

            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                if (stav)
                {
                    zvukPozadi.volume = Mathf.Lerp(zvukPozadi.volume, 0, (elapsedTime / time));
                }
                else
                {
                    zvukPozadi.volume = Mathf.Lerp(zvukPozadi.volume, 0.2f, (elapsedTime / time));
                }

                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (zvukPozadi.volume == 0)
            {
                zvukPozadi.Pause();
            }
        }
    }
}
