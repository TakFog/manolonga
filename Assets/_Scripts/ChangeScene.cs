using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneName(string sceneName)
    {
        Debug.Log("Changing scene name to: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
