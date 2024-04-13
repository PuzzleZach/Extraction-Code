using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 30f;
    public float lookAhead = 10f; // For hitscan
    public int damage = 1;

    private GameObject player;
    private Vector3 firePoint;
    private bool enemyHit = false;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //firePoint = player.transform.position;
    }

    // use fixed update

    // Update is called once per frame
    void Update()
    {
        if (enemyHit == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, lookAhead))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Trigger the enemy damage
                    enemy.TakeDamage(damage);
                    Debug.Log("Damage being dealt.");
                    enemyHit = true;
                    distance = Vector3.Distance(enemy.transform.position, transform.position);
                }
            }
        }
       
        if (enemyHit)
        {
            // Now we want to destroy our bullet once it passes an enemy.
            distance -= bulletSpeed * Time.deltaTime;
            if (distance < 0)
            {
                Debug.Log("passed enemy");
                Destroy(gameObject);
            }

        }
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);

        // Cast ray to see if enemy is within 10 m of us.
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage being dealt.");
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage being dealt.");
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }



}
