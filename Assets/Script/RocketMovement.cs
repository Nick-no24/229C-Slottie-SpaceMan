using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour
{
    public float thrustPower = 10f;
    public float rotationSpeed = 2f;
    public float brakeForce = 5f;
    public float maxSpeed = 20f;

    public int missileCount = 5;
    private float cooldown = 0.5f;
    private float nextFireTime = 0f;

    public GameObject missilePrefab;
    public Transform spawnPoint;

    public TextMeshProUGUI missileText;
    public TextMeshProUGUI reloadText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI hpText;    
    public Image damageOverlay;      
    public float maxHP = 100f;
    private float currentHP;
    private bool isFlashing = false;

    private Rigidbody rb;
    private float reloadTimer = 0f;
    private bool isReloading = false;

    public GameObject gameOverUI; 
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reloadText.gameObject.SetActive(false);
        currentHP = maxHP;
        damageOverlay.color = new Color(1, 0, 0, 0);
        gameOverUI.SetActive(false);
        UpdateUI();
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        HandleShooting();
        HandleMovement();
        UpdateUI();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && missileCount > 0 && Time.time >= nextFireTime)
        {
            ShootMissile();
            nextFireTime = Time.time + cooldown;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartReload();
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                FinishReload();
            }
        }
    }

    void StartReload()
    {
        isReloading = true;
        reloadTimer = 3f;
        reloadText.gameObject.SetActive(true);
    }

    void FinishReload()
    {
        isReloading = false;
        missileCount = 5;
        reloadText.gameObject.SetActive(false);
    }

    void ShootMissile()
    {
        if (!isReloading)
        {
            GameObject missile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
            missile.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * 50f;
            missileCount--;
        }
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

    void UpdateUI()
    {
        missileText.text = $"Missiles: {missileCount}";
        speedText.text = $"Speed: {rb.linearVelocity.magnitude:F1} m/s";
        hpText.text = $"HP: {currentHP}/{maxHP}"; 
        if (isReloading)
        {
            reloadText.text = $"Reloading: {reloadTimer:F1}s";
        }
    }

  
    public void TakeDamage(float damage)
    {
        Debug.Log($"Taking Damage: {damage}");
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();
        FlashDamageOverlay();

        if (currentHP <= 0)
        {
            GameOver();

        }
    }

   
    void FlashDamageOverlay()
    {
        if (!isFlashing)
        {
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        isFlashing = true;
        for (float i = 0.5f; i > 0; i -= 0.1f)
        {
            damageOverlay.color = new Color(1, 0, 0, i);
            yield return new WaitForSeconds(0.1f);
        }
        damageOverlay.color = new Color(1, 0, 0, 0);
        isFlashing = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Meteor"))
        {
            

            Rigidbody asteroidRb = collision.gameObject.GetComponent<Rigidbody>();
            if (asteroidRb != null)
            {
                float playerForce = rb.mass * rb.linearVelocity.magnitude;
                float asteroidForce = asteroidRb.mass * asteroidRb.linearVelocity.magnitude;

                float impactForce = Mathf.Abs(playerForce - asteroidForce);
                float damage = impactForce * 0.1f;

               

                TakeDamage(damage);
            }

        }
    }
    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Galaxy");
    }

    void QuitGame()
    {
        Application.Quit(); 
    }
}
