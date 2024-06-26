using UnityEngine;
using Random = UnityEngine.Random;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private int targetValue;
    [SerializeField] private float dropForce;

    [SerializeField] private Transform[] Edge;
    
    

    private Rigidbody rigidbody;

    private void Awake()
    {
        
        Debug.Log(Vector3.Angle(transform.up, Edge[5].position));
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       // Drop();
    }

    private void Drop()
    {
        GetRandomRotation();
        rigidbody.AddForce(Vector3.forward*dropForce,ForceMode.Impulse);
    }

    private void Update()
    {
       
    }

    private void GetRandomRotation()
    {
        float randomX = Random.Range(0, 360);
        float randomY = Random.Range(0, 360);
        float randomZ = Random.Range(0, 360);

        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, randomZ);
        transform.rotation = randomRotation;
    }
}
