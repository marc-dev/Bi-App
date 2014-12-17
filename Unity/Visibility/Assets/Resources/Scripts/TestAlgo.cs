using UnityEngine;
using System.Collections;
using System;

//Y = 0 cause we are in 2D
//Axes : X, Z


public class TestAlgo : MonoBehaviour 
{
	Vector3 a;
	Vector3 pointIntersection;
	Vector3 mousePosition;
	
	private bool playerMove;
	private Vector3[] endPoints;

	//Vector3 c;
	//Vector3 d;

	Segment [] segments;
	Color colorSegement = Color.red;


	public bool IntersectionParametrique;
	public int nbSegments;
	public int rayon;

	private bool _algoReady = false;

	// Use this for initialization
	void Start () 
	{
		a.x = 1;
		a.z = 2; 

		pointIntersection.x = 5;
		pointIntersection.z = 7;

	
		createSegment ();
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
		//2 points per segment
		segments = new Segment[nbSegments];

		for (int i = 0; i < nbSegments ; i++) 
		{
			Segment seg = new Segment();
			segments[i] = seg.randomSegment(20);

		}
	}

	void getAllSegment()
	{

	}

	//para : 2 points of the segment
	float coefDir(Vector3 pA, Vector3 pB)
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
		float pente1 = coefDir (a, mousePosition);
		float pente2 = coefDir (seg.pointA, seg.pointB);
		float constante1 = constante (a, pente1);
		float constante2 = constante (seg.pointA, pente2);
		Vector3 intersec = new Vector3 ();
		//y = ax+b

		if (pente1 != pente2)
		{
			//Pente non parallèle

			//Calcul du point d'intersection
			//a* x + b -(a2 * x + b2) = 0;

			//X = point d'intersection

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

	bool startIntersecParametrique (Segment seg)
	{

		Vector2 I= new Vector2 ();
		Vector2	J = new Vector2 ();
	
		float m, k;

		I = transformPointsToVector (a, mousePosition);
		J = transformPointsToVector(seg.pointA, seg.pointB);

		m = -(-I.x * a.z + I.x * seg.pointA.z + I.y * a.x - I.y * seg.pointA.x) / (I.x * J.y - I.y * J.x);
		k = -(a.x * J.y - seg.pointA.x * J.y - J.x * a.z + J.x * seg.pointA.z) / (I.x * J.y - I.y * J.x);

		if (I.x * J.y - I.y * J.x == 0) 
		{
			Debug.Log("Parallel case");
			return false;
		}

		if (m > 0 && m < 1 && k > 0 && k < 1) 
		{
			pointIntersection.x = a.x + k * I.x;
			pointIntersection.z =  a.z + k * I.y;
			colorSegement = Color.red;
			return true;

		}
		else
			colorSegement = Color.yellow;

		return false;

	}


	private void getPositionMouseAndPlayer()
	{
		if (true) 
		{
			RaycastHit hit;
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);

			playerMove = false;
			if (a.x != this.gameObject.transform.position.x || a.z != this.gameObject.transform.position.z)
		 	{
				a.x = this.gameObject.transform.position.x;
			    a.z = this.gameObject.transform.position.z;
				playerMove = true;
			}
			if (mousePosition.x != hit.point.x || mousePosition.z != hit.point.z)
			{
				mousePosition.x = hit.point.x;
				mousePosition.z = hit.point.z;
				playerMove = true;
			}

			if (playerMove)
			    lookingForIntersection();
		}

	}

	private void lookingForIntersection()
	{

		for (int i = 0; i < nbSegments; i++)
		{
			if (segments[i] != null)
			{
				if (IntersectionParametrique)
				{
					if (startIntersecParametrique (segments[i]))
					{
						break;
					}
					else
					{
						pointIntersection = mousePosition;
					}
				}
				else
					startIntersecCartesiennes (segments[i]);
			}
			else
				Debug.Log("Segment null "+i);
		}
	}

	private Vector3 cutSegment()
	{
		float slop = coefDir (a, mousePosition);
		Vector3 ret = new Vector3 ();

		//width player to mouse 
		Vector3 newVec = new Vector3 ();
		newVec.x = mousePosition.x - a.x;
		newVec.y = 0;
		newVec.z = mousePosition.z - a.z;

		float angle = Mathf.Atan2(newVec.z, newVec.x);
	
		ret.x = a.x + rayon * Mathf.Cos(angle);
		ret.y = 0;
		ret.z = a.z + rayon * Mathf.Sin(angle);
		return ret;
	}



	private void drawLine()
	{
	//	Debug.DrawLine(	a, b, Color.yellow, 0F, false);
	//	Debug.DrawLine(	c, d, Color.green, 0F, false);

		Vector3 endPoint = cutSegment();//pointIntersection;//

		Debug.DrawLine(a, endPoint, colorSegement, 0f,false);

		if (nbSegments >= 1)
			for (int i = 0; i < nbSegments; i++)
				Debug.DrawLine(	segments[i].pointA, segments[i].pointB, Color.green, 0F, false);
				
		if (nbRay >= 1)
			for (int i = 0; i < nbRay; i++)
				Debug.DrawLine(	a, rays[i], Color.white, 0F, false);


	}

	private void updateRay()
	{
		for (int i = 0; i < nbRay; i++)
		{
			rays[i].x = a.x + rayon * Mathf.Cos(angles[i] * Mathf.PI / 180 ); 
			rays[i].y = 0;
			rays[i].z = a.z + rayon * Mathf.Sin(angles[i] * Mathf.PI / 180); 
		}
	}

	public int nbRay;
	private Vector3[] rays;
	float [] angles;

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
			rays[i].x = a.x + rayon * Mathf.Cos(angles[i]); 
			rays[i].y = 0;
			rays[i].z = a.z + rayon * Mathf.Sin(angles[i]); 
		}
		
	}


	#region utils
	//Faire une méthode générique
	private Vector2  transformPointsToVector (Vector3 p1, Vector3 p2)
	{
		return new Vector2 (p2.x - p1.x , p2.z - p1.z); 
	}	


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

	#endregion
}
