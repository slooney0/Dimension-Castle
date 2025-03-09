using TMPro;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    public static bool leverActive = false;
    public static bool resetLever = false;

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
        if (LeversScript.leverActive)
        {
            leverActive = true;
            LeversScript.leverActive = false;
            lever2.SetActive(true);
        }
        else if (LeversScript2.leverActive)
        {
            leverActive = false;
            PlayerScript.rotation = false;
            CameraScript.rotation = false;
            LeversScript2.leverActive = false;
            lever1.SetActive(true);
        }
        if (resetLever == true)
        {
            resetLever = false;
            PlayerScript.rotation = false;
            CameraScript.rotation = false;
            lever1.SetActive(true);
            lever2.SetActive(true);
            leverActive = false;
        }
    }

    public static void resetLevers()
    {
        resetLever = true;
    }
}
