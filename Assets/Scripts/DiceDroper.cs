using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceDroper : MonoBehaviour
{
    [SerializeField] private int diceCount = 4;
    [SerializeField] private Transform[] dices;
    [SerializeField] private int simulationTime;
    
    private Quaternion randomRotation;
    private Vector3 randomForce;
    private Vector3 randomTorque;
    private List<Vector3> startingDicePositions = new List<Vector3>();
    private List<Rigidbody> diceRigidbody = new List<Rigidbody>();
    private Dictionary<int, List<AnimationRecordData>> animationFrames =new Dictionary<int, List<AnimationRecordData>>();

    private void Start()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            startingDicePositions.Add(dices[i].position);
            diceRigidbody.Add(dices[i].GetComponent<Rigidbody>());
            
            DisablePhysics(diceRigidbody[i]);
        }

        for (int i = 0; i < dices.Length; i++)
        {
            animationFrames.Add(i,new List<AnimationRecordData>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DropDice();
        }
    }

    private void InitializeDicesState()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].position = startingDicePositions[i];
            dices[i].rotation = InitializeRotation();
            
            EnablePhysics(diceRigidbody[i]);

            diceRigidbody[i].velocity = InitializeForce();
            diceRigidbody[i].AddTorque(InitializeTorque(),ForceMode.VelocityChange);
        }
    }

    private void DropDice()
    {
        Physics.simulationMode = SimulationMode.Script;
        
        InitializeDicesState();
        ClearAnimationRecordData();
        RecordAnimation();
        RotateDices();

        Physics.simulationMode = SimulationMode.FixedUpdate;

        StartCoroutine(PlayAnimation());
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
    
    private void RotateDices()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].GetComponent<DiceRotator>().RotateDiceMesh(diceCount);
        }
    }
    
    private void RecordAnimation()
    {
        for (int i = 0; i <= simulationTime ; i++)
        {
            for (int j = 0; j < dices.Length; j++)
            {
                animationFrames[j].Add(new AnimationRecordData(dices[j].position,dices[j].rotation));
                
            }
            
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }

    private IEnumerator PlayAnimation()
    {
        for (int i = 0; i <= simulationTime; i++)
        {
            for (int j = 0; j < dices.Length; j++)
            {
                dices[j].transform.position = animationFrames[j][i].FramePosition;
                dices[j].transform.rotation = animationFrames[j][i].FrameRotation;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void ClearAnimationRecordData()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            if(animationFrames[i] != null)
                animationFrames[i].Clear();
        }
    }
}


[System.Serializable]
public class AnimationRecordData
{
    private Vector3 framePosition;
    private Quaternion frameRotation;

    public Vector3 FramePosition => framePosition;
    public Quaternion FrameRotation => frameRotation;

    public AnimationRecordData(Vector3 position, Quaternion rotation)
    {
        this.framePosition = position;
        this.frameRotation = rotation;
    }
}
