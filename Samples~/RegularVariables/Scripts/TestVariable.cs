using System;
using System.Collections;
using UnityEngine;

namespace RegularVariables
{
    /// <summary>
    /// Test domain script representing a chess match state without using IStat.
    /// Simulates game progress and variable changes using a coroutine.
    /// </summary>
    public class TestVariable : MonoBehaviour
    {
        [Header("Chess Game State")]
        [SerializeField] private int numberOfMoves = 0;
        [SerializeField] private int whitePiecesLeft = 16;
        [SerializeField] private int blackPiecesLeft = 16;
        [SerializeField] private string gameStatus = "Game Initialized. Ready!";

        public int NumberOfMoves => numberOfMoves;
        public int WhitePiecesLeft => whitePiecesLeft;
        public int BlackPiecesLeft => blackPiecesLeft;
        public int TotalPiecesLeft => whitePiecesLeft + blackPiecesLeft;
        public string GameStatus => gameStatus;

        // Events for variable changes
        public event Action<int> OnNumberOfMovesChanged;
        public event Action<int> OnWhitePiecesChanged;
        public event Action<int> OnBlackPiecesChanged;

        // Shared Events (triggered when either white or black pieces change, or status updates)
        public event Action<int> OnTotalPiecesChanged;
        public event Action<string> OnGameStatusChanged;

        [Header("Simulation Settings")]
        [SerializeField] private float stepInterval = 1.5f;
        [SerializeField] private bool autoStartSimulation = true;

        private Coroutine _simulationCoroutine;

        private void Start()
        {
            if (autoStartSimulation)
            {
                StartSimulation();
            }
        }

        public void StartSimulation()
        {
            if (_simulationCoroutine != null)
            {
                StopCoroutine(_simulationCoroutine);
            }
            _simulationCoroutine = StartCoroutine(SimulateChessGameRoutine());
        }

        public void StopSimulation()
        {
            if (_simulationCoroutine == null) return;

            StopCoroutine(_simulationCoroutine);
            _simulationCoroutine = null;
        }

        private IEnumerator SimulateChessGameRoutine()
        {
            UpdateStatus("Match Started! All 32 pieces on board.");

            while (whitePiecesLeft > 0 && blackPiecesLeft > 0)
            {
                yield return new WaitForSeconds(stepInterval);

                // Increment moves count
                numberOfMoves++;
                OnNumberOfMovesChanged?.Invoke(numberOfMoves);

                // 60% chance to capture a piece on a turn
                if (UnityEngine.Random.value < 0.6f)
                {
                    bool isWhiteCaptured = UnityEngine.Random.value > 0.5f;

                    if (isWhiteCaptured && whitePiecesLeft > 0)
                    {
                        whitePiecesLeft--;
                        OnWhitePiecesChanged?.Invoke(whitePiecesLeft);

                        // Trigger SHARED events
                        OnTotalPiecesChanged?.Invoke(TotalPiecesLeft);
                        UpdateStatus($"Move {numberOfMoves}: White piece captured! White: {whitePiecesLeft} | Black: {blackPiecesLeft}");
                    }
                    else if (!isWhiteCaptured && blackPiecesLeft > 0)
                    {
                        blackPiecesLeft--;
                        OnBlackPiecesChanged?.Invoke(blackPiecesLeft);

                        // Trigger SHARED events
                        OnTotalPiecesChanged?.Invoke(TotalPiecesLeft);
                        UpdateStatus($"Move {numberOfMoves}: Black piece captured! White: {whitePiecesLeft} | Black: {blackPiecesLeft}");
                    }
                }
                else
                {
                    UpdateStatus($"Move {numberOfMoves}: Positional move made. White: {whitePiecesLeft} | Black: {blackPiecesLeft}");
                }
            }

            // End game handling
            string outcome = whitePiecesLeft > blackPiecesLeft
                ? "White Wins by Checkmate!"
                : (blackPiecesLeft > whitePiecesLeft ? "Black Wins by Checkmate!" : "Draw!");

            UpdateStatus($"Game Over! {outcome} Total Moves: {numberOfMoves}");
        }

        private void UpdateStatus(string newStatus)
        {
            gameStatus = newStatus;
            OnGameStatusChanged?.Invoke(gameStatus);
        }

        private void OnDestroy()
        {
            StopSimulation();
        }
    }
}
