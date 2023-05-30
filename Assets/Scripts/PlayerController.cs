using System;
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
    [SerializeField] private float bounceSpeed;
    
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask ground;
    
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
        
        if (Utilities.IsGrounded(transform.position, ground))
        {
            _rb.AddForce((Utilities.GetCamF(cam) * (_verticalInput * speed) + Utilities.GetCamR(cam) * (_horizontalInput * speed)), ForceMode.Acceleration);
        }
        
        Utilities.ResetGame(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 targetPos = other.transform.position - gameObject.transform.position;
        
        if (other.transform.parent.CompareTag("Enemy"))
        {
            //get Rigidbody of parent
            Transform parent = other.transform.parent;
            Rigidbody enemyRb = parent.GetComponent<Rigidbody>();
            Debug.Log(enemyRb);

            parent.GetComponent<EnemyController>().resistanceToOutOfBounds = 1000;
            
            //Apply Forces to both objects
            enemyRb.AddForce((Utilities.GetCamF(cam) + targetPos * bounceSpeed) + (Utilities.GetCamR(cam) + targetPos * bounceSpeed), ForceMode.Impulse);
            _rb.AddForce((Utilities.GetCamF(cam) - targetPos * (bounceSpeed / 2)) + (Utilities.GetCamR(cam) - targetPos * (bounceSpeed / 2)), ForceMode.Impulse);

            parent.GetComponent<EnemyController>().Invoke(nameof(EnemyController.ResetResistance),2);
        }
    }
}
