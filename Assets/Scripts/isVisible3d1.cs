using UnityEngine;
using UnityEngine.Tilemaps;

public class isTileVisible3d : MonoBehaviour
{

    [SerializeField] TilemapRenderer tilemapRenderer;

    void Start()
    {
        tilemapRenderer.enabled = true;
    }

    
    void Update()
    {
        if (LeverScript.leverActive)
        {
            tilemapRenderer.enabled = false;
        }
        else
        {
            tilemapRenderer.enabled = true;
        }
    }
}
