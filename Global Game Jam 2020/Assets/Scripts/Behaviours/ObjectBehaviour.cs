using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public uint height;
    public float halfWidthDistance;

    public float spawnProbability;
    public float redProbability;

    public ScrewdriverController screwdriverController;
    #endregion

    void Start()
    {
        SpawnHoles();
    }

    void Update()
    {
        
    }

    void SpawnHoles()
    {
        for (uint i = 0; i < height; ++i)
        {
            for (uint j = 0; j < 4; ++j)
            {
                if (Random.value <= spawnProbability)
                {
                    float y = height * screwdriverController.heightDistance;
                    float x = 0.0f;
                    float z = 0.0f;

                    switch (j)
                    {
                        case 0:
                            x = halfWidthDistance;
                            break;
                        case 1:
                            x = -halfWidthDistance;
                            break;
                        case 2:
                            z = halfWidthDistance;
                            break;
                        case 3:
                            z = -halfWidthDistance;
                            break;
                    }
                    
                    Vector3 spawnPosition = new Vector3(x, y, z);
                    if (Random.value <= redProbability)
                    {
                        Instantiate(Resources.Load("RedHole") as GameObject, spawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("BlueHole") as GameObject, spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
