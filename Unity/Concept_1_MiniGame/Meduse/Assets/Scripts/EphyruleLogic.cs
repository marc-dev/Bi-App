using UnityEngine;
using System.Collections;

public class EphyruleLogic : MonoBehaviour 
{
	private GameObject [] Ephyrules;
	private GameObject _currentEphyrule;
	
	//Behavior Ephyrule

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void onTouch(GameObject pObject)
	{
		Debug.Log ("Click on");
	}
	
	public void moveTaRaceBordel()
	{
		Debug.Log ("the strange function");
	}
}
