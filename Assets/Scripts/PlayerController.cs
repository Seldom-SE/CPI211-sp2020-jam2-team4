using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Credit to Yvridio for the file some of this code was originally based on.
// https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html

/// <summary>
/// This script will control the player's movement which involves
/// moving the player as well as looking around with the player
/// 
/// Will most likely incorporate the ladder control here as well
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Rigidbody Rigidbody
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }
    private Collider playerCollider;
    public GameObject playerCam;
    public Slider healthSlider;
    public Text healthText;
    public GameObject[] ammoIndicators;
    public Text killCounter;
    public Text tutorialText;

    public GameObject bullet;

    private bool level3;

    public float lookSensitivity = 1f;
    public float maxVerticalAngle = 60f;
    public float movementSpeed = 180f;
    public bool isInControl = true;
    public float jumpSpeed;

    public int maxHealth;
    private int health;
    public int ammo = 30;
    public int zombiesKilled = 0;
    private int hurtTimer;

    private float distToGround;

    private void Awake()
    {
        //Locks mouse cursor on screen so player does not see it
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
        distToGround = playerCollider.bounds.extents.y;
        SetHealth(maxHealth);
        SetAmmo(ammo);
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            level3 = true;
            tutorialText.text = "Kill as many zombies as you can!";
            SetZombiesKilled(48);
        }
        else
        {
            level3 = false;
            tutorialText.text = "Use WASD and mouse to move. Click to shoot. Can you see a pattern for which zombies drop what powerups in what situations?";
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            SetAmmo(ammo - 1);
            GameObject bullet = Instantiate(this.bullet);
            Transform cameraTrans = playerCam.transform;
            bullet.transform.position = new Vector3(cameraTrans.position.x, cameraTrans.position.y, cameraTrans.position.z);
            bullet.transform.rotation = new Quaternion(cameraTrans.rotation.x, cameraTrans.rotation.y, cameraTrans.rotation.z, cameraTrans.rotation.w);
            Physics.IgnoreCollision(playerCollider, bullet.GetComponent<Collider>());
        }
    }

    private void FixedUpdate()
    {
        if(isInControl)
        {
            MouseControl();
            MovementControl();
        }

        if (hurtTimer > 0) hurtTimer--;
    }

    /// <summary>
    /// Method that handles mouse input and rotating the
    /// players view based on that output
    /// </summary>
    private void MouseControl()
    {
        //May not be a issue in the long run but I noticed that once an angular 
        //velocity is added the player would keep rotating so I added this to ensure it doesnt happen
        Rigidbody.angularVelocity = Vector3.zero;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //This moves the PLAYER gameobject's rotation
        if (mouseX != 0)
        {
            Vector3 newRotation = transform.eulerAngles;
            //I added * Time.deltaTime because it generally smoothes things out but wasnt sure if this was an appropriate case. Delete if not
            newRotation.y += mouseX * lookSensitivity * Time.deltaTime;
            transform.eulerAngles = newRotation;
        }
        //This moves the CAMERA's rotation. This was done to avoid collision issues if the player obj were to rotate
        if (mouseY != 0)
        {
            Vector3 newRotation = playerCam.transform.localEulerAngles;
            //I added * Time.deltaTime because it generally smoothes things out but wasnt sure if this was an appropriate case. Delete if not
            newRotation.x += mouseY * lookSensitivity * -1 * Time.deltaTime;

            /**
             * This prevents the player from looking beyond our set _maxVerticalAngle;
             * 
             * Note: .localEulerAngles does not return a negative number as the inspector shows.
             * For example, -5 in the inspector is 355. This is the reason why I have to do two differnt
             * checks
             */
            if (newRotation.x < maxVerticalAngle || newRotation.x > 360 - maxVerticalAngle)
                playerCam.transform.localEulerAngles = newRotation;
        }
    }

    /// <summary>
    /// Method that controls input for player movement
    /// </summary>
    private void MovementControl()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        bool jump = Input.GetAxisRaw("Jump") > 0.01f;
        bool grounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

        if (hInput != 0)
        {
            transform.position += transform.right * Mathf.Sign(hInput) * movementSpeed * Time.deltaTime;
        }

        if (vInput != 0)
        {
            transform.position += transform.forward * Mathf.Sign(vInput) * movementSpeed * Time.deltaTime;
        }

        if (jump && grounded)
        {
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, jumpSpeed, Rigidbody.velocity.z);
        }
    }

    private void SetHealth (int health)
    {
        this.health = health;
        healthSlider.value = health;
        healthText.text = health.ToString();
    }

    private void SetAmmo (int ammo)
    {
        this.ammo = ammo;
        for (int i = 0; i < ammo; i++)
        {
            ammoIndicators[i].SetActive(true);
        }
        for (int i = ammo; i < ammoIndicators.Length; i++)
        {
            ammoIndicators[i].SetActive(false);
        }
    }

    private void SetZombiesKilled (int zombiesKilled)
    {
        this.zombiesKilled = zombiesKilled;
        if (level3)
        {
            killCounter.text = "Kills: " + zombiesKilled;
        }
        else
        {
            if (zombiesKilled >= 24)
            {
                killCounter.text = "Climb the stairs to progress to the next floor!";
            }
            else
            {
                killCounter.text = "Kills Remaining: " + (24 - zombiesKilled);
            }
        }
    }

    public int IncrementZombiesKilled ()
    {
        SetZombiesKilled(zombiesKilled + 1);
        return zombiesKilled;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "HealthPowerUp")
        {
            if (health <= 70)
                SetHealth(health + 30);
            else
                SetHealth(100);
        }
        else if (col.gameObject.name == "AmmoPowerUp")
        {
            ammo = 30;
        }
        else if (col.gameObject.CompareTag("Enemy") && hurtTimer <= 0)
        {
            SetHealth(health - 5);
            hurtTimer = 30;
        }
    }
}
