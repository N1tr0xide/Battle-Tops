using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private float impulseMultiplier;
    private Transform _cam;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        _playerController = GetComponentInParent<PlayerController>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            _playerController.CanMove(false);
            other.transform.parent.GetComponent<EnemyController>().CanMove(false);
            HandleCollision(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerController.CanMove(true);
        other.transform.parent.GetComponent<EnemyController>().CanMove(true);
    }

    private void HandleCollision(Collider otherCollider)
    {
        Vector3 bounceDirection = otherCollider.transform.position - transform.position;
        
        //get Rigidbody of gameObject parent
        Transform parent = transform.parent;
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        
        //get Rigidbody of other.parent
        Transform otherParent = otherCollider.transform.parent;
        Rigidbody enemyRb = otherParent.GetComponent<Rigidbody>();

        //Calculate direction and impulse
        Vector3 forceToEnemy = (Utilities.GetCamF(_cam) + bounceDirection * rb.velocity.magnitude * impulseMultiplier) + 
                               (Utilities.GetCamR(_cam) + bounceDirection * rb.velocity.magnitude * impulseMultiplier);
        Vector3 forceToPlayer = (Utilities.GetCamF(_cam) - bounceDirection * enemyRb.velocity.magnitude * (impulseMultiplier / 4)) + 
                                (Utilities.GetCamR(_cam) - bounceDirection * enemyRb.velocity.magnitude * (impulseMultiplier / 4));
        
        //Apply force
        enemyRb.AddForce(forceToEnemy, ForceMode.Impulse);
        rb.AddForce(forceToPlayer, ForceMode.Impulse);
    }
}
