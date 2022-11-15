using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public GameObject key;
    Rigidbody keyRB;
    bool inLock = false;
    // Start is called before the first frame update
    void Start()
    {
        keyRB = key.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inLock == true)
        {
            if (key.transform.eulerAngles.x >= 0 || key.transform.eulerAngles.x <= -180f)
            {
                keyRB.constraints = RigidbodyConstraints.None;
                keyRB.isKinematic = false;
                key.GetComponent<PerObjectOoB>().BackToStartPos();
                inLock = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Key"))
        {
            inLock = true;
            key.transform.eulerAngles = new Vector3(-90, 180, 0);
            keyRB.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            keyRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
            keyRB.constraints = RigidbodyConstraints.FreezePositionX;
        }
        
    }
}
