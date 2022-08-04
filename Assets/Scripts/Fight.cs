using System.Collections;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField]
    private GameObject battleDialog;
    [SerializeField]
    private GameObject particleMinul;
    [SerializeField]
    private GameObject hrac;

    private int pocet = 0;

    public int Cil { get; set; }
    public bool Minul1 { get; set; }
    public bool PoZniceni1 { get; set; }

    private int nahoda;
    GameObject[] lode;

    public void Increment()
    {
        pocet++;

        if (pocet >= Cil)
        {
            battleDialog.SetActive(false);
            StartCoroutine(PoZniceni());
            hrac.GetComponent<Hrac>().ModFight = false;
            hrac.GetComponent<PowerUpSystem>().Zavrit();
            hrac.GetComponent<PowerUpSystem>().Povoleno = false;
            PoZniceni1 = true;
            Cil = 0;
            pocet = 0;
        }
    }

    public void ZniceniPowerUpem()
    {
        GetComponent<Udalosti>().Tlacitko = false;
    }

    public void Minul()
    {
        if (!Minul1)
        {
            lode = GameObject.FindGameObjectsWithTag("EnemyShip");
            nahoda = Random.Range(0, lode.Length);

            lode[nahoda].GetComponent<EnemyShip>().Minul();
            hrac.GetComponent<PowerUpSystem>().Zavrit();
            Minul1 = true;

            StartCoroutine(MinulDelay());
        }
    }

    private IEnumerator MinulDelay()
    {
        GetComponent<Nastaveni>().ZvukovyEfekt(5);
        yield return new WaitForSeconds(0.45f);
        particleMinul.SetActive(true);
        yield return new WaitForSeconds(2);
        particleMinul.SetActive(false);
    }

    public void Restart()
    {
        Minul1 = false;
        particleMinul.SetActive(false);
        lode[nahoda].GetComponent<EnemyShip>().Restart();
    }

    private IEnumerator PoZniceni()
    {
        yield return new WaitForSeconds(0.2f);
        hrac.GetComponent<PowerUpSystem>().Povoleno = false;
        GetComponent<Udalosti>().Tlacitko = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Udalosti>().Tlacitko = false;
        yield return new WaitForSeconds(0.6f);
        hrac.GetComponent<Hrac>().ModFight = false;
        hrac.GetComponent<Hrac>().Tap();
        GetComponent<Udalosti>().Let();
        Minul1 = false;
        PoZniceni1 = false;
    }
}
