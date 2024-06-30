using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDroper : MonoBehaviour
{
    //[SerializeField] private Transform [] Faces;
    [SerializeField] private Transform[] dices;

    private Quaternion randomRotation;
    private Vector3 randomForce;
    private Vector3 randomTorque;
    private List<Vector3> startingDicePositions;
    private List<Rigidbody> diceRigidbody;

    private void Start()
    {
        foreach (var dice in dices)
        {
            startingDicePositions.Add(dice.position);
            diceRigidbody.Add(dice.GetComponent<Rigidbody>());
            
        }
    }

    private void InitializeDicesState()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].position = startingDicePositions[i];
            dices[i].rotation = InitializeRotation();
            
            EnablePhysics(diceRigidbody[i]);
            
            diceRigidbody[i].AddForce(InitializeForce());
            diceRigidbody[i].AddTorque(InitializeTorque());
        }
    }

    private Vector3 InitializeTorque()
    {
        int randomX = Random.Range(0, 1);
        int randomY = Random.Range(0, 1);
        int randomZ = Random.Range(0, 1);
        
        randomTorque.Set(randomX,randomY,randomZ);
        return randomTorque;
    }

    private Vector3 InitializeForce()
    {
        int randomX = Random.Range(0, 2);
        int randomY = Random.Range(0, 2);
        int randomZ = Random.Range(3, 8);
        
        randomForce.Set(randomX,randomY,randomZ);
        return randomForce;
    }

    private Quaternion InitializeRotation()
    {
        int randomX = Random.Range(0, 360);
        int randomY = Random.Range(0, 360);
        int randomZ = Random.Range(0, 360);
        
        randomRotation = Quaternion.Euler(randomX,randomY,randomZ);
        return randomRotation;
    }

    private void EnablePhysics(Rigidbody diceRb)
    {
        diceRb.useGravity = true;
        diceRb.isKinematic = false;
    }
    
    private void DisablePhysics(Rigidbody diceRb)
    {
        diceRb.useGravity = false;
        diceRb.isKinematic = true;
    }
}
