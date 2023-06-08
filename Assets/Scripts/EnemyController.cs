using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rb;
    private GameObject _player;
    private Transform _cam;
    public float speed;
    private float _spinningVelocity = 30;
    [SerializeField] private bool canMove = true;
    [SerializeField]private LayerMask ground;

    public float resistanceToOutOfBounds = 200;
    public float maxDistanceToCenter = 9;
    public Vector3 fieldCenter = new Vector3(0, -0.5f, 0);
    
    //UI
    public TMP_Text velocity;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        _player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        _rb.maxAngularVelocity = _spinningVelocity;
        _rb.maxLinearVelocity = 15;
    }
    
    void FixedUpdate()
    {
        _rb.angularVelocity = new Vector3(.5f,_spinningVelocity,0);
        Movement();
        
        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        velocity.text = ((int)_rb.velocity.magnitude).ToString();
        Quaternion rotation = _cam.transform.rotation;
        canvas.transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    void Movement()
    {
        Vector3 enemyPos = transform.position;
        Vector3 targetPlayer = enemyPos - _player.transform.position;
        Vector3 targetFieldCenter = enemyPos - fieldCenter;
        float distanceToFieldCenter = Vector3.Distance(enemyPos, fieldCenter);

        if (Utilities.IsGrounded(transform.position, ground) && canMove)
        {
            if (distanceToFieldCenter > maxDistanceToCenter)
            {
                _rb.AddForce((Utilities.GetCamF(_cam) - targetFieldCenter * resistanceToOutOfBounds) + (Utilities.GetCamR(_cam) - targetFieldCenter * resistanceToOutOfBounds), ForceMode.Force);
            }
            else
            {
                _rb.AddForce((Utilities.GetCamF(_cam) - targetPlayer * speed) + (Utilities.GetCamR(_cam) - targetPlayer * speed), ForceMode.Acceleration);
            }
        }
    }
    
    public void CanMove(bool state)
    {
        canMove = state;
    }
}
