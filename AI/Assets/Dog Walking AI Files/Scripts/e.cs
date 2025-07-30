using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e : MonoBehaviour
{

    public float force;
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddTorque(force);
    }
}
