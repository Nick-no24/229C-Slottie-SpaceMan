using UnityEngine;

public class ZeroGravityZone : MonoBehaviour
{
    public float explosionForce = 10f; 
    public Vector3 explosionDirection = Vector3.up; 
    public float explosionRadius = 5f; 
    public string playerTag = "Player"; 

    private void OnEnable()
    {
        ApplyExplosionForce();
    }

    public void ApplyExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            
            if (col.CompareTag(playerTag))
                continue;

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                
                rb.useGravity = false;
                rb.angularDamping = 0;

               
                rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
            }
        }
    }
    public void ResetGravity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
          

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                
                rb.useGravity = true;
                rb.angularDamping = 0.05f; 

                
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
