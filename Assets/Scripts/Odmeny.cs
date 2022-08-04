using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Odmeny : MonoBehaviour
{
    [SerializeField]
    private Text starText;
    [SerializeField]
    private Text starTextGO;
    [SerializeField]
    private Text starShop;
    [SerializeField]
    private Text starPlanets;
    [SerializeField]
    private Text starLab;

    [SerializeField]
    private Text antimon;
    [SerializeField]
    private Text aurus;

    [SerializeField]
    private Text cenaPokracovani;

    public int Star { get; set; }
    public int Aurus { get; set; }
    public int Antimon { get; set; }
    public int Multiplikator { get; set; } = 1;

    private void Start()
    {
        Star = PlayerPrefs.GetInt("stars");
        Antimon = PlayerPrefs.GetInt("AntimonPocet");
        Aurus = PlayerPrefs.GetInt("AurusPocet");

        AktualizaceHodnotPrvky();
        AktualizaceHodnot();
    }

    public void SetAurus()
    {
        Aurus = PlayerPrefs.GetInt("AurusPocet");
        Aurus++;
        PlayerPrefs.SetInt("AurusPocet", Aurus);
        AktualizaceHodnotPrvky();
    }

    public void SetAurus(int kolik)
    {
        Aurus = PlayerPrefs.GetInt("AurusPocet");
        Aurus += kolik;
        PlayerPrefs.SetInt("AurusPocet", Aurus);
        AktualizaceHodnotPrvky();
    }

    public void SetAntimon()
    {
        Antimon = PlayerPrefs.GetInt("AntimonPocet");
        Antimon++;
        PlayerPrefs.SetInt("AntimonPocet", Antimon);
        AktualizaceHodnotPrvky();
    }

    public void SetAntimon(int kolik)
    {
        Antimon = PlayerPrefs.GetInt("AntimonPocet");
        Antimon += kolik;
        PlayerPrefs.SetInt("AntimonPocet", Antimon);
        AktualizaceHodnotPrvky();
    }

    public void AktualizaceHodnotPrvky()
    {
        antimon.text = PlayerPrefs.GetInt("AntimonPocet").ToString();
        aurus.text = PlayerPrefs.GetInt("AurusPocet").ToString();
    }

    public void SetStar()
    {
        Star += Multiplikator;
        PlayerPrefs.SetInt("stars", Star);
        AktualizaceHodnot();
    }

    public void SetStar(int hodnota)
    {
        Star -= hodnota;
        PlayerPrefs.SetInt("stars", Star);

        AktualizaceHodnot();
    }

    public bool Continue(int n)
    {
        int cena = n;
        int pocetRobotu = PlayerPrefs.GetInt("PocetRobotu");

        if (cena <= pocetRobotu)
        {
            pocetRobotu -= cena;
            AktualizaceHodnot();
            GetComponent<Vyzkum>().AktualizacePowerUpu();
            PlayerPrefs.SetInt("PocetRobotu", pocetRobotu);
            AktualizaceHodnot(cena);
            AktualizaceHodnot();
            return true;
        }

        GetComponent<Nastaveni>().Vibruj();
        return false;
    }

    public void AktualizaceHodnot(int cena)
    {
        cenaPokracovani.text = (cena + 1).ToString() + " / " + PlayerPrefs.GetInt("PocetRobotu");
        ShopButton();
    }

    public void AktualizaceHodnot()
    {
        Star = PlayerPrefs.GetInt("stars");
        starText.text = Star.ToString();
        starTextGO.text = Star.ToString();
        starPlanets.text = Star.ToString();
        starLab.text = Star.ToString();
        starShop.text = PlayerPrefs.GetInt("stars").ToString();
        PlayerPrefs.SetInt("stars", Star);
    }

    public void ShopButton()
    {
        starShop.text = PlayerPrefs.GetInt("stars").ToString();
    }
}
