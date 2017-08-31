using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class OnMouseScript : MonoBehaviour {
	Collider collider;
	Renderer rend;
	Transform t;
    Transform choose;

	TileMap tm;
    int selectedIndex;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider> ();
		rend = GetComponent<Renderer> ();
		t = GetComponent<Transform> ();
		tm = GetComponent<TileMap> ();
        choose = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            OnMousePressed();
        }
		//MouseOver ();
	}

	void OnMouseOver()
	{
		
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit info;


		if (collider.Raycast (r, out info, Mathf.Infinity)) {
			//rend.material.color = Color.red;
			//Debug.Log ();
			Vector3 p = t.InverseTransformPoint (info.point);
//			Debug.Log ("X: " + p.x);
//			Debug.Log ("Z: " + p.z);
			int x = (int) Mathf.Round(p.x );
			int z = (int) Mathf.Round(p.z );
            Vector3 v = new Vector3(p.x, 0f, p.z);
            //Debug.Log(v);
			int sizex = tm.size_x;
//			Debug.Log ("X: " + x + "Z: " + z);
//
//
////			Debug.Log ("X: " + p.x);
////			Debug.Log ("Z: " + p.z);
//			Debug.Log ("Point with index: " + (sizex * z + (x + z)) + " ");
            int index = sizex*(z) + (x + z);
            //Debug.Log((sizex + 1) * (tm.size_z + 1));
            if ((sizex + 1) * (tm.size_z + 1) <= tm.GetArray().Length)
            {
                choose.transform.position = tm.GetArray()[index];
            }
            else
            {
                for (int i = 0; i < tm.GetArray().Length; i++)
                {
                    Vector3 vec = tm.GetArray()[i];

                    if ((v.x - vec.x) * (v.x - vec.x) + (v.z - vec.z) * (v.z - vec.z) <= 0.5f * 0.5f)
                    {
                        //Debug.Log((v.x - vec.x) * (v.x - vec.x) + (v.z - vec.z) * (v.z - vec.z));
                        //Debug.Log((vec.x - v.x) + (vec.z - v.z));
                        choose.transform.position = tm.GetArray()[i];
                    }
                }
            }
			//Debug.Log ("Tile: " + x + ", " + z); 
		} else {
			//rend.material.color = Color.green;
		}
	}

    void OnMouseUp()
    {
        if (selectedIndex != -1)
            selectedIndex = -1;
    }

	void OnMousePressed()
	{
        if (selectedIndex == -1)
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;

            bool change = false;


            if (collider.Raycast(r, out info, Mathf.Infinity))
            {
                //debug.log ();
                Vector3 p = t.InverseTransformPoint(info.point);
                int x = (int)Mathf.Round(p.x);
                int z = (int)Mathf.Round(p.z);

                int sizex = tm.size_x;
                int i = (sizex * z + (x + z));
                //selectedVector = tm.wl.getPoints()[i];
                selectedIndex = i;
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                tm.wl.modifyPoint(selectedIndex, new Vector3(0f, -0.2f, 0f));

                //debug.log(tm.wl.getpoints()[index]);
                change = true;
                //debug.log ("tile: " + x + ", " + z); 
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {



                Debug.Log(tm.wl.modifyPoint(selectedIndex, new Vector3(0f, 0.2f, 0f)));

                //debug.log(tm.wl.getpoints()[index]);
                change = true;
                //debug.log ("tile: " + x + ", " + z); 


            }

            if (change)
            {
                tm.updateMesh();
            }
        }
	}
}
