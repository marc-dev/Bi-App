using UnityEngine;
using System.Collections;

public class Done_DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit2D (Collider2D other) 
	{
		Debug.Log ("what");
		Destroy(other.gameObject);
	}
}