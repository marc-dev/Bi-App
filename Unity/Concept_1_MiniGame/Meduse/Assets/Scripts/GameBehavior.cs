using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameBehavior : MonoBehaviour 
{

	//UI
	public GameObject scoreUI;

	//polype
	public GameObject polypeGO;
	public int waitPolype;

	//Ephyrule
	public EphyruleBehavior _selectedEphyrule;

	//Damage Image
	public Image damageImage;
	public float flashSpeed;

	public GameObject [] animals;
	
	private Vector3 [] patternMove;
	
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	

	private bool inGame;
	private bool gameOver;
	private bool restart;
	private int score;
	
	private int _score;





	float y, x;


	#region Unity function
	void Start () 
	{
		inGame = true;
		createPolype();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		// If the player has just been damaged...
		if(damageImage.color != Color.clear)
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
	
	}

	void  OnMouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (_selectedEphyrule != null)	
			_selectedEphyrule.moveTowards( new Vector2(ray.origin.x, ray.origin.y) );
	}
	#endregion


	#region MyFunction

	public void clickOnEphyrule(EphyruleBehavior ephy)
	{
		if (_selectedEphyrule == ephy)
		{
			Debug.Log("1");
			//Do nothing
		}

		else if (_selectedEphyrule == null)
		{
			Debug.Log("2");
			ephy.select();
			_selectedEphyrule = ephy;
		}

		else
		{
			Debug.Log("3");
			_selectedEphyrule.unSelect();
			ephy.select();
			_selectedEphyrule = ephy;
		}

	}


	public void updateScore(int score)
	{
		if (_score + score >= 0)
			_score += score;

		foreach(Transform tr in scoreUI.transform)
		{
			tr.GetComponent<Text>().text = "Scrore : " + _score;
		}
	}

	#region spawn
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		
		while (inGame)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				y = Random.Range (- spawnValues.y, spawnValues.y);
				x = (y > 0 ) ? spawnValues.x : -spawnValues.x ;
				GameObject hazard = animals [Random.Range(0, animals.Length )];
				Vector3 spawnPosition = new Vector3 (x, y , spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
		}
	}

	public void createPolype()
	{
		x = Random.Range (GameManager.instance.upperLeft.x + 0.5f, GameManager.instance.botRight.x - 0.5f);
		y = Random.Range (-4.6f, -4.4f);
		Vector3 spawnPosition = new Vector3 (x, y , -1f);
		Instantiate (polypeGO, spawnPosition, Quaternion.identity);
	}
	#endregion



	#endregion

}
