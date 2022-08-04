using System.Collections;
using UnityEngine;

public class EnemyProjektil : MonoBehaviour
{
    public float rychlost;
    private GameObject hrac;

    private void Awake()
    {
        hrac = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().velocity = Vector3.down * rychlost;
        StartCoroutine(ProjektilRutina());
    }

    private void Update()
    {
        float step = rychlost * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, hrac.transform.position, step);
    }

    private IEnumerator ProjektilRutina()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
