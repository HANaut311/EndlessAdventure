using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spiked_Ball : MonoBehaviour 
{

    [SerializeField] private float MaxSize;
    [SerializeField] private float MinSize;
    public  float direction; // initial direction
    [SerializeField] private float speed = 20f; // speed of rotation

    void FixedUpdate ()
    {
        float angle = transform.eulerAngles.z;
        if (angle > 180f) angle -= 360f;

        if ((angle <= -MaxSize) || (angle >= MinSize)) direction *= -1f; // reverse direction (toggles between 1 & -1)

        transform.Rotate (0, 0, speed * direction * Time.fixedDeltaTime);
    }

}
