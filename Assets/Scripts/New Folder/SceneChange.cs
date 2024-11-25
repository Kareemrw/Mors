using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoToExampleScene()
    {
        SceneManager.LoadScene("PracticeScene_Monika");
    }
}
