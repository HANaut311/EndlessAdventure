using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WindArea : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    [SerializeField] private float SizeY;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.up * range* colliderDistance,
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * SizeY, boxCollider.bounds.size.z));
    }
}
