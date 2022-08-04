using UnityEngine;

public class PlanetaScript : MonoBehaviour
{
    public Sprite[] vzhled;
    public Color[] barvy;

    public ParticleSystem objevParticle;
    private void Start()
    {
        int vyber = Random.Range(0, vzhled.Length);
        GetComponent<SpriteRenderer>().sprite = vzhled[vyber];
        objevParticle.startColor = barvy[vyber];
        GameObject.FindGameObjectWithTag("Manager").GetComponent<SpravaPlanet>().PridejPlanetu(GetComponent<SpriteRenderer>().sprite);
        GameObject.FindGameObjectWithTag("Manager").GetComponent<Nastaveni>().ZvukovyEfekt(6);
    }
}
