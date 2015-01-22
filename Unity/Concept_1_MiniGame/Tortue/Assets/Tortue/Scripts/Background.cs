using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{


	public float scrollSpeed;
	private float _offset;
	
	
	private Vector3 startPosition;
	
	private GameObject _currentBackground;
	private float _tileSizeX;
	

	private float _newPosition;
	private float _newPositionRight;
	
	
	private Transform _child;
	private float reset;
	
	#region Unity Function
	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
		_newPosition = startPosition.x;
		_tileSizeX = this.transform.localScale.x;
		reset = startPosition.x - 2 * _tileSizeX;
						

	}
	
	// Update is called once per frame
	void Update () 
	{	
		
		_offset = Mathf.Repeat(Time.time * (scrollSpeed + GameManager.instance.speed), 1 );
		if(_offset < 0)
			Debug.Log( "kjnbkjivgbndfkjbvdfkljb vdfkjbvfdkj ");
		renderer.sharedMaterial.SetTextureOffset("_MainTex", new Vector2 (_offset, 0));
	
	//	transform.position = startPosition + Vector3.left * newPosition;
		/*
		if(reset <= transform.position.x) 
		{
			transform.position = (new Vector3(startPosition.x, 0, 0));
		}
		transform.position += new Vector3((Time.deltaTime *(scrollSpeed + GameManager.instance.speed) ), 0, 0);
		
		_newPosition -= Time.deltaTime * (scrollSpeed + GameManager.instance.speed);
		_newPositionRight = _newPosition +_tileSizeX;
	//	Debug.Log(startPosition.x -  _tileSizeX  +" "+ _newPosition+ " "+this.gameObject.name);
		if ( _newPosition  < startPosition.x - _tileSizeX )
		{
			_newPosition = startPosition.x + _tileSizeX;	
//			_currentBackground = rightBackground;
//			rightBackground = this;
		}
		else
		{
			transform.position = startPosition + Vector3.left * -(_newPosition);
			rightBackground.transform.position = startPosition + Vector3.left * - (_newPositionRight);
		}
		*/
			
	}
	#endregion
	
	
}
