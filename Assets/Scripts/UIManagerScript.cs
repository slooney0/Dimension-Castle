using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{

    [SerializeField] Button start;

    void Start()
    {
        start.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        ScenesManager.instance.LoadNewGame();
    }
}
