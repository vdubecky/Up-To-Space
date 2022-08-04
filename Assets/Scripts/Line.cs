using UnityEngine;

public class Line : MonoBehaviour
{
    [Header("Nastavení mířidla")]

    [Tooltip("Menší hodnota = větší rychlost")]
    [SerializeField]
    private float rychlost;

    [SerializeField]
    private float maxUhel;

    [SerializeField]
    private GameObject miridlo;
    [SerializeField]
    private GameObject pozice;

    private bool rotaceHrace = false;
    private float casovac = 0;

    private bool spusten = true;

    public float Uhel
    {
        get => transform.rotation.z;
    }
    public float Hodnota
    {
        get
        {
            return Mathf.Sin(casovac / rychlost) * maxUhel;
        }
        set
        {
            miridlo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(value)));
        }
    }

    public float StartovaciHodnota
    {
        get
        {
            return Mathf.Sin(casovac / rychlost) * maxUhel;
        }
        set
        {
            casovac = value;
            miridlo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(value)));
        }
    }


    public bool Aktivni
    {
        get
        {
            return miridlo.activeSelf;
        }
        set
        {
            miridlo.SetActive(value);
        }
    }

    private void Update()
    {
        if (spusten)
        {
            casovac += Time.deltaTime;
            Hodnota = Mathf.Sin(casovac / rychlost) * maxUhel;
            miridlo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Hodnota));

            if (rotaceHrace)
            {
                transform.rotation = Quaternion.Euler(0, 0, Hodnota + 45);
            }
        }
    }

    public Vector3 GetVector()
    {
        return pozice.transform.position * -1;
    }

    public void Spusten(bool spusten)
    {
        if (spusten)
        {
            this.spusten = spusten;
            miridlo.SetActive(true);
        }
        else
        {
            this.spusten = spusten;
            miridlo.SetActive(false);
        }
    }


    public void Spusten2(bool spusten)
    {
        if (spusten)
        {
            this.spusten = spusten;
        }
        else
        {
            this.spusten = spusten;
        }
    }

    public void NastaveniRotace(bool rotace)
    {
        rotaceHrace = rotace;
    }
}
