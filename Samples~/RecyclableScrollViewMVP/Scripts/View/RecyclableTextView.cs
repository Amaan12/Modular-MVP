using TMPro;
using UnityEngine;
using PolyAndCode.UI;
using DesignPatterns.UI.MVP;

namespace _Project.Scripts.View
{
    /// <summary>
    /// Dumb cell view component that implements ICell (for RecyclableScrollRect) and IView<string> (for MVP).
    /// Pure visual renderer with zero presenter or model references.
    /// </summary>
    public class RecyclableTextView : MonoBehaviour, ICell, IView<string>
    {
        [SerializeField] private TextMeshProUGUI textComponent;

        private void Awake()
        {
            if (textComponent == null)
            {
                textComponent = GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        private void OnValidate()
        {
            if (textComponent == null)
            {
                textComponent = GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        /// <summary>
        /// Pure 1-way render method from IView<string>.
        /// </summary>
        public void Render(string value)
        {
            if (textComponent != null)
            {
                textComponent.text = value;
            }
        }
    }
}
