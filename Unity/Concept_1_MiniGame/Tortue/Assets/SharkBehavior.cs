using UnityEngine;
using System.Collections;

public class SharkBehavior : MonoBehaviour 
{
	public GameObject player;
	
	// Update is called once per frame
	void Update () 
	{
		
		this.transform.position = new Vector3 (this.transform.position.x, player.transform.position.y, -2f);
	
	}
}
