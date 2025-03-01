using System;
using UnityEngine;

public class BreakingBlockScript : MonoBehaviour
{

    private Boolean broken;
    bool isGroundedBreak = false;
    private float regenTimer;
    private float regenCounter = 6;
    private float breakTimer;
    private float breakCounter = 2;

    [SerializeField] MeshCollider mColl;
    [SerializeField] MeshRenderer mRend;

    void Start()
    {
        broken = false;
        breakTimer = breakCounter;
        regenTimer = regenCounter;
        mColl.isTrigger = false;
        mRend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGroundedBreak && broken == false)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGroundedBreak = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGroundedBreak = false;
        }
    }
}
