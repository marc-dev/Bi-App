using UnityEngine;
using System.Collections;

public class EphyruleBehavior : MonoBehaviour 
{

	void Start()
	{
		this.gameObject.rigidbody2D.velocity = Vector2.up;
	}
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == LayerConst.FISH)
		{
			this.gameObject.GetComponent<Animator>().SetBool("dieMF", true);
		}
	}
	
	
	public void endAnimationDie()
	{
		this.gameObject.GetComponent<Animator>().SetBool("dieMF", false);
		Destroy(this.gameObject);
	}
	
}
