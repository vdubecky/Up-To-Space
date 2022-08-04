using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DenniOdmeny : MonoBehaviour
{
    [SerializeField]
    private GameObject uiDailyParticle;
    [SerializeField]
    private Text text;

    private TimeSpan zbyva;
    private DateTime cil;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator animaceUnvitr;

    [SerializeField]
    private GameObject daily;

    [SerializeField]
    private GameObject[] bg;
    [SerializeField]
    private GameObject bgPosledniho;
    [SerializeField]
    private Text[] dny;

    private int pocetDni = 0;
    private int pocetOdmen = 0;

    [SerializeField]
    private GameObject lodeOdmena;
    [SerializeField]
    private GameObject ostatniOdmeny;
    [SerializeField]
    private Image icona;
    [SerializeField]
    private Sprite[] ikonyLodi;

    [SerializeField]
    private GameObject[] polozky;

    private bool odmenyMimoLod = false;

    private void Start()
    {
        cil = Datum();
        Aktualizace(0);
        PocetDni();
    }

    private void Update()
    {
        if (cil > DateTime.Now)
        {
            if (animator.isActiveAndEnabled)
            {
                animator.SetFloat("DailyRewardOpen", 0);
                animator.SetBool("DailyRewardOpen", false);
            }

            zbyva = cil - DateTime.Now;
            text.text = zbyva.ToString(@"hh\:mm\:ss");
        }
        else
        {
            animator.SetBool("DailyRewardOpen", true);
            text.text = "Daily Reward";
        }
    }

    //12.04.2019 23:37:10
    private DateTime Datum()
    {
        if (DateTime.TryParse(PlayerPrefs.GetString("dat"), out DateTime vystup))
        {
            return vystup;
        }

        return DateTime.Now;
    }

    private void Aktualizace(int typ)
    {
        int pocet = PlayerPrefs.GetInt("odmeny");

        for (int i = 0; i < pocet; i++)
        {
            bg[i].SetActive(true);
        }

        if (typ == 6)
        {
            bgPosledniho.SetActive(true);
        }
    }

    private void PocetDni()
    {
        int pocet = PlayerPrefs.GetInt("pocetDni");

        if (pocet > 10)
        {
            icona.sprite = ikonyLodi[2];
        }
        else if (pocet > 5)
        {
            icona.sprite = ikonyLodi[1];
        }
        else
        {
            icona.sprite = ikonyLodi[0];
        }

        for (int i = 0; i < pocet; i++)
        {
            if ((i + 1) % 5 == 0 && i != 0)
            {
                dny[i].color = Color.yellow;
                GetComponent<Shop>().DailyRewardLod((i + 1) / 5);
            }
            else
            {
                dny[i].color = Color.white;
            }
        }
    }

    public void Tlacitko()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        if (cil <= DateTime.Now)
        {
            pocetOdmen = PlayerPrefs.GetInt("odmeny");
            pocetOdmen++;

            pocetDni = PlayerPrefs.GetInt("pocetDni");

            if (pocetDni < 15)
            {
                pocetDni++;

                PlayerPrefs.SetInt("pocetDni", pocetDni);
                PocetDni();
            }
            else
            {
                lodeOdmena.SetActive(false);
                ostatniOdmeny.SetActive(true);
                odmenyMimoLod = true;
            }

            if (pocetOdmen > bg.Length)
            {
                for (int i = 0; i < bg.Length; i++)
                {
                    bg[i].SetActive(false);
                }

                bgPosledniho.SetActive(false);
                pocetOdmen = 1;
            }

            PlayerPrefs.SetInt("odmeny", pocetOdmen);

            cil = DateTime.Now;
            cil = cil.AddSeconds(86400);
            text.text += cil.ToString() + "\n";
            PlayerPrefs.SetString("dat", cil.ToString());

            StartCoroutine(Animace(pocetOdmen));
        }

        daily.SetActive(true);
    }

    private IEnumerator Animace(int typ)
    {
        yield return new WaitForSeconds(0.01f);

        if (typ < 5)
        {
            GetComponent<Odmeny>().SetStar(-(10 + ((typ - 1) * 5)));
        }
        else if (typ == 5 && odmenyMimoLod)
        {
            GetComponent<Odmeny>().SetStar(-(10 + ((typ - 1) * 5)));
            typ = 6;
        }

        GameObject particle = Instantiate(uiDailyParticle, bg[typ - 1].transform.position, Quaternion.identity);
        particle.transform.SetParent(polozky[typ - 1].transform);
        particle.transform.localScale = new Vector3(1, 1, 1);
        Destroy(particle, 5);

        animaceUnvitr.SetInteger("Typ", typ);

        yield return new WaitForSeconds(1f);
        Aktualizace(typ);
    }

    public void Close()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        foreach (GameObject polozka in polozky)
        {
            polozka.transform.localScale = new Vector3(1, 1, 1);
        }

        daily.SetActive(false);
        GetComponent<UI>().VDailyReward = false;
    }
}
