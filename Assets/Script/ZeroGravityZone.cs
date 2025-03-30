using UnityEngine;

public class ZeroGravityZone : MonoBehaviour
{
    public float explosionForce = 10f; // �ç���Դ
    public Vector3 explosionDirection = Vector3.up; // ��ȷҧ�ͧ�ç���Դ
    public float explosionRadius = 5f; // ����բͧ�ç���Դ
    public string playerTag = "Player"; // Tag �ͧ Player

    private void OnEnable()
    {
        ApplyExplosionForce();
    }

    public void ApplyExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            // ���������� Player
            if (col.CompareTag(playerTag))
                continue;

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                // �Դ�ç�����ǧ
                rb.useGravity = false;
                rb.angularDamping = 0;

                // �����ç���Դ
                rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
            }
        }
    }
    public void ResetGravity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            // ���������� Player
            if (col.CompareTag(playerTag))
                continue;

            if (col.attachedRigidbody != null)
            {
                Rigidbody rb = col.attachedRigidbody;

                // �Դ�ç�����ǧ��Ѻ��
                rb.useGravity = true;
                rb.angularDamping = 0.05f; // ��駤�ҡ�Ѻ�繤��������� (������ͧ���)

                // ��ش�������͹���
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
