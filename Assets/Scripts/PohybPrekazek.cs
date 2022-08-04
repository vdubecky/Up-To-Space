using UnityEngine;

public class PohybPrekazek : MonoBehaviour
{
    public GameObject objekt1;
    public GameObject objekt2;

    public float rychlost;
    private float rychlost2;

    private float pozice1;
    private float pozice2;

    private void Start()
    {
        rychlost2 = -rychlost;
    }
	
	void Update ()
    {
        objekt1.transform.position += new Vector3(rychlost * Time.deltaTime, 0, 0);
        objekt2.transform.position += new Vector3(rychlost2 * Time.deltaTime, 0, 0);

        pozice1 = Mathf.Abs(objekt1.transform.position.x - transform.position.x);
        pozice2 = Mathf.Abs(objekt2.transform.position.x - transform.position.x);

        if (pozice1 > 3)
        {
            rychlost *= -1;
        }
        else if(pozice1 < -3)
        {
            rychlost *= -1;
        }

        if (pozice2 > 3)
        {
            rychlost2 *= -1;
        }
        else if (pozice2 < -3)
        {
            rychlost2 *= -1;
        }
    }
}
