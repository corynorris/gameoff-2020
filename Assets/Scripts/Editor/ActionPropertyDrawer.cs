using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;


[CustomPropertyDrawer(typeof(ActionPattern))]
public class ActionPatternPropertyDrawer : PropertyDrawer
{

    private void BuildPatternGrid(Rect position, SerializedProperty property)
    {

        SerializedProperty data = property.FindPropertyRelative("pattern");

        if (data.arraySize != 8)
        {
            data.arraySize = 8;

        }

        GridDirection[] indexMapping = { GridDirection.NW, GridDirection.N, GridDirection.NE, GridDirection.W, GridDirection.E, GridDirection.SW, GridDirection.S, GridDirection.SE };

        Rect newPosition = new Rect(position);
        newPosition.height = 18f;
        newPosition.width = 18f;

        for (int j = 0; j < 3; j++)
        {

            newPosition.x = position.x;
            newPosition.y = position.y + ((j+1)*18f);

            for (int i = 0; i < 3; i++)
            {
                int index = j * 3 + i;

                if (index >= 5)
                {
                    index -= 1;
                }
                
                if (i != 1 || j != 1)
                {
                    int direction = (int)indexMapping[index];
                    SerializedProperty row = data.GetArrayElementAtIndex(direction);
                    EditorGUI.PropertyField(newPosition, row, GUIContent.none);
                }

                newPosition.x += newPosition.width;
            }

         
            
        }
    }

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var actionRect = new Rect(position.x, position.y, position.width, 18);
        var expandRect = new Rect(position.x, position.y + 18, position.width, 18);
        var patternRect = new Rect(position.x, position.y + 6, position.width, 18*3);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(actionRect, property.FindPropertyRelative("action"), GUIContent.none);
        if (property.FindPropertyRelative("action").intValue == (int)Action.Expand)
        {
            patternRect.y += 18;
            EditorGUI.PropertyField(expandRect, property.FindPropertyRelative("expandDirection"), GUIContent.none);
        }


        BuildPatternGrid(patternRect, property);
        //EditorGUI.PropertyField(patternRect, property.FindPropertyRelative("pattern"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }



    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 5 + 5;
    }


}
