using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int speed = 10;
	Vector2 mousePos;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{    
		//Screen.showCursor = false; 
		//Debug.Log("adsf");
	}
	

	

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//Move left X axis
			this.transform.position += Vector3.left * Time.deltaTime * speed;

		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			//Move left X axis
			this.transform.position += Vector3.right * Time.deltaTime * speed;
			
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			//Move left Z axis
			this.transform.position += Vector3.forward * Time.deltaTime * speed;
			
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			//Move left Z axis
			this.transform.position += Vector3.back * Time.deltaTime * speed;
			
		}


	

	}


}
