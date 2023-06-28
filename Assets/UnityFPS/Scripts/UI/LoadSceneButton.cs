using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UnityFPS.Scripts.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string sceneName = "";

        private void Update()
        {
            if(EventSystem.current.currentSelectedGameObject == gameObject 
               && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
