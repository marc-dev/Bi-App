using UnityEngine;
using System.Collections;

public class Segment 
{
	public Vector3 pointA;
	public Vector3 pointB;
	
	public Segment ()
	{
		pointA = new Vector3();
		pointB = new Vector3();
	}
	
	public Segment (Vector3 a, Vector3 b)
	{
		pointA = a;
		pointB = b;
	}
	
	public Segment randomSegment(int maxDist)
	{
		Vector2 espace = new Vector2(-90, 90);
		
		pointA.x = UnityEngine.Random.Range (espace.x, espace.y);
		pointA.y = 0; //Work in 2D
		pointA.z = UnityEngine.Random.Range (espace.x, espace.y);
		
		pointB.x = UnityEngine.Random.Range (pointA.x, pointA.x + maxDist);
		pointB.y = 0; //Work in 2D
		pointB.z = UnityEngine.Random.Range (pointA.z, pointA.z + maxDist);
		
		return this;
	}

}
