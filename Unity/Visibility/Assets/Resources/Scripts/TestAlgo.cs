using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


//Y = 0 cause we are in 2D
//Axes : X, Z


public class TestAlgo : MonoBehaviour 
{
	Vector3 playerPosition;
	Vector3 pointIntersection;
	Vector3 mousePosition;
	
	private bool playerMove;
	private Vector3[] endPoints;

	Segment [] segments;
	Color colorSegement = Color.red;


	public bool IntersectionParametrique;
	public int nbSegments;
	public int rayon;
	public int distanceMaxWall = 20;
	public bool optimiseAlgo;

	private bool _algoReady = false;
	
	
	public int nbRay;
	private Vector3[] rays;
	float [] angles;



	// Use this for initialization
	void Start () 
	{
		createSegment ();
		getUniquePoints ();
		createRay ();
		_algoReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_algoReady) 
		{
			getPositionMouseAndPlayer ();
			updateRay();
			drawLine ();
		}
	}

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

	//para : 2 points of the segment
	float slop(Vector3 pA, Vector3 pB)
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

	//Calcul intersection with cartesiennes method
	void startIntersecCartesiennes(Segment seg)
	{
		float pente1 = slop (playerPosition, mousePosition);
		float pente2 = slop (seg.pointA, seg.pointB);
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

			//if (playerMove)
			  //  lookingForIntersection();
		}

	}

	private Vector3 cutSegment()
	{
		Vector3 ret = new Vector3 ();

		//width player to mouse 
		Vector3 newVec = new Vector3 ();
		newVec.x = mousePosition.x - playerPosition.x;
		newVec.y = 0;
		newVec.z = mousePosition.z - playerPosition.z;

		float angle = Mathf.Atan2(newVec.z, newVec.x);
	
		ret.x = playerPosition.x + rayon * Mathf.Cos(angle);
		ret.y = 0;
		ret.z = playerPosition.z + rayon * Mathf.Sin(angle);
		return ret;
	}

	List <Vector2> uniquePoints = new List<Vector2> ();

	private void getUniquePoints()
	{
		
		List <Segment> points = new List<Segment> ();

		for(int i = 0; i < segments.Length; i++) 
		{
			uniquePoints.Add(new Vector2(segments[i].pointA.x, segments[i].pointA.z));
			uniquePoints.Add(new Vector2(segments[i].pointB.x, segments[i].pointB.z));
		}
	}

	private void getAngles()
	{

	}

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

	

	private void drawLine()
	{
		List <Intersection>  intersctionList = new List<Intersection> ();
		List <int> testIndice = new List<int> ();
		Segment ray = new Segment();
		Intersection closestIntersec = new Intersection ();
		Intersection intersec = new Intersection ();


		if (!optimiseAlgo) 
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
				intersctionList.Add (closestIntersec);
			}

		} 
		else
		{
			float angle ;
			int cpt = 0;
			foreach (Vector2 pts in uniquePoints)
			{
				Vector2 newVec = new Vector3 ();
				newVec.x = pts.x - playerPosition.x;
				newVec.y = pts.y - playerPosition.z;



				angle = Mathf.Atan2(newVec.y, newVec.x);

				ray.pointB.x = playerPosition.x + rayon * Mathf.Cos(angle);
				ray.pointB.y = 0;
				ray.pointB.z = playerPosition.z + rayon * Mathf.Sin(angle);


				ray.pointA = playerPosition;
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
				Debug.Log(closestIntersec);
				intersctionList.Add (closestIntersec);
			}
		}



		Vector3 endPoint = cutSegment();//pointIntersection;//

		//DRAW !!!!!!!!!!!!!!!!

		Debug.DrawLine(playerPosition, endPoint, Color.blue, 0f,false);

		//Draw segments
		if (nbSegments >= 1)
			for (int i = 0; i < nbSegments; i++)
				Debug.DrawLine(	segments[i].pointA, segments[i].pointB, Color.green, 0F, false);

		//Draxw rays
		foreach (Intersection inter in intersctionList)
		{
			if (inter != null)
				Debug.DrawLine(	playerPosition, inter.segment.pointB, Color.red, 0F, false);
		}

		for (int i = 0; i < rays.Length; i++) 
		{	
			if (!testIndice.Contains(i))
				Debug.DrawLine (playerPosition, rays [i], Color.white, 0F, false);
		}

		intersctionList = null;
	}

	private void updateRay()
	{
		for (int i = 0; i < nbRay; i++)
		{
			rays[i].x = playerPosition.x + rayon * Mathf.Cos(angles[i] * Mathf.PI / 180 ); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + rayon * Mathf.Sin(angles[i] * Mathf.PI / 180); 
		}
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
			rays[i].x = playerPosition.x + rayon * Mathf.Cos(angles[i]); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + rayon * Mathf.Sin(angles[i]); 
		}
		
	}

	private Vector2  transformPointsToVector (Vector3 p1, Vector3 p2)
	{
		return new Vector2 (p2.x - p1.x , p2.z - p1.z); 
	}	



}
