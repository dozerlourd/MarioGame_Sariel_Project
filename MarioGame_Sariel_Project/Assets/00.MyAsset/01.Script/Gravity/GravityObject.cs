using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    [SerializeField] bool isCustomizingGravity = false;
    [SerializeField] float gravity = 9.81f;

    private void FixedUpdate()
    {
        if(isCustomizingGravity)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (gravity * Time.deltaTime), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (9.81f * Time.deltaTime), transform.position.z);
        }
    }
}
