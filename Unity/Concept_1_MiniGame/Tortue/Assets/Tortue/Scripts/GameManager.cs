
using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour 
{
	//We make a static variable to our MusicManager instance
	public static GameManager instance { get; private set; }
	
	public float speed;
	public int nbMeduse;
	public int nbMeduseForBoost;
	public bool playing;
	public bool boostReady;
	
	//In pixel
	public int WIDTH_SCREEN = Screen.width;
	public int HEIGHT_SCREEN = Screen.height;
	
	
	//When the object awakens, we assign the static variable
	void Awake() 
	{
		instance = this;
		speed = 0.1f;
		nbMeduse = 0;
		nbMeduseForBoost = 3;
	}
	
	public void someFunction()
	{
		//Play some audio!
	}
	
	
	
	
	

}


/*...
//Now in another class, we can call Play() by using the static variable!
public class LevelController : MonoBehaviour
{
	void PlayMusic()
	{
		MusicManager.instance.Play();
	}
}
*/