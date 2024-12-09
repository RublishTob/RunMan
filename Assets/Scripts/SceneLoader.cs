
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int Scene;
    public void LoadinCurrentScene()
    {
        SceneManager.LoadScene(Scene);
    }
}
