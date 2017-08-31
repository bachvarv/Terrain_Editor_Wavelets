using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour {

	public int size_x = 100;
	public int size_z = 50;
	public float tileSize = 1.0f;
	[SerializeField]
	public Wavelet wl;
    Vector3 SelectedPoint;
    int SelectedIndex;

	private static int level = 0;

	Vector3[] array;
    List<GameObject> editPoints;

	// Use this for initialization
	void Start () {
		array = new Vector3[0];
        editPoints = new List<GameObject>();
        BuildMesh();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.L))
		{
			wl.currentLevel++;
			wl.update ();
            updateArray();

		}
		else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.D))
		{
			wl.currentLevel--;
			wl.updateDownLevel ();
            updateArray();
		}
	}

	public void BuildMesh()
	{
        //Debug.Log(size_x);
		wl = new Wavelet ();
		int numTiles = size_x * size_z;
		int numTri = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVert = vsize_x * vsize_z;


		Vector3[] vertecies = new Vector3[ numVert ] ;
		int[] triangles = new int[ numTri * 3 ];
		Vector3[] normals = new Vector3[ numVert ];



//		vertecies [0] = new Vector3 (0, 0, 0);
//		vertecies [1] = new Vector3 (1, 0, 0);
//		vertecies [2] = new Vector3 (0, 0, -1);
//		vertecies [3] = new Vector3 (1, 0, -1);
//
//		triangles [0] = 0;
//		triangles [1] = 3;
//		triangles [2] = 2;
//
//		triangles [3] = 0;
//		triangles [4] = 1;
//		triangles [5] = 3;
//
//		normals [0] = Vector3.up;
//		normals [1] = Vector3.up;
//		normals [2] = Vector3.up;
//		normals [3] = Vector3.up;

		int z = 0;
		int x = 0;
		//int tind = 0;

		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertecies [z * vsize_x + x] = new Vector3 (x * tileSize, 0, z * tileSize);
				wl.addDrawnPoint (vertecies [z * vsize_x + x]);
				normals [z * vsize_x + x] = Vector3.up;

			}
		}

		for (z = 0; z < size_z; z++) {
			for (x = 0; x < size_x ; x++) {
				int square = z * size_x + x;
				int triIndex = square * 6; 
				triangles[triIndex] = 		z * vsize_x + x; 	
				triangles[triIndex + 1] =	(z + 1) * vsize_x + x ;
				triangles[triIndex + 2] =	(z + 1) * vsize_x + x + 1;
						  
				triangles[triIndex + 3] = 	z * vsize_x + x;
				triangles[triIndex + 4] = 	(z + 1) * vsize_x + x + 1;
				triangles[triIndex + 5] = 	z * vsize_x + x + 1;
			}	
		}

		wl.determineLevelOfDetail ();
        

		Mesh m = new Mesh ();
		m.vertices = vertecies;
		m.triangles = triangles;
		m.normals = normals;

		MeshFilter mf = GetComponent<MeshFilter>();
		MeshRenderer mr = GetComponent<MeshRenderer>();
		MeshCollider mc = GetComponent<MeshCollider>();

		mf.mesh = m;
		mc.sharedMesh = m;

        if (numVert % 2 != 0)
        {
            array = new Vector3[numVert + 1];
            
            wl.addDrawnPoint(vertecies[numVert - 1]);
            wl.setMaxPoints(numVert + 1);
            array = wl.getPoints();
        }
        else
        {
            array = new Vector3[numVert];
            wl.setMaxPoints(numVert);
            array = wl.getPoints();
        }

	}

	void RebuildMeshWithLevel(int level)
	{
		
		int sizex = (size_x / ((int)Mathf.Pow (2, level)));
		int sizez = (size_z / ((int)Mathf.Pow (2, level)));

		int numTiles = sizex * sizez;
		numTiles = numTiles >= 1 ? numTiles : 1;
		int numTri = numTiles * 2;

		int vsize_x = sizex + 1;
		int vsize_z = sizez + 1;
		vsize_x = vsize_x >= 2 ? vsize_x : 2;
		vsize_z = vsize_z >= 2 ? vsize_z : 2;
		int numVert = vsize_x * vsize_z;



		Vector3[] vertecies = new Vector3[ numVert ] ;
		int[] triangles = new int[ numTri * 3 ];
		Vector3[] normals = new Vector3[ numVert ];

		int z = 0;
		int x = 0;
		//int tind = 0;

		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertecies [z * vsize_x + x] = new Vector3 (x * tileSize, 0, z * tileSize);
				wl.addDrawnPoint (vertecies [z * vsize_x + x]);
				normals [z * vsize_x + x] = Vector3.up;

			}
		}

		for (z = 0; z < sizez; z++) {
			for (x = 0; x < sizex ; x++) {
				int square = z * size_x + x;
				int triIndex = square * 6; 
				triangles[triIndex] = 		z * vsize_x + x; 	
				triangles[triIndex + 1] =	(z + 1) * vsize_x + x ;
				triangles[triIndex + 2] =	(z + 1) * vsize_x + x + 1;

				triangles[triIndex + 3] = 	z * vsize_x + x;
				triangles[triIndex + 4] = 	(z + 1) * vsize_x + x + 1;
				triangles[triIndex + 5] = 	z * vsize_x + x + 1;
			}	
		}

		MeshFilter mf = GetComponent<MeshFilter>();
		MeshCollider mc = GetComponent<MeshCollider>();

		Mesh m = new Mesh ();
		m.vertices = wl.getPoints();
		m.triangles = triangles;
		m.normals = normals;


		mf.mesh = m;
		mc.sharedMesh = m;

		array = new Vector3[numVert];
		array = wl.getPoints ();

	}

	public void updateMesh()
	{
		MeshFilter mf = GetComponent<MeshFilter>();

		mf.mesh.vertices = wl.getPoints ();
        array = new Vector3[wl.getPoints().Length];
        array = wl.getPoints();
	}

    //void drawWaveleteSphere()
    //{


    //    GameObject s;
        
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            
    //        s.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    //        s.transform.position = array[i];
    //        s.transform.parent = transform;
    //        editPoints.Add(s);
            
    //    }
    //}

    void OnDrawGizmos()
    {
        if (array != null)
        {
            int le = array.Length;
            for (int i = 0; i < le; i++)
            {
                Gizmos.DrawSphere(array[i], 0.2f);
            }
        }
    }

    public void ImportMesh( Mesh m)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshCollider mc = GetComponent<MeshCollider>();

        mf.mesh = m;
        wl.setPoints(m.vertices);

        array = new Vector3[m.vertices.Length];
        array = m.vertices;

    }

    public Vector3[] GetArray()
    {
        return array;
    }

    public void SetSelectedPoint(Vector3 point, int index)
    {
        SelectedPoint = point;
        SelectedIndex = index;
    }

    void updateArray()
    {
        array = new Vector3[wl.getPoints().Length];
        array = wl.getPoints();
    }
}
