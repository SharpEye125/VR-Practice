using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalHand;
    public float showNonPhysicalHandDistance = 0.05f;
    private Collider[] handColliders;
    public float physDelay = 1f;
    bool isGrabbing;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > showNonPhysicalHandDistance)
        {
            nonPhysicalHand.enabled = true;
        }
        else
        {
            nonPhysicalHand.enabled = false;
        }
        if (rb.isKinematic == true)
        {
            isGrabbing = true;
            DisableHandCollider();
        }
        else if (rb.isKinematic == false && isGrabbing == true)
        {
            isGrabbing = false;
            EnableColliderDelay(physDelay);
        }
    }
    void FixedUpdate()
    {
        //
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree *Mathf.Deg2Rad/ Time.fixedDeltaTime);

    }
    public void EnableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }
    public void DisableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }
    public void EnableColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }
}
