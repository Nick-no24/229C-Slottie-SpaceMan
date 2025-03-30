using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZeroGMovement : MonoBehaviour
{
    public float thrustPower = 10f;    // กำลังขับเคลื่อน
    public float strafePower = 10f;    // แรงเคลื่อนที่ด้านข้าง
    public float verticalPower = 10f;  // แรงขึ้น-ลง
    public float rotationSpeed = 2f;   // ความเร็วการหมุน
    public float maxSpeed = 20f;       // ความเร็วสูงสุด

    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 0.2f;          // ลดการลื่นไหล
        rb.angularDamping = 0.2f;   // ลดการหมุนเกินไป

        // หากมีกล้องหลัก ใช้กล้องนั้นเป็น reference
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        // ใช้ทิศทางของกล้องเพื่อเคลื่อนที่
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            Vector3 up = Vector3.up; // ใช้แกน Y คงที่

            // ป้องกันการเคลื่อนที่ขึ้น/ลงเมื่อมองขึ้นฟ้า
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // เคลื่อนที่เดินหน้า/ถอยหลังตามกล้อง
            float moveForward = Input.GetAxis("Vertical");  // W = 1, S = -1
            rb.AddForce(forward * moveForward * thrustPower, ForceMode.Acceleration);

            // ขึ้น-ลง (Space / Left Ctrl)
            float moveUp = (Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftControl) ? 1f : 0f);
            rb.AddForce(up * moveUp * verticalPower, ForceMode.Acceleration);
        }

        // หมุนซ้าย/ขวา (Q/E)
        float yaw = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        transform.Rotate(0, yaw * rotationSpeed, 0);

        // จำกัดความเร็วสูงสุด
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
