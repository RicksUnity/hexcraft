
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayeru : MonoBehaviour {

    UnityEngine.Rigidbody rb;
    private float xpos = 0.0f;
    private float zpos = 0.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        rb = GetComponent<UnityEngine.Rigidbody>();
    }

    void Update()
    {

        xpos = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        zpos = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        yaw += Input.GetAxis("Mouse X") * Time.deltaTime * 155.0f;
        pitch -= Input.GetAxis("Mouse Y") * Time.deltaTime * 155.0f;
        var jumping = 0.0f;
        var zxpos = Mathf.Sin((Mathf.PI*yaw / 180))*zpos;
        var zzpos = Mathf.Cos((Mathf.PI * yaw / 180) )*zpos;

        if (Input.GetKeyDown(KeyCode.Space))
            jumping = 3.2f;

        transform.Translate(zxpos, 0, zzpos,Space.World);
        transform.Translate(xpos, 0,0);
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
        rb.AddForce(new Vector3(0, jumping, 0), ForceMode.Impulse);

    }
}



