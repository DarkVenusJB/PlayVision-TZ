using UnityEngine;

public class DiceRotator : MonoBehaviour
{
    [SerializeField] private Transform diceMesh;
    [SerializeField] private DiceRotationData rotationData;
    
    private void ResetMeshRotation() => diceMesh.rotation = Quaternion.Euler(Vector3.zero).normalized;
    
    public void RotateDiceMesh(int trueDiceIndex)
    {
        ResetMeshRotation();

        diceMesh.rotation = Quaternion.Euler(rotationData.FaceRotations[trueDiceIndex]);
    }
}
