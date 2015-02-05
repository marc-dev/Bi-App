using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
 {
	void OnCollisionEnter2D(Collision2D col)
	{
		Destroy(col.gameObject);
	//	col.gameObject.die();//		Destroy(col.gameObject);
	}
}
