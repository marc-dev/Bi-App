using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public GameObject player;
	private PlayerManager _playerManager;
	
	// Use this for initialization
	void Awake () 
	{
		if (player == null)
		{
			Debug.Log(" Attach player GO to InputManager " + this);
			Application.Quit();
		}
		
		else
			_playerManager = player.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		controllerDesktop();
		
		if (Input.touches.Length <= 0)
		{
			//If no touche then execute the code
		}
		
		else
		{	
			//Loop through all the touches on screnn
			for (int i = 0; i < Input.touchCount; i++)
			{
				//executes this code for cuurent touch (i) on screen
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended)
				{
					_playerManager.moveplayer(Vector3.up);
					
				}
				
				
				
			}
		}
	
	}
	
	void OnMouseDrag()
	{
		Debug.Log("dragr"+Time.time);
	} 
	
	
	
	
	private void controllerDesktop()
	{
		//Boost accelaration
		if ( (Input.GetKeyUp(KeyCode.RightArrow) ) && GameManager.instance.boostReady)
		{
			_playerManager.hurryUp();
			GameManager.instance.boostReady = false;
			
		}
		//Boost accelaration
		if ( (Input.GetKeyUp(KeyCode.LeftArrow) ))
		{
			
			_playerManager.slowDown();
			
		}
		
		//Swim Up
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
		{
			_playerManager.moveplayer(Vector3.up);
			
		}
		
		//Swim down
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			_playerManager.moveplayer(Vector3.down);//this.transform.position += Vector3.down * Time.deltaTime * speed;
		}
		
		
		if (Input.GetKey(KeyCode.Space) )//&& !boostActive)
		{
			GameManager.instance.playing = true;
			
		}
	}
}
