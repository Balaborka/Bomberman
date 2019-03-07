using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 _movementDiretion;
    Vector3 _destenation;
    public float moveSpeed = 1f;

    void Start()
    {
        _destenation = transform.position;
    }
    
    void Update()
    {
        MovementPlayer();
        RotatePlayer();
    }

    void MovementPlayer()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0)
            _movementDiretion.Set(input.x, 0.0f, 0.0f);
        else if (input.y != 0)
            _movementDiretion.Set(0.0f, 0.0f, input.y);
        else if (input == Vector3.zero)
            _movementDiretion = Vector3.zero;

        RaycastHit hitInfo;
        Physics.Raycast(new Ray(transform.position, _movementDiretion), out hitInfo, 1f, LayerMask.GetMask("Block", "Obstacle"));

        if (_destenation == transform.position && hitInfo.collider == null)
            _destenation = transform.position + _movementDiretion;

        transform.position = Vector3.MoveTowards(transform.position, _destenation, moveSpeed * Time.deltaTime * 2);
    }

    string checkRotate = "Up";
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow) && checkRotate != "Up")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
            checkRotate = "Up";
        }

        if (Input.GetKey(KeyCode.DownArrow) && checkRotate != "Down")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
            checkRotate = "Down";
        }

        if (Input.GetKey(KeyCode.RightArrow) && checkRotate != "Right")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            checkRotate = "Right";
        }

        if (Input.GetKey(KeyCode.LeftArrow) && checkRotate != "Left")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            checkRotate = "Left";
        }
    }
}
