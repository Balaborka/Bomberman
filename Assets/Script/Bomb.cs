using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    RaycastHit hitInfo;
    bool checkObs0 = true;
    bool checkObs1 = true;
    bool checkObs2 = true;
    bool checkObs3 = true;

    public GameObject bomb;
    private GameObject inst_bomb;

    public GameObject blast;
    private GameObject inst_blast;

    Vector3 bombPosition;
    public Vector3 bombBlast;

    bool isBomb = false;

    float timeRemainingBomb = 2f;
    float timeRemainingBlast = 0f;
    public static float blastLength = 1;

    public static int bombCount = 0;
    int bombCounter = 0;

    void Start()
    {
        bombBlast.y = 0.5f;
        bombPosition.y = 0.5f;
    }
    void Update()
    {
        AddBomb();
    }

    void AddBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBomb)
        {
            inst_bomb = Instantiate(bomb, new Vector3(PlayerMovement._destination.x, 0.5f, PlayerMovement._destination.z), Quaternion.identity) as GameObject;

            bombPosition.x = PlayerMovement._destination.x;
            bombPosition.z = PlayerMovement._destination.z;

            if (bombCounter < bombCount)
                bombCounter++;
            else
                isBomb = true;
        }

        DestroyBomb();
        DestroyBlasts();
    }

    void DestroyBomb()
    {
        if (timeRemainingBomb > 0 && isBomb)
            timeRemainingBomb -= Time.deltaTime;
        else if (isBomb)
        {
            Instantiate(blast, new Vector3(bombPosition.x, bombPosition.y, bombPosition.z), Quaternion.identity);
            for (float i = 0; i < blastLength; i += 1)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0 && checkObs0)
                    {
                        bombBlast.x = bombPosition.x + 1f + i;
                        bombBlast.z = bombPosition.z;
                        Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity);

                        CheckBlast(checkObs0);
                    }
                    else if (j == 1 && checkObs1)
                    {
                        bombBlast.x = bombPosition.x - 1f - i;
                        bombBlast.z = bombPosition.z;
                        Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity);

                        CheckBlast(checkObs1);
                    }
                    else if (j == 2 && checkObs2)
                    {
                        bombBlast.x = bombPosition.x;
                        bombBlast.z = bombPosition.z + 1f + i;
                        Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity);

                        CheckBlast(checkObs2);
                    }
                    else if (j == 3 && checkObs3)
                    {
                        bombBlast.x = bombPosition.x;
                        bombBlast.z = bombPosition.z - 1f - i;
                        Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity);

                        CheckBlast(checkObs3);
                    }
                }
            }
            isBomb = false;
            timeRemainingBomb = 2f;
            timeRemainingBlast = 0.5f;

            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            foreach (GameObject bomb in bombs)
            {
                Destroy(bomb);
            }
            bombCounter = 0;
        }
    }

    //Check this method at home!!! 
    void CheckBlast(bool checkObs)
    {
        checkObs = true;
        Physics.Raycast(new Ray(bombPosition, bombBlast), out hitInfo, 1f, LayerMask.GetMask("Block", "Obstacle"));
        if (hitInfo.collider != null)
            checkObs = !checkObs;
    }

    void DestroyBlasts()
    {
        if (timeRemainingBlast > 0)
            timeRemainingBlast -= Time.deltaTime;
        else
        {
            GameObject[] blasts = GameObject.FindGameObjectsWithTag("Blast");

            foreach (GameObject blast in blasts)
            {
                Destroy(blast);
            }

            timeRemainingBlast = 0;
        }
    }
}

