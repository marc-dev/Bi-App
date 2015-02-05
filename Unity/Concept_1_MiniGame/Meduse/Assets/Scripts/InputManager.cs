using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager	 : MonoBehaviour 
{
	public int timeAnimation;
	public int speed;
	
	
	private bool _selectOk;
	private Vector2 _position;
	private Vector2 _initPosition;
	private Vector2 _coeff;
	
	private bool _moveGameobject;
		
	
	void  OnMouseDown()
	{
		Debug.Log(" dz " );
		getPoint();
	}

	
	private void getPoint()
	{
		Vector3 mousePt = Input.mousePosition;
		Debug.Log(" dz " +mousePt  +  " "+ Camera.main.name);
		Ray ray = Camera.main.ViewportPointToRay(mousePt);
		//Debug.Log(ray.direction);
		//if (selectedEphyrule != null)	
		//	selectedEphyrule.moveTowards( new Vector2(mousePt.x, mousePt.y) );
	}

	

}
