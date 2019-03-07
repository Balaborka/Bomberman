using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private System.Random rand = new System.Random();
    public float speed = 40.0f;

    void Start()
    {
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
            yield return new WaitForSeconds(1);
            int k = rand.Next(3);

            if (k == 0)
                transform.position += (Vector3.right * speed);
            if (k == 1)
                transform.position += (Vector3.left * speed);
        }
        //if (k == 2)
        //    transform.Translate(Vector3.up * speed * Time.deltaTime);
        //if (k == 3)
        //    transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
