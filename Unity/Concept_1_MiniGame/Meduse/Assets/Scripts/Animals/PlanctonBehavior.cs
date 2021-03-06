using UnityEngine;
using System.Collections;

public class PlanctonBehavior : MonoBehaviour
{
	public int speed;
	public GameObject hundredPointsUI;	

	private Vector3 _destinationPoint;
	private Transform _transform;

	#region
	void Start () 
	{
		_transform = this.gameObject.transform;
		_destinationPoint = getNewPoint();

	}

	void Update()
	{
		_transform.position = Vector2.MoveTowards(_transform.position, _destinationPoint, speed * Time.deltaTime);

		if (_transform.position.x == _destinationPoint.x && _transform.position.y == _destinationPoint.y)
		{
			_destinationPoint = getNewPoint();
		//	_transform.Translate(_destinationPoint * Time.deltaTime);
		}
	}


	#endregion

	// Instantiate the 100 points prefab at this point.
//	Instantiate(hundredPointsUI, scorePos, Quaternion.identity);

	private Vector2 getNewPoint()
	{
		return new Vector3(Random.Range(GameManager.instance.upperLeft.x, GameManager.instance.botRight.x) ,
		                   Random.Range(GameManager.instance.upperLeft.y, GameManager.instance.botRight.y),
		                   -1f);
	}

	public void eaten()
	{
		Instantiate(hundredPointsUI, _transform.position, Quaternion.identity);
		die();
	}

	private void die()
	{
		Destroy(this.gameObject);
	}

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       