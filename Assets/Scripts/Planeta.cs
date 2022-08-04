using System;
using UnityEngine;
using UnityEngine.UI;

public class Planeta : MonoBehaviour
{
    private DateTime cil;

    public Image prubeh;

    private float rychlostZmeny;
    private bool sklidit = false;

    public GameObject body;
    public GameObject vyroba;
    public GameObject stop;

    private GameObject manager;
    private float vyslednaOdmena = 0;
    
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        cil = Datum();
        sklidit = false;

        manager.GetComponent<UI>().UpozorneniPlanety(false);
        GetComponent<Animator>().SetBool("Upozornit", false);
    }

    public void NastavCas()
    {
        cil = DateTime.Now;
        cil = cil.AddSeconds(86400);

        PlayerPrefs.SetString("dobaVyroby" + Jmeno(), cil.ToString());
    }

    public void Tlacitko()
    {
        if(PlayerPrefs.GetInt(Jmeno() + "l") > 0)
        {         
            if (vyslednaOdmena <= PlayerPrefs.GetInt(Jmeno() + "l") * 0.01f)
            {         
                manager.GetComponent<SpravaPlanet>().OtevritInfo(Jmeno());
                sklidit = false;
            }
            else
            {
                manager.GetComponent<XPsystem>().PridejXP(vyslednaOdmena);
                NastavCas();
                sklidit = true;
            }
        }
        else
        {
            manager.GetComponent<SpravaPlanet>().OtevritInfo(Jmeno());
            manager.GetComponent<UI>().ZavriTutorialPlanet();
            PlayerPrefs.SetInt("NovaPlanetaTutorial", 0);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(Jmeno() + "l") > 0)
        {
            vyslednaOdmena = Sklidit();
            body.SetActive(true);
            vyroba.SetActive(true);
            stop.SetActive(false);

            body.GetComponent<Text>().text = vyslednaOdmena.ToString() + " / " + (PlayerPrefs.GetInt(Jmeno() + "l") * 3 * 24).ToString();

            if (!sklidit)
            {
                if (cil >= DateTime.Now)
                {
                    prubeh.fillAmount = vyslednaOdmena / (PlayerPrefs.GetInt(Jmeno() + "l") * 3 * 24);
                }
                else
                {
                    vyroba.SetActive(false);
                    stop.SetActive(true);
                    prubeh.fillAmount = 1;
                    manager.GetComponent<UI>().UpozorneniPlanety(true);
                    GetComponent<Animator>().SetBool("Upozornit", true);
                }
            }
            else
            {
                manager.GetComponent<UI>().UpozorneniPlanety(false);
                GetComponent<Animator>().SetBool("Upozornit", false);
                prubeh.fillAmount = Mathf.Lerp(prubeh.fillAmount, 0, rychlostZmeny * Time.deltaTime);
                rychlostZmeny += 0.3f;

                if (prubeh.fillAmount <= 0.1f)
                {
                    rychlostZmeny = 0;
                    sklidit = false;
                }
            }
        }
        else
        {
            prubeh.fillAmount = 0;
        }
    }

    private float Sklidit()
    {
        float odmena = 0;

        if(cil <= DateTime.Now)
        {
            odmena = 24 * (PlayerPrefs.GetInt(Jmeno() + "l") * 3);
        }
        else
        {
            TimeSpan rozdil = cil - DateTime.Now;
            float ziskZaSekundu = (PlayerPrefs.GetInt(Jmeno() + "l") * 3) / (float)3600;
            odmena = (86400 - (int)rozdil.TotalSeconds) * ziskZaSekundu;
            odmena = (float)Math.Round(odmena, 2);
        }

        return odmena;
    }

    private string Jmeno()
    {
        string jmeno = gameObject.name;
        jmeno = jmeno.Replace("(Clone)", "");

        return jmeno;
    }

    private DateTime Datum()
    {
        if (DateTime.TryParse(PlayerPrefs.GetString("dobaVyroby" + Jmeno()), out DateTime vystup))
        {
            return vystup;
        }

        return DateTime.Now;
    }
}
