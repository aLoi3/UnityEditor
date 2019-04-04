using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float rotSpeed = 200.0f;
    public float movSpeed = 8.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // var rot = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;

        // transform.Rotate(0, rot, 0);
        transform.Translate(x, 0, z);

    }
}