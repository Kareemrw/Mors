using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("PracticeScene_Monika");

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FadeToLevel("PracticeScene_Monika");
            print("enters update");
        }
    }

    public void FadeToLevel(string levelName)
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(Pause());
        print("enters fadetolevel");
    }
}
