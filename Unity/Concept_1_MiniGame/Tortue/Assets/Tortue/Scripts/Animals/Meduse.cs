using UnityEngine;
using System.Collections;

public class Meduse : MonoBehaviour
{
	
	void Update () 
	{
		this.transform.position = new Vector3(  (this.transform.position.x - (GameManager.instance.speed * Time.deltaTime * 10) ),this.transform.position.y, this.transform.position.z) ;
		
		if (this.transform.position.x < 0 || this.transform.position.y > 0)
			Destroy(this.gameObject);
			
		
		
		
	}	
}
