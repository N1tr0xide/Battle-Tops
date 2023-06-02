using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject player;
    public Transform cam;
    [SerializeField]private LayerMask ground;
        
    public float speed;
    [SerializeField]private float spinningVelocity;
    [SerializeField] private float maxDistanceToCenter = 10;
    public float resistanceToOutOfBounds = 3000;
    [SerializeField] private Vector3 fieldCenter = new Vector3(0, -0.5f, 0);

    //UI
    public TMP_Text velocity;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        _rb.maxAngularVelocity = spinningVelocity;
        _rb.maxLinearVelocity = 10;
    }
    
    void FixedUpdate()
    {
        _rb.angularVelocity = new Vector3(.5f,spinningVelocity,0);
        Movement();
        
        if (transform.position.y < -4f)
        {
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        velocity.text = ((int)_rb.velocity.magnitude).ToString();
        Quaternion rotation = cam.transform.rotation;
        canvas.transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    void Movement()
    {
        Vector3 enemyPos = transform.position;
        
        Vector3 targetPlayer = enemyPos - player.transform.position;
        Vector3 targetFieldCenter = (enemyPos - fieldCenter);
        float distanceToFieldCenter = Vector3.Distance(enemyPos, fieldCenter);
        
        if (Utilities.IsGrounded(transform.position, ground))
        {
            if (distanceToFieldCenter > maxDistanceToCenter)
            {
                Vector3 force = (Utilities.GetCamF(cam) - targetFieldCenter * resistanceToOutOfBounds) + (Utilities.GetCamR(cam) - targetFieldCenter * resistanceToOutOfBounds);
                _rb.AddForce(force, ForceMode.Force);
            }
            else
            {
                Vector3 force = (Utilities.GetCamF(cam) - targetPlayer * speed) + (Utilities.GetCamR(cam) - targetPlayer * speed);
                _rb.AddForce(force, ForceMode.Acceleration);
            }
        }
    }
}
