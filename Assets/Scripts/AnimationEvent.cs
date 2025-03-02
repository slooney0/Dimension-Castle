using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void loadNextScene()
    {
        ScenesManager.instance.LoadNextScene();
    }
}
