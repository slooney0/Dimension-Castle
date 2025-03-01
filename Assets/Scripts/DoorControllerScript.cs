using Unity.VisualScripting;
using UnityEngine;

public class DoorControllerScript : MonoBehaviour
{
    [SerializeField] LayerMask player;
    private bool doorActive;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.forward * 0.1f;
        doorActive = Physics.Raycast(rayOrigin,-Vector3.forward, 10f, player);
        if (KeyScript.keyActive && doorActive && KeyScript2.keyActive && LeverScript.leverActive == false)
        {
            ScenesManager.instance.LoadNextScene();
        }
    }
}
