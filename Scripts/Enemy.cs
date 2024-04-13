using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 2f;
    public int enemyHealth = 10;

    private GameObject player;
    private Animator enemyAnim;
    private float rotationSpeed = 3f;
    private bool seePlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAnim = GetComponent<Animator>();
    }

    
    // Update is called once per frame
    void Update()
    {  
        if (player != null)
        {
            FollowPlayer();
        }

        if (enemyHealth < 1)
        {
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }

    void FollowPlayer()
    {
        // Direction to player
        Vector3 directionToPlayer = player.transform.position - transform.position;
        transform.position += directionToPlayer.normalized * enemySpeed * Time.deltaTime;

        // Move into walk state when we follow the player.
        if (!seePlayer)
        {
            Debug.Log("Setting bool");
            enemyAnim.SetBool("Walk_Cycle_1", true);
            seePlayer = true;
        }

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // Ensure the enemy does not tilt vertically

        // Rotate enemy to face the player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }
}
