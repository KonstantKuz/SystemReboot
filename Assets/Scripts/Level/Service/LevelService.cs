using UnityEngine.SceneManagement;

namespace Level.Service
{
    public class LevelService
    {
        public const string LEVEL_ID = "Level 01";
        public LevelService()
        {
            SceneManager.LoadScene(LEVEL_ID, LoadSceneMode.Additive);
        }
    }
}
