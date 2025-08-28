using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class ComponentBatchAddTool : EditorWindow
{
    private MonoScript sourceComponent;
    private MonoScript targetComponent;
    private Vector2 scrollPosition;
    private string statusMessage = "";
    private MessageType statusType = MessageType.Info;

    [MenuItem("Tools/Component Batch Add Tool")]
    public static void ShowWindow()
    {
        GetWindow<ComponentBatchAddTool>("Component Adder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Component Add Tool", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Drag components to add target component to all objects with source component", MessageType.Info);

        // Поля для перетаскивания компонентов
        EditorGUILayout.BeginVertical("box");
        sourceComponent = (MonoScript)EditorGUILayout.ObjectField("Source Component (Has this)", sourceComponent, typeof(MonoScript), false);
        targetComponent = (MonoScript)EditorGUILayout.ObjectField("Target Component (Add this)", targetComponent, typeof(MonoScript), false);
        EditorGUILayout.EndVertical();

        // Кнопка выполнения
        EditorGUI.BeginDisabledGroup(sourceComponent == null || targetComponent == null);
        if (GUILayout.Button("Add Component to Objects", GUILayout.Height(30)))
        {
            AddComponents();
        }
        EditorGUI.EndDisabledGroup();

        // Статус
        if (!string.IsNullOrEmpty(statusMessage))
        {
            EditorGUILayout.HelpBox(statusMessage, statusType);
        }

        // Информация о найденных объектах
        if (sourceComponent != null)
        {
            EditorGUILayout.Space();
            GUILayout.Label("Objects Preview", EditorStyles.boldLabel);

            Type sourceType = sourceComponent.GetClass();
            if (sourceType != null && sourceType.IsSubclassOf(typeof(Component)))
            {
                var objects = FindObjectsByType(sourceType, FindObjectsInactive.Include, FindObjectsSortMode.None);
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(150));

                foreach (var obj in objects)
                {
                    Component comp = obj as Component;
                    EditorGUILayout.ObjectField(comp.gameObject, typeof(GameObject), true);
                }

                EditorGUILayout.EndScrollView();
                GUILayout.Label($"Found {objects.Length} objects with {sourceType.Name}");
            }
        }
    }

    private void AddComponents()
    {
        if (sourceComponent == null || targetComponent == null)
        {
            statusMessage = "Please assign both components";
            statusType = MessageType.Error;
            return;
        }

        Type sourceType = sourceComponent.GetClass();
        Type targetType = targetComponent.GetClass();

        if (sourceType == null || targetType == null)
        {
            statusMessage = "Could not get component types from scripts";
            statusType = MessageType.Error;
            return;
        }

        if (!sourceType.IsSubclassOf(typeof(Component)) || !targetType.IsSubclassOf(typeof(Component)))
        {
            statusMessage = "Both scripts must derive from Component";
            statusType = MessageType.Error;
            return;
        }

        try
        {
            Undo.SetCurrentGroupName("Add Components Batch");
            int undoGroup = Undo.GetCurrentGroup();

            var objects = FindObjectsByType(sourceType, FindObjectsInactive.Include, FindObjectsSortMode.None);
            int addedCount = 0;

            foreach (var obj in objects)
            {
                Component comp = obj as Component;
                if (comp != null && comp.GetComponent(targetType) == null)
                {
                    Undo.AddComponent(comp.gameObject, targetType);
                    addedCount++;
                }
            }

            Undo.CollapseUndoOperations(undoGroup);

            statusMessage = $"Successfully added {targetType.Name} to {addedCount} objects with {sourceType.Name}";
            statusType = MessageType.Info;
        }
        catch (Exception e)
        {
            statusMessage = $"Error: {e.Message}";
            statusType = MessageType.Error;
        }
    }
}