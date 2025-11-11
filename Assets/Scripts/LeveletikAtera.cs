using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LeveletikAtera : MonoBehaviour
{
    [SerializeField] float itxaronDenbora = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<PertsonaiMugimendua>().bizirikDago = false;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(itxaronDenbora);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindFirstObjectByType<EszenaIraunkorra>().ResetEszenaIraunkorra();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
