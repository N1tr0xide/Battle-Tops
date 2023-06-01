using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private float bounceSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        rb = gameObject.transform.parent.GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            Utilities.HandleCollision(gameObject, other, bounceSpeed, cam);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            Utilities.HandleCollision(gameObject, other, bounceSpeed, cam);
        }
    }
}
