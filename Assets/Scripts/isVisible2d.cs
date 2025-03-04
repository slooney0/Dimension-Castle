using UnityEngine;

public class isVisible2d : MonoBehaviour
{
    [SerializeField] MeshRenderer mRend;
    [SerializeField] MeshCollider mColl;

    void Start()
    {
        mRend.enabled = false;
        mColl.enabled = false;
    }


    void Update()
    {
        if (LeverScript.leverActive)
        {
            mRend.enabled = true;
            mColl.enabled = true;
        }
        else
        {
            mRend.enabled = false;
            mColl.enabled = false;
        }
    }
}
