using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class DockMenu : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton = null;
        [SerializeField] private Button _startButton = null;
        
        [Header("Equipment")]
        [SerializeField] private EquipmentItemView _equipmentViewOriginal = null;
        [SerializeField] private RectTransform _equipmentListView = null;
        [field: SerializeField] public EquipmentColors EquipmentListColors { get; private set; } = default;
        
        
        private List<EquipmentItemView> _equipmentList = null;
        
        public static DockMenu Instance { get; private set; } = null;
        
        [Serializable]
        public struct EquipmentColors
        {
            [field: SerializeField] public Color LockedColor { get; private set; }
            [field: SerializeField] public Color AvailableColor { get; private set; }
            [field: SerializeField] public Color SelectedColor { get; private set; }
        }

        
        private void Awake()
        {
            Instance = this;
            _equipmentList = EquipmentUtils.InitializeItemViewList(_equipmentViewOriginal, in _equipmentListView);
        }

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(ToMainMenu);
            _startButton.onClick.AddListener(StartGameSession);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(ToMainMenu);
            _startButton.onClick.RemoveListener(StartGameSession);
        }

        private void ToMainMenu()
        {
            SceneManager.Load(Scene.LOADER);
        }

        private void StartGameSession()
        {
            SceneManager.Load(Scene.CHANNEL);
        }

        public void UpdateItemView(EquipmentItemType type, EquipmentItemState newState)
        {
            _equipmentList.Find((itemView)=>itemView.Type == type).SetState(newState);
        }
        
        public void UpdateListView()
        {
            foreach (EquipmentItemView view in _equipmentList)
            {
                view.Update();
            }
        }

    }
}