using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPutBomb : MonoBehaviour
{
    public static bool canPut = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Obstacle")
            canPut = false;
    }
    private void OnTriggerExit(Collider other)
    {
        canPut = true;
    }
}
