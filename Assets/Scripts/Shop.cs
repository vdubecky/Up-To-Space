using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePoKoupi;
    [SerializeField]
    private int[] ceny;
    [SerializeField]
    private GameObject[] polozkyNakup;
    [SerializeField]
    private GameObject[] polozkyNakupPouziti;
    [SerializeField]
    private GameObject[] pozadi;
    [SerializeField]
    private Image[] nahled;

    [SerializeField]
    private Color32 pozadiKoupeneho;

    [SerializeField]
    private GameObject[] dailyLodButton;
    [SerializeField]
    private Image[] dailyLodNahled;

    [SerializeField]
    private Text stavba1;
    [SerializeField]
    private Text stavba2;
    [SerializeField]
    private Text stavba3;

    private void Start()
    {
        Aktualizace();
        AktualizaceLodi();

        if (PlayerPrefs.GetInt("aktualni") > 0)
        {
            Pouzit(PlayerPrefs.GetInt("aktualni"));
        }
    }

    public void DailyRewardLod(int polozkaID)
    {
        int dailyLod = PlayerPrefs.GetInt("PocetDailyLodi");
        dailyLod++;
        PlayerPrefs.SetInt("PocetDailyLodi", dailyLod);
        PlayerPrefs.SetInt("d" + polozkaID.ToString(), 1);
        AktualizaceLodi();
    }

    private void AktualizaceLodi()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("d" + i) == 1)
            {
                dailyLodButton[i - 1].SetActive(true);
                dailyLodNahled[i - 1].color = Color.white;
            }
        }
    }

    public void Koupit(int polozkaID)
    {
        int pocetKoupenych = PlayerPrefs.GetInt("PocetKoupenych");

        if (PlayerPrefs.GetInt("stars") >= ceny[polozkaID] - 1)
        {
            pocetKoupenych++;
            PlayerPrefs.SetInt("PocetKoupenych", pocetKoupenych);
            GameObject particle = Instantiate(particlePoKoupi, nahled[polozkaID].transform.position, Quaternion.identity);
            particle.transform.SetParent(nahled[polozkaID].transform);
            particle.transform.localScale = new Vector3(1, 1, 1);
            Destroy(particle, 2);

            PlayerPrefs.SetInt(polozkaID.ToString(), 1);
            GetComponent<Odmeny>().SetStar(ceny[polozkaID]);
            Aktualizace();
        }
        else
        {
            GetComponent<Nastaveni>().Vibruj();
        }
    }

    public void StavbaLodi(int ktera)
    {
        switch (ktera)
        {
            case 1:
                {
                    int stavbaLodi1 = PlayerPrefs.GetInt("stavbaLodiJedna");
                    stavbaLodi1++;
                    PlayerPrefs.SetInt("stavbaLodiJedna", stavbaLodi1);
                }
                break;
            case 2:
                {
                    int stavbaLodi2 = PlayerPrefs.GetInt("stavbaLodiDva");
                    stavbaLodi2++;
                    PlayerPrefs.SetInt("stavbaLodiDva", stavbaLodi2);
                }
                break;
            case 3:
                {
                    int stavbaLodi3 = PlayerPrefs.GetInt("stavbaLodiTri");
                    stavbaLodi3++;
                    PlayerPrefs.SetInt("stavbaLodiTri", stavbaLodi3);
                }
                break;
        }

        Aktualizace();
    }

    private void Aktualizace()
    {
        for (int i = 0; i < polozkyNakup.Length - 3; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                polozkyNakup[i].SetActive(false);
                polozkyNakupPouziti[i].SetActive(true);
                nahled[i].color = Color.white;
                nahled[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            }
        }

        if (PlayerPrefs.GetInt("stavbaLodiJedna") >= 40)
        {
            polozkyNakupPouziti[9].SetActive(true);
            nahled[9].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            stavba1.text = "";
        }
        else
        {
            stavba1.text = PlayerPrefs.GetInt("stavbaLodiJedna").ToString() + " / 40";
        }

        if (PlayerPrefs.GetInt("stavbaLodiDva") >= 80)
        {
            polozkyNakupPouziti[10].SetActive(true);
            nahled[10].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            stavba2.text = "";
        }
        else
        {
            stavba2.text = PlayerPrefs.GetInt("stavbaLodiDva").ToString() + " / 80";
        }

        if (PlayerPrefs.GetInt("stavbaLodiTri") >= 120)
        {
            polozkyNakupPouziti[11].SetActive(true);
            nahled[11].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            stavba3.text = "";
        }
        else
        {
            stavba3.text = PlayerPrefs.GetInt("stavbaLodiTri").ToString() + " / 120";
        }
    }

    public void Pouzit(int polozkaID)
    {
        foreach (GameObject _pozadi in pozadi)
        {
            _pozadi.SetActive(false);
        }

        try
        {
            pozadi[polozkaID].SetActive(true);
        }
        catch
        {

        }
    }
}
