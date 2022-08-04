using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Materializator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem efektMaterializatoru;
    [SerializeField]
    private GameObject efektMaterializatoruGO;
    [SerializeField]
    private Transform cil;
    [SerializeField]
    private GameObject logCelk;
    [SerializeField]
    private Transform listBox;

    [SerializeField]
    private ScrollRect scrollRect;
    private string log;

    private bool obnov = false;

    [SerializeField]
    private Sprite failed;
    [SerializeField]
    private Sprite star1;
    [SerializeField]
    private Sprite star2;
    [SerializeField]
    private Sprite star3;
    [SerializeField]
    private Sprite starJackpot;
    [SerializeField]
    private Sprite aurus;
    [SerializeField]
    private Sprite antimon;
    [SerializeField]
    private Sprite aurusJackpot;
    [SerializeField]
    private Sprite antimonJackpot;
    [SerializeField]
    private Sprite lod1;
    [SerializeField]
    private Sprite lod2;
    [SerializeField]
    private Sprite lod3;
    [SerializeField]
    private Sprite emp;
    [SerializeField]
    private Sprite ai;
    [SerializeField]
    private Sprite warp;

    private GameObject starAnim;
    [SerializeField]
    private GameObject starAnimMed;
    [SerializeField]
    private GameObject starAnimMax;
    [SerializeField]
    private GameObject starAnimJackpot;
    [SerializeField]
    private GameObject antimonAnim;
    [SerializeField]
    private GameObject aurusAnim;
    [SerializeField]
    private GameObject antimonAnimJackpot;
    [SerializeField]
    private GameObject aurusAnimJackpot;
    [SerializeField]
    private GameObject lod1Anim;
    [SerializeField]
    private GameObject lod2Anim;
    [SerializeField]
    private GameObject lod3Anim;
    [SerializeField]
    private GameObject EMPanim;
    [SerializeField]
    private GameObject Warpanim;
    [SerializeField]
    private GameObject AIanim;

    private bool tlacitko = true;

    private enum MozneOdmeny
    {
        WARP,
        AI,
        HOMING,
        COIN15,
        COIN10,
        COIN5,
        LOD1,
        LOD2,
        LOD3,
        AURUS,
        ANTIMON,
        JACKPOTCOIN,
        JACKPOTAURUS,
        JACKPOTANTIMON
    }

    private IEnumerator RestartAnimace()
    {
        yield return new WaitForSeconds(0.4f);
        tlacitko = true;
    }

    private void Start()
    {
        tlacitko = true;
    }


    private void Odmena(int odmena, Sprite sprite)
    {
        switch (odmena)
        {
            case 0:
                {
                    log = "1x Warp start";
                    int pocetWarpu = PlayerPrefs.GetInt("PocetWarpu");

                    pocetWarpu++;
                    PlayerPrefs.SetInt("PocetWarpu", pocetWarpu);
                    GetComponent<Vyzkum>().AktualizacePowerUpu();

                    Instantiate(Warpanim, cil);
                }
                break;
            case 1:
                {
                    log = "1x AI";
                    int pocetRobotu = PlayerPrefs.GetInt("PocetRobotu");

                    pocetRobotu++;
                    PlayerPrefs.SetInt("PocetRobotu", pocetRobotu);
                    GetComponent<Vyzkum>().AktualizacePowerUpu();

                    Instantiate(AIanim, cil);
                }
                break;
            case 2:
                {
                    log = "1x Homing missile";
                    int pocetEMP = PlayerPrefs.GetInt("PocetEMP");

                    pocetEMP++;
                    PlayerPrefs.SetInt("PocetEMP", pocetEMP);
                    GetComponent<Vyzkum>().AktualizacePowerUpu();

                    Instantiate(EMPanim, cil);
                }
                break;
            case 3:
                {
                    log = "15x coins";
                    GetComponent<Odmeny>().SetStar(-15);
                    Instantiate(starAnimMax, cil);
                }
                break;
            case 4:
                {
                    log = "10x coins";
                    GetComponent<Odmeny>().SetStar(-10);
                    Instantiate(starAnimMed, cil);
                }
                break;
            case 5:
                {
                    log = "5x coins";
                    GetComponent<Odmeny>().SetStar(-5);
                    Instantiate(starAnim, cil);
                }
                break;
            case 6:
                {
                    Instantiate(lod1Anim, cil);
                    GetComponent<Shop>().StavbaLodi(1);
                    log = "1x Ship part";
                }
                break;
            case 7:
                {
                    Instantiate(lod2Anim, cil);
                    GetComponent<Shop>().StavbaLodi(2);
                    log = "1x Ship part";
                }
                break;
            case 8:
                {
                    Instantiate(lod3Anim, cil);
                    GetComponent<Shop>().StavbaLodi(3);
                    log = "1x Ship part";
                }
                break;
            case 9:
                {
                    log = "1x Aurus";
                    Instantiate(aurusAnim, cil);
                    GetComponent<Odmeny>().SetAurus();
                }
                break;
            case 10:
                {
                    log = "1x Antimon";
                    GetComponent<Odmeny>().SetAntimon();
                    Instantiate(antimonAnim, cil);
                }
                break;
            case 11:
                {
                    log = "1000x COINS JACKPOT!";
                    GetComponent<Odmeny>().SetStar(-1000);
                    Instantiate(starAnimJackpot, cil);
                }
                break;
            case 12:
                {
                    log = "20x AURUS JACKPOT!";
                    GetComponent<Odmeny>().SetAurus(20);
                    Instantiate(aurusAnimJackpot, cil);
                }
                break;
            case 13:
                {
                    log = "15x ANTIMON JACKPOT!";
                    GetComponent<Odmeny>().SetAntimon(15);
                    Instantiate(antimonAnimJackpot, cil);
                }
                break;
        }

        logCelk.transform.GetChild(3).GetComponent<Image>().sprite = sprite;
        logCelk.transform.GetChild(3).GetComponent<Image>().rectTransform.sizeDelta = new Vector3(50, 50);
    }

    public void Tlacitko()
    {
        if (tlacitko && PlayerPrefs.GetInt("stars") >= 10)
        {
            efektMaterializatoruGO.SetActive(true);
            GetComponent<Nastaveni>().ZvukovyEfekt(1);
            GetComponent<Odmeny>().SetStar(10);
            GameObject[] animace = GameObject.FindGameObjectsWithTag("MatAnimace");

            foreach (GameObject anim in animace)
            {
                Destroy(anim);
            }

            int nahoda = Random.Range(0, 180); // 0 az 150

            if (nahoda > 175)
            {
                Odmena((int)MozneOdmeny.WARP, warp);
            }
            else if (nahoda > 170)
            {
                Odmena((int)MozneOdmeny.AI, ai);
            }
            else if (nahoda > 165)
            {
                Odmena((int)MozneOdmeny.HOMING, emp);
            }
            else if (nahoda > 140)
            {
                Odmena((int)MozneOdmeny.COIN15, star3);
            }
            else if (nahoda > 110)
            {
                Odmena((int)MozneOdmeny.COIN10, star2);
            }
            else if (nahoda > 70)
            {
                Odmena((int)MozneOdmeny.COIN5, star1);
            }
            else if (nahoda > 55)
            {
                Odmena((int)MozneOdmeny.AURUS, aurus);
            }
            else if (nahoda > 45)
            {
                Odmena((int)MozneOdmeny.ANTIMON, antimon);
            }
            else if (nahoda > 30)
            {
                if (PlayerPrefs.GetInt("stavbaLodiJedna") < 40)
                {
                    Odmena((int)MozneOdmeny.LOD1, lod1);
                }
                else
                {
                    Odmena((int)MozneOdmeny.ANTIMON, antimon);
                }

            }
            else if (nahoda > 20)
            {
                if (PlayerPrefs.GetInt("stavbaLodiDva") < 80)
                {
                    Odmena((int)MozneOdmeny.LOD2, lod2);
                }
                else
                {
                    Odmena((int)MozneOdmeny.AURUS, aurus);
                }
            }
            else if (nahoda > 10)
            {
                if (PlayerPrefs.GetInt("stavbaLodiTri") < 120)
                {
                    Odmena((int)MozneOdmeny.LOD3, lod3);
                }
                else
                {
                    Odmena((int)MozneOdmeny.COIN15, star3);
                }
            }
            else if (nahoda > 3)
            {
                int jackpot = Random.Range(0, 15);

                if (jackpot == 0)
                {
                    Odmena((int)MozneOdmeny.JACKPOTCOIN, starJackpot);
                }
                else if (jackpot == 1)
                {
                    Odmena((int)MozneOdmeny.JACKPOTAURUS, aurusJackpot);
                }
                else if (jackpot == 2)
                {
                    Odmena((int)MozneOdmeny.JACKPOTANTIMON, antimonJackpot);
                }
                else
                {
                    log = "Failed";
                    logCelk.transform.GetChild(3).GetComponent<Image>().sprite = failed;
                    logCelk.transform.GetChild(3).GetComponent<Image>().rectTransform.sizeDelta = new Vector3(50, 50);
                    logCelk.transform.GetChild(3).GetComponent<Image>().color = new Color(logCelk.transform.GetChild(3).GetComponent<Image>().color.r, logCelk.transform.GetChild(3).GetComponent<Image>().color.g, logCelk.transform.GetChild(3).GetComponent<Image>().color.b, 255);
                }
            }
            else
            {
                log = "Failed";
                logCelk.transform.GetChild(3).GetComponent<Image>().sprite = failed;
                logCelk.transform.GetChild(3).GetComponent<Image>().rectTransform.sizeDelta = new Vector3(50, 50);
                logCelk.transform.GetChild(3).GetComponent<Image>().color = new Color(logCelk.transform.GetChild(3).GetComponent<Image>().color.r, logCelk.transform.GetChild(3).GetComponent<Image>().color.g, logCelk.transform.GetChild(3).GetComponent<Image>().color.b, 255);
            }

            StartCoroutine(RestartAnimace());
            logCelk.transform.GetChild(0).GetComponent<Text>().text = log;
            Instantiate(logCelk, listBox);

            obnov = true;
            efektMaterializatoru.Stop();
            efektMaterializatoru.Play();
            StartCoroutine(Obnova());

            tlacitko = false;
        }
        else if (PlayerPrefs.GetInt("stars") < 10)
        {
            GetComponent<Nastaveni>().Vibruj();
        }
    }

    private IEnumerator Obnova()
    {
        yield return new WaitForSeconds(0.4f);
        yield return new WaitForSeconds(0.3f);
        obnov = false;
    }

    private void Update()
    {
        if (obnov)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}
