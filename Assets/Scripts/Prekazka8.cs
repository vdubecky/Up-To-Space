using UnityEngine;

public class Prekazka8 : MonoBehaviour
{
    public float spd;
    public GameObject zabaleni;
    public GameObject objekt;
    public GameObject objekt1;

    private float speed,speed1;

    private void Start()
    {
        speed = Random.Range(2, spd);
        speed1 = Random.Range(2, spd);
    }

    void LateUpdate()
    {
        if (Mathf.Abs(objekt.transform.position.x - zabaleni.transform.position.x) > 3)
        {
            speed *= -1;
        }
        else if (Mathf.Abs(objekt.transform.position.x - zabaleni.transform.position.x) < -3)
        {
            speed *= -1;
        }

        if (Mathf.Abs(objekt1.transform.position.x - zabaleni.transform.position.x) > 3)
        {
            speed1 *= -1;
        }
        else if (Mathf.Abs(objekt1.transform.position.x - zabaleni.transform.position.x) < -3)
        {
            speed1 *= -1;
        }


        objekt.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        objekt1.transform.position += new Vector3(speed1 * Time.deltaTime, 0, 0);
    }
}
