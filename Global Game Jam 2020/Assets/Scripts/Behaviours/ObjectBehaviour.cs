using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public uint height;
    public float widthDistance;

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
        bool hasSpawned = false;

        for (uint i = 0; i < height; ++i)
        {
            for (uint j = 0; j < 4; ++j)
            {
                if (Random.value <= spawnProbability
                    || (i == height - 1 && j == 3 && !hasSpawned))
                {
                    hasSpawned = true;

                    float halfHeightDistance = screwdriverController.heightDistance / 2.0f;
                    float y = height * halfHeightDistance;

                    float halfWidthDistance = widthDistance / 2.0f;
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
                        Instantiate(Resources.Load("RedHole") as GameObject, spawnPosition, Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(Resources.Load("BlueHole") as GameObject, spawnPosition, Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}
