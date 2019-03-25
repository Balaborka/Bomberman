using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AddComponent : MonoBehaviour
{

    private int width = 9;
    private int height = 9;
    private int iGridSizeX;
    private int iGridSizeY;

    public GameObject block; 
    private GameObject inst_block;

    public GameObject plane;
    private GameObject inst_plane;

    public GameObject obstacle;
    private GameObject inst_obstacle;
    List<GameObject> obstaclesList = new List<GameObject>();
    
    private System.Random rand = new System.Random();

    public GameObject player;
    private GameObject inst_player;
    public GameObject enemy;
    private GameObject inst_enemy;
    public GameObject smartEnemy;
    private GameObject inst_smartEnemy;

    public GameObject BlastLengthPowerUp;
    public GameObject BombCountPowerUp;
    public GameObject GhostPowerUp;
    public GameObject PlayerSpeedPowerUp;

    Node[,] NodeArray;
    public List<Node> FinalPath;

    void Start()
    {
        Instantiate(BlastLengthPowerUp, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(BombCountPowerUp, new Vector3(8.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(GhostPowerUp, new Vector3(8.0f, 0.0f, 8.0f), Quaternion.identity);
        Instantiate(PlayerSpeedPowerUp, new Vector3(0.0f, 0.0f, 8.0f), Quaternion.identity);

        CreateWalls();
        CreatePlayer(inst_smartEnemy, smartEnemy);
        CreatePlayer(inst_enemy, enemy);

        var playerPos = CreatePlayer(inst_player, player);
        CreateBlocks(playerPos.playerX, playerPos.playerY);

        iGridSizeX = width;
        iGridSizeY = height;
    }

    void Update()
    {
    }

    void CreateWalls()
    {
        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                if (i == -1 || i == width || j == -1 || j == height)
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

        if (k > 5)
        {
            inst_obstacle = Instantiate(obstacle, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
            obstaclesList.Add(inst_obstacle);
        }
    }

    void CreateBlocks(int ifPlayerX, int ifPlayerY)
    {
        int count = 0;
        for (int i = 1; i < width - 1; i++)
        {
            for (int j = 1; j < height - 1; j++)
            {
                if (i % 2 != 0 && j % 2 != 0)
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
        var playerX = 1;
        var playerY = 1;
        while (playerX % 2 != 0 || playerY % 2 != 0)
        {
            playerX = rand.Next(0, width);
            playerY = rand.Next(0, height);
        }
        playerInstObj = Instantiate(playerObj, new Vector3(playerX, 0.0f, playerY), Quaternion.identity) as GameObject;

        return (playerX, playerY);
    }    
}