using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;
    private GameObject inst_bomb;
    int maxBomb = 1;
    int bombCount = 0;
    Vector3 bombPosition;
    bool isBomb = false;
    float timeRemaining = 3f;


    void Start()
    {
        
    }
    void Update()
    {
        AddBomb();
        DestroyBomb();
    }

    void AddBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBomb)
        {
            bombPosition.x = PlayerMovement._destination.x;
            bombPosition.z = PlayerMovement._destination.z;
            inst_bomb = Instantiate(bomb, new Vector3(bombPosition.x, 0.5f, bombPosition.z), Quaternion.identity) as GameObject;
            bombCount++;
            isBomb = true;
        }
    }
    void DestroyBomb()
    {
        if (timeRemaining > 0 && isBomb)
            timeRemaining -= Time.deltaTime;
        else if (isBomb)
        {
            isBomb = false;
            timeRemaining = 3f;
            Destroy(inst_bomb);
        }
    }
}
