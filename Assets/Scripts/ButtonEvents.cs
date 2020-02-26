using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonEvents : MonoBehaviour
{
    public void PlayButton () {
        SceneManager.LoadScene("Level1");
    }

    public void QuitButton () {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TitleScreenButton () {
        SceneManager.LoadScene("TitleScreen");
    }
}
