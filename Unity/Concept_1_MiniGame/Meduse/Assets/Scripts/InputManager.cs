using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour 
{
	public int timeAnimation;
	public int speed;
	
	
	private bool _selectOk;
	private Vector2 _position;
	private Vector2 _initPosition;
	private Vector2 _coeff;
	
	private bool _moveGameobject;
		
	private int _tick;
	
	private float myTime;
	
	// Use this for initialization
	void Awake () 
	{
		_position = new Vector2(0, 0);
		_initPosition = this.gameObject.transform.position;
		_coeff = new Vector2(0, 0);
		myTime = Time.time;
	}
	
	void Start()
	{
		myTime = Time.time;
		Debug.Log("test time "+ Time.time);
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		_tick ++;
		
		
		//Debug.Log(myTime);
		
		if (Input.touches.Length <= 0)
		{
			//If no touche then execute the code
		}
		
		else if (false)
		{	
			//Loop through all the touches on screnn
			for (int i = 0; i < Input.touchCount; i++)
			{
				//executes this code for cuurent touch (i) on screen
			
					if (Input.GetTouch(i).phase == TouchPhase.Began)
					{	
						_position = Input.GetTouch(i).position;
						Debug.Log("init pos" + _initPosition + " endPosition " + _position );
						getPosition();
						
						
						
					}
					if (Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						Debug.Log("no method");//_playerManager.onTouch(Vector3.up);
						move ();
						_selectOk = true;
						
					}			
			}
		}
		
		if (_moveGameobject)
		{
			this.gameObject.GetComponent<RectTransform>().position = new Vector3(this.gameObject.GetComponent<RectTransform>().position.x + speed, this.gameObject.GetComponent<RectTransform>().position.y + speed, this.gameObject.GetComponent<RectTransform>().position.z);
			
			if (_tick > timeAnimation)
			{
				_tick = 0;
				_moveGameobject = false;
				speed *= -1;
			}
		}
	
	}
	
	
	public void getPosition()
	{
		if (_coeff.x == 0 && _coeff.y ==0)
		{
			Debug.Log(_coeff.x +" + " + _position.y);
			_coeff.x = _position.x / _initPosition.x ;
			_coeff.y = _position.y / _initPosition.y ;
			Debug.Log(_coeff);
		}
		else
		{
			_position.x = _position.x / _coeff.x;
			_position.y = _position.y / _coeff.y;
			Debug.Log(_coeff);
		}
	
	}
	
	
	public void changeColorBt()
	{
	
		this.gameObject.GetComponent<RawImage>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
	
	void OnMouseDrag()
	{
		Debug.Log("dragr"+Time.time);
	} 
	
	public void move ()
	{
		_moveGameobject = true;
	
		
	}
	
	
	private void controllerDesktop()
	{
		//Controll on click
	}
}
