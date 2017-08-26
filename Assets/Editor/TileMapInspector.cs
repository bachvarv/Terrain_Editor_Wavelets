using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector :  Editor {

	public override void OnInspectorGUI(){
		//base.OnInspectorGUI ();
		DrawDefaultInspector();
		//EditorGUILayout.

		if (GUILayout.Button ("Regenerate")) {
			TileMap tm = (TileMap) target;
			tm.BuildMesh ();
		}
	}
}
