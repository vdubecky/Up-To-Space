using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject hrac;

    private void Update()
    {
        transform.position = new Vector3(hrac.transform.position.x, hrac.transform.position.y, hrac.transform.position.z);
    }
}
