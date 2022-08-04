using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    [SerializeField]
    private AudioSource zvukPozadi;

    [SerializeField]
    private GameObject rankTutorial;
    [SerializeField]
    private GameObject rankList;

    [SerializeField]
    private GameObject tutorialStarGamePlanets;
    [SerializeField]
    private GameObject tutorialStarGameLVL;
    [SerializeField]
    private GameObject tutorialStarGameButton;

    [SerializeField]
    private GameObject tapNewPlanet;
    [SerializeField]
    private GameObject HlavniTlacitka;
    [SerializeField]
    private GameObject newPlanetIndikator;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Canvas canv;

    [SerializeField]
    private GameObject loginButton;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject continueFree;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text bestScoreText;

    [SerializeField]
    private GameObject GameOverScreenBestValue;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Text bestScoreMenuText;

    [SerializeField]
    private GameObject materializerList;
    [SerializeField]
    private GameObject planets;
    [SerializeField]
    private GameObject laboratory;
    [SerializeField]
    private GameObject gamePlay;
    [SerializeField]
    private GameObject shop;
    [SerializeField]
    private GameObject pozadiMenu;
    [SerializeField]
    private GameObject exitDialog;
    [SerializeField]
    private GameObject backToMenu;

    [SerializeField]
    private Animator upozorneniPlanety;
    [SerializeField]
    private Animator startMenuAnimace;
    [SerializeField]
    private Animator shopAnimace;
    [SerializeField]
    private Animator secondChanceAnim;
    [SerializeField]
    private Animator prihlasitDoPlay;
    [SerializeField]
    private GameObject LastChanceUI;
    [SerializeField]
    private GameObject popisPlanets;

    private bool secondChance = true;

    private bool vShopu = false;
    private bool prihlaseniDoPlay;
    private bool povolitSoftTlacitka = true;

    [SerializeField]
    private ParticleSystem bgParticles;
    [SerializeField]
    private Scrollbar sB;

    public bool VideoContinue { get; set; }
    public bool VDailyReward { get; set; }
    public bool VMenu { get; private set; }
    public bool GameOver { get; set; }
    public bool VPlanets { get; set; }
    public bool VLab { get; set; }
    public bool DialogOff { get; set; } = false;
    public bool DialogRank { get; set; } = false;
    public bool DialogMenu { get; set; } = false;
    public bool MaterializerInfo { get; set; }


    public void TutorialButton()
    {
        bool stav = !tutorialStarGamePlanets.activeSelf;
        tutorialStarGamePlanets.SetActive(stav);
        tutorialStarGameLVL.SetActive(stav);
        tutorialStarGameButton.SetActive(stav);

        if (!stav)
        {
            if (PlayerPrefs.GetInt("TutorialRank") == 0)
            {
                rankTutorial.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("tutorialek", 1);
        }
    }

    public void RankListButton()
    {
        rankTutorial.SetActive(false);
        PlayerPrefs.SetInt("TutorialRank", 1);
        GetComponent<Nastaveni>().ZvukovyEfekt(0);

        if (!rankList.activeSelf)
        {
            rankList.SetActive(true);
            DialogRank = true;
        }
        else
        {
            rankList.SetActive(false);
            DialogRank = false;
        }
    }

    private void Start()
    {
        GetComponent<Nastaveni>().StartCoroutine(GetComponent<Nastaveni>().NastavZvuk(false));

        VMenu = true;
        gamePlay.SetActive(false);
        bestScoreMenuText.text = PlayerPrefs.GetInt("bestScore").ToString();

        if (PlayerPrefs.GetInt("tutorialek") == 0)
        {
            TutorialButton();
        }


        if (PlayerPrefs.GetInt("tutorialek") == 1 && PlayerPrefs.GetInt("TutorialRank") == 0)
        {
            rankTutorial.SetActive(true);
        }

    }


    public void SouhlasPersonalizace()
    {
        povolitSoftTlacitka = true;
    }


    public void Exit()
    {
        Application.Quit();
    }

    public void MaterializatorList()
    {
        materializerList.SetActive(!materializerList.activeSelf);
        MaterializerInfo = materializerList.activeSelf;
    }

    public void Laboratory()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        VMenu = false;
        StartCoroutine(RestartStartAnimace(true));
        startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", true);

        StartCoroutine(DoLab());
    }

    public void CloseLaboratory()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        VycistiMaterializatorLog();
        materializerList.SetActive(false);
        VLab = false;
        mainMenu.SetActive(true);
        laboratory.SetActive(false);
        startMenuAnimace.GetComponent<Animator>().SetBool("Menu", true);
        StartCoroutine(RestartStartAnimace(false));
        VMenu = true;
    }

    public void VycistiMaterializatorLog()
    {
        GameObject[] animace = GameObject.FindGameObjectsWithTag("MatAnimace");
        GameObject[] log = GameObject.FindGameObjectsWithTag("log");

        if (animace != null)
        {
            foreach (GameObject anim in animace)
            {
                Destroy(anim);
            }
        }

        if (log != null)
        {
            foreach (GameObject zaznam in log)
            {
                Destroy(zaznam);
            }
        }
    }

    private IEnumerator DoLab()
    {
        yield return new WaitForSeconds(0.25f);
        laboratory.SetActive(true);
        VLab = true;
        yield return new WaitForSeconds(0.6f);
    }
    public void ZavriTutorialPlanet()
    {
        tapNewPlanet.SetActive(false);
    }
    public void Planets()
    {

        if (PlayerPrefs.GetInt("NewPlanet") == 1)
        {
            if (PlayerPrefs.GetInt("PocetPlanet") == 1)
            {
                PlayerPrefs.SetInt("NovaPlanetaTutorial", 1);
            }
            else
            {
                tapNewPlanet.SetActive(false);
            }
        }
        else
        {
            tapNewPlanet.SetActive(false);
        }

        if (PlayerPrefs.GetInt("NovaPlanetaTutorial") == 1)
        {
            tapNewPlanet.SetActive(true);
        }
        else
        {
            tapNewPlanet.SetActive(false);
        }

        PlayerPrefs.SetInt("NewPlanet", 0);
        newPlanetIndikator.SetActive(false);

        VMenu = false;
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        StartCoroutine(RestartStartAnimace(true));
        startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", true);

        if (PlayerPrefs.GetInt("PocetPlanet") <= 0)
        {
            popisPlanets.SetActive(true);
        }
        else
        {
            popisPlanets.SetActive(false);
        }

        StartCoroutine(DoPlanets());
    }

    private IEnumerator DoPlanets()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        yield return new WaitForSeconds(0.25f);
        planets.SetActive(true);
        VPlanets = true;

        yield return new WaitForSeconds(0.6f);
    }

    public void ClosePlanets()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        VPlanets = false;
        mainMenu.SetActive(true);
        planets.SetActive(false);
        startMenuAnimace.GetComponent<Animator>().SetBool("Menu", true);
        StartCoroutine(RestartStartAnimace(false));
        VMenu = true;
    }

    public void ZavritQuitDialog()
    {
        exitDialog.SetActive(false);
    }

    public void ZavritBackToMenuDialog()
    {
        backToMenu.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (povolitSoftTlacitka)
            {
                if (vShopu)
                {
                    ShopDoMenu();

                }
                else if (VDailyReward)
                {
                    GetComponent<DenniOdmeny>().Close();
                    VDailyReward = false;
                }
                else if (VMenu)
                {
                    if (DialogRank)
                    {
                        rankList.SetActive(false);
                        DialogRank = false;
                    }
                    else if (DialogOff)
                    {
                        DialogOff = false;
                        exitDialog.SetActive(false);
                    }
                    else
                    {
                        DialogOff = true;
                        exitDialog.SetActive(true);
                    }
                }
                else if (GameOver)
                {
                    if (DialogMenu)
                    {
                        backToMenu.SetActive(false);
                        DialogMenu = false;
                    }
                    else
                    {
                        DialogMenu = true;
                        backToMenu.SetActive(true);
                    }
                }
                else if (VPlanets)
                {
                    if (DialogRank)
                    {
                        rankList.SetActive(false);
                        DialogRank = false;
                    }
                    else
                    {
                        ClosePlanets();
                    }
                }
                else if (VLab)
                {
                    if (MaterializerInfo)
                    {
                        materializerList.SetActive(false);
                        MaterializerInfo = false;
                    }
                    else
                    {
                        CloseLaboratory();
                        GetComponent<Vyzkum>().Zavrit();
                    }
                }
            }
            else
            {
                GetComponent<Nastaveni>().Vibruj();
            }
        }

        if (prihlaseniDoPlay && PripojenoKI())
        {
            prihlasitDoPlay.SetBool("vMenu_login", true);
        }
        else
        {
            prihlasitDoPlay.SetBool("vMenu_login", false);
        }

        loginButton.SetActive(prihlaseniDoPlay);
    }


    private bool PripojenoKI()
    {
        return !(Application.internetReachability == NetworkReachability.NotReachable);
    }

    private IEnumerator SecondChanceDelay()
    {
        GameOver = false;
        yield return new WaitForSeconds(0.30f);
        LastChanceUI.SetActive(true);
        secondChanceAnim.GetComponent<Animator>().Play("SecondChance", -1, 0);
        Continue();
        player.GetComponent<Hrac>().ContinueStart();
        StartCoroutine(SecondChanceAnimRestart());
    }

    private IEnumerator SecondChanceAnimRestart()
    {
        yield return new WaitForSeconds(3f);
        LastChanceUI.SetActive(false);
    }

    public void GameOverUI()
    {
        GetComponent<Nastaveni>().StartCoroutine(GetComponent<Nastaveni>().NastavPauzuZvuku(true));
        int sance = 0;
        GameOver = true;

        if (secondChance)
        {
            sance = Random.Range(0, 10);
        }

        if (sance == 1)
        {
            secondChance = false;
            StartCoroutine(SecondChanceDelay());
        }
        else
        {
            GetComponent<Nastaveni>().ZvukovyEfekt(4);
            StartCoroutine(GameOverDelay());

            GameOverScreenBestValue.SetActive(true);

            if (GetComponent<Score>().GetScore() > 0)
            {
                if (player.GetComponent<Hrac>().PocetPokracovani <= PlayerPrefs.GetInt("PocetRobotu"))
                {
                    continueButton.SetActive(true);
                }
                else
                {
                    continueButton.SetActive(false);
                }
            }
            else
            {
                continueFree.SetActive(false);
                continueButton.SetActive(false);
            }
        }
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.25f);
        gamePlay.SetActive(false);
        gameOverScreen.SetActive(true);
        scoreText.text = GetComponent<Score>().GetScore().ToString();
        bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
    }

    public void UpozorneniPlanety(bool stav)
    {
        upozorneniPlanety.SetBool("UpozorneniPlanety", stav);
    }

    //Spustí menu
    public void RestartGame()
    {
        GetComponent<Nastaveni>().StartCoroutine(GetComponent<Nastaveni>().NastavZvuk(false));
        GetComponent<Score>().NulovaniScore();



        player.GetComponent<PowerUpSystem>().Zavrit();
        player.GetComponent<PowerUpSystem>().Restart();
        ZavritBackToMenuDialog();
        GameOver = false;
        VMenu = true;
        VideoContinue = true;
        pozadiMenu.SetActive(true);


        if (PlayerPrefs.GetInt("bestLeaderboard") == 1)
        {
            PlayerPrefs.SetInt("bestLeaderboard", 0);
        }


        gameOverScreen.SetActive(false);
        mainMenu.SetActive(true);
        GetComponent<BG>().Restart();

        secondChance = true;
        startMenuAnimace.GetComponent<Animator>().SetBool("Menu", true);
        startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", false);

        bestScoreMenuText.text = PlayerPrefs.GetInt("bestScore").ToString();
        Camera.main.GetComponent<NastaveniKamery>().Offset = 0.8f;

        StartCoroutine(RestartStartAnimace(false));

        GetComponent<Udalosti>().Tlacitko = false;

        if (PlayerPrefs.GetInt("NewPlanet") == 1)
        {
            newPlanetIndikator.SetActive(true);
        }
    }

    IEnumerator RestartStartAnimace(bool zpet)
    {
        yield return new WaitForSeconds(0.2f);

        startMenuAnimace.GetComponent<Animator>().SetBool("Menu", false);
        startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", false);

        if (zpet)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    public void Continue()
    {
        GetComponent<Nastaveni>().StartCoroutine(GetComponent<Nastaveni>().NastavPauzuZvuku(false));
        GameOver = false;
        gamePlay.SetActive(true);
        gameOverScreen.SetActive(false);
        GetComponent<Udalosti>().Tlacitko = true;
    }

    bool startGame = true;

    public void StartGame()
    {
        if (startGame)
        {
            GetComponent<Nastaveni>().StartCoroutine(GetComponent<Nastaveni>().NastavZvuk(true));
            GetComponent<Udalosti>().Tlacitko = false;
            player.GetComponent<Line>().StartovaciHodnota = 0;
            GameObject planeta = GameObject.FindGameObjectWithTag("Planeta");

            if (planeta != null)
            {
                GetComponent<GenerovaniObjektu>().GenerovatPlanetu = false;
                Destroy(planeta);
            }

            GameOver = false;
            VMenu = false;
            pozadiMenu.SetActive(false);
            Camera.main.GetComponent<NastaveniKamery>().Rychlost = 1.85f;
            Camera.main.GetComponent<NastaveniKamery>().Offset = 0;
            startMenuAnimace.GetComponent<Animator>().SetBool("Menu", false);
            startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", true);
            gamePlay.SetActive(true);
            StartCoroutine(RestartStartAnimace(true));
            startGame = false;
            StartCoroutine(ProdlevaTlacitka());
        }
    }

    public void DailyReward()
    {
        GetComponent<DenniOdmeny>().Tlacitko();
        VDailyReward = true;
    }

    private IEnumerator ProdlevaTlacitka()
    {
        yield return new WaitForSeconds(0.5f);
        startGame = true;
        GetComponent<Udalosti>().Tlacitko = true;
    }

    public void ShopButton()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        VMenu = false;
        Camera.main.GetComponent<NastaveniKamery>().Rychlost = 5;
        Camera.main.GetComponent<NastaveniKamery>().Offset = -3.7f;
        startMenuAnimace.GetComponent<Animator>().SetBool("ZMenu", true);
        StartCoroutine(RestartStartAnimace(true));
        StartCoroutine(ZShopu());
    }

    private IEnumerator ZShopu()
    {
        yield return new WaitForSeconds(0.25f);
        shop.SetActive(true);
        vShopu = true;
        NastavRenderMode(true);
        yield return new WaitForSeconds(0.6f);
    }

    public void NastavRenderMode(bool mod)
    {
        if (mod)
        {
            canv.renderMode = RenderMode.ScreenSpaceCamera;
            canv.planeDistance = 1;
        }
        else
        {
            canv.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    public void ShopDoMenu()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(0);
        sB.value = 0;
        VMenu = true;
        vShopu = false;
        NastavRenderMode(false);
        Camera.main.GetComponent<NastaveniKamery>().Offset = 0.8f;
        mainMenu.SetActive(true);
        shop.SetActive(false);
        startMenuAnimace.GetComponent<Animator>().SetBool("Menu", true);

        StartCoroutine(RestartStartAnimace(false));
    }
}
