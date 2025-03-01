using System;
using TMPro;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public static bool playerDead = false;
    [SerializeField] LayerMask player;
    [SerializeField] MeshCollider mColl;
    private float spikeBoundsX;
    private float spikeBoundsY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDead = false;
        spikeBoundsX = GetComponent<MeshCollider>().bounds.extents.x;
        spikeBoundsY = GetComponent<MeshCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerTouching() || IsPlayerTouchingLeft())
        {
            playerDead = true;
        }

        Debug.Log(IsPlayerTouching());
    }
    
    private bool IsPlayerTouching()
    {
        return Physics.BoxCast(mColl.bounds.center, new Vector3(spikeBoundsX,spikeBoundsY), new Vector3(Mathf.Sqrt(2), Mathf.Sqrt(2), 0), transform.rotation, 0.2f, player);
    }
    private bool IsPlayerTouchingLeft()
    {
        return Physics.BoxCast(mColl.bounds.center, new Vector3(spikeBoundsX, spikeBoundsY), new Vector3(-Mathf.Sqrt(2), Mathf.Sqrt(2), 0), transform.rotation, 0.2f, player);
    }
}
