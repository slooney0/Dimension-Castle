using TMPro;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public static bool leverActive = false;

    [SerializeField] BoxCollider bColl;
    [SerializeField] LayerMask playerLayer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leverActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated())
        {
            leverActive = true;
            gameObject.SetActive(false);
        }
    }



    bool IsActivated()
    {
        return Physics.BoxCast(bColl.bounds.center, new Vector3(0.5f, 0.5f, 0), new Vector3(1, 1, 0), transform.rotation, 0.4f, playerLayer);
    }


}
