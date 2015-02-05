using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour 
{

	public GameObject GUITEST;
	private int TESTCPT;


	public GameObject [] animals;
	
	public GameObject player;
	
	private GameObject _player;
	private Vector3 [] patternMove;

	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	
	private bool gameOver;
	private bool restart;
	private int score;

	// Use this for initialization
	void Start () 
	{
		//_player = Instantiate(player, player.transform.position, Quaternion.identity) as GameObject;
		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		float y = Random.Range (0, spawnValues.y);
		while (true)
		{
			TESTCPT++;
			GUITEST.GetComponent<Text>().text = "Nb Spawn "+TESTCPT;
			for (int i = 0; i < animals.Length; i++)
			{
				y = Random.Range (0, spawnValues.y);
				GameObject hazard = animals [Random.Range(0, animals.Length)];
				Vector3 spawnPosition = new Vector3 (spawnValues.x, y , spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
		}
	}
}
