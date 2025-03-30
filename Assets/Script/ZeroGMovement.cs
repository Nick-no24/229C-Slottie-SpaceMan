using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZeroGMovement : MonoBehaviour
{
    public float thrustPower = 10f;    // ���ѧ�Ѻ����͹
    public float strafePower = 10f;    // �ç����͹����ҹ��ҧ
    public float verticalPower = 10f;  // �ç���-ŧ
    public float rotationSpeed = 2f;   // �������ǡ����ع
    public float maxSpeed = 20f;       // ���������٧�ش

    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 0.2f;          // Ŵ���������
        rb.angularDamping = 0.2f;   // Ŵ�����ع�Թ�

        // �ҡ�ա��ͧ��ѡ ����ͧ����� reference
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        // ���ȷҧ�ͧ���ͧ��������͹���
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            Vector3 up = Vector3.up; // ��᡹ Y �����

            // ��ͧ�ѹ�������͹�����/ŧ������ͧ��鹿��
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // ����͹����Թ˹��/�����ѧ������ͧ
            float moveForward = Input.GetAxis("Vertical");  // W = 1, S = -1
            rb.AddForce(forward * moveForward * thrustPower, ForceMode.Acceleration);

            // ���-ŧ (Space / Left Ctrl)
            float moveUp = (Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftControl) ? 1f : 0f);
            rb.AddForce(up * moveUp * verticalPower, ForceMode.Acceleration);
        }

        // ��ع����/��� (Q/E)
        float yaw = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        transform.Rotate(0, yaw * rotationSpeed, 0);

        // �ӡѴ���������٧�ش
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
