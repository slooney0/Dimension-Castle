using UnityEngine;

public class Exists3d : MonoBehaviour
{

    [SerializeField] MeshCollider mColl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mColl.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LeverScript.leverActive)
        {
            mColl.enabled = false;
        }
        else
        {
            mColl.enabled = true;
        }
    }
}
