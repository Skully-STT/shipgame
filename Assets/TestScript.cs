using System;
using System.Collections.Generic;
using UnityEngine;



public class TestScript : MonoBehaviour
{
    public Vector3 waterUpDrag = new Vector3(0, 20, 0);
    public float viscosity = 2;
    public new Rigidbody rigidbody;
    public BoxCollider boxCollider;
    private Vector3 size;
    private Vector3[] colliderPoints;

    public void Start()
    {
        colliderPoints = new Vector3[9];
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        size = boxCollider.size;
        colliderPoints[0] = transform.position;
        colliderPoints[1] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[2] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[3] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[4] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[5] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[6] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, -size.z / 2));
        colliderPoints[7] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[8] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, -size.z / 2));
    }   

    public void OnDrawGizmosSelected()
    {
        colliderPoints = new Vector3[9];
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        size = boxCollider.size;
        colliderPoints[0] = transform.position;
        colliderPoints[1] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[2] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[3] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[4] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[5] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[6] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, -size.z / 2));
        colliderPoints[7] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[8] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, -size.z / 2));
        Gizmos.color = Color.red;
        foreach (Vector3 point in colliderPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, rigidbody.velocity);
    }

    private void Update()
    {
        colliderPoints[0] = transform.position;
        colliderPoints[1] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[2] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, size.z / 2));
        colliderPoints[3] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[4] = transform.TransformPoint(new Vector3(size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[5] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, size.z / 2));
        colliderPoints[6] = transform.TransformPoint(new Vector3(size.x / 2, -size.y / 2, -size.z / 2));
        colliderPoints[7] = transform.TransformPoint(new Vector3(-size.x / 2, size.y / 2, -size.z / 2));
        colliderPoints[8] = transform.TransformPoint(new Vector3(-size.x / 2, -size.y / 2, -size.z / 2));

        Vector3 vec3 = new Vector3(0, 0, 0);
        int count = 0;
        foreach (Vector3 point in colliderPoints)
        {
            if (CheckIfPointIsUnderwater(point))
            {
                count++;
                vec3 += point;
            }
        }
        if (count>0)
        {
            rigidbody.AddForceAtPosition(waterUpDrag, new Vector3(vec3.x / count, vec3.y / count, vec3.z / count));
        }
        else
        {
            rigidbody.AddForce(new Vector3(0, 0, 0));
        }
        rigidbody.AddForce(rigidbody.velocity * -1 * viscosity);
        //Vector3 v1 = Vector3.Cross(rigidbody.velocity, Vector3.up);
        //Vector3 v2 = Vector3.Cross(rigidbody.velocity, v1);
        //RaycastHit hit;

        //Vector3 pointOutFront = transform.position + (rigidbody.velocity * 10);
        //float width = GetLongestSide();
        //for (float x = -width; x <= width; x++)
        //{
        //    for (float y = -width; y <= width; y+=1)
        //    {
        //        Vector3 start = pointOutFront +(v1*x)+(v2*y);
        //        if (Physics.Raycast(start,-rigidbody.velocity,out hit,10))
        //        {
        //            rigidbody.AddForceAtPosition(new Vector3(viscosity,viscosity,viscosity), hit.point);
        //        }
        //    }
        //}
        //foreach
        //if (transform.position.y < 0)
        //{
        //    rigidbody.AddForce(waterUpDrag);
        //    rigidbody.AddForce(rigidbody.velocity * -1 * viscosity);
        //}
    }

    private float GetLongestSide()
    {
        float biggest = size.x;
        biggest = size.y > biggest ? size.y : biggest;
        biggest = size.z > biggest ? size.z : biggest;
        return biggest;
    }

    private bool CheckIfPointIsUnderwater(Vector3 point)
    {
        if (point.y<0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}