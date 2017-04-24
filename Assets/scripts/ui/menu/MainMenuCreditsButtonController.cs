using UnityEngine;
using UnityEngine.SceneManagement;

namespace ui.menu
{
    public class MainMenuCreditsButtonController : MonoBehaviour
    {
        public void LoadByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}