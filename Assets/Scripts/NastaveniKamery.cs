using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NastaveniKamery : MonoBehaviour
{
    [Header("Nastavení kamery")]
    [SerializeField]
    private GameObject hrac;

    private Vector3 cil;
    private float vzdalenost = 5;
    private float vzdalenostCil = 5;
    private float doba = 0;

    public float Offset
    {
        get;
        set;
    }

    public float Rychlost { get; set; }

    private void Awake()
    {
        Application.targetFrameRate = 360;
        Rychlost = 1.5f;
        Offset = 0.8f;
    }

    private void LateUpdate()
    {
        cil = new Vector3(hrac.transform.position.x, hrac.transform.position.y + 1 + Offset, -10);
        transform.position = Vector3.Slerp(transform.position, cil, Rychlost * Time.deltaTime);

        if (vzdalenostCil != gameObject.GetComponent<Camera>().orthographicSize)
        {
            doba += Time.deltaTime;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(vzdalenost, vzdalenostCil, doba);
        }
    }

    public void SetZmena(float vzdalenostCil)
    {
        vzdalenost = gameObject.GetComponent<Camera>().orthographicSize;
        this.vzdalenostCil = vzdalenostCil;
        doba = 0;
    }
}
