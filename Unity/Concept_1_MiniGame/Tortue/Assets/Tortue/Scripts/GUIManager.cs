using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour 
{
	
	public int nbJellyFish;
	
	private GameObject _distanceGUI;
	private GameObject _cptMeduseGUI;
	private GameObject _gaugeGUI;
	
	private float _distance;
	private float _textDistance;
	
	private float _widthGauge;
	private int _heightGauge;
	private Vector2 _posGauge;
	
	
	//Gauge Var
	private Color _colorRGB = new Color(0f, 0f, 0f);
	private bool _fillGauge;
	private float _speedFillGauge;
	private float _incrementationGauge;
	private float _incrementationGaugeColor;
	private GUIStyle currentStyle = null;
	public float barDisplay; //current progress
	public Vector2 pos ;
	public Vector2 size ;
	public Texture2D fullTex;
	
	// Use this for initialization
	void Start () 
	{	
		_distance = 0;
		_distanceGUI = transform.FindChild("distanceGUI").gameObject;
		_cptMeduseGUI = transform.FindChild("cptMeduseGUI").gameObject;
		
		_speedFillGauge = 1f;
		nbJellyFish = GameManager.instance.nbMeduseForBoost;
		pos = new Vector2(15,5);
		size = new Vector2(100,30);
		_incrementationGaugeColor =   1f/ nbJellyFish;
		_incrementationGauge = size.x / nbJellyFish;
		initValueGauge();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_distanceGUI.guiText.text = getDistance() + " m";
	
		if (_fillGauge)
			barDisplay += 1 * _speedFillGauge;
		
		if (barDisplay >= _widthGauge)
		{
			_fillGauge = false;
			_widthGauge += _incrementationGauge;
		}
	}
	
	private int getDistance()
	{
		//d = v * t  (   pixel, seconde   )
		
		_distance +=   GameManager.instance.speed;
		
		// Division by 100 because distance increase fast
		return (int) (_distance / 100);
	}
	
	public void updateScore(int score)
	{
		
		_cptMeduseGUI.guiText.text = score.ToString();
		
		
	}
	
	public void updateGauge()
	{
		_fillGauge = true;
		
		
		//Change Color
		if (_colorRGB.r > 0.1f)
			_colorRGB.r -= 0.1f;
		_colorRGB.g += _incrementationGaugeColor;
		_colorRGB.b += _incrementationGaugeColor;
	
		//Final color cyan
		currentStyle.normal.background = MakeTex( 2, 2, _colorRGB);
		
		
	}
	
	
	public void initValueGauge()
	{
		_colorRGB = new Color(0.7f, 0.0f, 0.0f);
		_widthGauge = 0;
		_fillGauge = false;
		barDisplay = 0;
	}
	
	
	private void InitStyles()
	{
		if( currentStyle == null )
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			currentStyle.normal.background = MakeTex( 2, 2, Color.cyan);
		}
	}
	
	/*
	//Gauge upper left
	private void createGuiTextureGauge()
	{
		
		_posGauge = new Vector2(0.05f,0f);
		_widthGauge = 0;
		_widthGaugeMax = 100;
		_heightGauge = 20;
		
		
		_gaugeGUI = new GameObject("SolidColour");
		_gaugeGUI.transform.parent = this.gameObject.transform;
		_gaugeGUI.name = "Gauge boost";
		_gaugeGUI.AddComponent("GUITexture");
		_gaugeGUI.transform.localScale = Vector3.zero;
		_gaugeGUI.transform.position = new Vector3(0.1f, 0.95f, 0);
		_gaugeGUI.guiTexture.pixelInset = new Rect(_posGauge.x, _posGauge.y ,_widthGauge, _heightGauge);
		Texture2D tex2d = new Texture2D(1, 1);
		tex2d.SetPixels(new Color[1] { Color.cyan });
		tex2d.Apply();
		_gaugeGUI.guiTexture.texture = tex2d;
	}
	*/
	

	
	void OnGUI()
	{
		InitStyles();
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), "");
		
		//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, barDisplay, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, currentStyle);
			GUI.EndGroup();
		GUI.EndGroup();
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}
	
}
