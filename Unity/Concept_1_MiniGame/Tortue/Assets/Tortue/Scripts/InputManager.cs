using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public GameObject player;
	private PlayerManager _playerManager;
	private Vector3 _startPositionDrag;
	private Ray _ray; 
	private int _distanceCamToObject;
	
	private float deltaDrag;
	
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
			
		deltaDrag = 1.5f;
		_distanceCamToObject = - 10;
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
				_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				//executes this code for cuurent touch (i) on screen
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					_startPositionDrag = _ray.GetPoint(-10f);
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended)
				{
					if ( (_ray.GetPoint(_distanceCamToObject)).x - _startPositionDrag.x  > deltaDrag)
					{
						horizontalDrag();
					}	
					else if ((_ray.GetPoint(_distanceCamToObject)).y - _startPositionDrag.y  < -deltaDrag)
					{
						verticalDrag();
					}
					else
						moveUp();
					
				}
			}
		}
	}
	
	private void moveUp()
	{
		_playerManager.moveplayer(Vector3.up);
	}
	
	private void horizontalDrag()
	{
		_playerManager.hurryUp();
		GameManager.instance.boostReady = false;
	} 
	
	private void verticalDrag()
	{
		if (_playerManager.transform.position.y > - 9.0f )
		{
			_playerManager.moveplayer(Vector3.down);
		}
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
			_playerManager.moveplayer(Vector3.down);
		}
		
		
		if (Input.GetKey(KeyCode.Space) )//&& !boostActive)
		{
			GameManager.instance.playing = true;
			
		}
	}
}
