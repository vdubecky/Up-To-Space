using System.Collections;
using UnityEngine;

public class Udalosti : MonoBehaviour
{
    [Header("Nastavení událostí")]
    [SerializeField]
    private GameObject hrac;
    [SerializeField]
    private GameObject canv;
    [SerializeField]
    private Animator novaPlanetaAnimace;
    [SerializeField]
    private GameObject novaPlaneta;

    public bool Tlacitko { get; set; }

    public bool PowerUpStart { get; set; } = false;

    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    public void Let()
    {
        int skore = GetComponent<Score>().GetScore();

        if (skore % 5 != 4 && skore % 5 != 0 && skore % 5 != 1 && skore % 12 != 0)
        {
            int generovaniPlanety;

            if (PlayerPrefs.GetInt("PocetPlanet") == 0)
            {
                generovaniPlanety = Random.Range(0, 5);
            }
            else
            {
                generovaniPlanety = Random.Range(0, 150);
            }

            if (generovaniPlanety == 1)
            {
                Tlacitko = false;
                GetComponent<GenerovaniObjektu>().GenerovatPlanetu = true;
            }
            else
            {
                GetComponent<Score>().SetScore('+');
            }
        }
        else
        {
            GetComponent<Score>().SetScore('+');
        }

        Camera.main.GetComponent<NastaveniKamery>().SetZmena(6);
        GetComponent<GenerovaniObjektu>().GenerovatScore(hrac.GetComponent<Hrac>().GetVyslednaPozice());

        if (!PowerUpStart) StartCoroutine(OsetreniStlaceni());
        else StartCoroutine(OsetreniStlaceniPowerUpStart());
    }

    private int PocetUFO()
    {
        return GameObject.FindGameObjectsWithTag("EnemyShipRodic").Length;
    }

    public void Tap()
    {
        if (Tlacitko)
        {
            int skore = GetComponent<Score>().GetScore();
            hrac.GetComponent<Hrac>().ModFight = false;

            if (skore == 1)
            {
                hrac.GetComponent<PowerUpSystem>().StartPowerUp = true;
                hrac.GetComponent<PowerUpSystem>().Otevrit();
            }
            else if (skore > 0 && PocetUFO() != 0)
            {
                hrac.GetComponent<Hrac>().ModFight = true;
            }
            else if (skore % 11 == 0 && skore % 5 != 0)
            {
                hrac.GetComponent<Hrac>().ModFight = false;
                GetComponent<GenerovaniObjektu>().GenerovatScoreBonus = true;
            }

            if (PowerUpStart)
            {
                hrac.GetComponent<Hrac>().TapPowerUp();
            }
            else
            {
                hrac.GetComponent<Hrac>().Tap();
            }

            if (!hrac.GetComponent<Hrac>().ModFight)
            {
                Let();
            }
            else
            {
                StartCoroutine(OsetreniStlaceni_Fight());
            }

            Tlacitko = false;
        }

        Tlacitko = false;
    }

    public void GameOver()
    {
        Tlacitko = false;
        StopAllCoroutines();
        Camera.main.GetComponent<NastaveniKamery>().SetZmena(5);
    }

    private IEnumerator OsetreniStlaceni_Fight()
    {
        yield return new WaitForSeconds(0.4f);
        hrac.GetComponent<Hrac>().AktualniPozice = hrac.transform.position;

        if (!hrac.GetComponent<Hrac>().Pritah1 && !GetComponent<Fight>().PoZniceni1)
        {
            Tlacitko = true;
        }
    }

    private IEnumerator NovaPlanetaDialog()
    {
        yield return new WaitForSeconds(2f);
        novaPlanetaAnimace.SetBool("Planeta", false);
        novaPlanetaAnimace.SetBool("Zpet", true);
        yield return new WaitForSeconds(1);
        novaPlanetaAnimace.SetBool("Zpet", false);
        novaPlaneta.transform.position = new Vector3(0, 348, 0);
        novaPlaneta.SetActive(false);
        Tlacitko = true;
    }

    private IEnumerator OsetreniStlaceni()
    {
        yield return new WaitForSeconds(1);

        if (GetComponent<GenerovaniObjektu>().GenerovatPlanetu)
        {
            Tlacitko = false;
            novaPlaneta.SetActive(true);
            novaPlanetaAnimace.SetBool("Planeta", true);
            StartCoroutine(NovaPlanetaDialog());
        }
        else
        {
            Tlacitko = true;
        }

        GetComponent<GenerovaniObjektu>().Generovat();
        GetComponent<GenerovaniObjektu>().ObnoveniPozic();
        Camera.main.GetComponent<NastaveniKamery>().SetZmena(5);
    }

    private IEnumerator OsetreniStlaceniPowerUpStart()
    {
        Tlacitko = false;
        yield return new WaitForSeconds(1);

        GetComponent<GenerovaniObjektu>().Generovat();
        GetComponent<GenerovaniObjektu>().ObnoveniPozic();

        yield return new WaitForSeconds(1);

        int nahodneSkore = Random.Range(2, 10);

        while (nahodneSkore % 5 == 0)
        {
            nahodneSkore = Random.Range(2, 10);
        }

        GetComponent<Score>().SetScore(nahodneSkore);
        GetComponent<GenerovaniObjektu>().GenerovatScore(hrac.GetComponent<Hrac>().GetVyslednaPozice());
        GetComponent<GenerovaniObjektu>().Generovat();
        GetComponent<GenerovaniObjektu>().ObnoveniPozic();
        Camera.main.GetComponent<NastaveniKamery>().SetZmena(5);

        yield return new WaitForSeconds(0.3f);
        Tlacitko = true;
    }
}
