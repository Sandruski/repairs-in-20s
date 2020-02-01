using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public bool TouchGround
    {
        get { return touchGround; }
    }

    public uint height;

    public float spawnProbability;
    public float redProbability;

    public Mesh mesh1;
    public Mesh mesh2;
    public Mesh mesh3;

    public ScrewdriverController screwdriverController;

    [HideInInspector]
    public Vector3 size;
    #endregion

    #region PRIVATE_VARIABLES
    private bool touchGround = false;
    public List<GameObject> holes;
    #endregion

    void Start()
    {
        holes = new List<GameObject>();
        size = Vector3.Scale(transform.localScale, GetComponent<MeshFilter>().mesh.bounds.size);
    }

    public void SpawnHoles()
    {
        touchGround = false;
        RemoveHoles();

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
                    float y = (i + 1) * halfHeightDistance;

                    float halfWidthDistance = size.x / 2.0f;
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

                    Vector3 spawnPosition = transform.position - new Vector3(0.0f, size.y / 2.0f, 0.0f) + new Vector3(x, y, z);
                    GameObject hole = null;
                    if (Random.value <= redProbability)
                    {
                        hole = Instantiate(Resources.Load("RedHole") as GameObject, spawnPosition, Quaternion.identity, transform);
                    }
                    else
                    {
                        hole = Instantiate(Resources.Load("BlueHole") as GameObject, spawnPosition, Quaternion.identity, transform);
                    }
                    holes.Add(hole);
                }
            }
        }
    }

    void RemoveHoles()
    {
        foreach (GameObject hole in holes)
        {
            Destroy(hole);
        }

        holes.Clear();
    }

    public bool AreAllHolesScrewed()
    {
        uint count = 0;

        foreach (GameObject hole in holes)
        {
            if (hole.GetComponent<HoleBehaviour>().screwed)
            {
                ++count;
            }
        }

        return count == holes.Count;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            touchGround = true;
        }
    }
}
