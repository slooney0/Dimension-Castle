using UnityEngine;

public class isVisible3d : MonoBehaviour
{

    [SerializeField] SpriteRenderer sRend;
    [SerializeField] MeshCollider mColl;

    void Start()
    {
        sRend.enabled = true;
        mColl.enabled = true;
    }

    
    void Update()
    {
        if (LeverScript.leverActive)
        {
            sRend.enabled = false;
            mColl.enabled = false;
        }
        else
        {
            sRend.enabled = true;
            mColl.enabled = true;
        }
    }
}
