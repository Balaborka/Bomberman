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
        _movementDiretion = GeneratePosition();
    }
    void Update()
    {
        MovementEnemyNew();
    }

    private void MovementEnemyNew()
    {
        var oldPositionX = transform.position.x;
        var oldPositionZ = transform.position.z;

 
        if (_destination == transform.position) {
            _movementDiretion = GeneratePosition();
            Rotation(_movementDiretion);

            RaycastHit hitInfo;
            Physics.Raycast(new Ray(transform.position, _movementDiretion), out hitInfo, 1f, LayerMask.GetMask("Block", "Bomb"));

            if (hitInfo.collider == null)
            {
                _destination = transform.position + _movementDiretion; 
                transform.rotation = Quaternion.identity;
                transform.Rotate(new Vector3(0.0f, transformY, 0.0f));
            }
        }

        float distance = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _destination, distance);
    }

    protected virtual Vector3 GeneratePosition()
    {
        int randomInt = GetNextPosition();

        if (randomInt == 0)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if (randomInt == 1)
        {
            return new Vector3(-1.0f, 0.0f, 0.0f);
        }
        else if (randomInt == 2)
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }
        else if (randomInt == 3)
        {
            return new Vector3(0.0f, 0.0f, -1.0f);
        }
        return Vector3.zero;
    }

    void Rotation(Vector3 vector)
    {
        transformY = vector.z != 1.0f ? vector.x * 90 + vector.z * 180 : 0;

        //if (vector.x == 1.0f)
        //    transformY = 90.0f;

        //else if (vector.x == -1.0f)
        //    transformY = -90.0f;

        //else if (vector.z == 1.0f)
        //    transformY = 0.0f;

        //else if (vector.z == -1.0f)
        //    transformY = -180.0f;
    }

    protected virtual int GetNextPosition()
    {
        return rand.Next(4);
    }
}
