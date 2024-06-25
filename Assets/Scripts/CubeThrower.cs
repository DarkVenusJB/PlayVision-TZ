using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private int targetValue;
    [SerializeField] private float dropForce;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Drop();
    }

    private void Drop()
    {
        GetRandomRotation();
        rigidbody.AddForce(Vector3.forward*dropForce,ForceMode.Impulse);
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
