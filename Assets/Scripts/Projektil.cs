using System.Collections;
using UnityEngine;

public class Projektil : MonoBehaviour
{
    public float rychlost;

    private void Awake()
    {
        GameObject hrac = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().velocity = (transform.position + hrac.GetComponent<Line>().GetVector()) * rychlost;
        StartCoroutine(ProjektilRutina());
    }

    private IEnumerator ProjektilRutina()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
