using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class OnMouseScript : MonoBehaviour {
	Collider collider;
	Renderer rend;
	Transform t;

	TileMap tm;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider> ();
		rend = GetComponent<Renderer> ();
		t = GetComponent<Transform> ();
		tm = GetComponent<TileMap> ();
	}
	
	// Update is called once per frame
	void Update () {
		//MouseOver ();
	}

	void OnMouseOver()
	{
		
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit info;


		if (collider.Raycast (r, out info, Mathf.Infinity)) {
			rend.material.color = Color.red;
			//Debug.Log ();
			Vector3 p = t.InverseTransformPoint (info.point);
//			Debug.Log ("X: " + p.x);
//			Debug.Log ("Z: " + p.z);
			int x = (int) Mathf.Round(p.x );
			int z = (int) Mathf.Round(p.z );

			int sizex = tm.size_x;
//			Debug.Log ("X: " + x + "Z: " + z);
//
//
////			Debug.Log ("X: " + p.x);
////			Debug.Log ("Z: " + p.z);
//			Debug.Log ("Point with index: " + (sizex * z + (x + z)) + " ");

			//Debug.Log ("Tile: " + x + ", " + z); 
		} else {
			rend.material.color = Color.green;
		}
	}

	void OnMouseDown()
	{
//		Debug.Log (Input.mousePosition);
//
//		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit info;
//
//		bool change = false;
//
//
//		if (collider.Raycast (r, out info, Mathf.Infinity)) {
//			//Debug.Log ();
//			Vector3 p = t.InverseTransformPoint (info.point);
//			int x = (int) Mathf.Round(p.x );
//			int z = (int) Mathf.Round(p.z );
//
//			int sizex = tm.size_x;
//			int index = (sizex * z + (x + z));
//
//			Debug.Log ("Point with index: " + (sizex * z + x) + " ");
//
//
//			Debug.Log(tm.wl.modifyPoint(index, new Vector3(0f, 0.2f, 0f)));
//
//			//Debug.Log(tm.wl.getPoints()[index]);
//			change = true;
//			//Debug.Log ("Tile: " + x + ", " + z); 
//		}
//
//		if (change) {
//			tm.updateMesh ();
//		}

	}
	

}
