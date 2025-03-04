using Unity.VisualScripting;
using UnityEngine;

public class DoorControllerScript : MonoBehaviour
{
    [SerializeField] LayerMask player;
    private bool doorActive;
    private static bool resetKey = false;

    [SerializeField] GameObject key1;
    [SerializeField] GameObject key2;

    private void Start()
    {
        doorActive = false;
        resetKey = true;
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
        if (resetKey)
        {
            resetKey = false;
            key1.SetActive(true);
            key2.SetActive(true);
        }
    }

    public static void keyReset()
    {
        resetKey = true;
    }

}
