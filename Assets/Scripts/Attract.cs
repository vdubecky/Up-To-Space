using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    public float rychlost;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime * rychlost);
        }
    }
}
