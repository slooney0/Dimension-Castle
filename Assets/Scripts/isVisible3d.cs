using UnityEngine;

public class isVisible3d : MonoBehaviour
{

    [SerializeField] SpriteRenderer sRend;
    [SerializeField] MeshCollider mColl;
    private bool resetRend;
    private bool leverActive;

    void Start()
    {
        sRend.enabled = true;
        mColl.enabled = true;
        resetRend = false;
    }

    
    void Update()
    {
        if (LeverScript.leverActive)
        {
            sRend.enabled = false;
            mColl.enabled = false;
            leverActive = true;
        }
        else if (resetRend)
        {
            sRend.enabled = true;
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
