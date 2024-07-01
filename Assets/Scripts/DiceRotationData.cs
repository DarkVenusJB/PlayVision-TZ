using UnityEngine;

[CreateAssetMenu(fileName = "D6 Rotation Data", menuName = "ScriptableObject/DiceData")]
public class DiceRotationData : ScriptableObject
{
    [SerializeField] private Vector3[] faceRotations = new Vector3[7];

    public Vector3[] FaceRotations => faceRotations;
}
