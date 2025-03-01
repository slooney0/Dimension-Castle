using UnityEngine;
using UnityEngine.InputSystem;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject key;
    public static bool keyActive;
    public static bool resetKey = false;

    private void Update()
    {
        if (resetKey)
        {
            resetKey = false;
            key.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            keyActive = true;
            key.SetActive(false);
        }
    }

    public static void keyReset()
    {
        resetKey = true;
    }
}
