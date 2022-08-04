using UnityEngine;

public class Prekazka6 : MonoBehaviour
{
    public float speed;
    public GameObject zabaleni;

	void Update ()
    {
		if(Mathf.Abs(gameObject.transform.position.x - zabaleni.transform.position.x )> 1.14f)
        {
            speed *= -1;
        }
        else if(Mathf.Abs(gameObject.transform.position.x - zabaleni.transform.position.x) < -1.14f)
        {
            speed *= -1;
        }

        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
	}
}
