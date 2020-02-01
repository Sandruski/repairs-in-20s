using UnityEngine;

public class FidgetSpinner : MonoBehaviour
{
    public float RotationForce = 45.0f;

    public bool Rotate = false;
    void Update()
    {
        if (Rotate)
        {
            Quaternion quaternion = Quaternion.AngleAxis(RotationForce * Time.deltaTime, Vector3.forward);
            transform.rotation = quaternion * transform.rotation;
        }
    }
}
