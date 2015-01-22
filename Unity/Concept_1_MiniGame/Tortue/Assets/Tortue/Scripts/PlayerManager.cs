using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public float timeEffects = 2.0f;
	public const float SPEED_INIT = 0.5f;
	public GameObject guiObject;

	delegate void MyDelegate ();
	MyDelegate myDelegate;
	
	private PlayerController _controll;
	private GUIManager _gui;
	private const float MAX_SPEED = 0.85f;
	private int testVar;
	
	
	private bool _accelerate = false;
	private bool _slow = false;
	
	
	
	// Jump property 
	private int _tickJump;
	public int timeJump;				// timeJump in Fps
	public float speedJump;				// HeightJump in Pixel
	private bool _jump;
	
	
	#region UnityFunction
	// Use this for initialization
	void Start () 
	{
		init ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		velocity();
		
		if (_jump)
			movementVertical();
			
		else if (this.transform.position.y > -10)
			this.transform.position += Vector3.down * Time.deltaTime;
		
	}
	
	private void movementVertical()
	{	
		_tickJump ++;
		this.transform.position += Vector3.up * Time.deltaTime * speedJump;
		
		if (_tickJump >= timeJump || this.transform.position.y >= 0)
		{
			//End jump
			_tickJump = 0;
			_jump = false;
		}
	}
	
	private void velocity()
	{
		//Boost accelarate active
		if (_accelerate)
		{
			Debug.Log("_accelerate");
			if (GameManager.instance.speed < MAX_SPEED)
				GameManager.instance.speed += 0.01f;
		}
		
		//Slow effect active
		else if (_slow)
		{
			Debug.Log("_slow");
			if (GameManager.instance.speed > SPEED_INIT / 2)
				GameManager.instance.speed -= 0.01f;	
		}
		
		
		//After slow or boost effect, set at a steady speed
		else if (! _slow && !_accelerate && SPEED_INIT != GameManager.instance.speed)
		{
			Debug.Log("steady");
			if ( GameManager.instance.speed < SPEED_INIT)
				GameManager.instance.speed += 0.01f;
			else if (GameManager.instance.speed > SPEED_INIT)
				GameManager.instance.speed -= 0.01f;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) 
	{
	
		if (collision.gameObject.tag == Tags.MEDUSE)
		{
			Destroy(collision.gameObject);
			GameManager.instance.nbMeduse += 1;
			_gui.updateScore(GameManager.instance.nbMeduse);
			_gui.updateGauge();
			
			if (GameManager.instance.nbMeduse > 0 && GameManager.instance.nbMeduse % GameManager.instance.nbMeduseForBoost == 0)
				GameManager.instance.boostReady = true;
		}
		
		if (collision.gameObject.tag == Tags.BORDER)
		{
			Debug.Log("border");
		}
		
	}
	#endregion
	
	#region MyFunction
	private void init()
	{
		GameManager.instance.speed = SPEED_INIT;
		if (_controll == null)
		{
			//Attach PlayerControll script to player
		//	_controll = this.gameObject.AddComponent<PlayerController>();
		//	_controll.init();	
		}
		
		if (null != guiObject) 	
			_gui = guiObject.GetComponent<GUIManager>();
		
	}
	
	
	//Increase (change background velocity) velocity
	public void hurryUp()
	{
		if (!_accelerate)
		{ //Si le joueur n'utilise pas déjà le boost
			_accelerate = true;
			_slow = false;
			myDelegate = slowDown;
			StartCoroutine(waitSecAndCall(timeEffects, true));
			_gui.initValueGauge();
			
		}
	}
	
	
	//Reduce (change background velocity) velocity 
	public void slowDown()
	{
		if (!_slow)
		{	//Si le joueur n'est pas déjà en train de ralentir
			_slow = true;	
			_accelerate = false;
			myDelegate = hurryUp;
			StartCoroutine(waitSecAndCall(timeEffects, false));
		}
	}
	
	
	#region move player
	public void moveplayer(Vector3 direction)
	{
		if (timeJump == 0d)
		{
			Debug.Log("Did you forget to set the timeJump value ???" +this.gameObject.name );
			
		}
/*	
		//bool correctMove = true;
		//If player move is out of boundery -> correctMove = True
		
		
		if (direction == Vector3.up)
		{
			//If no touche then execute the code
			if ( (this.transform.position + Vector3.up * Time.deltaTime * timeJump).y <= - 10 )
				_jump = false;
		}
		
		if(direction == Vector3.down)
		{
			if ( (this.transform.position + Vector3.down * Time.deltaTime * timeJump).y  >= - 0 )
				_jump = false;
		}
		
		if (_jump && false)
			this.transform.position += direction * Time.deltaTime * timeJump;
*/	
		_jump = true;
	}
	#endregion
	
	
	
	/* Secondes : time WaitForSeconds
	** Callback : callback method
	** type : true = accelerate , false = slow TODO change to int value
	**/
	private IEnumerator waitSecAndCall(float secondes, bool type, MyDelegate callback = null)
	{
		
		yield return new WaitForSeconds(secondes);
		Debug.Log("after yield");
		if (type)
			_accelerate = false;
		else
			_slow	= false;
	}
	
	
	
	#endregion
	

	
	
	
	#region utils
	public void msg(string msg)
	{
		Debug.Log("Player manager "+msg);
	}
	#endregion
	
}
