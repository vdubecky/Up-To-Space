using System.Collections;
using UnityEngine;

public class Hrac : MonoBehaviour
{
    [Header("Nastavení hráče")]
    [SerializeField]
    private float rychlost;

    [SerializeField]
    private ParticleSystem vlnaEfekt;
    public int PocetPokracovani { get; set; } = 1;

    private Vector3 smernice;
    private Vector3 vyslednySmer;

    private Line miridloScript;
    private Rigidbody2D fyzikaHrace;

    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject trail;

    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private GameObject vlna;
    [SerializeField]
    private GameObject starParticle;
    [SerializeField]
    private GameObject bgParticle;
    [SerializeField]
    private GameObject projektil;

    private bool pozadiZmena = false;

    public bool ModFight { get; set; } = false;
    public Vector3 AktualniPozice { get; set; }
    public bool Pritah1 { get; set; }

    [SerializeField]
    private GameObject tap;

    [SerializeField]
    private ParticleSystem pozadiParticleParallax;
    [SerializeField]
    private ParticleSystem pozadiParticleParallax2;

    private Vector3 cilPritah;
    private bool pritah2 = false;

    private void Start()
    {
        miridloScript = GetComponent<Line>();
        fyzikaHrace = GetComponent<Rigidbody2D>();
    }

    public void NastavRychlostParticle(float rychlost)
    {
        int particleCount = pozadiParticleParallax.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
        pozadiParticleParallax.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].velocity = Vector3.down * rychlost;
        }

        pozadiParticleParallax.SetParticles(particles, particleCount);
    }

    public void NastavRychlostParticle2(float rychlost)
    {
        int particleCount = pozadiParticleParallax2.particleCount;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
        pozadiParticleParallax2.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].velocity = Vector3.down * rychlost;
        }

        pozadiParticleParallax2.SetParticles(particles, particleCount);
    }

    public void Tap()
    {
        if (!ModFight)
        {
            Camera.main.GetComponent<NastaveniKamery>().Offset = 0;
            gameManager.GetComponent<Nastaveni>().ZvukovyEfektMotoru();

            if (tap.activeSelf)
            {
                tap.SetActive(false);
            }

            GetComponent<PowerUpSystem>().Povoleno = true;
            GetComponent<PowerUpSystem>().Zavrit();
            GetComponent<Line>().NastaveniRotace(false);
            AktualniPozice = transform.position;
            smernice = miridloScript.GetVector();
            vyslednySmer = smernice + transform.position;

            fyzikaHrace.velocity = vyslednySmer * rychlost;

            miridloScript.Spusten(false);
            NastavRychlostParticle(0.3f);
            NastavRychlostParticle2(0.7f);
            StartCoroutine(DobaLetu());
        }
        else
        {
            Vystrel();
        }
    }

    public void TapPowerUp()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<PowerUpSystem>().Zavrit();
        GetComponent<Line>().NastaveniRotace(false);
        AktualniPozice = transform.position;
        smernice = miridloScript.GetVector();
        vyslednySmer = smernice + transform.position;

        fyzikaHrace.velocity = vyslednySmer * rychlost;

        miridloScript.Spusten2(false);
        NastavRychlostParticle(0.3f);
        NastavRychlostParticle2(0.7f);
        StartCoroutine(DobaLetuPowerUp());
    }

    private void Vystrel()
    {
        gameManager.GetComponent<Nastaveni>().ZvukovyEfekt(2);
        Instantiate(projektil, transform.position, Quaternion.identity);
    }

    public Vector3 GetVyslednaPozice()
    {
        return vyslednySmer * rychlost;
    }

    IEnumerator DobaLetuPowerUp()
    {
        yield return new WaitForSeconds(2);
        GetComponent<CircleCollider2D>().enabled = true;
        fyzikaHrace.velocity = Vector3.zero;
        miridloScript.Spusten2(true);
        gameManager.GetComponent<Udalosti>().PowerUpStart = false;
        GetComponent<Line>().NastaveniRotace(true);
        yield return new WaitForSeconds(0.1f);
        NastavRychlostParticle(0);
        NastavRychlostParticle2(0);
    }

    IEnumerator DobaLetu()
    {
        yield return new WaitForSeconds(1);
        gameManager.GetComponent<Nastaveni>().StopniZvukovyEfektMotoru();
        fyzikaHrace.velocity = Vector3.zero;
        miridloScript.Spusten(true);
        GetComponent<Line>().NastaveniRotace(true);
        yield return new WaitForSeconds(0.1f);
        NastavRychlostParticle(0);
        NastavRychlostParticle2(0);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Prekazka"))
        {
            Destroy(Instantiate(particle, gameObject.transform.position, Quaternion.identity), 5f);
            Destroy(Instantiate(vlna, gameObject.transform.position, Quaternion.identity), 2.2f);
            gameManager.GetComponent<Nastaveni>().Vibruj();
            gameObject.SetActive(false);
            gameManager.GetComponent<Udalosti>().GameOver();
            gameManager.GetComponent<Score>().SetScore('-');
            gameManager.GetComponent<UI>().GameOverUI();
            NastavRychlostParticle(0);
            NastavRychlostParticle2(0);
        }
        else if (coll.CompareTag("Star"))
        {
            gameManager.GetComponent<Odmeny>().SetStar();
            gameManager.GetComponent<Nastaveni>().ZvukovyEfekt(3);
            Destroy(Instantiate(starParticle, gameObject.transform.position, Quaternion.identity), 5f);
            Destroy(coll.gameObject);
        }
        else if (coll.CompareTag("NextBG"))
        {
            pozadiZmena = !pozadiZmena;
            gameManager.GetComponent<BG>().ZmenPoziciGradientu(pozadiZmena);
        }
        else if (coll.CompareTag("EnemyProjektil"))
        {
            GameObject[] projektily = GameObject.FindGameObjectsWithTag("EnemyProjektil");

            foreach (GameObject projektil in projektily)
            {
                Destroy(projektil);
            }

            Destroy(coll.gameObject);
            Destroy(Instantiate(particle, gameObject.transform.position, Quaternion.identity), 5f);
            gameManager.GetComponent<Nastaveni>().Vibruj();
            gameObject.SetActive(false);
            gameManager.GetComponent<Udalosti>().GameOver();
            gameManager.GetComponent<UI>().GameOverUI();
        }
        else if (coll.CompareTag("EnemyShipRodic"))
        {
            pritah2 = false;
            Pritah1 = false;
            // Destroy(Instantiate(particle, gameObject.transform.position, Quaternion.identity), 5f);
            gameManager.GetComponent<Nastaveni>().Vibruj();
            gameObject.SetActive(false);
            gameManager.GetComponent<Udalosti>().GameOver();
            gameManager.GetComponent<UI>().GameOverUI();
        }
    }

    private void Update()
    {
        if (pritah2)
        {
            transform.position = Vector3.MoveTowards(transform.position, cilPritah, Time.deltaTime * 3);
        }
    }

    public void Pritah(Vector3 pozice)
    {
        pritah2 = true;
        trail.SetActive(false);
        cilPritah = pozice;
        miridloScript.Spusten(false);
        GetComponent<Line>().NastaveniRotace(false);
    }

    public void RestartGame()
    {
        trail.SetActive(false);
        pozadiZmena = false;
        GetComponent<Line>().NastaveniRotace(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(true);

        Camera.main.transform.position = Vector3.zero;
        transform.position = Vector3.zero;

        gameManager.GetComponent<Score>().SetScore(0);
        gameManager.GetComponent<GenerovaniObjektu>().RestartPrekazek();
        gameManager.GetComponent<GenerovaniObjektu>().ObnoveniPozic();
        transform.rotation = Quaternion.Euler(0, 0, 45);
    }

    public void Continue()
    {
        if (gameManager.GetComponent<Odmeny>().Continue(PocetPokracovani))
        {
            PocetPokracovani++;
            ContinueStart();
        }
    }

    public void ContinueStart()
    {
        trail.SetActive(true);
        gameObject.SetActive(true);
        miridloScript.Spusten(true);
        GetComponent<Line>().NastaveniRotace(true);
        transform.position = AktualniPozice;
        gameManager.GetComponent<UI>().Continue();

        if (!ModFight)
        {
            gameManager.GetComponent<GenerovaniObjektu>().RestartScore();
        }
        else
        {
            gameManager.GetComponent<Fight>().Restart();
        }


        gameManager.GetComponent<GenerovaniObjektu>().ObnoveniPozic();
        vlnaEfekt.Play();
    }

    private IEnumerator TutorialCor()
    {
        while (transform.eulerAngles.z < 42 || transform.eulerAngles.z > 48)
        {
            yield return null;
        }

        gameManager.GetComponent<Udalosti>().Tlacitko = true;
        tap.SetActive(true);
    }

    bool novaHra = true;

    public void NewGame()
    {
        if (novaHra)
        {
            PocetPokracovani = 1;
            gameManager.GetComponent<Odmeny>().AktualizaceHodnot(0);

            if (PlayerPrefs.GetInt("Tu") == 0)
            {
                miridloScript.Aktivni = true;
                miridloScript.Spusten(false);
                miridloScript.Aktivni = true;
                miridloScript.Hodnota = 45;
                StartCoroutine(TutorialCor());
                gameManager.GetComponent<GenerovaniObjektu>().GenerovatTutorialPrekazku();
            }
            else
            {
                gameManager.GetComponent<GenerovaniObjektu>().Generovat();
                miridloScript.Spusten(true);
                GetComponent<Line>().NastaveniRotace(true);
            }

            gameManager.GetComponent<Fight>().Minul1 = false;
            trail.SetActive(true);
            novaHra = false;

            StartCoroutine(RestartBool());
        }
    }

    private IEnumerator RestartBool()
    {
        yield return new WaitForSeconds(0.5f);
        novaHra = true;
    }
}
