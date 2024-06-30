using UnityEngine;

public class DiceRotator : MonoBehaviour
{
    [SerializeField] private Transform diceMesh;
    [SerializeField] private DiceRotationData rotationData;

    private void ResetMeshRotation() => diceMesh.rotation = Quaternion.Euler(Vector3.zero);
    
    public void RotateDiceMesh()
    {
        ResetMeshRotation();
        
        // TODO Дописать вращение меша
    }
}
