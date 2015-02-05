using UnityEngine;
using System.Collections;

public class SafetyZone : MonoBehaviour 
{

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == LayerConst.FISH)
		{
			Debug.Log("collider ", this);//Destroy(this.gameObject);//this.gameObject.GetComponent<Animator>().SetBool("dieMF", true);
		}
	}
}
