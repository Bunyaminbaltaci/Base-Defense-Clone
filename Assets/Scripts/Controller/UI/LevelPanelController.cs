using TMPro;
using UnityEngine;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        public void SetLevelText()
        {
        }

        public void SetMultipler()
        {
            multiplierText.alpha = 1;
        }

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI multiplierText;

        #endregion

        #endregion
    }
}