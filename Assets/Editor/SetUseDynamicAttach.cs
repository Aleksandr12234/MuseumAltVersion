using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SetUseDynamicAttach : EditorWindow
{
    [MenuItem("Tools/Set UseDynamicAttach")]
    public static void ShowWindow()
    {
        GetWindow<SetUseDynamicAttach>("Set UseDynamicAttach");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Set All XRGrabInteractable UseDynamicAttach to True"))
        {
            SetAllUseDynamicAttach();
        }
    }

    private void SetAllUseDynamicAttach()
    {
        // Найти все компоненты XRGrabInteractable в сцене
        XRGrabInteractable[] grabs = FindObjectsOfType<XRGrabInteractable>(true);
        foreach (XRGrabInteractable grab in grabs)
        {
            SerializedObject serializedGrab = new SerializedObject(grab);
            SerializedProperty property = serializedGrab.FindProperty("m_UseDynamicAttach");
            if (property != null)
            {
                property.boolValue = true;
                serializedGrab.ApplyModifiedProperties();
                EditorUtility.SetDirty(grab); // Пометить объект как измененный
            }
        }
        Debug.Log($"Updated {grabs.Length} XRGrabInteractable components.");
    }
}