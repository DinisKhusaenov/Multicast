using System;
using Gameplay.Input;
using Infrastructure.Services.LogService;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay.Clusters
{
    public class Cluster : MonoBehaviour, ICluster
    {
        public event Action<Cluster> OnDragBegun;
        public event Action<Cluster> OnDragEnded;
        
        [SerializeField] private TMP_Text _letters;
        
        [field: SerializeField, Range(1, 20)] public int ClusterLength { get; private set; }

        private IInputService _inputService;
        private ILogService _logService;

        public string Letters { get; private set; }
        public IClusterContainer CurrentClusterContainer { get; private set; }

        [Inject]
        private void Construct(IInputService inputService, ILogService logService)
        {
            _logService = logService;
            _inputService = inputService;
        }

        public void Initialize(string letters)
        {
            if (string.IsNullOrEmpty(letters))
                throw new Exception("Letters can't be empty");
            
            if (letters.Length != ClusterLength)
                _logService.LogWarning("The number of letters does not match the length of the cluster");
            
            _letters.SetText(letters);
            Letters = letters;
        }

        public void SetContainer(IClusterContainer clusterContainer)
        {
            CurrentClusterContainer = clusterContainer;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragBegun?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 position = new Vector3(
                _inputService.GetTouchPosition().x, 
                _inputService.GetTouchPosition().y,
                0);
            
            transform.position = position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnded?.Invoke(this);
        }
    }
}