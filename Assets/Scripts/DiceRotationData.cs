using UnityEngine;

[CreateAssetMenu(fileName = "D6 Rotation Data", menuName = "ScriptableObject/DiceData")]
public class DiceRotationData : ScriptableObject
{
    // Инкес элемента массива равен номеру на кубике, [0] не используется
    [SerializeField] private Vector3[] faceRotations = new Vector3[7];

    public Vector3[] FaceRotations => faceRotations;
}
