using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LSToScene4 : MonoBehaviour
{
    public float timeToNextScene = 5f; 

    private float timer = 0f;

    public Animator animator;

    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= timeToNextScene)

        {
            FadeToLevel("Scene 4 Official");

        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene 4 Official");

    }
    
    public void FadeToLevel(string levelName)
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(Pause());
        //print("enters fadetolevel");
    }
}
