using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(GridController))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//Reference to our script
		GridController gridGen = (GridController)target;

		//Only show the mapsettings UI if we have a reference set up in the editor
		if (GUILayout.Button("Generate"))
		{
			gridGen.GenerateGrid();
		}


		if (GUILayout.Button("Clear"))
		{
			gridGen.Clear();
		}


	}
}