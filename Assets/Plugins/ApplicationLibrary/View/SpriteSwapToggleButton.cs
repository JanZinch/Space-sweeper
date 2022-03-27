using UnityEngine;
using UnityEngine.UI;

namespace Alexplay.OilRush.Library.View
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button)), RequireComponent(typeof(CanvasGroup))]
    public class SpriteSwapToggleButton : ToggleButton
    {
        [SerializeField] private Sprite _checkedSprite;
        [SerializeField] private Sprite _uncheckedSprite;

        protected override void UpdateState()
        {
            GetComponent<Image>().sprite = IsChecked ? _checkedSprite : _uncheckedSprite;
            GetComponent<CanvasGroup>().alpha = IsChecked ? 0.4f : 1;
        }
    }
}