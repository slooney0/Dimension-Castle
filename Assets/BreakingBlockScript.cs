using System;
using UnityEngine;

public class BreakingBlockScript : MonoBehaviour
{

    private Boolean broken;
    private float regenTimer;
    private float regenCounter = 6;
    private float breakTimer;
    private float breakCounter = 2;

    [SerializeField] MeshCollider mColl;
    [SerializeField] LayerMask player;
    [SerializeField] MeshRenderer mRend;

    private float spikeBoundsX;
    private float spikeBoundsY;

    private float blockHeight;
    private float raycastDistance;

    void Start()
    {
        broken = false;
        breakTimer = breakCounter;
        regenTimer = regenCounter;
        mColl.isTrigger = false;
        mRend.enabled = true;
        spikeBoundsX = GetComponent<MeshCollider>().bounds.extents.x;
        spikeBoundsY = GetComponent<MeshCollider>().bounds.extents.y;

        blockHeight = GetComponent<MeshCollider>().bounds.size.y;
        raycastDistance = (blockHeight / 2) + 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.playerStandingBreakingBlock)
        {
            breakTimer -= Time.deltaTime;
            Debug.Log(breakTimer);
        }
        else if (breakTimer < breakCounter && broken == false)
        {
            breakTimer += Time.deltaTime;
        }
        if (breakTimer < 0)
        {
            broken = true;
            breakTimer = breakCounter;
            mColl.isTrigger = true;
            mRend.enabled = false;
            regenTimer = regenCounter;
        }
        if (broken == true)
        {
            regenTimer -= Time.deltaTime;
            Debug.Log(regenTimer);
        }
        if (regenTimer < 0)
        {
            broken = false;
            regenTimer = regenCounter;
            mColl.isTrigger = false;
            mRend.enabled = true;
            breakTimer = breakCounter;
        }
    }

    private bool IsPlayerTouching()
    {
        return Physics.BoxCast(mColl.bounds.center, new Vector3(spikeBoundsX, spikeBoundsY), new Vector3(Mathf.Sqrt(2), Mathf.Sqrt(2)), transform.rotation, 0.4f, player);
    }
    private bool IsPlayerTouchingLeft()
    {
        return Physics.BoxCast(mColl.bounds.center, new Vector3(spikeBoundsX, spikeBoundsY), new Vector3(Mathf.Sqrt(2), Mathf.Sqrt(2)), transform.rotation, 0.4f, player);
    }
}
