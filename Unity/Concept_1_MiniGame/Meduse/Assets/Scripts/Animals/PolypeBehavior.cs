using UnityEngine;
using System.Collections;

public class PolypeBehavior : MonoBehaviour 
{
	public GameObject animal;
	
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	
	private Vector3 spawnPosition ;
		
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
		
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{				
				spawnPosition = new Vector3 (this.transform.position.x, this.transform.position.y , -21);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (animal, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
		}
	}
}
