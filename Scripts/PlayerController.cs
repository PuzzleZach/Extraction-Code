using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // We will want be able to shoot our gun
    // but the gun will handle finding and hitting enemies.

    public int health = 100;
    public float walkSpeed;
    public float sprintMeter = 10f; // Can sprint for 10 seconds.

    public AudioClip walkSounds;
    public AudioClip playerDamage;

    private float horizontalInput; // X
    private float verticalInput; // Z
    private bool isAlive = true;
    private bool isSprinting = false;
    private Rigidbody rb;
    private float lowerBound = -5f;
    private Quaternion originalRotation;
    private float rotationSpeed = 360f;


    private Animator playerAnim;
    private AudioSource playerAudio;

    // Since this is a SEAF soldier,
    // we will have them move around based on WASD inputs
    // however, they will rotate based on the mouse position
    // when they rotate, it will not impede how they move

    // Mouse tracking could be done in a child script
    // so i dont have to worry about this

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (isAlive)
        {
            MovementHandler();
            TrackMouse();
        }
        
    }

    void TrackMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, Camera.main.transform.position.y));

        Vector3 direction = mousePos - transform.position;
        direction.y = 0;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    void MovementHandler()
    {
        // Move in X or Z direction.
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = originalRotation * movementDirection * walkSpeed * Time.deltaTime;
        transform.position += movement;

        //transform.Translate(Vector3.forward * verticalInput * walkSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * horizontalInput * walkSpeed * Time.deltaTime);

        if (transform.position.y < lowerBound)
        {
            Death();
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (playerAudio.clip != walkSounds)
            {
                playerAudio.clip = walkSounds;
            }   
            // Whenever we move, play noise.
            if (!playerAudio.isPlaying)
            {
                playerAudio.volume = 0.2f;
                playerAudio.Play();
            }
            playerAnim.SetFloat("Speed_f", 1);
        }
        else
        {
            // Idle mode.
            playerAudio.Pause();
            playerAnim.SetFloat("Speed_f", 0);
        }
    }

    void Death()
    {
        isAlive = false;
        Debug.Log("The player has died.");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        playerAudio.volume = 0.5f;
        playerAudio.PlayOneShot(playerDamage);
    }
}
