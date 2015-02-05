
using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour 
{
	//We make a static variable to our MusicManager instance
	public static GameManager instance { get; private set; }
	
	public  Vector2 upperLeft;
	public  Vector2 botRight;
	
	//In pixel
	public int WIDTH_SCREEN = Screen.width;
	public int HEIGHT_SCREEN = Screen.height;

	public GameBehavior gameBehavior;
	
	
	//When the object awakens, we assign the static variable
	void Awake() 
	{
		instance = this;

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