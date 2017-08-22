using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

	Vector3 dragOrigin;
	Vector3 rotOrigin;
	public float dragSpeed = 15.0f;
	bool rot = false;
	bool drag = false;

	float x;
	float y;
	float z;
	// Use yawn this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			drag = true;
			return;
		}
		else if (Input.GetMouseButtonDown(1))
		{
			rotOrigin = Input.mousePosition;
			rot = true;
			return;
		}

		if (!Input.GetMouseButton (0) && drag) {
			drag = false;
			return;
		}
		if (!Input.GetMouseButton (1) && rot) {
			rot = false;
			return;
		}

		if(rot)
			OnMouseDragRot();
		
		if (drag)
			OnMouseDragPos ();

	}

	void OnMouseDragRot()
	{
		
//		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - rotOrigin);
//		Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.z * dragSpeed);
//
//		transform.Rotate(move);
//
//		rotOrigin = Input.mousePosition;


		//Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - rotOrigin);

//		x = Input.GetAxis ("Mouse X");
//		y = Input.GetAxis ("Mouse Y");
//
//		Matrix4x4 rotAroundX = new Matrix4x4 ();
//		rotAroundX.m11 = Mathf.Cos (x);
//		rotAroundX.m12 = -Mathf.Sin (x);
//		rotAroundX.m21 = Mathf.Sin (x);
//		rotAroundX.m22 = Mathf.Cos (x);
//
//		Matrix4x4 rotAroundY = new Matrix4x4 ();
//		rotAroundY.m00 = Mathf.Cos (x);
//		rotAroundY.m12 = Mathf.Sin (x);
//		rotAroundY.m20 = -Mathf.Sin (x);
//		rotAroundY.m22 = Mathf.Cos (x); 
//
//		Matrix4x4 rotMatrix = rotAroundX * rotAroundY;
//
//		Vector3 eul =  rotMatrix.MultiplyVector( transform.eulerAngles);
//
//		transform.Rotate (eul);
	}

	void OnMouseDragPos()
	{
		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);

		transform.Translate(move, Space.World);

		dragOrigin = Input.mousePosition;
	}
}
