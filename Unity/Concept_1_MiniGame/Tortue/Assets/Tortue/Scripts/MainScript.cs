using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour 
{

	public GameObject [] animals;
	
	private Vector3 [] patternMove;

	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	
	private bool gameOver;
	private bool restart;
	private int score;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		float y = Random.Range (0, spawnValues.y);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				y = Random.Range (0, spawnValues.y);
				GameObject hazard = animals [0];
				Vector3 spawnPosition = new Vector3 (spawnValues.x, y , spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
		}
	}
}
