using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    bool checkObs0 = true;
    bool checkObs1 = true;
    bool checkObs2 = true;
    bool checkObs3 = true;

    public GameObject Bomba;
    private GameObject bombaClone;

    public GameObject Blast;
    private GameObject inst_blast;

    Vector3 bombPosition;

    float timeRemainingBomb = 2.85f;
    float timeRemainingBlast = 0f;
    public static float blastLength = 1;

    public void Start()
    {
        bombPosition.y = 0.5f;
    }
    public void Update()
    {
        DestroyBomb();
        DestroyBlasts();
    }



    public void AddBomb(float x, float z)
    {
        bombaClone = Instantiate(Bomba, new Vector3(x, 0.5f, z), Quaternion.identity);

        bombPosition.x = x;
        bombPosition.z = z;
    }

    void DestroyBomb()
    {
        if (timeRemainingBomb > 0)
            timeRemainingBomb -= Time.deltaTime;
        else
        {
            Instantiate(Blast, new Vector3(bombPosition.x, bombPosition.y, bombPosition.z), Quaternion.identity);

            for (float i = 1; i <= blastLength; i += 1)
            {
                if (checkObs0)
                {
                    var x = i;
                    var z = 0;
                    var direction = Vector3.right * x + Vector3.forward * z;

                    checkObs0 = IsBlock(direction, i);

                    InstantiateBlast(bombPosition + direction, checkObs0);

                    checkObs0 = IsObstacle(direction, i);
                }
                if (checkObs1)
                {
                    var x = -i;
                    var z = 0;
                    var direction = Vector3.right * x + Vector3.forward * z;

                    checkObs1 = IsBlock(direction, i);

                    InstantiateBlast(bombPosition + direction, checkObs1);

                    checkObs1 = IsObstacle(direction, i);
                }
                if (checkObs2)
                {
                    var x = 0;
                    var z = i;
                    var direction = Vector3.right * x + Vector3.forward * z;

                    checkObs2 = IsBlock(direction, i);

                    InstantiateBlast(bombPosition + direction, checkObs2);

                    checkObs2 = IsObstacle(direction, i);
                }
                if (checkObs3)
                {
                    var x = 0;
                    var z = -i;
                    var direction = Vector3.right * x + Vector3.forward * z;

                    checkObs3 = IsBlock(direction, i);

                    InstantiateBlast(bombPosition + direction, checkObs3);

                    checkObs3 = IsObstacle(direction, i);
                }

            }
            Destroy(bombaClone);
            timeRemainingBomb = 2.85f;
            timeRemainingBlast = 0.5f;
        }

    }

    private void InstantiateBlast(Vector3 vector, bool active)
    {
        if (active)
            Instantiate(Blast, new Vector3(vector.x, vector.y, vector.z), Quaternion.identity);
    }

    bool IsBlock(Vector3 direction, float distance)
    {
        return !Physics.Raycast(new Ray(bombPosition, direction), distance, LayerMask.GetMask("Block"));
    }

    bool IsObstacle(Vector3 direction, float distance)
    {
        return !Physics.Raycast(new Ray(bombPosition, direction), distance, LayerMask.GetMask("Obstacle"));
    }

    bool CheckBlast(int x, int z, float distance)
    {
        bool isBlock;
        Vector3 bombBlast = bombPosition;
        if (distance == 1)
            isBlock = Physics.Raycast(new Ray(bombPosition, Vector3.right * x + Vector3.forward * z), distance, LayerMask.GetMask("Block"));
        else
            isBlock = Physics.Raycast(new Ray(bombPosition, Vector3.right * x + Vector3.forward * z), distance, LayerMask.GetMask("Block", "Obstacle"));
        return !isBlock;
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

            enabled = false;
        }
    }
}

