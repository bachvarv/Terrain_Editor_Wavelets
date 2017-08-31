using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector :  Editor {

    Vector3[] points;
    TileMap tm;
    
    public string MeshName = "Mesh name";

	public override void OnInspectorGUI(){
		//base.OnInspectorGUI ();
		DrawDefaultInspector();
		//EditorGUILayout.

        

		if (GUILayout.Button ("Regenerate")) {

            tm = (TileMap)target;
            points = tm.GetArray();
			tm.BuildMesh ();
		}

        MeshName = GUILayout.TextField(MeshName, 50, "textfield");

        if(GUILayout.Button("Save Terrain"))
        {
            tm = (TileMap)target;
            if (tm != null)
            {
                Debug.Log("False");
                if (MeshName != "Mesh name")
                {
                    MeshFilter mf = tm.GetComponent<MeshFilter>();
                    //string path = EditorUtility.SaveFilePanelInProject("Meshes", MeshName, "asset", "Saved");
                    //if (string.IsNullOrEmpty(path)) return;

                    
                    AssetDatabase.CreateAsset(mf.mesh, "Assets/Resources/Meshes/" + MeshName);
                    AssetDatabase.SaveAssets();
                }

            }
        }

        if (GUILayout.Button("Load Terrain"))
        {
            tm = (TileMap)target;
            if (tm != null)
            {

                if (MeshName != "")
                {
                    Debug.Log(MeshName);
                    //string path = EditorUtility.SaveFilePanelInProject("Meshes", MeshName, "asset", "Saved");
                    //if (string.IsNullOrEmpty(path)) return;

                    Mesh m = new Mesh();
                    Object obj;

                    string[] filters = new string[1];
                    filters[0] = "Assets/Resources/Meshes";
                    string[] guids = AssetDatabase.FindAssets(MeshName, filters);
                    if (guids.Length > 0)
                    {
                        obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(Object));
                        if (obj != null)
                        {
                            m = (Mesh)obj;
                            m.name = MeshName;
                            Debug.Log("Mesh exists");
                            tm.ImportMesh(m);

                        }
                        else
                            Debug.Log("The file you tried to load is not existant");
                    }
                }
            }
        }
	}

    void OnDrawGizmos()
    {
        if (tm != null && points.Length > 0)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawSphere(points[i], 0.1f);
            }
        }
    }
}
