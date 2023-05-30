using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    [SerializeField]private float spinningVelocity;
    [SerializeField] private float maxDistanceToCenter = 10;
    public float resistanceToOutOfBounds = 3000;
    [SerializeField] private Vector3 fieldCenter = new Vector3(0, -0.5f, 0);
    
    public GameObject player;
    public Transform cam;
    [SerializeField]private LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        _rb.maxAngularVelocity = spinningVelocity;
    }
    
    void FixedUpdate()
    {
        _rb.angularVelocity = new Vector3(1,spinningVelocity,0);

        Vector3 enemyPos = transform.position;
        
        Vector3 targetFieldCenter = (enemyPos - fieldCenter);
        float distanceToFieldCenter = Vector3.Distance(enemyPos, fieldCenter);
        
        Vector3 targetPlayer = enemyPos - player.transform.position;

        if (Utilities.IsGrounded(transform.position, ground))
        {
            if (distanceToFieldCenter > maxDistanceToCenter)
            {
                _rb.AddForce((Utilities.GetCamF(cam) - targetFieldCenter * resistanceToOutOfBounds) + (Utilities.GetCamR(cam) - targetFieldCenter * resistanceToOutOfBounds).normalized, ForceMode.Force);
            }
            else
            {
                _rb.AddForce((Utilities.GetCamF(cam) - targetPlayer * speed) + (Utilities.GetCamR(cam) - targetPlayer * speed).normalized, ForceMode.Acceleration);
            }
        }
        
        Utilities.ResetGame(gameObject);
    }

    public void ResetResistance()
    {
        resistanceToOutOfBounds = 3000;
    }
}
