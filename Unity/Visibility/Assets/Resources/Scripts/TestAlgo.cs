using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


//Y = 0  2D
//Axes : X, Z


public class TestAlgo : MonoBehaviour 
{
	#region variables

	#region public
	public bool IntersectionParametrique;
	public int nbSegments;
	public int rayonWidth;
	public int distanceMaxWall = 20;
<<<<<<< HEAD
	public int nbRay;
	#endregion
=======
	public bool optimiseAlgo;

	private bool _algoReady = false;
>>>>>>> c30843089f68c491ba4cc67e12cfef77e706041b
	
	
	private bool playerMove = false;
	private bool _algoReady = false;

	private Vector3 playerPosition;
	private Vector3 pointIntersection;
	private Vector3 mousePosition;

	private Vector3[] rays;
	private Vector3[] endPoints;

	private Segment [] segments;
	private float [] angles;
		
	#region GUI
	private List <String> fields = new List<String>();
	private List <String> textFields = new List<String>();
	private int castFields ;
	private int widthFieldGUI = 60;
	private int heightFieldGUI = 20;
	private int topY = 20;
	private int dY = 40;
	private int LeftX = 20 ;
	private Color colorSegement = Color.red;
	#endregion

	#endregion

	#region Unity function
	void Start () 
	{
<<<<<<< HEAD
		//GUI
		textFields.Add ("Segments");
		textFields.Add ("Width seg");
		textFields.Add ("Wdth rayon");
		textFields.Add ("NB rays");

		fields.Add ("0");
		fields.Add ("1");
		fields.Add ("2");
		fields.Add ("3");

		init ();
=======
		createSegment ();
		getUniquePoints ();
		createRay ();
		_algoReady = true;
>>>>>>> c30843089f68c491ba4cc67e12cfef77e706041b
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

	#endregion

	private void init(bool changeOnGui = false)
	{
		_algoReady = false;
		if (changeOnGui)
		{
			nbSegments = castCharToInt(fields[0]);
			distanceMaxWall  = castCharToInt(fields[1]);
			rayonWidth  = castCharToInt(fields[2]);
			nbRay = castCharToInt(fields[3]);
		}
		createSegment ();
		createRay ();
		getUniquePoints ();
		_algoReady = true;
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
		playerMove = false;

		if (true) 
		{
			RaycastHit hit;
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);

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
	
		ret.x = playerPosition.x + rayonWidth * Mathf.Cos(angle);
		ret.y = 0;
		ret.z = playerPosition.z + rayonWidth * Mathf.Sin(angle);
		return ret;
	}

<<<<<<< HEAD

	private List <Vector3> getUniquePoints()
	{
		List <Vector3> points = new List <Vector3>(); 
		for (int i = 0; i < segments.Length; i++) 
		{
			if (! ( points.Contains(segments[i].pointA) ) )
				points.Add(segments[i].pointA);
			if (! ( points.Contains(segments[i].pointB) ) )
				points.Add(segments[i].pointB);
		}
		getAngles (points);
		return points;
=======
	List <Vector2> uniquePoints = new List<Vector2> ();

	private void getUniquePoints()
	{
		
		List <Segment> points = new List<Segment> ();

		for(int i = 0; i < segments.Length; i++) 
		{
			uniquePoints.Add(new Vector2(segments[i].pointA.x, segments[i].pointA.z));
			uniquePoints.Add(new Vector2(segments[i].pointB.x, segments[i].pointB.z));
		}
>>>>>>> c30843089f68c491ba4cc67e12cfef77e706041b
	}
	
	private List <double>  getAngles(List <Vector3> pts)
	{
<<<<<<< HEAD
		List <double> angles = new List<double> ();
		foreach (Vector3 vec in pts) 
		{
			angles.Add(Math.Atan2(vec.z - rayonWidth, vec.x - rayonWidth));
		}

		return angles;
=======

>>>>>>> c30843089f68c491ba4cc67e12cfef77e706041b
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
<<<<<<< HEAD
					continue;
				}
				if (closestIntersec == null || intersec.distance < closestIntersec.distance)
=======
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
>>>>>>> c30843089f68c491ba4cc67e12cfef77e706041b
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
			rays[i].x = playerPosition.x + rayonWidth * Mathf.Cos(angles[i] * Mathf.PI / 180 ); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + rayonWidth * Mathf.Sin(angles[i] * Mathf.PI / 180); 
		}
	}


	public bool optimizeAlgo = false;

	private void createRay()
	{
		if (optimizeAlgo) 
		{
			//Ray -> Point segment
			int cpt = 0;
			List <double> anglesList = new List<double> ();
			anglesList = getAngles (getUniquePoints());
			rays = new Vector3[lengthList(anglesList)];
			angles = new float[rays.Length];
			
			foreach (double ang in anglesList) 
			{
				double ang2 = ang * 180 / Mathf.PI;
				float dx = Mathf.Cos((float)ang2);
				float dz = Mathf.Sin((float)ang2);
				angles[cpt] = (float)ang;
				rays[cpt] = new Vector3(playerPosition.x + dx, 0.0f ,playerPosition.z + dz);
			}
		}
		else 
		{	
			//Send 360 rays
			float angle = 360.0f / nbRay;
			rays = new Vector3[nbRay];
			angles = new float[nbRay];
			for (int i = 0; i < angles.Length; i++)
			{
				angles[i] = angle * i;
			}

		}

		for (int i = 0; i < nbRay; i++)
		{
			rays[i].x = playerPosition.x + rayonWidth * Mathf.Cos(angles[i]); 
			rays[i].y = 0;
			rays[i].z = playerPosition.z + rayonWidth * Mathf.Sin(angles[i]); 
		}
		
	}

	private Vector2  transformPointsToVector (Vector3 p1, Vector3 p2)
	{
		return new Vector2 (p2.x - p1.x , p2.z - p1.z); 
	}	

	void OnGUI()
	{
		int cpt = 0;
		foreach (string str in fields) 
		{
			string strTemp = str;
			GUI.Label (new Rect(LeftX, cpt * dY, widthFieldGUI, heightFieldGUI), textFields[cpt]);
			strTemp = GUI.TextField (new Rect (LeftX + 60, cpt * dY, widthFieldGUI, heightFieldGUI), strTemp);
			fields[cpt] = strTemp;
			cpt++;
		}

		if (GUI.Button (new Rect (LeftX, cpt * dY, 50, 20), "okay")) 
		{
			init (true);
			GUI.FocusControl("nothing");
		}

	}

	#region utils

	private int castCharToInt(string str)
	{
		//Check all char
		for (int i = 0; i < str.Length; i++)
		{
			if ((int)str[i] < 47 && (int)str[i] > 	57)
			{
				return 0;
			}
		}
		return Int32.Parse(str);
	}

	private int lengthList <T>(List<T> l)
	{
		int cpt = 0;
		foreach (T o in l)
			if (o != null)
				cpt++;
		return cpt;
	}

	#endregion
}
