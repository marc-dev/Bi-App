using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


//Y = 0 cause we are in 2D
//Axes : X, Z


public class TestAlgo : MonoBehaviour 
{
	#region VAR
	Vector3 playerPosition;
	Vector3 pointIntersection;
	Vector3 mousePosition;
	
	private bool playerMove;
	private Vector3[] endPoints;

	Segment [] segments;
	Color colorSegement = Color.red;


	public bool IntersectionParametrique;
	public int nbSegments;
	public int nbRay;
	public int raysWidth;
	public int distanceMaxWall = 20;
	public bool optimiseAlgo;

	private bool _algoReady = false;
	
	
	private Vector3[] rays;
	float [] angles;

	 #endregion

	#region Unity function
	// Use this for initialization
	void Start () 
	{
		createSegment ();
		
		if (optimiseAlgo)
			getUniquePoints ();
		else
			createRay ();
			
		_algoReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_algoReady) 
		{
			getPositionMouseAndPlayer ();
			if (true)
			{
				if (optimiseAlgo)
					getUniqueAngles();
				
				else
					updateRay();
				
				castRays();
				drawLine ();
			 }
		}
	}
	#endregion

	#region create
	void createSegment()
	{
		segments = new Segment[nbSegments];	

		for (int i = 0; i < nbSegments ; i++) 
		{
			Segment seg = new Segment();
			segments[i] = seg.randomSegment(distanceMaxWall);
		}
	}

	void createPolygone()
	{

	}

	private void createRay()
	{
		float angle = 360.0f / nbRay;
		
		rays = new Vector3[nbRay];
		angles = new float[nbRay];
		
		for (int i = 0; i < angles.Length; i++)
		{
			angles[i] = angle * i;
		}
		
		for (int i = 0; i < nbRay; i++)
		{
			rays[i].x = playerPosition.x + raysWidth * Mathf.Cos(angles[i]); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + raysWidth * Mathf.Sin(angles[i]); 
		}
		 
	}
	#endregion
	 
	//This method return an intersection point if the ray cut the segment.
	Intersection startIntersecParametrique (Segment ray, Segment seg)
	{
		
		
		Vector2 I= new Vector2 ();
		Vector2	J = new Vector2 ();
		
		float m, k;
		
		I = transformPointsToVector (ray.pointA, ray.pointB);
		J = transformPointsToVector(seg.pointA, seg.pointB);
		
		m = -(-I.x * ray.pointA.z + I.x * seg.pointA.z + I.y * ray.pointA.x - I.y * seg.pointA.x) / (I.x * J.y - I.y * J.x);
		k = -(ray.pointA.x * J.y - seg.pointA.x * J.y - J.x * ray.pointA.z + J.x * seg.pointA.z) / (I.x * J.y - I.y * J.x);
		
		if (I.x * J.y - I.y * J.x == 0) 
		{
			return null;
		}
		
		if (m > 0 && m < 1 && k > 0 && k < 1) 
		{
			pointIntersection.x = ray.pointA.x + k * I.x;
			pointIntersection.z =  ray.pointA.z + k * I.y;
			return new Intersection( new Segment(ray.pointA, pointIntersection), k);
			
			
		}
		return null;
	}
	

	//Calcul intersection with cartesiennes method
	void startIntersecCartesiennes(Segment seg)
	{
		float pente1 = slope (playerPosition, mousePosition);
		float pente2 = slope (seg.pointA, seg.pointB);
		float constante1 = constante (playerPosition, pente1);
		float constante2 = constante (seg.pointA, pente2);
		Vector3 intersec = new Vector3 ();
		//y = ax+b

		if (pente1 != pente2)
		{
			pointIntersection.x = (constante2 - constante1) / (pente1  - pente2);
			pointIntersection.z =  pente1 * intersec.x + constante1;

			if (intersec.x >= Mathf.Min(seg.pointA.x, seg.pointB.x) && intersec.x <= Mathf.Max(seg.pointA.x, seg.pointB.x))
			{
				colorSegement = Color.red;
			}
			else
				colorSegement = Color.yellow;
		}
		

	}

	// Cut a segment 
	// origin point = player position 
	// End point = player.pos.x + rayon * angle 
	// Angle's calculate according to the mouse position
	// Return new end po
	private Vector3 cutSegment(Vector3 endPoint)
	{	
	
		Vector3 ret = new Vector3 ();

		//width player to mouse 
		Vector3 newVec = new Vector3 ();
		newVec.x = endPoint.x - playerPosition.x;
		newVec.y = 0;
		newVec.z = endPoint.z - playerPosition.z;

		float angle = Mathf.Atan2(newVec.z, newVec.x);
	
		ret.x = playerPosition.x + raysWidth * Mathf.Cos(angle);
		ret.y = 0;
		ret.z = playerPosition.z + raysWidth * Mathf.Sin(angle);
		return ret;
	}

	
	#region Optimize light algo
	private List <Vector2> _uniquePoints = new List<Vector2> ();
	private List <float> _uniqueAngles = new List<float> ();
	//FIXME CLEAR DES LISTES 

	//Get all unique points 
	private void getUniquePoints()
	{
		for(int i = 0; i < segments.Length; i++) 
		{
			//TODO tester si le point existe déjà dans la list (Contains)
			_uniquePoints.Add(new Vector2(segments[i].pointA.x, segments[i].pointA.z));
			_uniquePoints.Add(new Vector2(segments[i].pointB.x, segments[i].pointB.z));
		}
	}

	//Get all unique angles 
	private void getUniqueAngles()
	{
		float angle;
		_uniqueAngles.Clear();
		
		foreach(Vector2 pt in _uniquePoints)
		{
			angle = Mathf.Atan2((pt.y - playerPosition.z), (pt.x - playerPosition.x));
			
			//+- 0.00001 two rays  are needed to hit the wall(s) behind any given segment corner 
			_uniqueAngles.Add(angle - 0.00001f);
			_uniqueAngles.Add(angle);
			_uniqueAngles.Add(angle - 0.00001f);	
		}
	}
	#endregion

	#region cast rays
	List <Intersection>  intersctionList = new List<Intersection> ();
	List <int> testIndice = new List<int> ();
	
	private void castRays()
	{
	
		intersctionList.Clear();
		testIndice.Clear();
		
		Segment ray = new Segment();
		Intersection closestIntersec = new Intersection ();
		Intersection intersec = new Intersection ();
		
		// cast X rays 
		// Cast a ray every ( 360/nbRays ) degree
		
		if (optimiseAlgo) 
		{
			foreach (float a in _uniqueAngles)
			{
				//create ray
				ray.pointA = playerPosition;

//RADIAN ???????????
				ray.pointB.x = playerPosition.x + raysWidth * Mathf.Cos(a) ;
				ray.pointB.y = 0;
				ray.pointB.z = playerPosition.z + raysWidth * Mathf.Sin(a);
				
				
				//Find closeset intersection
				closestIntersec = null;
				
				for (int j = 0; j < segments.Length; j++) 
				{
					intersec = startIntersecParametrique (ray, segments [j]);
					
					if (intersec == null) 
					{
						continue;
					}
					if (closestIntersec == null || intersec.distance < closestIntersec.distance)
					{
						//testIndice.Add (i);
						closestIntersec = intersec;
					}
				}
				
				if (closestIntersec != null)
					intersctionList.Add (closestIntersec);
			}
		} 
		
		else
		{
			for (int i = 0; i < rays.Length; i++) 
			{
				ray.pointA = playerPosition;
				ray.pointB = rays [i];
				//Find closeset intersection
				closestIntersec = null;
				
				for (int j = 0; j < segments.Length; j++) 
				{
					intersec = startIntersecParametrique (ray, segments [j]);
					
					if (intersec == null) 
					{
						continue;
					}
					if (closestIntersec == null || intersec.distance < closestIntersec.distance)
					{
						testIndice.Add (i);
						closestIntersec = intersec;
					}
				}
				
				if (closestIntersec != null)
					intersctionList.Add (closestIntersec);
			}
			
		}
		
	}
	#endregion
	
	#region Draw
	private void drawLine()
	{
		Vector3 endPoint = cutSegment(mousePosition);
		
		//Draw segments
		if (nbSegments >= 1)
			for (int i = 0; i < nbSegments; i++)
				Debug.DrawLine(	segments[i].pointA, segments[i].pointB, Color.green, 0F, false);

		Debug.DrawLine(playerPosition, endPoint, Color.blue, 0f,false);

		//Draw rays with intersection RED
		foreach (Intersection inter in intersctionList)
		{
			if (inter != null)
				Debug.DrawLine(	playerPosition, inter.segment.pointB, Color.red, 0F, false);
		}
		
		//Draw rays without intersection WHITE
		if (!optimiseAlgo)
		{
			for (int i = 0; i < rays.Length; i++) 
			{	
				if (! testIndice.Contains(i))
					Debug.DrawLine (playerPosition, rays [i], Color.white, 0F, false);
			}
		}
	}
	#endregion

	#region Update
	private void updateRay()
	{
		for (int i = 0; i < nbRay; i++)
		{
			rays[i].x = playerPosition.x + raysWidth * Mathf.Cos(angles[i] * Mathf.PI / 180 ); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + raysWidth * Mathf.Sin(angles[i] * Mathf.PI / 180); 
		}
	}

	private void getPositionMouseAndPlayer()
	{
		if (true) 
		{
			RaycastHit hit;
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			
			playerMove = false;
			if (playerPosition.x != this.gameObject.transform.position.x || playerPosition.z != this.gameObject.transform.position.z)
			{
				playerPosition.x = this.gameObject.transform.position.x;
				playerPosition.z = this.gameObject.transform.position.z;
				playerMove = true;
			}
			if (mousePosition.x != hit.point.x || mousePosition.z != hit.point.z)
			{
				mousePosition.x = hit.point.x;
				mousePosition.z = hit.point.z;
				playerMove = true;
			}
		}
		
	}
	#endregion
	
	#region Utils
	
	//Return a new Vector2
	private Vector2  transformPointsToVector (Vector3 p1, Vector3 p2)
	{
		return new Vector2 (p2.x - p1.x , p2.z - p1.z); 
	}	
	
	// Calcul the slope of the vectore
	// Para : 2 points of the segment
	// Return : Slope (float)
	float slope(Vector3 pA, Vector3 pB)
	{
		//Calcul de la pente
		float pente =  (pB.z - pA.z )/ (pB.x - pA.x);
		return pente;
	}
	
	//para : One point and slope
	float constante(Vector3 pA, float pente)
	{
		//y = ax + b
		float constanteB = pA.z - pA.x * pente;
		return constanteB;
	}
	
	//origin is the player
	Vector3 getPointWithAngle ()
	{
		Vector3 pt = new Vector3();
		return pt;
	}
	#endregion

}
