using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int _Score { private set; get; }
    private int bestScore = 0;
    private bool best = false;

    public GameObject notifikace;

    private void Start()
    {
        _Score = 0;
        bestScore = PlayerPrefs.GetInt("bestScore");
    }

    public void SetScore(int kolik)
    {
        _Score += kolik;

        if (_Score > PlayerPrefs.GetInt("bestScore"))
        {
            best = true;
            PlayerPrefs.SetInt("bestLeaderboard", 1);
            PlayerPrefs.SetInt("bestScore", _Score);
            bestScore = _Score;
        }
    }

    public void SetScore(char zn)
    {
        if (zn == '+')
        {
            _Score++;

            if (_Score > PlayerPrefs.GetInt("bestScore"))
            {
                best = true;
                PlayerPrefs.SetInt("bestLeaderboard", 1);
                PlayerPrefs.SetInt("bestScore", _Score);
                bestScore = _Score;
            }

            if (_Score > 0)
            {
                PlayerPrefs.SetInt("Tu", 1);
            }
        }
        else
        {
            if (best)
            {
                bestScore--;
                PlayerPrefs.SetInt("bestScore", bestScore);

                best = false;
            }

            _Score--;
        }

        if (_Score < 0)
        {
            _Score = 0;
        }
    }

    public void NulovaniScore()
    {
        best = false;
        int meziSkore = _Score;
        _Score = 0;
        StartCoroutine(NastaveniXP(meziSkore));
    }

    private IEnumerator NastaveniXP(int score)
    {
        yield return new WaitForSeconds(0.5f);

        if (score > 0)
        {
            notifikace.SetActive(true);
            notifikace.GetComponent<Text>().text = "+ " + (score * PlayerPrefs.GetInt("XPMultak")).ToString();
            StartCoroutine(RestartAnimace());
        }

        yield return new WaitForSeconds(1f);

        GetComponent<XPsystem>().PridejXP(score);
    }

    private IEnumerator RestartAnimace()
    {
        yield return new WaitForSeconds(2.5f);
        notifikace.transform.localPosition = new Vector3(notifikace.transform.localPosition.x, 285, 0);
        notifikace.SetActive(false);
    }

    public int GetScore()
    {
        return _Score;
    }

    public bool GetBest()
    {
        return best;
    }
}
