using UnityEngine;
using System.Collections;

public class RollOverCube : MonoBehaviour {
	
	void Start () {
	}
	
	void Update () {
		if(gameObject.GetComponent(typeof(Rigidbody))) {
			if(rigidbody.IsSleeping()) {
				Destroy(rigidbody);
			}
		}
	}
		
	void OnMouseOver () {
		if(!gameObject.GetComponent(typeof(Rigidbody))) gameObject.AddComponent<Rigidbody>();
	}
}
