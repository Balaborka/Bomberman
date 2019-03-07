using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private System.Random rand = new System.Random();
    public float speed = 1;
    Vector3 _destenation;

    void Start()
    {
        _destenation = transform.position;
        StartCoroutine(MovementEnemy());
    }
    void Update()
    {
        //MovementEnemy();
    }

    IEnumerator MovementEnemy()
    {
        while (true)
        {
            RaycastHit hitInfo;
            Physics.Raycast(new Ray(_destenation, transform.position), out hitInfo, 1f, LayerMask.GetMask("Block", "Obstacle"));

            yield return new WaitForSeconds(1);
            int k = rand.Next(4);

            if (k == 0 && hitInfo.collider == null)
            {
                transform.position += (Vector3.right * speed);
                _destenation = transform.position;
            }
            if (k == 1 && hitInfo.collider == null)
            {
                transform.position += (Vector3.left * speed);
                _destenation = transform.position;
            }
            if (k == 2 && hitInfo.collider == null)
            {
                transform.position += (Vector3.forward * speed);
                _destenation = transform.position;
            }
            if (k == 3 && hitInfo.collider == null)
            {
                transform.position += (-Vector3.forward * speed);
                _destenation = transform.position;
            }
        }
    }
}
