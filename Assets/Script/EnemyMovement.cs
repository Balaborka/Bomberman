﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private System.Random rand = new System.Random();
    public float moveSpeed = 1;
    Vector3 _movementDiretion;
    Vector3 _destination;   

    void Start()
    {
        _destination = transform.position;
        StartCoroutine(MovementEnemy());
    }
    void Update()
    {
        //MovementEnemy();
    }

    IEnumerator MovementEnemy()
    {
        int k;
        float oldPositionX;
        float oldPositionZ;
        while (true)
        {
            yield return new WaitForSeconds(1);
            do
            {
                
                k = rand.Next(4);

                if (k == 0)
                {
                    _movementDiretion.Set(1.0f, 0.0f, 0.0f);
                }
                else if (k == 1)
                {
                    _movementDiretion.Set(-1.0f, 0.0f, 0.0f);
                }
                else if (k == 2)
                {
                    _movementDiretion.Set(0.0f, 0.0f, 1.0f);
                }
                else if (k == 3)
                {
                    _movementDiretion.Set(0.0f, 0.0f, -1.0f);
                }

                RaycastHit hitInfo;
                Physics.Raycast(new Ray(transform.position, _movementDiretion), out hitInfo, 1f, LayerMask.GetMask("Block", "Obstacle"));

                if (_destination == transform.position && hitInfo.collider == null)
                    _destination = transform.position + _movementDiretion;

                oldPositionX = transform.position.x;
                oldPositionZ = transform.position.z;

                float distance = moveSpeed / 2;
                Debug.Log(distance);
                transform.position = Vector3.MoveTowards(transform.position, _destination, distance);
            } while (oldPositionX == transform.position.x && oldPositionZ == transform.position.z);
        }
    }
}
