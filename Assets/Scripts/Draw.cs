using UnityEngine;
using System.Collections;

public class Draw : MonoBehaviour {

	Wavelet wl;

	// Use this for initialization
	void Start () {
		wl = new Wavelet ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		Debug.Log (Input.mousePosition);
	}
}
