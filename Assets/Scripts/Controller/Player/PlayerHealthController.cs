using System;
using System.Collections;
using Enums;
using Managers.Core;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject healthObject;
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private Coroutine _regeneration;

        #endregion

        #endregion


        private void Start()
        {
            _regeneration = StartCoroutine(Regeneration());
        }

        public void ChangedLayer(LayerType type)
        {
            switch (type)
            {
                case LayerType.Default:
                    _regeneration = StartCoroutine(Regeneration());
                    break;
                case LayerType.BattleArea:
                    if (_regeneration != null)
                        StopCoroutine(_regeneration);
                    SetVisible(true);
                    break;
            }
        }

        private void SetVisible(bool active)
        {
            healthObject.SetActive(active);
        }

        IEnumerator Regeneration()
        {
            WaitForSeconds waiter = new WaitForSeconds(0.1f);


            while (true)
            {
                if (playerManager.Health == 100)
                {
                    break;
                }

                playerManager.Health++;
                yield return waiter;
            }

            SetVisible(false);
        }

        public void SetHealthText(int health)
        {
            healthText.text = health.ToString();
            SetHealthBar(health);
        }

        private void SetHealthBar(int health)
        {
            healthBar.transform.localScale = new Vector3((health / 100f), healthBar.transform.localScale.y,
                healthBar.transform.localScale.z);
        }
    }
}