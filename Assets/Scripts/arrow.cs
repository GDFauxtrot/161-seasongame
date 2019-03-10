using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float radius;
    public Transform destination;
    
    void Update()
    {
        transform.localPosition = Vector3.zero;

        if (destination)
        {
            //take difference in position so we know where to point
            Vector3 pointing = (destination.position - transform.position).normalized;
            float rotateZ = Mathf.Atan2(pointing.y, pointing.x) * Mathf.Rad2Deg;

            //rotate the arrow using the differences saved
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

            // Move forward by radius amount from the center of where we are
            transform.position = transform.position + transform.right * radius;
        }
    }
}
