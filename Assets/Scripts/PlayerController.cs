using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float angularVelocity;
    public float speed;
    
    public Transform cam;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.angularVelocity = new Vector3(0,angularVelocity,0);
        _rb.maxAngularVelocity = angularVelocity;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;
        
        _rb.AddForce(camF * (_verticalInput * speed) + camR * (_horizontalInput * speed));
    }
}
