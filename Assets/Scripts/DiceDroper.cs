using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceDroper : MonoBehaviour
{
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
            diceRigidbody.Add(dices[i].GetComponent<Rigidbody>());
            DisablePhysics(diceRigidbody[i]);
            
            startingDicePositions.Add(dices[i].position);
            
            animationFrames.Add(i,new List<AnimationRecordData>());
        }
    }
     
    // Симулируем бросок кубика, поворачиваем, меш и воспроизводим анимацию
    
    public void DropDice(params int[] trueValues)
    {
        Physics.simulationMode = SimulationMode.Script;
        
        InitializeDicesState();
        ClearAnimationRecordData();
        RecordAnimation();
        
        Physics.simulationMode = SimulationMode.FixedUpdate;
        
        RotateDices(trueValues);
        StartCoroutine(PlayAnimation());
    }

    #region Initialize Dices

    // Задаём кубикам позицую, поворот, физическое вращение и силу
    
    private void InitializeDicesState()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].position = startingDicePositions[i];
            
            EnablePhysics(diceRigidbody[i]);
            
            dices[i].rotation = InitializeRotation();
            diceRigidbody[i].velocity = InitializeForce();
            diceRigidbody[i].AddTorque(InitializeTorque());
        }
    }

    private Vector3 InitializeTorque()
    {
        int randomX = Random.Range(0, 15);
        int randomY = Random.Range(0, 15);
        int randomZ = Random.Range(0, 15);
        
        randomTorque.Set(randomX,randomY,randomZ);
        return randomTorque;
    }

    private Vector3 InitializeForce()
    {
        int randomX = Random.Range(0, 3);
        int randomY = Random.Range(0, 3);
        int randomZ = Random.Range(5, 10);
        
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

    #endregion

    #region On/off Physics

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

    #endregion
    
    #region Animation

    
    // Во время симуляции записываем позицию и вращение кубиков в каждом кадре
    
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

    // Проигрываем анимацию, задавая  новую позицию и вращение кубикам
    
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

    // Очищаем контейнер с анимацией, перед записью новой анимации
    
    private void ClearAnimationRecordData()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            if(animationFrames[i] != null)
                animationFrames[i].Clear();
        }
    }

    #endregion
    
    // Поворачиваем меш кубиков под нужным углом, в зависимости от параметров trueValues
    
    private void RotateDices( params int[] trueValues)
    {
        for (int i = 0; i < trueValues.Length; i++)
            dices[i].GetComponent<DiceRotator>().RotateDiceMesh(trueValues[i]);
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
        framePosition = position;
        frameRotation = rotation;
    }
}
