using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadMenuNagusia()
    {
        SceneManager.LoadScene("MenuNagusia");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadWithDelay("GameOver", delay));
    }

    IEnumerator LoadWithDelay(string sceneName, float sceneLoadDelay)
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Debug.Log("Jokua bukatzen..."); Application.Quit();
    }
}
