using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public PlayerType playerType;
    public void ChangeSceneName(string sceneName)
    {
        Globals.HasWinner = false;
        Globals.PlayerType = playerType;
        SceneTransitionManager.Instance.ChangeScene(sceneName);
    }
}
