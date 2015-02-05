using UnityEngine;
using System.Collections;

public class MalusFishBehavior : MonoBehaviour 
{
	public int speed;
	
	
	private GameObject _player;
	private string _name;
	private Vector3 _swimTowards;
	private int _nbPlacton;
	private int waveWait = 1;
	private int spawnWait = 2;

	// Use this for initialization
	void Start () 
	{
		float step = speed * Time.deltaTime;
		_name = "MalusFish";
		_player =  GameObject.Find("PlayerWithspriteSwim");
		_swimTowards = getNewPoint();
		transform.position = Vector3.MoveTowards(this.transform.position, _swimTowards, step);
		StartCoroutine (move ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(this.transform.position, _swimTowards, step);
		if (this.transform.position.x < 0)
			kill();
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{	
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerManager>().slowDown();
			kill();
		}
	}
	
	
	private void kill()
	{
		Destroy(this.gameObject);
	}
	
	private Vector3 getNewPoint()
	{
		return new Vector3 (-2f , _player.transform.position.y);
	}
	
	IEnumerator move ()
	{
		while (true)
		{
			yield return new WaitForSeconds (waveWait);
			_swimTowards = getNewPoint();
		}
	}
}
