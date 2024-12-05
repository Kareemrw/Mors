using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoToExampleScene()
    {
        SceneManager.LoadScene("PracticeScene_Monika");
    }

    public void GoToSceneTwo()
    {
        SceneManager.LoadScene("DialogueTesting");
    }

    public void GoToSceneThree()
    {
        SceneManager.LoadScene("Scene 3 Official");
    }
}
