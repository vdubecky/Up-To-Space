using UnityEngine;
using UnityEngine.UI;

public class Vyzkum : MonoBehaviour
{
    [SerializeField]
    private GameObject tapAniamceLab;
    //
    [SerializeField]
    private GameObject potrebneZdroje;
    [SerializeField]
    private Button vyzkumButton;
    [SerializeField]
    private Image panel;
    [SerializeField]
    private Text textButtonu;
    [SerializeField]
    private Text UpgradeAurus;
    [SerializeField]
    private Text UpgradeAntimon;
    [SerializeField]
    private Text UpgradeStars;
    [SerializeField]
    private GameObject[] indikatorScoreLevel;

    private int coinUpgradeLevel = 0;
    //

    //
    [SerializeField]
    private GameObject potrebneZdrojeXPBOOST;
    [SerializeField]
    private Button vyzkumButtonXP;
    [SerializeField]
    private Image panelXP;
    [SerializeField]
    private Text textButtonuXP;
    [SerializeField]
    private Text UpgradeAurusXP;
    [SerializeField]
    private Text UpgradeAntimonXP;
    [SerializeField]
    private Text UpgradeStarsXP;
    [SerializeField]
    private GameObject[] indikatorScoreLevelXP;

    private int XPUpgradeLevel = 0;

    [SerializeField]
    private Text multakMenu;
    [SerializeField]
    private Text multakPlanets;
    //

    [SerializeField]
    private GameObject materializerList;
    [SerializeField]
    private GameObject materializer;
    [SerializeField]
    private GameObject lab;

    [SerializeField]
    private int cenaPowerUpAurus;
    [SerializeField]
    private int cenaPowerUpAntimon;

    private int pocetEMP;
    private int pocetRobotu;
    private int pocetWarpu;

    [SerializeField]
    private Text pocetEMPtext;
    [SerializeField]
    private Text pocetRobotutext;
    [SerializeField]
    private Text pocetWarputext;

    [SerializeField]
    private GameObject bonusGO;
    [SerializeField]
    private Text bonus;

    private void Start()
    {
        coinUpgradeLevel = PlayerPrefs.GetInt("LevelCoinUpgradu");
        XPUpgradeLevel = PlayerPrefs.GetInt("LevelXPUpgradu");
        AktualizacePowerUpu();
        AktualizaceCoin();
        AktualizaceXP();
    }
    public void CoinUpgrade()
    {
        coinUpgradeLevel = PlayerPrefs.GetInt("LevelCoinUpgradu");
        if (coinUpgradeLevel < 4)
        {
            int aurus = PlayerPrefs.GetInt("AurusPocet");
            int antimon = PlayerPrefs.GetInt("AntimonPocet");
            int stars = PlayerPrefs.GetInt("stars");

            if (aurus >= 5 * (coinUpgradeLevel + 1) &&
               antimon >= 5 * (coinUpgradeLevel + 1) &&
               stars >= 150 * (coinUpgradeLevel + 1))
            {
                aurus -= 5 * (coinUpgradeLevel + 1);
                antimon -= 5 * (coinUpgradeLevel + 1);

                GetComponent<Odmeny>().SetStar(150 * (coinUpgradeLevel + 1));
                PlayerPrefs.SetInt("AurusPocet", aurus);
                PlayerPrefs.SetInt("AntimonPocet", antimon);

                coinUpgradeLevel++;
                PlayerPrefs.SetInt("LevelCoinUpgradu", coinUpgradeLevel);
                AktualizaceCoin();
            }
            else
            {
                GetComponent<Nastaveni>().Vibruj();
            }
        }
    }

    public void XPUpgrade()
    {
        XPUpgradeLevel = PlayerPrefs.GetInt("LevelXPUpgradu");

        if (XPUpgradeLevel < 4)
        {
            int aurus = PlayerPrefs.GetInt("AurusPocet");
            int antimon = PlayerPrefs.GetInt("AntimonPocet");
            int stars = PlayerPrefs.GetInt("stars");

            if (aurus >= 5 * (XPUpgradeLevel + 1) &&
               antimon >= 5 * (XPUpgradeLevel + 1) &&
               stars >= 150 * (XPUpgradeLevel + 1))
            {
                aurus -= 5 * (XPUpgradeLevel + 1);
                antimon -= 5 * (XPUpgradeLevel + 1);

                GetComponent<Odmeny>().SetStar(150 * (XPUpgradeLevel + 1));
                PlayerPrefs.SetInt("AurusPocet", aurus);
                PlayerPrefs.SetInt("AntimonPocet", antimon);

                XPUpgradeLevel++;
                PlayerPrefs.SetInt("LevelXPUpgradu", XPUpgradeLevel);
                AktualizaceXP();
            }
            else
            {
                GetComponent<Nastaveni>().Vibruj();
            }
        }
    }

    public void Materializer()
    {
        PlayerPrefs.SetInt("LabTutorial", 1);
        tapAniamceLab.SetActive(false);

        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        lab.SetActive(false);
        materializer.SetActive(true);

        if (PlayerPrefs.GetInt("AutomatickyZobrazitList") == 0)
        {
            materializerList.SetActive(true);
            PlayerPrefs.SetInt("AutomatickyZobrazitList", 1);
            GetComponent<UI>().MaterializerInfo = true;
        }
        else
        {
            materializerList.SetActive(false);
            GetComponent<UI>().MaterializerInfo = false;
        }
    }

    public void KoupitEMP()
    {
        if (PlayerPrefs.GetInt("AurusPocet") >= 2 && PlayerPrefs.GetInt("AntimonPocet") >= 1)
        {
            int pocetAntimon = PlayerPrefs.GetInt("AntimonPocet");
            int pocetAurus = PlayerPrefs.GetInt("AurusPocet");

            pocetAurus -= 2;
            pocetAntimon--;

            PlayerPrefs.SetInt("AurusPocet", pocetAurus);
            PlayerPrefs.SetInt("AntimonPocet", pocetAntimon);

            pocetRobotu = PlayerPrefs.GetInt("PocetRobotu");
            pocetEMP = PlayerPrefs.GetInt("PocetEMP");
            pocetEMP++;
            PlayerPrefs.SetInt("PocetEMP", pocetEMP);
            AktualizacePowerUpu();
            GetComponent<Odmeny>().AktualizaceHodnotPrvky();
        }
        else
        {
            GetComponent<Nastaveni>().Vibruj();
        }
    }

    public void KoupitRobota()
    {
        if (PlayerPrefs.GetInt("AurusPocet") >= 1)
        {
            int pocetAntimon = PlayerPrefs.GetInt("AntimonPocet");
            int pocetAurus = PlayerPrefs.GetInt("AurusPocet");

            pocetAurus--;

            PlayerPrefs.SetInt("AurusPocet", pocetAurus);
            PlayerPrefs.SetInt("AntimonPocet", pocetAntimon);

            pocetRobotu = PlayerPrefs.GetInt("PocetRobotu");

            pocetRobotu++;
            PlayerPrefs.SetInt("PocetRobotu", pocetRobotu);
            AktualizacePowerUpu();
            GetComponent<Odmeny>().AktualizaceHodnotPrvky();
        }
        else
        {
            GetComponent<Nastaveni>().Vibruj();
        }
    }

    public void KoupitWarp()
    {
        if (PlayerPrefs.GetInt("AurusPocet") >= 2 && PlayerPrefs.GetInt("AntimonPocet") >= 1)
        {
            int pocetAntimon = PlayerPrefs.GetInt("AntimonPocet");
            int pocetAurus = PlayerPrefs.GetInt("AurusPocet");

            pocetAurus -= 2;
            pocetAntimon--;

            PlayerPrefs.SetInt("AurusPocet", pocetAurus);
            PlayerPrefs.SetInt("AntimonPocet", pocetAntimon);

            pocetWarpu = PlayerPrefs.GetInt("PocetWarpu");
            pocetWarpu++;
            PlayerPrefs.SetInt("PocetWarpu", pocetWarpu);
            AktualizacePowerUpu();

            GetComponent<Odmeny>().AktualizaceHodnotPrvky();
        }
        else
        {
            GetComponent<Nastaveni>().Vibruj();
        }
    }

    public void AktualizacePowerUpu()
    {
        pocetWarpu = PlayerPrefs.GetInt("PocetWarpu");
        pocetRobotu = PlayerPrefs.GetInt("PocetRobotu");
        pocetEMP = PlayerPrefs.GetInt("PocetEMP");

        pocetEMPtext.text = pocetEMP.ToString();
        pocetRobotutext.text = pocetRobotu.ToString();
        pocetWarputext.text = pocetWarpu.ToString();
    }

    public void Zavrit()
    {
        lab.SetActive(true);
        materializer.SetActive(false);
    }

    public void Lab()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        lab.SetActive(true);
        materializer.SetActive(false);
    }

    public void Otevrit()
    {
        AktualizacePowerUpu();

        if (PlayerPrefs.GetInt("LabTutorial") == 0)
        {
            tapAniamceLab.SetActive(true);
        }
        else
        {
            tapAniamceLab.SetActive(false);
        }
    }

    public void AktualizaceCoin()
    {
        if (coinUpgradeLevel > 0)
        {
            bonusGO.SetActive(true);
            bonus.text = "BONUS x" + (coinUpgradeLevel + 1);
        }

        for (int i = 0; i < 4; i++)
        {
            indikatorScoreLevel[i].SetActive(false);
        }

        for (int i = 0; i < coinUpgradeLevel; i++)
        {
            indikatorScoreLevel[i].SetActive(true);
        }

        GetComponent<Odmeny>().AktualizaceHodnot();
        GetComponent<Odmeny>().AktualizaceHodnotPrvky();

        if (coinUpgradeLevel > 0 && coinUpgradeLevel < 4)
        {
            UpgradeAurus.text = (5 * (coinUpgradeLevel + 1)).ToString();
            UpgradeAntimon.text = (5 * (coinUpgradeLevel + 1)).ToString();
            UpgradeStars.text = (150 * (coinUpgradeLevel + 1)).ToString();
            GetComponent<Odmeny>().Multiplikator = (coinUpgradeLevel + 1);
        }
        else if (coinUpgradeLevel == 4)
        {
            GetComponent<Odmeny>().Multiplikator = (coinUpgradeLevel + 1);
            textButtonu.text = "FULL";
            vyzkumButton.interactable = false;
            potrebneZdroje.SetActive(false);
        }
        else
        {
            UpgradeAurus.text = "5";
            UpgradeAntimon.text = "5";
            UpgradeStars.text = "150";
            GetComponent<Odmeny>().Multiplikator = 1;
        }
    }

    public void AktualizaceXP()
    {
        for (int i = 0; i < 4; i++)
        {
            indikatorScoreLevelXP[i].SetActive(false);
        }

        for (int i = 0; i < XPUpgradeLevel; i++)
        {
            indikatorScoreLevelXP[i].SetActive(true);
        }

        GetComponent<Odmeny>().AktualizaceHodnot();
        GetComponent<Odmeny>().AktualizaceHodnotPrvky();

        if (XPUpgradeLevel > 0 && XPUpgradeLevel < 4)
        {
            multakMenu.text = "x" + (XPUpgradeLevel + 1).ToString();
            multakPlanets.text = "x" + (XPUpgradeLevel + 1).ToString();
            UpgradeAurusXP.text = (5 * (XPUpgradeLevel + 1)).ToString();
            UpgradeAntimonXP.text = (5 * (XPUpgradeLevel + 1)).ToString();
            UpgradeStarsXP.text = (150 * (XPUpgradeLevel + 1)).ToString();
            PlayerPrefs.SetInt("XPMultak", (XPUpgradeLevel + 1));
        }
        else if (XPUpgradeLevel == 4)
        {
            PlayerPrefs.SetInt("XPMultak", 5);
            multakMenu.text = "x5";
            multakPlanets.text = "x5";
            textButtonuXP.text = "FULL";
            vyzkumButtonXP.interactable = false;
            potrebneZdrojeXPBOOST.SetActive(false);
        }
        else
        {
            UpgradeAurusXP.text = "5";
            UpgradeAntimonXP.text = "5";
            UpgradeStarsXP.text = "150";
            PlayerPrefs.SetInt("XPMultak", 1);
            multakMenu.text = "";
            multakPlanets.text = "";
        }
    }
}
