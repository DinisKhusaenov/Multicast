using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class GameMenuView : MonoBehaviour, IGameMenuView
    {
        public event Action StartClicked;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _level;

        public void SetLevel(int level)
        {
            _level.SetText(level.ToString());
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            StartClicked?.Invoke();
        }
    }
}