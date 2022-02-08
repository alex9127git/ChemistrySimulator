using UnityEngine;

public class MouseFollowScript : MonoBehaviour
{
    bool placed = false;
    // Update is called once per frame
    void Update()
    {
        if (!placed)
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.x = (int)(v.x) - 0.5f;
            v.y = (int)(v.y) - 0.5f;
            v.z = 0;
            transform.position = v;
            transform.rotation = Quaternion.identity;
            if (Input.GetMouseButtonDown(0))
            {
                placed = true;
            }
        }
    }
}
