using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerovaniObjektu : MonoBehaviour
{
    [Header("Generování překážek a score ukazatele")]
    [SerializeField]
    private GameObject coinBonusStage;
    [SerializeField]
    private GameObject battleDialog;
    [SerializeField]
    private GameObject testovaciObjekt;
    [SerializeField]
    private GameObject objekt;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemyShip;
    [SerializeField]
    private GameObject miss;
    [SerializeField]
    private GameObject planeta;

    private Vector3 vyslednaPoziceScore;

    [SerializeField]
    private List<GameObject> seznamObjektuStart;
    [SerializeField]
    private List<GameObject> seznamObjektu;
    [SerializeField]
    private TextMesh scoreText;
    [SerializeField]
    private TextMesh scoreRecordText;

    private int score = 0;

    private int intervalGenerovaniL = 0;
    private int intervalGenerovaniP = 3;

    public Vector3 Cil { get; private set; }
    public bool GenerovatPlanetu { get; set; }
    public bool GenerovatScoreBonus { get; set; }

    public void ObnoveniPozic()
    {
        vyslednaPoziceScore = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }

    public void GenerovatScore(Vector3 cil)
    {
        vyslednaPoziceScore += new Vector3(cil.x, cil.y - 1.25f, cil.z);

        if (!GenerovatPlanetu)
        {
            score = gameObject.GetComponent<Score>().GetScore();

            if (GetComponent<Score>().GetBest())
            {
                scoreRecordText.text = score.ToString();
                Instantiate(scoreRecordText, vyslednaPoziceScore, Quaternion.identity);
            }
            else
            {
                scoreText.text = score.ToString();
                Instantiate(scoreText, vyslednaPoziceScore, Quaternion.identity);
            }
        }
    }

    public void GenerovatTutorialPrekazku()
    {
        Cil = new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, 0);
        Instantiate(seznamObjektu[1], Cil, Quaternion.identity);
    }

    public void Generovat()
    {
        IntervalGenerovani();
        Cil = new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, 0);

        if (GenerovatPlanetu)
        {
            Instantiate(planeta, new Vector3(Cil.x, Cil.y - 1.5f, Cil.z), Quaternion.identity);
            GenerovatPlanetu = false;
            PlayerPrefs.SetInt("NewPlanet", 1);
        }
        else if (GenerovatScoreBonus)
        {
            Instantiate(coinBonusStage, new Vector3(Cil.x, Cil.y - 0.5f, Cil.z), Quaternion.identity);
            GenerovatScoreBonus = false;
        }
        else
        {
            int score = GetComponent<Score>().GetScore();
            if (score % 5 == 0 && score > 0)
            {
                player.GetComponent<PowerUpSystem>().StartPowerUp = false;
                player.GetComponent<PowerUpSystem>().Otevrit();
                battleDialog.SetActive(true);
                Camera.main.GetComponent<NastaveniKamery>().Offset = 1;

                if (score >= 14)
                {
                    Instantiate(enemyShip, new Vector3(Cil.x + 1.8f, Cil.y - 0.7f, Cil.z), Quaternion.identity);
                    Instantiate(enemyShip, new Vector3(Cil.x - 1.8f, Cil.y - 0.7f, Cil.z), Quaternion.identity);
                    Instantiate(enemyShip, Cil, Quaternion.identity);
                    GetComponent<Fight>().Cil = 3;
                }
                else if (score >= 9)
                {
                    Instantiate(enemyShip, new Vector3(Cil.x + 1.8f, Cil.y - 0.7f, Cil.z), Quaternion.identity);
                    Instantiate(enemyShip, new Vector3(Cil.x - 1.8f, Cil.y - 0.7f, Cil.z), Quaternion.identity);
                    GetComponent<Fight>().Cil = 2;
                }
                else
                {
                    Instantiate(enemyShip, Cil, Quaternion.identity);
                    GetComponent<Fight>().Cil = 1;
                }


                Instantiate(miss, new Vector3(Cil.x, Cil.y + 0.3f, Cil.z), Quaternion.identity);
            }
            else
            {
                if (score < 4)
                {
                    Instantiate(seznamObjektuStart[Random.Range(0, seznamObjektuStart.Count)], Cil, Quaternion.identity);
                }
                else
                {
                    Instantiate(seznamObjektu[Random.Range(intervalGenerovaniL, intervalGenerovaniP)], Cil, Quaternion.identity);
                }

                /*Random.Range(intervalGenerovaniL, intervalGenerovaniP)*/
                //Instantiate(seznamObjektu[Random.Range(intervalGenerovaniL, intervalGenerovaniP)], Cil, Quaternion.identity);
                //Instantiate(testovaciObjekt, Cil, Quaternion.identity);
            }
        }
    }

    private void IntervalGenerovani()
    {
        score = gameObject.GetComponent<Score>().GetScore();

        if (score < 4)
        {
            intervalGenerovaniL = 0;
            intervalGenerovaniP = 6;
        }
        else if (score >= 4 && score < 12)
        {
            intervalGenerovaniL = 6;
            intervalGenerovaniP = 13;
        }
        else
        {
            intervalGenerovaniL = 12;
            intervalGenerovaniP = seznamObjektu.Count;
        }
    }

    public void RestartPrekazek()
    {
        GenerovatPlanetu = false;
        battleDialog.SetActive(false);
        GameObject[] objekty = GameObject.FindGameObjectsWithTag("Prekazka_rodic");

        if (objekty != null)
        {
            foreach (GameObject prekazka in objekty)
            {
                Destroy(prekazka);
            }
        }

        objekty = GameObject.FindGameObjectsWithTag("Score");

        if (objekty != null)
        {
            foreach (GameObject score in objekty)
            {
                Destroy(score);
            }
        }

        GameObject[] lode = GameObject.FindGameObjectsWithTag("EnemyShipRodic");
        GameObject[] miss = GameObject.FindGameObjectsWithTag("Miss");
        GameObject[] planety = GameObject.FindGameObjectsWithTag("Planeta");
        GameObject[] extraCoins = GameObject.FindGameObjectsWithTag("bonusCoin");

        if (extraCoins != null)
        {
            foreach (GameObject c in extraCoins)
            {
                Destroy(c);
            }
        }

        if (lode != null)
        {
            foreach (GameObject lod in lode)
            {
                Destroy(lod);
            }
        }

        if (miss != null)
        {
            foreach (GameObject m in miss)
            {
                Destroy(m);
            }
        }

        if (planety != null)
        {
            foreach (GameObject planeta in planety)
            {
                Destroy(planeta);
            }
        }
    }

    public void RestartScore()
    {
        try
        {
            GameObject[] score = GameObject.FindGameObjectsWithTag("Score");
            Destroy(score[score.Length - 1]);
        }
        catch
        {
            Debug.Log("Vyjimka - restart score ukazatelu");
        }
    }
}
