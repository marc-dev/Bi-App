using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundRawImage : MonoBehaviour 
{
	
	public float scrollSpeed;
	private float _offset;
	
	
	// Update is called once per frame
	void Update () 
	{
		//_offset = Mathf.Repeat(Time.time * (scrollSpeed + GameManager.instance.speed), 1 );
		_offset +=  scrollSpeed ;//+ GameManager.instance.speed;
		this.gameObject.GetComponent<RawImage>().uvRect = new Rect(_offset / 1000 , 0, 1, 1);/// = new Vector2(_offset, 0);
		if (_offset > 1000000)
			_offset = 0;
	
	}
}
