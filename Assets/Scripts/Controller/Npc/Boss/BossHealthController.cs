using Managers.Core;
using TMPro;
using UnityEngine;

namespace Controller.Npc.Boss
{
    public class BossHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject healthObject;
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private BossManager bossManager;

        #endregion

        #region Private Variables
        

        #endregion

        #endregion

        

        public void SetVisible(bool active)
        {
            healthObject.SetActive(active);
        }

   
        public void SetHealthText(int health)
        {
            healthText.text = health.ToString();
            SetHealthBar(health);
        }

        private void SetHealthBar(int health)
        {
            healthBar.transform.localScale = new Vector3((health / 1000f), healthBar.transform.localScale.y,
                healthBar.transform.localScale.z);
        }
    }
}