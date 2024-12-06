using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
public class Scene2ToLS : MonoBehaviour
{
    public Animator animator;
    internal static bool active;

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scene2toLS");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FadeToLevel("Scene2toLS");
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
