using UnityEngine;

public class StarEnemyShip : MonoBehaviour
{
    public float rychlost;

    void Update()
    {
        float step = rychlost * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, step);
    }
}
