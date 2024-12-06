using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillSound : MonoBehaviour
{
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "Scene2toLS")
        {
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);

    }
}
