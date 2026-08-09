#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DesignPatterns.UI.MVP;

namespace RegularVariables.Editor
{
    public static class RegularVariablesSetup
    {
        private const string RootFolder = "Assets/RegularVariables";
        private const string PrefabsFolder = RootFolder + "/Prefabs";
        private const string ScenesFolder = RootFolder + "/Scenes";

        [MenuItem("Tools/RegularVariables/Generate Scene & Prefabs")]
        public static void GenerateAllAssets()
        {
            EnsureFoldersExist();

            // Create new scene
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // Create Camera
            GameObject cameraGo = new GameObject("Main Camera");
            var camera = cameraGo.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.12f, 0.14f, 0.18f);
            cameraGo.AddComponent<AudioListener>();

            // Create EventSystem
            GameObject eventSystemGo = new GameObject("EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();

            // Create Root GameObject
            GameObject rootGo = new GameObject("ChessMVP_Root");

            // Attach TestVariable
            var testVariable = rootGo.AddComponent<TestVariable>();

            // Create Canvas
            GameObject canvasGo = new GameObject("Canvas");
            canvasGo.transform.SetParent(rootGo.transform, false);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            canvasGo.AddComponent<GraphicRaycaster>();

            // Attach UIManager to Root or Canvas
            var uiManager = rootGo.AddComponent<UIManager>();

            // Create UI Panel Container
            GameObject containerGo = new GameObject("UIContainer");
            containerGo.transform.SetParent(canvasGo.transform, false);
            var containerRect = containerGo.AddComponent<RectTransform>();
            containerRect.anchorMin = new Vector2(0.5f, 0.5f);
            containerRect.anchorMax = new Vector2(0.5f, 0.5f);
            containerRect.sizeDelta = new Vector2(800, 600);
            containerRect.anchoredPosition = Vector2.zero;

            var bgImage = containerGo.AddComponent<Image>();
            bgImage.color = new Color(0.08f, 0.1f, 0.14f, 0.95f);

            var vlg = containerGo.AddComponent<VerticalLayoutGroup>();
            vlg.padding = new RectOffset(40, 40, 40, 40);
            vlg.spacing = 15;
            vlg.childAlignment = TextAnchor.UpperLeft;
            vlg.childControlWidth = true;
            vlg.childControlHeight = true;
            vlg.childForceExpandWidth = true;
            vlg.childForceExpandHeight = false;

            // Title
            CreateLabel(containerGo.transform, "TitleText", "CHESS MATCH MVP SIMULATOR", 32, FontStyles.Bold, new Color(0.9f, 0.75f, 0.3f));

            // Create TextView rows
            TextView movesView = CreateTextViewRow(containerGo.transform, "MovesView", "Total Moves: ", new Color(0.8f, 0.9f, 1f));
            TextView whitePiecesView = CreateTextViewRow(containerGo.transform, "WhitePiecesView", "White Pieces Left: ", new Color(0.9f, 0.9f, 0.9f));
            TextView blackPiecesView = CreateTextViewRow(containerGo.transform, "BlackPiecesView", "Black Pieces Left: ", new Color(0.6f, 0.6f, 0.6f));
            TextView totalPiecesView = CreateTextViewRow(containerGo.transform, "TotalPiecesView", "Total Pieces Left (Shared Event): ", new Color(0.4f, 0.85f, 0.6f));
            TextView statusView = CreateTextViewRow(containerGo.transform, "GameStatusView", "Status: ", new Color(1f, 0.85f, 0.4f));

            // Wire UIManager references via SerializedObject
            SerializedObject serializedUiManager = new SerializedObject(uiManager);
            serializedUiManager.FindProperty("testVariable").objectReferenceValue = testVariable;
            serializedUiManager.FindProperty("movesTextView").objectReferenceValue = movesView;
            serializedUiManager.FindProperty("whitePiecesTextView").objectReferenceValue = whitePiecesView;
            serializedUiManager.FindProperty("blackPiecesTextView").objectReferenceValue = blackPiecesView;
            serializedUiManager.FindProperty("totalPiecesTextView").objectReferenceValue = totalPiecesView;
            serializedUiManager.FindProperty("gameStatusTextView").objectReferenceValue = statusView;
            serializedUiManager.ApplyModifiedProperties();

            // Save individual TextView Prefab
            string textPrefabPath = $"{PrefabsFolder}/TextViewPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(movesView.gameObject, textPrefabPath);

            // Save Root Prefab
            string rootPrefabPath = $"{PrefabsFolder}/ChessMVP_RootPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(rootGo, rootPrefabPath);
            Debug.Log($"[RegularVariablesSetup] Saved Root Prefab to {rootPrefabPath}");

            // Save Scene
            string scenePath = $"{ScenesFolder}/RegularVariablesDemo.unity";
            EditorSceneManager.SaveScene(scene, scenePath);
            Debug.Log($"[RegularVariablesSetup] Saved Demo Scene to {scenePath}");

            AssetDatabase.Refresh();
        }

        private static void EnsureFoldersExist()
        {
            if (!AssetDatabase.IsValidFolder(RootFolder))
                AssetDatabase.CreateFolder("Assets", "RegularVariables");
            if (!AssetDatabase.IsValidFolder(PrefabsFolder))
                AssetDatabase.CreateFolder(RootFolder, "Prefabs");
            if (!AssetDatabase.IsValidFolder(ScenesFolder))
                AssetDatabase.CreateFolder(RootFolder, "Scenes");
        }

        private static void CreateLabel(Transform parent, string name, string text, float fontSize, FontStyles fontStyle, Color color)
        {
            GameObject labelGo = new GameObject(name);
            labelGo.transform.SetParent(parent, false);
            var tmp = labelGo.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = fontSize;
            tmp.fontStyle = fontStyle;
            tmp.color = color;
            tmp.alignment = TextAlignmentOptions.Left;
        }

        private static TextView CreateTextViewRow(Transform parent, string name, string labelText, Color textColor)
        {
            GameObject rowGo = new GameObject(name);
            rowGo.transform.SetParent(parent, false);
            var hlg = rowGo.AddComponent<HorizontalLayoutGroup>();
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childForceExpandWidth = true;
            hlg.childForceExpandHeight = true;
            hlg.spacing = 10;

            // Label
            GameObject labelGo = new GameObject("Label");
            labelGo.transform.SetParent(rowGo.transform, false);
            var labelTmp = labelGo.AddComponent<TextMeshProUGUI>();
            labelTmp.text = labelText;
            labelTmp.fontSize = 22;
            labelTmp.fontStyle = FontStyles.Bold;
            labelTmp.color = textColor;
            labelTmp.alignment = TextAlignmentOptions.Left;

            // Value text (with TextView component)
            GameObject valueGo = new GameObject("ValueText");
            valueGo.transform.SetParent(rowGo.transform, false);
            var valueTmp = valueGo.AddComponent<TextMeshProUGUI>();
            valueTmp.text = "--";
            valueTmp.fontSize = 22;
            valueTmp.color = Color.white;
            valueTmp.alignment = TextAlignmentOptions.Left;

            var textView = valueGo.AddComponent<TextView>();

            return textView;
        }
    }
}
#endif
