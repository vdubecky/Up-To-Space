using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class XPsystem : MonoBehaviour
{
    private int zakladni = 40;
    private float xp;
    private int level = 0;
    private int min = 0;
    private float zmena = 0;

    [SerializeField]
    private Text xpText;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text nextLevelText;

    [SerializeField]
    private Slider xpBar;

    [SerializeField]
    private Text xpTextMenu;
    [SerializeField]
    private Text levelTextMenu;
    [SerializeField]
    private Text nextLevelTextMenu;

    [SerializeField]
    private Slider xpBarMenu;

    private bool barAnimace = false;

    [SerializeField]
    private ParticleSystem startGameParticle;
    [SerializeField]
    private ParticleSystem planetsGameParticle;

    [SerializeField]
    private GameObject levelUpGO;
    [SerializeField]
    private ParticleSystem levelUPMenu;

    [SerializeField]
    private GameObject levelUpGOPlanets;
    [SerializeField]
    private ParticleSystem levelUPPlanets;

    [SerializeField]
    private Sprite[] hodnosti;
    [SerializeField]
    private Image hodnostMenu;
    [SerializeField]
    private Image pozadiHodnosti;

    [SerializeField]
    private Image hodnostPlanet;
    [SerializeField]
    private Image pozadiHodnostiPlanet;

    private void Start()
    {
        xp = PlayerPrefs.GetFloat("XPcka");
        barAnimace = false;
        Aktualizuj();
    }

    public void PridejXP(float kolik)
    {
        xp = PlayerPrefs.GetFloat("XPcka");
        xp += kolik * PlayerPrefs.GetInt("XPMultak");
        PlayerPrefs.SetFloat("XPcka", xp);

        Aktualizuj();
    }

    private IEnumerator NewLevelEfekt()
    {
        yield return new WaitForSeconds(4f);
        levelUPMenu.Stop();
        levelUpGO.SetActive(false);

        levelUPPlanets.Stop();
        levelUpGOPlanets.SetActive(false);
    }

    public void PridejXP()
    {
        xp = PlayerPrefs.GetFloat("XPcka");
        xp += 50 * PlayerPrefs.GetInt("XPMultak");
        PlayerPrefs.SetFloat("XPcka", xp);
        Aktualizuj();
    }


    //0 - 2 = 1.
    //3 - 5 = 2.
    //6 - 8 = 3.
    //9 - 11 = 4.
    //12 - 14 = 5.
    //15 - 17 = 6.
    //> 18 = 7.

    public void Aktualizuj()
    {
        if (PlayerPrefs.GetInt("zakladni") == 0)
        {
            zakladni = 40;
        }
        else
        {
            zakladni = PlayerPrefs.GetInt("zakladni");
        }

        level = PlayerPrefs.GetInt("lvlXP");
        min = PlayerPrefs.GetInt("mini");

        while (xp >= zakladni)
        {
            level++;
            min = zakladni;
            zakladni += (level + 1) * 25;

            levelUpGO.SetActive(true);
            levelUPMenu.Play();

            levelUpGOPlanets.SetActive(true);
            levelUPPlanets.Play();

            StartCoroutine(NewLevelEfekt());

            PlayerPrefs.SetInt("zakladni", zakladni);
            PlayerPrefs.SetInt("lvlXP", level);
            PlayerPrefs.SetInt("mini", min);
        }

        int hodnost = Mathf.FloorToInt(level / 3);

        if (hodnost > 6)
        {
            hodnost = 6;
        }
        else if (hodnost < 0)
        {
            hodnost = 0;
        }

        if (hodnost < 2)
        {
            pozadiHodnosti.transform.localPosition = new Vector3(pozadiHodnosti.transform.localPosition.x, 90, 0);
            pozadiHodnostiPlanet.transform.localPosition = new Vector3(pozadiHodnosti.transform.localPosition.x, 90, 0);
        }
        else
        {
            pozadiHodnosti.transform.localPosition = new Vector3(pozadiHodnosti.transform.localPosition.x, 111, 0);
            pozadiHodnostiPlanet.transform.localPosition = new Vector3(pozadiHodnosti.transform.localPosition.x, 111, 0);
        }

        hodnostMenu.sprite = hodnosti[hodnost];
        hodnostPlanet.sprite = hodnosti[hodnost];

        xpBar.minValue = min;
        xpBar.maxValue = zakladni;

        xpBarMenu.minValue = min;
        xpBarMenu.maxValue = zakladni;

        zmena = 0;
        barAnimace = true;

        levelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

        levelTextMenu.text = level.ToString();
        nextLevelTextMenu.text = (level + 1).ToString();

        xpText.text = System.Math.Round(xp, 2) + " / " + zakladni;
        xpTextMenu.text = System.Math.Round(xp, 2) + " / " + zakladni;
    }

    private void Update()
    {
        if (barAnimace)
        {
            startGameParticle.Play();
            planetsGameParticle.Play();
            zmena += 0.01f;
            xpBar.value = Mathf.Lerp(xpBar.value, xp, zmena);
            xpBarMenu.value = Mathf.Lerp(xpBar.value, xp, zmena);

            if (xpBar.value >= xp)
            {
                zmena = 0;
                barAnimace = false;
            }
        }
        else
        {
            planetsGameParticle.Stop();
            startGameParticle.Stop();
        }
    }
}
