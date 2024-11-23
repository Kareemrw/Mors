using UnityEngine;

public class SceneFade : MonoBehaviour
{

    public Animator animator;
    //public GameObject button;

    // Update is called once per frame
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
        print("enters fadetolevel");
    }
}
