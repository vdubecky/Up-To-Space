using UnityEngine;

public class EnemyShipChild : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Projektil"))
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Fight>().Minul();
        }
    }
}
