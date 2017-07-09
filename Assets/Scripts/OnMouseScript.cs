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
		MouseOver ();
	}

	void MouseOver()
	{
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit info;


		if (collider.Raycast (r, out info, Mathf.Infinity)) {
			rend.material.color = Color.red;
			//Debug.Log ();
			Vector3 p = t.InverseTransformPoint (info.point);
			int x = Mathf.FloorToInt(p.x / tm.tileSize);
			int z = Mathf.FloorToInt(p.z / tm.tileSize);

			Debug.Log ("Tile: " + x + ", " + z); 
		} else {
			rend.material.color = Color.green;
		}
	}
	

}
