using System.Collections.Generic;
using UnityEngine;

public class VzhledaBonusy : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem trail;
    [SerializeField]
    private ParticleSystem kolize;

    [SerializeField]
    private List<Sprite> vzhledy = new List<Sprite>();

    private void Start()
    {
        NastavitVzhled(PlayerPrefs.GetInt("aktualni"));
    }

    public void NastavitVzhled(int polozkaID)
    {
        GetComponent<SpriteRenderer>().sprite = vzhledy[polozkaID];
        PlayerPrefs.SetInt("aktualni", polozkaID);
    }
}
