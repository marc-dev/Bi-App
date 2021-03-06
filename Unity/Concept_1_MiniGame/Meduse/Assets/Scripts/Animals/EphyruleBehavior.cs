using UnityEngine;
using System.Collections;

public class EphyruleBehavior : MonoBehaviour 
{
	public int neededPlacton;
	public int speed;
	public GameObject jellyFish;

	private int _nbPlacton;
	private bool _moveTo;
	private Vector2 _destinationPt;
	private Transform _transform;
	private bool _waitToTransform;

	#region Unity method	
	

	
	void Start ()
	{
		_transform = this.gameObject.transform;
		this.rigidbody2D.velocity = Vector2.up;
		_nbPlacton = 0;
		_waitToTransform = false;
		
	}

	// Update is called once per frame
	void Update () 
	{
		//Move to destination point
		if (_moveTo)
		{
			this.transform.position = Vector2.MoveTowards(_transform.position, _destinationPt, speed * Time.deltaTime);
		} 

		//Destination reached
		if (_transform.position.x == _destinationPt.x && _transform.position.y == _destinationPt.y )
		{
			_moveTo = false;
			this.rigidbody2D.velocity = Vector2.up;
		}

		//If Ephyrule eat enough placton & this position is on top of the screen he turn to jellyFish
		if (_waitToTransform && this.transform.position.y > 4.5f)
		{
			transformToJellyFish();
			GameManager.instance.gameBehavior.updateScore(300);
		}
	}

	void OnMouseDown()
	{
		Debug.Log("Mouse Down");
		select();
		//GameManager.instance.gameBehavior.clickOnEphyrule(this);
	}
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == LayerConst.PLANCTON)
		{
			eat(col.gameObject.GetComponent<PlanctonBehavior>());
		}
		
		if (col.gameObject.tag == LayerConst.FISH)
		{
			die();
		}
		
	}
	
	#endregion

	#region my function
	public void select()
	{
		if (GameManager.instance.gameBehavior._selectedEphyrule != null)
			GameManager.instance.gameBehavior._selectedEphyrule.unSelect();
		GameManager.instance.gameBehavior._selectedEphyrule = this;

		if (_waitToTransform)
			this.gameObject.renderer.material.color = Color.blue;

		else
			this.gameObject.renderer.material.color = new Color(0.6f, 0.8f, 0.2f);
	}

	public void unSelect()
	{
		this.gameObject.renderer.material.color = Color.white;
	}	

	public void eat(PlanctonBehavior plancton)
	{
		
		GameManager.instance.gameBehavior.updateScore(100);
		plancton.eaten();

		if (_nbPlacton >= neededPlacton -1)
		{
			this.rigidbody2D.renderer.material.color = Color.blue;
			_waitToTransform = true;
		}
		else if (_waitToTransform == false)
		{
			_nbPlacton ++;
			this.rigidbody2D.transform.localScale *= 1.3f;
		}
	//	_animator.SetBool("willEat", true);
		//this.animation = _animator.animation.GetClip("FishEat");
	}
	
	public void die()
	{
		Destroy(this.gameObject);
	}

	public void moveTowards(Vector2 pt)
	{
		_destinationPt = pt;
		_moveTo = true;
	}

	private void transformToJellyFish()
	{
		Instantiate(jellyFish, this.transform.position, Quaternion.identity);
		die();
	}

	#endregion 
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                