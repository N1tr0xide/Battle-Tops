using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private float impulseMultiplier;
    [SerializeField] private Transform cam;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        _playerController = GetComponentInParent<PlayerController>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            _playerController.CanMove(false);
            Utilities.HandleCollision(gameObject, other, impulseMultiplier, cam);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerController.CanMove(true);
    }
}
