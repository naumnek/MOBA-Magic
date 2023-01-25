//Attach this script to a GameObject. Make sure it has a Collider component by clicking the Add Component button. Then click Physics>Box Collider to attach a Box Collider component.
//This script creates a BoxCast in front of the GameObject and outputs a message if another Collider is hit with the Collider’s name.
//It also draws where the ray and BoxCast extends to. Just press the Gizmos button to see it in Play Mode.
//Make sure to have another GameObject with a Collider component for the BoxCast to collide with.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastCubeTest : MonoBehaviour
{
    public float m_Size = 3.0f;
    public float m_MaxDistance = 2.0f;
    public float m_Speed = 1.0f;
    public bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        //Simple movement in x and z axes
        float xAxis = Input.GetAxis("Horizontal") * m_Speed;
        float zAxis = Input.GetAxis("Vertical") * m_Speed;
        transform.Translate(new Vector3(xAxis, 0, zAxis));
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        foreach (var hit in Physics.BoxCastAll(m_Collider.bounds.center, transform.localScale, transform.forward, transform.rotation, m_MaxDistance))
        {
            if (hit.collider != null)
            {
                m_HitDetect = true;
                m_Hit = hit;
                //Output the name of the Collider your Box hit
                Debug.Log("Hit : " + m_Hit.collider.name);
            }
            else
            {
                m_HitDetect = false;
            }
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 size = new Vector3(m_Size, m_Size, m_Size);
        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, size);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, size);
        }
    }
}
