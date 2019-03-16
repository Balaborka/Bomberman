using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private System.Random rand = new System.Random();
    public float moveSpeed = 1;
    Vector3 _movementDiretion;
    Vector3 _destination;
    float transformY;

    void Start()
    {
        _destination = transform.position;
        GeneratePosition();
    }
    void Update()
    {
        MovementEnemyNew();
    }

    private void MovementEnemyNew()
    {
        var oldPositionX = transform.position.x;
        var oldPositionZ = transform.position.z;

        RaycastHit hitInfo;
        Physics.Raycast(new Ray(transform.position, _movementDiretion), out hitInfo, 1f, LayerMask.GetMask("Block", "Bomb"));

        if (_destination == transform.position) {
            if (hitInfo.collider == null)
            {
                _destination = transform.position + _movementDiretion; 
                transform.rotation = Quaternion.identity;
                transform.Rotate(new Vector3(0.0f, transformY, 0.0f));
            }
            GeneratePosition();
        }

        float distance = moveSpeed * Time.deltaTime * 2;
        transform.position = Vector3.MoveTowards(transform.position, _destination, distance);
    }

    private void GeneratePosition()
    {
        int randomInt = rand.Next(4);

        if (randomInt == 0)
        {
            transformY = 90.0f;
            _movementDiretion.Set(1.0f, 0.0f, 0.0f);
        }
        else if (randomInt == 1)
        {
            transformY = -90.0f;
            _movementDiretion.Set(-1.0f, 0.0f, 0.0f);
        }
        else if (randomInt == 2)
        {
            transformY = 0.0f;
            _movementDiretion.Set(0.0f, 0.0f, 1.0f);
        }
        else if (randomInt == 3)
        {
            transformY = - 180.0f;
            _movementDiretion.Set(0.0f, 0.0f, -1.0f);
        }
    }
}
