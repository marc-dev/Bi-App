using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishBehavior : MonoBehaviour 
{
	public int speed;
	public Image damage; 
	
	private bool _direction;
	
	private Animator _animator;
	private Animation  _swim, _eat;
	private Transform _transform;
	private GameObject _currentEphyrule;
	
	#region Unity method	
	

	void Start ()
	{
		_transform = this.gameObject.transform;
		_animator = this.gameObject.GetComponent<Animator>();
	
		direction();
	
	}
	
	private void direction()
	{
		//Choice animation
		if (_transform.position.x > 0)
		{
			_animator.SetBool("ToTheLeft" , true);
			speed *= -1;
		}
		else 
		{
			_animator.SetBool("ToTheLeft", false);
		}
	}
	// Update is called once per frame
	void Update () 
	{	
		_transform.position = new Vector3(_transform.position.x + speed * Time.deltaTime , _transform.position.y, _transform.position.z);
		
		
		//+ 2 marge instantiate
		if (this.gameObject.transform.position.x   < GameManager.instance.botRight.x + 2 || this.gameObject.transform.position.x  > GameManager.instance.upperLeft.x - 2
		    || this.gameObject.transform.position.y < GameManager.instance.botRight.y - 2|| this.gameObject.transform.position.y >  GameManager.instance.upperLeft.y + 2)
		{
		//	die();
		}
	}

	
    void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == LayerConst.Ephyrule)
		{
			eat();
		}
		
		if (col.gameObject.tag == LayerConst.BOUNDERY)
		{
			die();
		}
		
	}
	
	#endregion
	

	public void eat()
	{
		_animator.SetBool("willEat", true);
		
		// ... set the colour of the damageImage to the flash colour.
		GameManager.instance.gameBehavior.damageImage.color = Color.red;
		//this.animation = _animator.animation.GetClip("FishEat");
		
		
	}
	
	public void die()
	{
		Destroy(this.gameObject);
	}
	
				
	public void endEat()
	{
		_animator.SetBool("willEat", false);
		//Destroy(_currentEphyrule);
		//this.animation.GetClip ("willEat", false);
		
	}
	
	
}
