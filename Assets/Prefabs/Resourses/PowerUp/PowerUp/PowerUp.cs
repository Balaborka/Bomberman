﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Vector3 identPosition;
    public static float counter = 0.0f;

    AudioSource audioData;
    private void Start()
    {
        identPosition.x = -2.0f;
        identPosition.y = 0.5f;

        audioData = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            identPosition.z = 9.0f - counter;
            counter += 1.0f;

            this.gameObject.transform.position = identPosition;

            PowerUpAction();

            audioData.Play();
        }
    }

    protected virtual void PowerUpAction()
    {
    }
}
