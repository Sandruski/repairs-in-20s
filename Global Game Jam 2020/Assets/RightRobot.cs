using UnityEngine;

public class RightRobot : MonoBehaviour
{
    enum States
    {
        PreparationPhase,
        Play,
        SecureTheBombs
    }

    States state = States.PreparationPhase;

    public Vector3 MinHeight;
    public Vector3 MaxHeight;
    public float AnimationTime;

    float lerpTick = 0.0f;
    Transform child;
    void Start()
    {
        child = transform.Find("clawTube").transform;
    }
    void Update()
    {
        switch(state)
        {
            case States.PreparationPhase:
                float tick = lerpTick / AnimationTime;
                transform.position = Vector3.Lerp(MaxHeight, MinHeight, tick);

                if (tick >= 1.0f)
                {
                    state = States.Play;
                }

                break;
        }
    }
}
