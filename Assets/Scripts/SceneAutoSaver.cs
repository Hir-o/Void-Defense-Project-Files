//using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEditor.SceneManagement;
// using UnityEngine;
// 
// [InitializeOnLoad]
// public class SceneAutoSaver
// {
//     static SceneAutoSaver() { EditorApplication.playModeStateChanged += SaveOnPlay; }
// 
//     public static void SaveOnPlay(PlayModeStateChange state)
//     {
//         if (state == PlayModeStateChange.ExitingEditMode)
//         {
//             #if UNITY_EDITOR
//             Debug.Log("Auto-saving...");
//             #endif
//             
//             EditorSceneManager.SaveOpenScenes();
//             AssetDatabase.SaveAssets();
//         }
//     }
// }