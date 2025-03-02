using System;
using UnityEngine;

public class BreakingBlockScript : MonoBehaviour
{

    private Boolean broken;
    bool isGroundedBreak = false;
    private float regenTimer;
    private float regenCounter = 4;
    private float breakTimer;
    private float breakCounter = 1;

    [SerializeField] MeshCollider mColl;
    [SerializeField] MeshRenderer mRend;
    [SerializeField] GameObject bBlock;

    [SerializeField] Sprite s1;
    [SerializeField] Sprite s2;
    [SerializeField] Sprite s3;
    [SerializeField] Sprite s4;
    [SerializeField] SpriteRenderer sRend;

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
            bBlock.layer = 0;
        }
        if (broken == true)
        {
            regenTimer -= Time.deltaTime;
        }
        if (regenTimer < 0)
        {
            broken = false;
            isGroundedBreak = false;
            regenTimer = regenCounter;
            mColl.isTrigger = false;
            mRend.enabled = true;
            breakTimer = breakCounter;
            bBlock.layer = 7;
        }

        if (breakTimer == 1 && broken == false)
        {
            sRend.sprite = s1;
        }
        else if (breakTimer < 0.75f && broken == false)
        {
            sRend.sprite = s2;
        }
        else if (breakTimer < 0.5f && broken == false)
        {
            sRend.sprite = s3;
        }
        else if (breakTimer < 0.25f && broken == false)
        {
            sRend.sprite = s4;
        }
        if (broken == true)
        {
            sRend.enabled = false;
        }
        else
        {
            sRend.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGroundedBreak = true;
        }
    }
}
