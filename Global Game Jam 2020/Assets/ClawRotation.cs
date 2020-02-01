using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawRotation : MonoBehaviour
{
    public static ClawRotation instance;

    public bool drill_L = false;
    public bool drill_R = false;
    private float drill_L_timer = 0.0f;
    private float drill_R_timer = 0.0f;
    private AudioSource audioSource;

    public float drill_time = 1.0f;
    public float rotationSpeed = 5.0f;
    public float Speed = 15.0f;
    public Transform drill_L_transform;
    public Transform drill_R_transform;
    public AudioClip drillClip;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drill_L && drill_L_timer <= drill_time)
        {
            drill_L_timer += Time.deltaTime;

            drill_L_transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);
             
            if (drill_L_timer <= drill_time/2)
                drill_L_transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
            else
                drill_L_transform.position -= new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        else
        {
            drill_L_timer = 0.0f;
            drill_L = false;
        }

        if (drill_R && drill_R_timer <= drill_time)
        {
            drill_R_timer += Time.deltaTime;
            drill_R_transform.Rotate(Vector3.left, Time.deltaTime * rotationSpeed);

            if (drill_R_timer <= drill_time / 2)
                drill_R_transform.position -= new Vector3(Time.deltaTime * Speed, 0, 0);
            else
                drill_R_transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        else
        {
            drill_R_timer = 0.0f;
            drill_R = false;
        }
    }

    public void DrillLeft()
    {
        drill_L = true;
        audioSource.PlayOneShot(drillClip);
    }

    public void DrillRight()
    {
        drill_R = true;
        audioSource.PlayOneShot(drillClip);
    }
}
