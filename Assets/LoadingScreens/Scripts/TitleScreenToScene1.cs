using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class TitleScreenToScene1 : MonoBehaviour
{

    public Animator animator;

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("DialogueTesting");

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FadeToLevel("DialogueTesting");
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
