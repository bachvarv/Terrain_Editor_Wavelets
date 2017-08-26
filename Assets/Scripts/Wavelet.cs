using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wavelet {
	
	[SerializeField]
	List<Vector3> editPoints;
	[SerializeField]
	List<Vector3> waveletCoeff;


	//Vector3[] editPointsArr;
	//Vector3[] waveletCoeff;

	public int currentLevel;
	int maxLevel;

	int band;

	Matrix4x4 matrixPQ;

	public Wavelet()
	{
		editPoints = new List<Vector3> ();
		waveletCoeff = new List<Vector3> ();
		currentLevel = 1;
		matrixPQ = new Matrix4x4 ();
		maxLevel = band = 0;
	}

	private void syntesizePoints()
	{
		if (currentLevel == 0 )
			return;
		
		int length = editPoints.Count;
		bool odd;
		Vector3[] output;
		Vector3[] diff;
		if (length % 2 != 0) {

			//Debug.Log ("Doing Stuff");

			output 		= new Vector3[((length) * 2) + 1];
			diff 		= new Vector3[((length) * 2) + 1];
			odd = true;
		} else {
			output 	= new Vector3[((length) * 2)];
			diff 	= new Vector3[((length) * 2)];
			odd = false;
		}
//		Debug.Log (length + ": " + output.Length);
		for (int i = 0; i < length - 1; i++) {
			Debug.Log(i);
			output [i * 2 ] = editPoints [i] + waveletCoeff[i];
			output [i * 2 + 1] = editPoints [i] - waveletCoeff [i];

		}

		if (currentLevel > 1) {
			for (int i = 0; i < length - 1; i++) {
				waveletCoeff.RemoveAt (i);
			}
		} else {
			waveletCoeff.Clear ();
		}

		if(odd)
			output [((length - 1) * 2)] = editPoints [length - 1];



		editPoints.Clear ();
		editPoints.AddRange (output);


	}

	private void analyzePoints(int lvl)
	{
		if(lvl == maxLevel)
			return;

		Vector3[] output;

		Vector3[] diff;
		//TODO: Раздели двата арея, един за уейвлет коеф., другия за контролните точки, синтезиране на арея и пресмятане на контролните точки при всяка промяна на левела.
		//TODO: Прилагане на Точките и промяна на височината им.
		int length = editPoints.Count;
		bool odd;
		if (length % 2 != 0) {

			output 	= new Vector3[length / 2 + 1];

			diff 	= new Vector3[length / 2];
			odd = true;
		} else {
			output 	= new Vector3[length / 2 ];

			diff 	= new Vector3[length / 2];
			odd = false;
		}

		//int le = length / 2;
		int i = 0;
		for (int le = length / 2 ;; le = le / 2) {
			
			for (int j = i; j < le; j++) {
				Vector3 average = editPoints [j * 2] + editPoints[j * 2 + 1];
				average /= 2;
				Vector3 difference = editPoints [j * 2] - average; 
				Debug.Log (difference + ": " + j);
				output [j] = average;
				diff [j] = difference;
				//i+= 1;
			}
			//Debug.Log (i + 1 + " " + (length + 1 )/ 2);
			if (odd) {
				

				Debug.Log ("True" + i);
				output [length/2] = editPoints [length - 1];

			}


			break;
		}

		waveletCoeff.InsertRange(0,diff);

		editPoints.Clear ();
		editPoints.AddRange (output);

	}

	public void addDrawnPoint(Vector3 point)
	{
		editPoints.Add (point);

	}

	int Level()
	{
		return currentLevel;
	}

	public Vector3[] draw()
	{
		Vector3[] field = new Vector3[editPoints.Count];



		return field;
	}

	public void determineLevelOfDetail()
	{
		int length = editPoints.Count;
//		editPointsArr = new Vector3[length];
//		editPointsArr = editPoints.ToArray ();
		Vector3[] output = new Vector3[length];
		output = editPoints.ToArray ();

		for (int le = length / 2 ;; le = le / 2) {
			for (int i = 0; i < le; i++) {
				Vector3 average = output [i * 2] + output [i * 2 + 1];
				average /= 2;
				Vector3 difference = output [i * 2] - output [i * 2 + 1];
				output [i * 2] = average;
				output [le + i] = difference;
				maxLevel++;
			}

			if (le == 1) {
				break;
			}
		}
	}

	public void update()
	{
		analyzePoints (currentLevel);
	}

	public void updateDownLevel(){
		syntesizePoints ();
	}

	public Vector3[] getPoints()
	{
		return editPoints.ToArray ();
	}

	public Vector3 modifyPoint(int index, Vector3 vector)
	{
		editPoints [index] += vector;

		return editPoints [index];
	}

}
