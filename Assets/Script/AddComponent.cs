using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AddComponent : MonoBehaviour
{

    public int width = 7;
    private int height = 7;

    public GameObject block;
    private GameObject inst_block;

    public GameObject plane;
    private GameObject inst_plane;

    public GameObject obstacle;
    private GameObject inst_obstacle;
    private System.Random rand = new System.Random();

    public GameObject player;
    private GameObject inst_player;
    public GameObject enemy;
    private GameObject inst_enemy;
    int playerX;
    int playerY;

    void Start()
    {
        CreateWalls();
        CreatePlayer(inst_enemy, enemy);
        CreatePlayer(inst_player, player);
        CreateBlocks(playerX, playerY);
    }

    void Update()
    {
    }

    void CreateWalls()
    {
        for (int i = -2; i <= width + 1; i++)
        {
            for (int j = -2; j <= height + 1; j++)
            {
                if (i == -2 || i == width + 1 || j == -2 || j == height + 1)
                {
                    inst_block = Instantiate(block, new Vector3(0.0f + i, 0.5f, 0.0f + j), Quaternion.identity) as GameObject;
                }
            }
        }
        inst_plane = Instantiate(plane, new Vector3(height/2, 0.0f, width/2), Quaternion.identity) as GameObject;
    }

    void CreateObstacles(int x, int y)
    {
        int k = rand.Next(10);

        if (k > 7)
        {
            GameObject inst_obstacle = Instantiate(obstacle, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
        }
    }

    void CreateBlocks(int ifPlayerX, int ifPlayerY)
    {
        int count = 0;
        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                {
                    inst_block = Instantiate(block, new Vector3(0.0f + i, 0.5f, 0.0f + j), Quaternion.identity) as GameObject;
                }
                else if (i != ifPlayerX || j != ifPlayerY)
                {
                    CreateObstacles(i, j);
                    if ((i == ifPlayerX + 1 && j == ifPlayerY) ||
                        (i == ifPlayerX && j == ifPlayerY + 1) ||
                        (i == ifPlayerX - 1 && j == ifPlayerY) ||
                        (i == ifPlayerX && j == ifPlayerY - 1))
                    {
                        count++;
                        if (count == 2)
                        {
                            Destroy(inst_obstacle);
                            count--;
                        }
                    }
                }
            }
        }
    }
    
    public (int playerX, int playerY) CreatePlayer(GameObject playerInstObj, GameObject playerObj)
    {
        playerX = 0;
        playerY = 0;
        while (playerX % 2 == 0 || playerY % 2 == 0)
        {
            playerX = rand.Next(-1, width + 1);
            playerY = rand.Next(-1, height + 1);
        }
        playerInstObj = Instantiate(playerObj, new Vector3(playerX, 0.0f, playerY), Quaternion.identity) as GameObject;

        return (playerX, playerY);
    }
}