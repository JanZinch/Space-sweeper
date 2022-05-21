using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueGameButton = null;
        [SerializeField] private Button _newGameButton = null;
        [SerializeField] private Button _settingsButton = null;
        [SerializeField] private Button _exitButton = null;

        private void ContinueGame()
        {
            SceneManager.Load(Scene.DOCK);
        }
        
        private void NewGame()
        {
            Context.DataHelper.Clear();
            SceneManager.Load(Scene.DOCK);
        }
        
        private void Exit()
        {
            Application.Quit();
        }


        private void OnEnable()
        {
            _continueGameButton.onClick.AddListener(ContinueGame);
            _newGameButton.onClick.AddListener(NewGame);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _continueGameButton.onClick.RemoveListener(ContinueGame);
            _newGameButton.onClick.RemoveListener(NewGame);
            _exitButton.onClick.RemoveListener(Exit);
        }
    }
}