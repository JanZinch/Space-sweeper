namespace SimpleSQL.Demos
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using SimpleSQL;

    public class MainMenuButton : MonoBehaviour
    {
        public SimpleSQLManager dbManager;

        public void MainMenu()
        {
            dbManager.Close();
            dbManager.Dispose();

            SceneManager.LoadScene(0);
        }
    }
}