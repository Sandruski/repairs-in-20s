using UnityEngine;

public class FidgetSpinner : MonoBehaviour
{
    public float RotationForce = 65.0f;

    private bool spin = false;
    public GameController gameController;
    void Update()
    {
        spin = gameController.startSpinning;

        if (spin)
        {
            Quaternion quaternion = Quaternion.AngleAxis(RotationForce * Time.deltaTime, Vector3.forward);
            transform.rotation = quaternion * transform.rotation;
        }
    }
}
