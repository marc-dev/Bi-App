using UnityEngine;
using System.Collections;

public class FishBehavior : MonoBehaviour 
{

	public int speed;
	
	private bool _direction;
	
	private Animator _animator;
	private Animation  _swim, _eat;
	private Transform _transform;
	private GameObject _currentEphyrule;
	
	#region Unity method	
	
	void onAnimatorMove()
	{
	}
	
	void Start ()
	{
		_transform = this.gameObject.transform;
		_animator = this.gameObject.GetComponent<Animator>();
		
		
		direction();
	
	}
	
	private void direction()
	{
		//Choice animation
		if (_transform.position.x > 6.0f)
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
	}

	
    void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == LayerConst.Ephyrule)
		{
			_currentEphyrule = col.gameObject;
			eat();
		}
		
	}
	
	#endregion
	

	public void eat()
	{
		_animator.SetBool("willEat", true);
		//this.animation = _animator.animation.GetClip("FishEat");
		
		
	}
	
		
				
	public void endEat()
	{
		_animator.SetBool("willEat", false);
		//Destroy(_currentEphyrule);
		//this.animation.GetClip ("willEat", false);
		
	}
	
	
}
