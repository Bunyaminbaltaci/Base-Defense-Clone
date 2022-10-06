using System;
using Enums;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate=60;
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
      
    }

   

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    
    private void OnChangeGameState(GameStates states)
    {
     CoreGameSignals.Instance.onSetGameState?.Invoke(states);
    }

}