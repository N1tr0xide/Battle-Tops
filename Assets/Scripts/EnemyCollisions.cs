using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private float impulseMultiplier;
    private Transform _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            HandleCollision(other);
        }
    } 
    
    private void HandleCollision(Collider otherCollider)
    {
        Vector3 bounceDirection = otherCollider.transform.position - transform.position;
        
        //get Rigidbody of gameObject parent
        Transform parent = transform.parent;
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        
        //get Rigidbody of other.parent
        Transform otherParent = otherCollider.transform.parent;
        Rigidbody oherRb = otherParent.GetComponent<Rigidbody>();

        //Calculate direction and impulse
        Vector3 forceToOther = (Utilities.GetCamF(_cam) + bounceDirection * rb.velocity.magnitude * impulseMultiplier) + 
                               (Utilities.GetCamR(_cam) + bounceDirection * rb.velocity.magnitude * impulseMultiplier);
        Vector3 forceToThis = (Utilities.GetCamF(_cam) - bounceDirection * oherRb.velocity.magnitude * (impulseMultiplier)) + 
                              (Utilities.GetCamR(_cam) - bounceDirection * oherRb.velocity.magnitude * (impulseMultiplier));
        
        //Apply force
        oherRb.AddForce(forceToOther, ForceMode.Impulse);
        rb.AddForce(forceToThis, ForceMode.Impulse);
    }
}
