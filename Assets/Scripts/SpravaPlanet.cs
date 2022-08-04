using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpravaPlanet : MonoBehaviour
{
    [SerializeField]
    private Sprite[] nahledy;
    [SerializeField]
    private Transform cil;
    [SerializeField]
    private GameObject vzor;
    [SerializeField]
    private GameObject prazdna;

    private int pocetPlanet = 0;

    private List<GameObject> Planety;

    [SerializeField]
    private GameObject dialog;
    [SerializeField]
    private Image nahledPlanety;
    [SerializeField]
    private Text jmenoPlanety;
    [SerializeField]
    private Text aktualniLevel;
    [SerializeField]
    private Text aktualniProdukce;
    [SerializeField]
    private Text dalsiProdukce;
    [SerializeField]
    private Text cena;
    [SerializeField]
    private Text aktualniKapacita;
    [SerializeField]
    private Text dalsiKapacita;

    [SerializeField]
    private Transform content;

    private int x = 0;

    private void Start()
    {
        Planety = new List<GameObject>();
        x = 0;
        pocetPlanet = PlayerPrefs.GetInt("PocetPlanet");

        for (int i = 1; i <= pocetPlanet; i++)
        {
            NovaPlaneta(PlayerPrefs.GetString(i.ToString() + "j"), nahledy[PlayerPrefs.GetInt(i.ToString())]);
        }
    }

    public List<GameObject> VratPlanety()
    {
        return Planety;
    }

    public void PridejPlanetu(Sprite nahled)
    {
        pocetPlanet++;
        string dnes = DateTime.Now.DayOfWeek.ToString();
        string jmeno = "U" + UnityEngine.Random.Range(100, 999).ToString() + dnes[0] + "-" + pocetPlanet.ToString();

        PlayerPrefs.SetInt("bestPlanets", 1);
        PlayerPrefs.SetInt("PocetPlanet", pocetPlanet);
        PlayerPrefs.SetString(pocetPlanet.ToString() + "j", jmeno);

        switch (nahled.name)
        {
            case "planeta":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 0);
                break;
            case "planeta2":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 1);
                break;
            case "planeta3":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 2);
                break;
            case "planeta4":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 3);
                break;
            case "planeta5":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 4);
                break;
            case "planeta6":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 5);
                break;
            case "planeta7":
                PlayerPrefs.SetInt(pocetPlanet.ToString(), 6);
                break;
        }

        Planety.Add(NovaPlaneta(jmeno, nahled));
    }

    private void AktualizaceTextu(string jmeno)
    {
        aktualniLevel.text = "Level: " + PlayerPrefs.GetInt(jmeno + "l").ToString();
        aktualniProdukce.text = "XP Production: " + (PlayerPrefs.GetInt(jmeno + "l") * 3).ToString() + " / hour";
        dalsiProdukce.text = "XP Production: " + ((PlayerPrefs.GetInt(jmeno + "l") + 1) * 3).ToString() + " / hour";

        aktualniKapacita.text = "XP Capacity: " + (PlayerPrefs.GetInt(jmeno + "l") * 3 * 24).ToString();
        dalsiKapacita.text = "XP Capacity: " + ((PlayerPrefs.GetInt(jmeno + "l") + 1) * 3 * 24).ToString();
    }

    public void Close()
    {
        dialog.SetActive(false);
    }

    private GameObject NovaPlaneta(string jmeno, Sprite nahled)
    {
        GameObject novaPlaneta = vzor;
        novaPlaneta.transform.GetChild(0).GetComponent<Text>().text = jmeno;
        novaPlaneta.transform.GetChild(1).GetComponent<Image>().sprite = nahled;
        novaPlaneta.name = jmeno;

        x++;

        if (x % 2 == 1)
        {
            Vypis(novaPlaneta);
            Vypis(prazdna);
        }
        else
        {
            Vypis(prazdna);
            Vypis(novaPlaneta);
        }

        return novaPlaneta;
    }

    private void Vypis(GameObject planeta)
    {
        Instantiate(planeta, cil);
    }


    public void OtevritInfo(string jmeno)
    {
        pocetPlanet = PlayerPrefs.GetInt("PocetPlanet");
        dialog.SetActive(true);
        dialog.name = jmeno;

        for (int i = 1; i <= pocetPlanet; i++)
        {
            if (PlayerPrefs.GetString(i.ToString() + "j") == jmeno)
            {
                AktualizaceTextu(jmeno);

                nahledPlanety.sprite = nahledy[PlayerPrefs.GetInt(i.ToString())];
                break;
            }
        }

        cena.text = ((PlayerPrefs.GetInt(jmeno + "l") + 1) * 50).ToString();
        jmenoPlanety.text = jmeno;
    }

    public void Upgrade()
    {
        string jmeno = dialog.name;
        int level = PlayerPrefs.GetInt(jmeno + "l");

        if (GetComponent<Odmeny>().Star >= (level + 1) * 50)
        {
            GetComponent<Odmeny>().SetStar((level + 1) * 50);
            level++;
            PlayerPrefs.SetInt(jmeno + "l", level);
            AktualizaceTextu(jmeno);

            cena.text = ((PlayerPrefs.GetInt(jmeno + "l") + 1) * 50).ToString();
        }
        else
        {
            GetComponent<Nastaveni>().Vibruj();
        }

        if (level == 1)
        {
            GameObject planeta = GameObject.Find(jmeno + "(Clone)");
            planeta.GetComponent<Planeta>().NastavCas();
        }
    }
}

