using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float thrustPower = 10f;   // แรงขับเคลื่อน
    public float rotationSpeed = 2f;  // ความเร็วในการหมุน
    public float brakeForce = 5f;     // แรงเบรก
    public float maxSpeed = 20f;      // ความเร็วสูงสุด
    public float misssileCount = 5;
    float cooldown = 0.5f; // ตั้งเวลาหน่วง (0.5 วินาที)
    float nextFireTime = 0f;

    private Rigidbody rb;
    public GameObject misslie;
    public GameObject spawnpoint;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && misssileCount!= 0 && Time.time >= nextFireTime)
        {
          Instantiate(misslie, transform.position, spawnpoint.transform.rotation);
            misssileCount--;
            nextFireTime = Time.time + cooldown; // หน่วงเวลา
           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            misssileCount = 5;

        }
    }
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * thrustPower, ForceMode.Acceleration);
        }

        // เชิ่ดหัวขึ้น-ลงด้วย W/S
        float pitch = Input.GetAxis("Vertical") * rotationSpeed;
        transform.Rotate(-pitch, 0, 0);

        // หมุนซ้าย-ขวาด้วย A/D
        float yaw = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, yaw, 0);

      
        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * brakeForce);
        }

        // จำกัดความเร็วสูงสุด
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}