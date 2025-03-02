using UnityEngine;

public class LeversScript : MonoBehaviour
{
    [SerializeField] GameObject lever;
    public static bool leverActive;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            leverActive = true;
            lever.SetActive(false);
        }
    }
}
