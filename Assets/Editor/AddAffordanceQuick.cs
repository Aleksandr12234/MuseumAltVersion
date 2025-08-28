using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AffordanceAdder : EditorWindow
{
    [MenuItem("Tools/Add Affordance to Outline Objects")]
    public static void ShowWindow()
    {
        GetWindow<AffordanceAdder>("Affordance Adder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Affordance to all objects with Outline", EditorStyles.boldLabel);

        if (GUILayout.Button("Run"))
        {
            AddAffordance();
        }
    }

    private void AddAffordance()
    {
        // ����� ��� ������� � ����������� Outline
        Outline[] outlineObjects = FindObjectsByType<Outline>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int count = 0;

        foreach (Outline outline in outlineObjects)
        {
            // ���� � ������� ��� Affordance, �������� ���
            if (outline.GetComponent<Affordance>() == null)
            {
                outline.gameObject.AddComponent<Affordance>();
                count++;
            }
        }

        // ������� ���������
        Debug.Log($"�������� Affordance � {count} ��������.");
    }
}