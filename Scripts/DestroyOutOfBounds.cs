using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float bottomLimit = -5f;
    private float planeLimit = 30f;

    // Update is called once per frame
    void Update()
    {
        if (BoundsCheck())
        {
            Destroy(gameObject);
        }
    }

    bool BoundsCheck()
    {
        if (transform.position.y < bottomLimit)
        {
            return true;
        }
        if (Math.Abs(transform.position.x) > planeLimit)
        {
            return true;
        }
        if (Math.Abs(transform.position.z) > planeLimit)
        {
            return true;
        }
        return false;
    }
}
