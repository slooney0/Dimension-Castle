using UnityEngine;
using UnityEngine.InputSystem;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject key;
    public static bool keyActive;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            keyActive = true;
            key.SetActive(false);
        }
    }
}
