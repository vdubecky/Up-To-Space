using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazac : MonoBehaviour
{
    [SerializeField]
    private GameObject hrac;
    [SerializeField]
    private float offsetY;

    private void LateUpdate()
    {
        transform.position = new Vector3(hrac.transform.position.x, hrac.transform.position.y + offsetY, 0);
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
    }
}
