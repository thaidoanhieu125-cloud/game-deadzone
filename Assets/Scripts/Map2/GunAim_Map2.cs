using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim_Map2 : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos =
            Camera.main.ScreenToWorldPoint(mousePos);

        mousePos.z = 0;

        Vector2 direction =
            mousePos - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angle);
        if (transform.parent.localScale.x < 0)
        {
            transform.rotation =
                Quaternion.Euler(0, 0, angle + 180f);
        }
        // flip súng
        if (mousePos.x < transform.position.x)
        {
            transform.localScale =
                new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale =
                new Vector3(1, 1, 1);
        }
    }
}
