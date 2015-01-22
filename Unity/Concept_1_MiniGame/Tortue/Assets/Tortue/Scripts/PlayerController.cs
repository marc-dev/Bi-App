/**
* Project:   BI APP
* Prototype: Mini Game
* Mini Game: Tortue
*
* Date: 13/01/2015
* Author: Marc Viguié 
*
**/




using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private PlayerManager _playerManager;
	
	public int speed;
	public float maxY = 5.6f;
	public float minY = -3.7f;
	
	#region Unity Function
	// Update is called once per frame
	void Update () 
	{
		
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			//A handheld device like mobile phone or a tablet.
			controllerMobilOrTablet();
		}
		
		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			//Desktop or laptop computer.
			controllerDesktop();
		}
		
	}
	
	
	
	#endregion
	
	
	public void init()
	{
		Debug.Log("device "+SystemInfo.deviceType);
		speed = 10; 
		_playerManager = this.GetComponent<PlayerManager>();
	}
	
	//Move the character 
	public void moveplayer(Vector3 direction)
	{
		bool correctMove = true;
	
		
		if (direction == Vector3.up)
			if ( (this.transform.position + Vector3.up * Time.deltaTime * speed).y >= maxY )
				correctMove = false;
		
		
		if(direction == Vector3.down)
			if ( (this.transform.position + Vector3.down * Time.deltaTime * speed).y <= minY )
				correctMove = false;
				
		if (correctMove)
			this.transform.position += direction * Time.deltaTime * speed;
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
			moveplayer(Vector3.up);
			
		}
		
		//Swim down
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			moveplayer(Vector3.down);//this.transform.position += Vector3.down * Time.deltaTime * speed;
		}
		
		
		if (Input.GetKey(KeyCode.Space) )//&& !boostActive)
		{
			GameManager.instance.playing = true;
			
		}
	}
	
	private void controllerMobilOrTablet()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//code
		}
	}
	
	
	
}
