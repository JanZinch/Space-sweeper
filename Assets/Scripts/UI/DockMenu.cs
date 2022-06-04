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
        
        [Header("Debug")]
        [SerializeField] private Button _debugButton = null;
        [SerializeField] private Button _closeDebugButton = null;
        [SerializeField] private GameObject _debugDialog = null;
        
        
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
            //EquipmentItemSlot.Refresh();
        }

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(ToMainMenu);
            _startButton.onClick.AddListener(StartGameSession);
            _debugButton.onClick.AddListener(ShowDebugDialog);
            _closeDebugButton.onClick.AddListener(HideDebugDialog);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(ToMainMenu);
            _startButton.onClick.RemoveListener(StartGameSession);
            _debugButton.onClick.RemoveListener(ShowDebugDialog);
            _closeDebugButton.onClick.RemoveListener(HideDebugDialog);
        }

        private void ToMainMenu()
        {
            SceneManager.Load(Scene.LOADER);
        }

        private void StartGameSession()
        {
            EquipmentItemSlot.UpdatePlayerUtils();
            SceneManager.Load(Scene.CHANNEL);
        }

        private void ShowDebugDialog()
        {
            _debugDialog.SetActive(true);
        }

        private void HideDebugDialog()
        {
            _debugDialog.SetActive(false);
        }
        
        public void UpdateItemView(EquipmentItemType type, EquipmentItemState newState)
        {
            _equipmentList.Find((itemView)=>itemView.Type == type).SetState(newState);
        }
        
        public void UpdateListView()
        {
            foreach (EquipmentItemView view in _equipmentList)
            {
                view.UpdateState();
            }
        }

    }
}