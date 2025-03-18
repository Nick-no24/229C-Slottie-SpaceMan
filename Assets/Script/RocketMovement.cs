using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float thrustPower = 10f;   // �ç�Ѻ����͹
    public float rotationSpeed = 2f;  // ��������㹡����ع
    public float brakeForce = 5f;     // �ç�á
    public float maxSpeed = 20f;      // ���������٧�ش
    public float misssileCount = 5;
    float cooldown = 0.5f; // �������˹�ǧ (0.5 �Թҷ�)
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
            nextFireTime = Time.time + cooldown; // ˹�ǧ����
           
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

        // �����Ǣ��-ŧ���� W/S
        float pitch = Input.GetAxis("Vertical") * rotationSpeed;
        transform.Rotate(-pitch, 0, 0);

        // ��ع����-��Ҵ��� A/D
        float yaw = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, yaw, 0);

      
        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * brakeForce);
        }

        // �ӡѴ���������٧�ش
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}