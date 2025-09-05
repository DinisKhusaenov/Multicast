using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.Windows
{
    public class GameOverView : MonoBehaviour, IGameOverView
    {
        public event Action NextLevelClicked;
        public event Action MenuClicked;
        
        [SerializeField] private TMP_Text _words;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _menuButton;

        public void Show(List<string> words)
        {
            gameObject.SetActive(true);

            _words.SetText("");
            foreach (string word in words)
            {
                _words.SetText(string.IsNullOrEmpty(_words.text) 
                    ? $"{word}" 
                    : $"{_words.text}\n{word}");
            }
            
            _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
            _menuButton.onClick.AddListener(OnMenuClicked);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
            _menuButton.onClick.RemoveListener(OnMenuClicked);
        }

        private void OnNextLevelClicked()
        {
            NextLevelClicked?.Invoke();
        }
        
        private void OnMenuClicked()
        {
            MenuClicked?.Invoke();
        }
    }
}