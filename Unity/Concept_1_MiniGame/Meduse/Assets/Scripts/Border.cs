using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
 {
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("collision");
		Destroy(col.gameObject);
	}
}
