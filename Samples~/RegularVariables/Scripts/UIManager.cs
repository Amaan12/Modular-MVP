using System;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.UI.MVP;

namespace RegularVariables
{
    /// <summary>
    /// UIManager responsible for binding TestVariable events to UI TextViews 
    /// using ActionPresenter<T> from Modular-MVP without requiring IStat.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Domain Model")]
        [SerializeField] private TestVariable testVariable;

        [Header("UI Views (TextView)")]
        [SerializeField] private TextView movesTextView;
        [SerializeField] private TextView whitePiecesTextView;
        [SerializeField] private TextView blackPiecesTextView;
        [SerializeField] private TextView totalPiecesTextView;
        [SerializeField] private TextView gameStatusTextView;

        private readonly List<IDisposable> _presenters = new List<IDisposable>();

        private void OnEnable()
        {
            if (testVariable == null)
            {
                Debug.LogError("[UIManager] TestVariable reference is missing!", this);
                return;
            }

            if (movesTextView == null)
            {
                Debug.LogError("[UIManager] movesTextView reference is missing!", this);
                return;
            }
            _presenters.Add(new ActionPresenter<int>(
                testVariable.NumberOfMoves,
                h => testVariable.OnNumberOfMovesChanged += h,
                h => testVariable.OnNumberOfMovesChanged -= h,
                movesTextView
            ));

            if (whitePiecesTextView == null)
            {
                Debug.LogError("[UIManager] whitePiecesTextView reference is missing!", this);
                return;
            }
            _presenters.Add(new ActionPresenter<int>(
                testVariable.WhitePiecesLeft,
                h => testVariable.OnWhitePiecesChanged += h,
                h => testVariable.OnWhitePiecesChanged -= h,
                whitePiecesTextView
            ));

            if (blackPiecesTextView == null)
            {
                Debug.LogError("[UIManager] blackPiecesTextView reference is missing!", this);
                return;
            }
            _presenters.Add(new ActionPresenter<int>(
                testVariable.BlackPiecesLeft,
                h => testVariable.OnBlackPiecesChanged += h,
                h => testVariable.OnBlackPiecesChanged -= h,
                blackPiecesTextView
            ));

            // Shared event binding 1: Total pieces (notified whenever white or black pieces change)
            if (totalPiecesTextView == null)
            {
                Debug.LogError("[UIManager] totalPiecesTextView reference is missing!", this);
                return;
            }
            _presenters.Add(new ActionPresenter<int>(
                testVariable.TotalPiecesLeft,
                h => testVariable.OnTotalPiecesChanged += h,
                h => testVariable.OnTotalPiecesChanged -= h,
                totalPiecesTextView
            ));

            // Shared event binding 2: Game status text (notified on move or piece capture)
            if (gameStatusTextView == null)
            {
                Debug.LogError("[UIManager] gameStatusTextView reference is missing!", this);
                return;
            }
            _presenters.Add(new ActionPresenter<string>(
                testVariable.GameStatus,
                h => testVariable.OnGameStatusChanged += h,
                h => testVariable.OnGameStatusChanged -= h,
                gameStatusTextView
            ));
        }

        private void OnDisable()
        {
            foreach (var presenter in _presenters)
            {
                presenter?.Dispose();
            }
            _presenters.Clear();
        }
    }
}
