using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private GameObject powerUpDialog;
    [SerializeField]
    private Animator powerUpDialogAnim;
    private bool otevreno = false;

    [SerializeField]
    private GameObject raketa;

    public bool StartPowerUp { get; set; } = true;
    public bool Povoleno { get; set; }

    [SerializeField]
    private Sprite warpImage;
    [SerializeField]
    private Sprite empImage;

    [SerializeField]
    private Text typPowerUpu;
    [SerializeField]
    private Text pocetPowerUpu;
    [SerializeField]
    private Image ikona;
    [SerializeField]
    private Button tlacitko;
    [SerializeField]
    private GameObject fastTravelEffect;

    private void Start()
    {
        Povoleno = true;
        StartPowerUp = true;
        fastTravelEffect.SetActive(false);
        tlacitko.interactable = true;
    }

    public void Restart()
    {
        StartPowerUp = true;
        tlacitko.interactable = true;
    }

    public void Otevrit()
    {
        if (!otevreno)
        {
            tlacitko.interactable = true;

            if (StartPowerUp && PlayerPrefs.GetInt("PocetWarpu") > 0)
            {
                pocetPowerUpu.text = PlayerPrefs.GetInt("PocetWarpu").ToString();
                otevreno = true;
                powerUpDialog.SetActive(true);
                ikona.sprite = warpImage;
                typPowerUpu.text = "WARP START";
                powerUpDialogAnim.SetBool("Clsoe", false);
                powerUpDialogAnim.SetBool("Open", true);
            }
            else if (!StartPowerUp && PlayerPrefs.GetInt("PocetEMP") > 0)
            {
                pocetPowerUpu.text = PlayerPrefs.GetInt("PocetEMP").ToString();
                otevreno = true;
                powerUpDialog.SetActive(true);
                ikona.sprite = empImage;
                typPowerUpu.text = "EMP";
                powerUpDialogAnim.SetBool("Clsoe", false);
                powerUpDialogAnim.SetBool("Open", true);
            }
        }
    }

    public void Zavrit()
    {
        tlacitko.interactable = false;
        otevreno = false;

        if (powerUpDialogAnim.isActiveAndEnabled)
        {
            powerUpDialogAnim.SetBool("Open", false);
            powerUpDialogAnim.SetBool("Clsoe", true);
        }
    }

    private IEnumerator Obnova()
    {
        yield return new WaitForSeconds(2f);
        fastTravelEffect.SetActive(false);
    }

    public void Tlacitko()
    {
        if (Povoleno)
        {
            if (StartPowerUp)
            {
                fastTravelEffect.SetActive(true);
                tlacitko.interactable = false;
                manager.GetComponent<Udalosti>().PowerUpStart = true;
                manager.GetComponent<Udalosti>().Tap();

                int pocetWarpu = PlayerPrefs.GetInt("PocetWarpu");
                pocetWarpu--;
                PlayerPrefs.SetInt("PocetWarpu", pocetWarpu);

                StartCoroutine(Obnova());

            }
            else
            {
                tlacitko.interactable = false;
                manager.GetComponent<Udalosti>().GetComponent<Udalosti>().Tlacitko = false;
                manager.GetComponent<Udalosti>().GetComponent<Udalosti>().Tlacitko = false;
                StartPowerUp = true;

                int pocetEMP = PlayerPrefs.GetInt("PocetEMP");
                pocetEMP--;
                PlayerPrefs.SetInt("PocetEMP", pocetEMP);

                GameObject[] lode = GameObject.FindGameObjectsWithTag("EnemyShipRodic");

                if (lode.Length == 1)
                {
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[0].transform;
                }
                else if (lode.Length == 2)
                {
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[0].transform;
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[1].transform;
                }
                else if (lode.Length == 3)
                {
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[0].transform;
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[1].transform;
                    Instantiate(raketa, transform.position, Quaternion.identity).GetComponent<Raketa>().Target = lode[2].transform;
                }
            }

            Zavrit();
        }
    }
}
