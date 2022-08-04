using UnityEngine;

public class Prekazka : MonoBehaviour
{
    public GameObject[] odmena;
	
	void Start ()
    {
        if(odmena.Length != 0)
        {
            int vyber = Random.Range(0, odmena.Length);
            odmena[vyber].SetActive(true);
        }
    }
}
