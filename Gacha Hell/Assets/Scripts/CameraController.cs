using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * camSpeed * Time.deltaTime, Space.World);
        }


        if(Input.GetKey("e"))
        {
            transform.Translate(Vector3.down * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("q"))
        {
            transform.Translate(Vector3.up * camSpeed * Time.deltaTime, Space.World);
        }
    }
}
