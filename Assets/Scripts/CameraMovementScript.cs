using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public float accelX;
    public float accelY;
    const float delta = 0.01f;
    const float slow = 0.005f;
    const float max = 5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(0, 0, -10);
        accelX = 0f;
        accelY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            accelX -= delta;
            if (accelX < -max)
            {
                accelX = -max;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            accelX += delta;
            if (accelX > max)
            {
                accelX = max;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            accelY -= delta;
            if (accelY < -max)
            {
                accelY = -max;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            accelY += delta;
            if (accelY > max)
            {
                accelY = max;
            }
        }
        if (accelX > 0)
        {
            accelX -= slow;
            if (accelX < 0) {
                accelX = 0f;
            }
        }
        else if (accelX < 0)
        {
            accelX += slow;
            if (accelX > 0)
            {
                accelX = 0f;
            }
        }
        if (accelY > 0)
        {
            accelY -= slow;
            if (accelY < 0)
            {
                accelY = 0f;
            }
        }
        else if (accelY < 0)
        {
            accelY += slow;
            if (accelY > 0)
            {
                accelY = 0f;
            }
        }
        transform.position += new Vector3(accelX * Time.deltaTime, accelY * Time.deltaTime);
    }
}
