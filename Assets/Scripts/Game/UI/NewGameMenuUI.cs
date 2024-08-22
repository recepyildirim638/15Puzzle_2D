using Game.GameModel;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class NewGameMenuUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI weightText;
        [SerializeField] TextMeshProUGUI heightText;
        [SerializeField] TextMeshProUGUI gameTypeText;

        public void SetAllText(BaseGameModel model)
        {
            weightText.text = model.weight.ToString();
            heightText.text = model.height.ToString();
            gameTypeText.text = model.name.ToString();
        }
    }
}

