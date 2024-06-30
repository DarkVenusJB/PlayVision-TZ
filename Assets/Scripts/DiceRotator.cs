using UnityEngine;

public class DiceRotator : MonoBehaviour
{
    [SerializeField] private Transform diceMesh;
    [SerializeField] private DiceRotationData rotationData;

    private void ResetMeshRotation()
    {
        diceMesh.rotation = Quaternion.Euler(Vector3.zero);
    }
    
    public void RotateDiceMesh(int trueDiceIndex)
    {
        ResetMeshRotation();
        
        Debug.Log("a");
        
        diceMesh.rotation = Quaternion.Euler(rotationData.FaceRotations[trueDiceIndex]);
        
        Debug.Log(transform.rotation);
        
        // TODO Дописать вращение меша
    }
}
