using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechanger : MonoBehaviour
{
    //you know her! you love her! it's the same scenechanger.cs as last time! <3
    public void GoToScene(string scene)
    {
        if (scene.ToLower() == "quit")
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        SceneManager.LoadScene(scene);
    }
}