using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;
    private GameObject inst_bomb;

    public GameObject blast;
    private GameObject inst_blastCenter;
    private GameObject inst_blast1;
    private GameObject inst_blast2;
    private GameObject inst_blast3;
    private GameObject inst_blast4;

    Vector3 bombPosition;
    public Vector3 bombBlast;
    
    //List<Vector3> blastsList = new List<Vector3>();

    bool isBomb = false;

    float timeRemainingBomb = 2f;
    float timeRemainingBlast = 0f;
    float blastLength = 1f;


    void Start()
    {
        bombBlast.y = 0.5f;
        bombPosition.y = 0.5f;
    }
    void Update()
    {
        AddBomb();
        DestroyBomb();
        DestroyBlasts();
    }

    void AddBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBomb)
        {
            inst_bomb = Instantiate(bomb, new Vector3(PlayerMovement._destination.x, 0.5f, PlayerMovement._destination.z), Quaternion.identity) as GameObject;

            bombPosition.x = PlayerMovement._destination.x;
            bombPosition.z = PlayerMovement._destination.z;

            isBomb = true;
        }
    }

    void DestroyBomb()
    {
        if (timeRemainingBomb > 0 && isBomb)
            timeRemainingBomb -= Time.deltaTime;
        else if (isBomb)
        {
            inst_blastCenter = Instantiate(blast, new Vector3(bombPosition.x, bombPosition.y, bombPosition.z), Quaternion.identity) as GameObject;
            for (float i = 0; i < blastLength; i += 1f)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        bombBlast.x = bombPosition.x + 1f + i;
                        bombBlast.z = bombPosition.z;
                        inst_blast1 = Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity) as GameObject;

                        //AddBlastsList();
                    }
                    else if (j == 1)
                    {
                        bombBlast.x = bombPosition.x - 1f - i;
                        bombBlast.z = bombPosition.z;
                        inst_blast2 = Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity) as GameObject;

                        //AddBlastsList();
                    }
                    else if (j == 2)
                    {
                        bombBlast.x = bombPosition.x;
                        bombBlast.z = bombPosition.z + 1f + i;
                        inst_blast3 = Instantiate(blast, new Vector3(bombBlast.x, bombBlast.y, bombBlast.z), Quaternion.identity) as GameObject;

                        //AddBlastsList(); ;
                    }
                    else if (j == 3)
                    {
                        bombBlast.x = bombPosition.x;
                        bombBlast.z = bombPosition.z - 1f - i;
                        inst_blast4 = Instantiate(blast, new Vector3(bombBlast.x, 0.5f, bombBlast.z), Quaternion.identity) as GameObject;

                        //AddBlastsList();
                    }
                }
            }
            isBomb = false;
            timeRemainingBomb = 2f;
            timeRemainingBlast = 0.5f;
            Destroy(inst_bomb);
        }
    }

    //void AddBlastsList()
    //{
    //    RaycastHit hitInfo;
    //    Physics.Raycast(new Ray(bombPosition, bombBlast), out hitInfo, 1f, LayerMask.GetMask("Obstacle"));

    //    if (hitInfo.collider == null)
    //        blastsList.Add(bombBlast);
    //}

    void DestroyBlasts()
    {
        if (timeRemainingBlast > 0)
            timeRemainingBlast -= Time.deltaTime;
        else
        {
            Destroy(inst_blast1);
            Destroy(inst_blast2);
            Destroy(inst_blast3);
            Destroy(inst_blast4);
            Destroy(inst_blastCenter);

            timeRemainingBlast = 0;

        //    foreach (var item in blastsList)
        //    {
        //        Object[] positionObjects = Physics.OverlapSphere(item, 1.0f);
        //        foreach (var item2 in positionObjects)
        //        {
        //            Destroy(item2);
        //        }
        //    }
        }
    }
}

