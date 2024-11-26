using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{

    public Animator animator;

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene 1 Testing");

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FadeToLevel("Scene 1 Testing");
            //print("enters update");
        }
    }

    public void FadeToLevel(string levelName)
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(Pause());
        //print("enters fadetolevel");
    }
}
