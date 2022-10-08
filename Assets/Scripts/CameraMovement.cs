using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float accelX;
    public float accelY;
    const float deltaPerSecond = 2f;
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
                accelX -= deltaPerSecond * Time.deltaTime;
                if (accelX < 0)
                {
                    accelX = 0f;
                }
            }
            else if (accelX < 0)
            {
                accelX += deltaPerSecond * Time.deltaTime;
                if (accelX > 0)
                {
                    accelX = 0f;
                }
            }
            if (accelY > 0)
            {
                accelY -= deltaPerSecond * Time.deltaTime;
                if (accelY < 0)
                {
                    accelY = 0f;
                }
            }
            else if (accelY < 0)
            {
                accelY += deltaPerSecond * Time.deltaTime;
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
            accelX -= deltaPerSecond * Time.deltaTime;
            if (accelX < -max)
            {
                accelX = -max;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            accelX += deltaPerSecond * Time.deltaTime;
            if (accelX > max)
            {
                accelX = max;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            accelY -= deltaPerSecond * Time.deltaTime;
            if (accelY < -max)
            {
                accelY = -max;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            accelY += deltaPerSecond * Time.deltaTime;
            if (accelY > max)
            {
                accelY = max;
            }
        }
    }
}
