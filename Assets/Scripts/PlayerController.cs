using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float spinningVelocity;
    [SerializeField] private float speed;

    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask ground;

    [SerializeField] private bool canMove = true;
    
    private float _horizontalInput;
    private float _verticalInput;
    
    //UI
    public TMP_Text velocity;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = spinningVelocity;
        _rb.maxLinearVelocity = 15;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.angularVelocity = new Vector3(.5f,spinningVelocity,0);

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        if (Utilities.IsGrounded(transform.position, ground) && canMove)
        {
            _rb.AddForce((Utilities.GetCamF(cam) * (_verticalInput * speed) + Utilities.GetCamR(cam) * (_horizontalInput * speed)), ForceMode.Acceleration);
        }
        
        Utilities.ResetGame(gameObject);
    }
    
    private void LateUpdate()
    {
        velocity.text = ((int)_rb.velocity.magnitude).ToString();
        Quaternion rotation = cam.transform.rotation;
        canvas.transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void CanMove()
    {
        canMove = true;
    }
    public void CanMove(bool state)
    {
        canMove = state;
    }
}
