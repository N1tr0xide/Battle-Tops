using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float spinningVelocity;
    [SerializeField] private float speed;
    
    [SerializeField] private Transform cam;
    
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = spinningVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.angularVelocity = new Vector3(1,spinningVelocity,0);
        
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0;

        _rb.AddForce(camF * (_verticalInput * speed) + camR * (_horizontalInput * speed));
    }
}
