using TMPro;
using UnityEngine;

namespace Controller
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI money;
        [SerializeField] private TextMeshProUGUI diamond;

        #endregion

        #endregion

        public void SetMoneyText(int value)
        {
            money.text = value.ToString();
        }

        public void SetDiamondText(int value)
        {
            diamond.text=value.ToString();
            
        }
    }
}