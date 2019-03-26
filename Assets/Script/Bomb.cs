using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    bool checkObs0 = true;
    bool checkObs1 = true;
    bool checkObs2 = true;
    bool checkObs3 = true;

    public GameObject bomba;
    private GameObject bombaClone;

    public GameObject blast;
    private GameObject inst_blast;

    Vector3 bombPosition;
    public Vector3 bombBlast;

    float timeRemainingBomb = 2.85f;
    float timeRemainingBlast = 0f;
    public static float blastLength = 1;

    public void Start()
    {
        bombBlast.y = 0.5f;
        bombPosition.y = 0.5f;
    }
    public void Update()
    {
        DestroyBomb();
        DestroyBlasts();
    }

    public void AddBomb()
    {
        bombaClone = Instantiate(bomba, new Vector3(PlayerMovement._destination.x, 0.5f, PlayerMovement._destination.z), Quaternion.identity);

        bombPosition.x = PlayerMovement._destination.x;
        bombPosition.z = PlayerMovement._destination.z;
    }

    void DestroyBomb()
    {
        if (timeRemainingBomb > 0)
            timeRemainingBomb -= Time.deltaTime;
        else
        {
            Instantiate(blast, new Vector3(bombPosition.x, bombPosition.y, bombPosition.z), Quaternion.identity);
            
            for (float i = 1; i <= blastLength; i += 1)
            {
                if (checkObs0)
                {
                    bombBlast.x = bombPosition.x + i;
                    bombBlast.z = bombPosition.z;

                    //checkObs0 = CheckBlast();

                    InstantiateBlast(bombBlast, checkObs0);
                }
                if (checkObs1)
                {
                    bombBlast.x = bombPosition.x - i;
                    bombBlast.z = bombPosition.z;

                    //checkObs1 = CheckBlast();

                    InstantiateBlast(bombBlast, checkObs1);                    
                }
                if (checkObs2)
                {
                    bombBlast.x = bombPosition.x;
                    bombBlast.z = bombPosition.z + i;

                    //checkObs2 = CheckBlast();

                    InstantiateBlast(bombBlast, checkObs2);
                }
                if (checkObs3)
                {
                    bombBlast.x = bombPosition.x;
                    bombBlast.z = bombPosition.z - i;

                    //checkObs3 = CheckBlast();

                    InstantiateBlast(bombBlast, checkObs3);
                }

            }
            timeRemainingBomb = 2.85f;
            timeRemainingBlast = 0.5f;
            Destroy(bombaClone);
        }
    }

    private void InstantiateBlast(Vector3 vector, bool active)
    {
        if (active)
            Instantiate(blast, new Vector3(vector.x, vector.y, vector.z), Quaternion.identity);
    }
    
    bool CheckBlast()
    {
        RaycastHit hitInfo;
        Physics.Raycast(new Ray(bombPosition, bombBlast), out hitInfo, 1f, LayerMask.GetMask("Block", "Obstacle"));
        if (hitInfo.collider != null)
            return false;
        return true;
    }

    void DestroyBlasts()
    {
        if (timeRemainingBlast > 0)
            timeRemainingBlast -= Time.deltaTime;
        else if (timeRemainingBlast < 0)
        {
            GameObject[] blasts = GameObject.FindGameObjectsWithTag("Blast");

            foreach (GameObject blast in blasts)
            {
                Destroy(blast);
            }
            timeRemainingBlast = 0;

            enabled = false;
        }
    }
}

