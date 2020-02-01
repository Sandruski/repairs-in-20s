using Cinemachine;
using UnityEngine;

public class DollyMovement : MonoBehaviour
{
    CinemachineVirtualCamera virtualCameraPrefab;
    public float _ConstantForce = 1.0f;
    CinemachineTrackedDolly dolly;

    public bool start = false;

    void Start()
    {
        virtualCameraPrefab = GetComponent<CinemachineVirtualCamera>();
        dolly = virtualCameraPrefab.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update()
    {
        if (start)
        {
            dolly.m_PathPosition += Time.deltaTime * _ConstantForce;

            if (_ConstantForce < 0.0f)
            {
                if (dolly.m_PathPosition <= 0.0f)
                {
                    start = false;
                    dolly.m_PathPosition = 0.0f;
                }
            }
            if (_ConstantForce >= 0.0f)
            {
                if (dolly.m_PathPosition >= 3.0f)
                {
                    dolly.m_PathPosition = 3.0f;
                    start = false;
                }
            }
        }
    }

    public void GotoTV()
    {
        _ConstantForce = -Mathf.Abs(_ConstantForce);
        start = true;
    }

    public void OutofTV()
    {
        _ConstantForce = Mathf.Abs(_ConstantForce);
        start = true;
    }

}
