using UnityEngine;
using System.Collections;

public class Intersection
{
	public Segment segment;
	public float distance;

	public Intersection()
	{
		segment = new Segment(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
		distance = 0;
	}

	public Intersection(Segment seg, float dist)
	{
		segment = seg;
		distance = dist;
	}

}
