using TMPro;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public static bool leverActive = false;

    [SerializeField] BoxCollider bColl1;
    [SerializeField] BoxCollider bColl2;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] GameObject lever1;
    [SerializeField] GameObject lever2;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leverActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated1() && leverActive == false)
        {
            leverActive = true;
            lever1.SetActive(false);
        }
        else if (IsActivated2() && leverActive == true)
        {
            leverActive = false;
            PlayerScript.rotation = false;
            CameraScript.rotation = false;
            lever2.SetActive(false);
        }
    }

    bool IsActivated1()
    {
        return Physics.BoxCast(bColl1.bounds.center, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1, 1, 1), transform.rotation, 0.4f, playerLayer);
    }

    bool IsActivated2()
    {
        return Physics.BoxCast(bColl2.bounds.center, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1, 1, 1), transform.rotation, 0.4f, playerLayer);
    }


}
