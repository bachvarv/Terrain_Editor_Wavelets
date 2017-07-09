using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour {

	public int size_x = 100;
	public int size_z = 50;
	public float tileSize = 1.0f;


	// Use this for initialization
	void Start () {
		BuildMesh ();
	}

	public void BuildMesh()
	{	
		int numTiles = size_x * size_z;
		int numTri = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVert = vsize_x * vsize_z;


		Vector3[] vertecies = new Vector3[ numVert ] ;
		int[] triangles = new int[ numTri * 3 ];
		Vector3[] normals = new Vector3[ numVert ];

		vertecies [0] = new Vector3 (0, 0, 0);
		vertecies [1] = new Vector3 (1, 0, 0);
		vertecies [2] = new Vector3 (0, 0, -1);
		vertecies [3] = new Vector3 (1, 0, -1);

		triangles [0] = 0;
		triangles [1] = 3;
		triangles [2] = 2;

		triangles [3] = 0;
		triangles [4] = 1;
		triangles [5] = 3;

		normals [0] = Vector3.up;
		normals [1] = Vector3.up;
		normals [2] = Vector3.up;
		normals [3] = Vector3.up;

		int z = 0;
		int x = 0;
		//int tind = 0;

		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertecies [z * vsize_x + x] = new Vector3 (x * tileSize, 0, z * tileSize);
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

		Mesh m = new Mesh ();
		m.vertices = vertecies;
		m.triangles = triangles;
		m.normals = normals;

		MeshFilter mf = GetComponent<MeshFilter>();
		MeshRenderer mr = GetComponent<MeshRenderer>();
		MeshCollider mc = GetComponent<MeshCollider>();

		mf.mesh = m;
		mc.sharedMesh = m;
	}

}
