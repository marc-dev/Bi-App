using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{

	public AudioClip [] sounds;
	public static int  BULLES = 2;
	public static int  GOUTTES = 1;
	public static int  FOND = 0;
	
	
	private static int _currentIdAudio;
	
	

	// Use this for initialization
	void Start () 
	{
		_currentIdAudio = -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_currentIdAudio >= 0)
			play ();
	}
	
	public static void playSound(int name)
	{
		_currentIdAudio = name;
		
	}
	
	private void play()
	{
		//audio = sounds[]
	}
}
