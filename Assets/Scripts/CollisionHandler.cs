using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    public Transform cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;

        Vector3 targetPos = other.transform.position - gameObject.transform.position;

        if (other.CompareTag("boxCollider"))
        {
            //get enemy Rigidbody
            Transform parent = other.transform.parent;
            Rigidbody enemyRb = parent.GetComponent<Rigidbody>();
            Debug.Log(enemyRb);
            
            //Apply Forces to both objects
            enemyRb.AddForce((camF + targetPos * speed) + (camR + targetPos * speed), ForceMode.Impulse);
            _rb.AddForce((camF - targetPos * (speed / 2)) + (camR - targetPos * (speed / 2)), ForceMode.Impulse);
        }
    }
}
