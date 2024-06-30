using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "D6 Rotation Data", menuName = "ScriptableObject/DiceData")]
public class DiceRotationData : ScriptableObject
{
    [SerializeField] private List<FaceRelativeRotation> faceRelativeRotations;

    [System.Serializable]
    public struct FaceRelativeRotation
    {
        [SerializeField] private int number;
        [SerializeField] private List<Vector3> rotation;
    }
}
