using System;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using DesignPatterns.UI.MVP;
using _Project.Scripts.Model;

namespace _Project.Scripts.Presenter
{
    /// <summary>
    /// List presenter/controller implementing IRecyclableScrollRectDataSource.
    /// Manages IStat<string> item models and binds them to pooled cell views via BinderOneWay<string>.
    /// </summary>
    public class RecyclableListPresenter : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        [SerializeField] private RecyclableScrollRect recyclableScrollRect;
        [SerializeField] private int initialItemCount = 500;

        private readonly List<IStat<string>> _itemStats = new List<IStat<string>>();
        private readonly Dictionary<ICell, IDisposable> _cellPresenters = new Dictionary<ICell, IDisposable>();

        private void Awake()
        {
            // Populate list with IStat<string> models
            _itemStats.Clear();
            for (int i = 0; i < initialItemCount; i++)
            {
                _itemStats.Add(new Stat<string>($"Item #{i} - Recyclable IStat MVP"));
            }

            if (recyclableScrollRect != null)
            {
                recyclableScrollRect.DataSource = this;
            }
            else
            {
                Debug.LogError("RecyclableScrollRect reference is missing on RecyclableListPresenter!", this);
            }
        }

        #region IRecyclableScrollRectDataSource Implementation

        public int GetItemCount()
        {
            return _itemStats.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            // 1. Dispose old binder associated with this pooled cell instance
            if (_cellPresenters.TryGetValue(cell, out var oldBinder))
            {
                oldBinder?.Dispose();
                _cellPresenters.Remove(cell);
            }

            // 2. Bind new IStat<string> to dumb cell view using standard BinderOneWay<string>
            if (index >= 0 && index < _itemStats.Count && cell is IView<string> cellView)
            {
                IStat<string> stat = _itemStats[index];
                BinderOneWay<string> binder = new BinderOneWay<string>(stat, cellView);
                _cellPresenters[cell] = binder;
            }
            else
            {
                Debug.LogWarning($"Cell at index {index} does not implement IView<string>!", this);
            }
        }

        #endregion

        /// <summary>
        /// Update item stat value at a specific index.
        /// The active cell showing this index will update automatically via its BinderOneWay<string>!
        /// </summary>
        public void UpdateItemStat(int index, string newValue)
        {
            if (index >= 0 && index < _itemStats.Count)
            {
                _itemStats[index].Set(newValue);
            }
        }

        private void OnDestroy()
        {
            // Dispose all active cell binders
            foreach (var binder in _cellPresenters.Values)
            {
                binder?.Dispose();
            }
            _cellPresenters.Clear();
        }
    }
}
