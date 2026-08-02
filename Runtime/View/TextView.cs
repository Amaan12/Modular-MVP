using TMPro;
using UnityEngine;

namespace DesignPatterns.UI.MVP
{
    /// <summary>
    /// View component that renders text for TMPro.TextMeshProUGUI components.
    /// </summary>
    public class TextView : MonoBehaviour, IView<string>, IView<int>, IView<float>, IView<StatRange>
    {
        [SerializeField] private TextMeshProUGUI textComponent;

        public void Render(string text) => SetText(text);

        public void Render(int value) => SetText(value.ToString());

        public void Render(float value) => SetText(value.ToString("F1"));

        public void Render(StatRange range) => SetText($"{Mathf.CeilToInt(range.Current)} / {Mathf.CeilToInt(range.Max)}");

        private void SetText(string content)
        {
            if (textComponent != null)
            {
                textComponent.text = content;
            }
        }
    }
}
