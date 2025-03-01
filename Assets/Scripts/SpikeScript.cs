using System;
using TMPro;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public static bool playerDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            playerDead = true;
        }
    }
}
