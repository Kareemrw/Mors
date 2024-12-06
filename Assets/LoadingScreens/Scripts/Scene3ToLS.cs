using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
public class Scene3ToLS : MonoBehaviour
{
    public Animator animator;

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene3toLS");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FadeToLevel("Scene3toLS");
    }
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel("Scene1toLS");
            //print("enters update");
        }
    }*/

    public void FadeToLevel(string levelName)
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(Pause());
        //print("enters fadetolevel");
    }
}
