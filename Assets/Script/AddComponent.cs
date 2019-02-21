using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AddComponent : MonoBehaviour
{
    public GameObject block;
    private GameObject inst_block;

    private int width = 9;
    private int height = 11;

    public GameObject obstacles;
    private GameObject inst_obstacles;
    private System.Random rand = new System.Random();

    void Start()
    {
        CreateBlocks();
        CreateWalls();
    }

    void CreateObstacles(int x, int y)
    {
        int k = rand.Next(10);

        if (k > 7)
        {
            GameObject inst_obstacles = Instantiate(block, new Vector3(x, 0.0f, y), Quaternion.identity) as GameObject;
            Renderer rend = inst_obstacles.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.green);
        }
    }

    void CreateBlocks()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                {
                    inst_block = Instantiate(block, new Vector3(0.0f + i, 0.0f, 0.0f + j), Quaternion.identity) as GameObject;
                }
                else
                {
                    CreateObstacles(i, j);
                }
            }
        }
    }

    void CreateWalls()
    {
        for (int i = -2; i <= width + 1; i++)
        {
            for (int j = -2; j <= height + 1; j++)
            {
                if (i == -2 || i == width + 1 || j == -2 || j == height + 1)
                {
                    inst_block = Instantiate(block, new Vector3(0.0f + i, 0.0f, 0.0f + j), Quaternion.identity) as GameObject;
                }
            }
        }
    }
}