using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private float impulseMultiplier;
    [SerializeField] private Transform cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider"))
        {
            Utilities.HandleCollision(gameObject, other, impulseMultiplier, cam);
        }
    }
}
