using Controller;
using Enums;
using Signals;
using UnityEngine;

namespace Controller
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

     

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetMoneyText += onSetMoneyText;
            UISignals.Instance.onSetDiamondText += OnSetDiamondText;

          
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetMoneyText -= onSetMoneyText;
            UISignals.Instance.onSetDiamondText -= OnSetDiamondText;
            
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }
        

        private void onSetMoneyText(int value)
        {
            levelPanelController.SetMoneyText(value);
        }

        private void OnSetDiamondText(int value)
        {
            levelPanelController.SetDiamondText(value);
        }


        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        
        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void Restart()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void WinPanelClose()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
        }

     
    }
}