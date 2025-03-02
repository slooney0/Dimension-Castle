using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public enum Scene
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Animation,
        EndMenu
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Level1.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int getCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}