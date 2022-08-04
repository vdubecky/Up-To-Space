using UnityEngine;

public class KolizeParticle : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if(rb)
        {
            Destroy(rb.gameObject);
        }
    }
}
