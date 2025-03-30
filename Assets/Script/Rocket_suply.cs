using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RocketSupply : MonoBehaviour
{
    public float thrustPower = 10f;
    public float rotationSpeed = 2f;
    public float brakeForce = 5f;
    public float maxSpeed = 20f;

    private Rigidbody rb;

    public GameObject supplyPrefab; // กล่องที่ปล่อย
    public Transform dropPoint; // จุดปล่อยกล่อง (ติดตั้งใต้ยาน)
    public LayerMask targetLayer; // Layer ของเป้าหมาย
    public Camera bottomCamera; // กล้องมองด้านล่าง
    public Camera mainCamera; // กล้องหลัก

    public TextMeshProUGUI supplyText; // UI จำนวนกล่อง
    public TextMeshProUGUI messageText; // UI แจ้งแพ้-ชนะ

    public int maxSupply = 3; // จำนวนกล่องสูงสุด
    private int supplyLeft; // จำนวนกล่องที่เหลือ
    private bool hasDroppedCorrectly = false; // ปล่อยถูกจุดหรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        supplyLeft = maxSupply;
        messageText.gameObject.SetActive(false); // ซ่อนข้อความ
        UpdateUI();
        SwitchToMainCamera();
    }

    void Update()
    {
        HandleMovement();
        UpdateUI();

        // กด C เพื่อเปลี่ยนกล้อง
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }

        // กดคลิกขวา (เฉพาะตอนใช้กล้องล่าง) เพื่อปล่อยกล่อง
        if (bottomCamera.enabled && Input.GetMouseButtonDown(1))
        {
            DropSupply();
        }
    }

    void DropSupply()
    {
        if (supplyLeft <= 0 || hasDroppedCorrectly) return;

        Ray ray = bottomCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, targetLayer)) // ยิง Raycast ตรวจจับ Layer เป้าหมาย
        {
            Instantiate(supplyPrefab, hit.point, Quaternion.identity);
            hasDroppedCorrectly = true;
            ShowMessage("✅ Supply dropped successfully!", Color.green);
        }
        else
        {
            ShowMessage("❌ Missed! Supply wasted!", Color.red);
        }

        supplyLeft--;
        UpdateUI();

        // เช็กว่าแพ้หรือชนะ
        if (supplyLeft <= 0)
        {
            if (!hasDroppedCorrectly)
            {
                ShowMessage("❌ Game Over! You failed to drop the supply correctly!", Color.red);
            }
        }
    }

    void UpdateUI()
    {
        supplyText.text = $"Supply Left: {supplyLeft}";
    }

    void ShowMessage(string text, Color color)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = text;
        messageText.color = color;
    }

    void SwitchCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        bottomCamera.enabled = !bottomCamera.enabled;
    }

    void SwitchToMainCamera()
    {
        mainCamera.enabled = true;
        bottomCamera.enabled = false;
    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * thrustPower, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(-transform.forward * thrustPower, ForceMode.Acceleration);
        }

        float pitch = Input.GetAxis("Vertical") * rotationSpeed;
        transform.Rotate(-pitch, 0, 0);

        float yaw = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, yaw, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * brakeForce);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
