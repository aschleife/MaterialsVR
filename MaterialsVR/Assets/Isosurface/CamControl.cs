using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    float move_speed = 2.0f;
    float rotate_speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += move_speed * Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += move_speed * Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += move_speed * Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += move_speed * Vector3.back;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.transform.position += move_speed * Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.transform.position += move_speed * Vector3.down;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.up, rotate_speed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(Vector3.down, rotate_speed);
        }


    }
}
