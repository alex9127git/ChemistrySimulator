using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float accelX;
    public float accelY;
    const float delta = 0.02f;
    const float max = 10f;

    void Start()
    {
        accelX = 0f;
        accelY = 0f;
    }

    void Update()
    {
        DecelerateOverTime();
        Accelerate();
        transform.position += new Vector3(accelX * Time.deltaTime, accelY * Time.deltaTime);
    }

    void DecelerateOverTime()
    {
        if (!Input.anyKey)
        {
            if (accelX > 0)
            {
                accelX -= delta;
                if (accelX < 0)
                {
                    accelX = 0f;
                }
            }
            else if (accelX < 0)
            {
                accelX += delta;
                if (accelX > 0)
                {
                    accelX = 0f;
                }
            }
            if (accelY > 0)
            {
                accelY -= delta;
                if (accelY < 0)
                {
                    accelY = 0f;
                }
            }
            else if (accelY < 0)
            {
                accelY += delta;
                if (accelY > 0)
                {
                    accelY = 0f;
                }
            }
        }
    }

    void Accelerate()
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
    }
}
