using UnityEngine;

public class isBlockVisible3d : MonoBehaviour
{

    [SerializeField] MeshRenderer mRend;
    [SerializeField] MeshCollider mColl;
    private bool resetRend;
    private bool leverActive;

    void Start()
    {
        mRend.enabled = true;
        mColl.enabled = true;
        resetRend = false;
    }


    void Update()
    {
        if (LeverScript.leverActive)
        {
            mRend.enabled = false;
            mColl.enabled = false;
            leverActive = true;
        }
        else if (resetRend)
        {
            mRend.enabled = true;
            mColl.enabled = true;
            resetRend = false;
        }

        if (LeverScript.leverActive == false && leverActive == true)
        {
            resetRend = true;
            leverActive = false;
        }
    }
}
