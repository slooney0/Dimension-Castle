using UnityEngine;

public class isVisible3d : MonoBehaviour
{

    [SerializeField] SpriteRenderer sRend;

    void Start()
    {
        sRend.enabled = true;
    }

    
    void Update()
    {
        if (LeverScript.leverActive)
        {
            sRend.enabled = false;
        }
        else
        {
            sRend.enabled = true;
        }
    }
}
