using UnityEngine;

public class MouseFollowScript : MonoBehaviour
{
    bool placed = false;
    public static bool busy;
    // Update is called once per frame
    void Update()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distX = transform.position.x - v.x;
        float distY = transform.position.y - v.y;
        float dist = Mathf.Sqrt(distX * distX + distY * distY);
        if (!placed)
        {
            v.x = (int)(v.x) - 0.5f;
            v.y = (int)(v.y) - 0.5f;
            v.z = 0;
            transform.position = v;
            transform.rotation = Quaternion.identity;
            busy = true;
            if (Input.GetMouseButtonDown(0))
            {
                placed = true;
                busy = false;
            }
        } else
        {
            if (ModeSwitcherScript.modeType == 1 && Input.GetMouseButtonDown(0) && dist < 1 && !busy)
            {
                placed = false;
                busy = true;
            }
            if (ModeSwitcherScript.modeType == 2 && Input.GetMouseButtonDown(0) && dist < 1 && !busy)
            {
                Destroy(gameObject);
            }
        }
    }
}
