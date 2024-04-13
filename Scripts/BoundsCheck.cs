using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    private float warningTime = 10f;
    private bool outOfBounds = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfBounds)
        {
            warningTime -= Time.deltaTime;
        }

        if (warningTime < 0)
        {
            Debug.Log("You have abandoned your mission.");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The player has entered out of bounds.");
            outOfBounds = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The player has returned to the battlefield.");
            outOfBounds = false;
            warningTime = 10f;
        }
    }
}
