using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AddComponent : MonoBehaviour
{

    public int Width = 7;
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
    int playerX = 0;
    int playerY = 0;

    public float moveSpeed = 1f;

    void Start()
    {
        CreateWalls();
        CreatePlayers();
        CreateBlocks(playerX, playerY);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            inst_player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.DownArrow))
            inst_player.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftArrow))
            inst_player.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.RightArrow))
            inst_player.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void CreateWalls()
    {
        for (int i = -2; i <= Width + 1; i++)
        {
            for (int j = -2; j <= height + 1; j++)
            {
                if (i == -2 || i == Width + 1 || j == -2 || j == height + 1)
                {
                    inst_block = Instantiate(block, new Vector3(0.0f + i, 0.5f, 0.0f + j), Quaternion.identity) as GameObject;
                }
            }
        }
        inst_plane = Instantiate(plane, new Vector3(height/2, 0.0f, Width/2), Quaternion.identity) as GameObject;
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
        for (int i = -1; i <= Width; i++)
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

    public (int playerX, int playerY) CreatePlayers()
    {
        while (playerX % 2 == 0 || playerY % 2 == 0)
        {
            playerX = rand.Next(-1, Width + 1);
            playerY = rand.Next(-1, height + 1);
        }
        inst_player = Instantiate(player, new Vector3(playerX, 0.0f, playerY), Quaternion.identity) as GameObject;

        return (playerX, playerY);
    }
}