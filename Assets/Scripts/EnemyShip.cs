using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour
{   
    [SerializeField]
    private int zivoty;
    [SerializeField]
    private float rychlost;
    private float kousek;

    [SerializeField]
    private GameObject celyObjekt;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private GameObject particleVlna;
    [SerializeField]
    private GameObject projektil;
    [SerializeField]
    private GameObject zasah;
    [SerializeField]
    private GameObject bar;

    [SerializeField]
    private Image healthBar;
    
    [SerializeField]
    private Sprite[] vzhled;
    [SerializeField]
    private GameObject star;
    [SerializeField]
    private Image paprsek;
    
    private float velikost = 0;
    private Vector3 cil;

    public bool Papr { get; set; }

    private GameObject manager;
    private GameObject hrac;

    private void Start()
    {
        paprsek.enabled = false;

        manager = GameObject.FindGameObjectWithTag("Manager");
        hrac = GameObject.FindGameObjectWithTag("Player");

        int score = manager.GetComponent<Score>().GetScore();

        if (score > 15)
        {
            zivoty = 4;
        }
        else if (score > 20)
        {
            zivoty = 5;
        }
        else
        {
            zivoty = 3;
        }

        velikost = healthBar.rectTransform.sizeDelta.x;
        kousek = velikost / zivoty;

        GetComponent<SpriteRenderer>().sprite = vzhled[Random.Range(0, vzhled.Length)];

        GetComponent<PolygonCollider2D>().SetPath(0, GetComponent<SpriteRenderer>().sprite.vertices);
    }

    private void Update()
    {
        if (Papr)
        {
            float skok = Mathf.Lerp(paprsek.rectTransform.sizeDelta.y, cil.y + 2f, 5 * Time.deltaTime);
            paprsek.rectTransform.sizeDelta = new Vector2(paprsek.rectTransform.sizeDelta.x, skok);
        }
    }

    private IEnumerator Zmena(float doba)
    {
        while (doba > 0)
        {
            float x = Mathf.Lerp(velikost - kousek, velikost, doba);
            healthBar.rectTransform.sizeDelta = new Vector2(x, healthBar.rectTransform.sizeDelta.y);
            doba -= Time.deltaTime;
            yield return null;
        }

        velikost -= kousek;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Projektil"))
        {
            zivoty--;
            StartCoroutine(Zmena(0.5f));
            Destroy(coll.gameObject);

            if (zivoty <= 0)
            {
                Znicit();
            }
            else
            {
                Destroy(Instantiate(zasah, coll.transform.position, Quaternion.identity), 5);
            }
        }
        else if (coll.CompareTag("raketa"))
        {
            Destroy(coll.gameObject);
            Znicit();
        }
    }

    private void Znicit()
    {
        if (manager == null)
        {
            manager = GameObject.FindGameObjectWithTag("Manager");
        }

        manager.GetComponent<Fight>().Increment();

        if (animator.isActiveAndEnabled)
        {
            animator.SetBool("play", true);
        }

        Destroy(Instantiate(particle, transform.position, Quaternion.identity), 5);
        Destroy(Instantiate(particleVlna, transform.position, Quaternion.identity), 2.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        paprsek.enabled = false;

        int nahoda = Random.Range(0, 2);

        if (nahoda == 1)
        {
            Instantiate(star, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
            Instantiate(star, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }

        Destroy(celyObjekt);
        Destroy(gameObject);
    }


    public void Minul()
    {
        paprsek.enabled = true;
        //Instantiate(projektil, transform.position, Quaternion.identity);
        hrac.GetComponent<Hrac>().Pritah1 = true;
        manager.GetComponent<Udalosti>().Tlacitko = false;
        Papr = true;
        cil = paprsek.transform.position - hrac.transform.position;
        Vector3 dir = paprsek.transform.position - hrac.transform.position;

        transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, new Vector3(0, 0, -1));
        paprsek.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, new Vector3(0, 0, -1));
        StartCoroutine(Pritah());
    }

    private IEnumerator Pritah()
    {
        yield return new WaitForSeconds(0.6f);
        hrac.GetComponent<Hrac>().Pritah(transform.position);
    }

    public void Restart()
    {
        paprsek.enabled = false;
        Papr = false;
        paprsek.rectTransform.sizeDelta = new Vector2(1.414f, 0.142f);
        paprsek.transform.eulerAngles = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
