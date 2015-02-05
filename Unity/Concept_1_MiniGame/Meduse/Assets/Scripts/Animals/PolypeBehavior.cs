using UnityEngine;
using System.Collections;

public class PolypeBehavior : MonoBehaviour 
{
	public GameObject euphyrule;

	public float spawnWait;
	public float startWait;
	public int maxPolype;	

	private int _nbPolype;
	private Vector3 spawnPosition ;
		
	// Use this for initialization
	void Start () 
	{
		init();
		StartCoroutine (SpawnWaves ());
	}

	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		
		while (_nbPolype < maxPolype)
		{
			spawnPosition = new Vector3 (this.transform.position.x, this.transform.position.y , - 2f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (euphyrule, spawnPosition, spawnRotation);
			_nbPolype ++;
			yield return new WaitForSeconds (spawnWait);
		}
		die();
	}

	private void die()
	{
		GameManager.instance.gameBehavior.createPolype();
		Destroy(this.gameObject);
	}

	private void init()
	{
		_nbPolype = 0;
	}
}
