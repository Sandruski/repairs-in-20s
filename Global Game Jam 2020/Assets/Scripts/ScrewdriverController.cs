using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public uint totalHeights;
    public float heightDistance;
    public float interpolateSeconds;
    #endregion

    #region PRIVATE_VARIABLES
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private float timer;
    private bool interpolate;
    private uint currentHeight;
    #endregion

    void Start()
    {
        fromPosition = Vector3.zero;
        toPosition = Vector3.zero;
        timer = 0.0f;
        interpolate = false;
        currentHeight = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            uint desiredHeight = currentHeight + 1;
            if (desiredHeight >= 0 && desiredHeight < totalHeights)
            {
                fromPosition = transform.position;
                toPosition = transform.position + new Vector3(0.0f, heightDistance, 0.0f);
                timer = 0.0f;
                interpolate = true;
                currentHeight = desiredHeight;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            uint desiredHeight = currentHeight - 1;
            if (desiredHeight >= 0 && desiredHeight < totalHeights)
            {
                fromPosition = transform.position;
                toPosition = transform.position - new Vector3(0.0f, heightDistance, 0.0f);
                timer = 0.0f;
                interpolate = true;
                currentHeight = desiredHeight;
            }
        }

        if (interpolate)
        {
            float t = timer / interpolateSeconds;
            transform.position = Vector3.Lerp(fromPosition, toPosition, t);

            if (timer >= interpolateSeconds)
            {
                interpolate = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
