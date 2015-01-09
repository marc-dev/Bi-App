using UnityEngine;
using System.Collections;
using System.IO;

public class ManipulateTexture : MonoBehaviour 
{
	
	public Texture2D image;

	void Start()
	{
		/*
		// duplicate the original texture and assign to the material
		Texture2D texture  = image;
		renderer.material.mainTexture = texture;
		// colors used to tint the first 3 mip levels
		Color[] colors = new Color[3];
		colors[0] = Color.red;
		colors[1] = Color.green;
		colors[2] = Color.blue;
		int mipCount = Mathf.Min( 3, texture.mipmapCount );
		// tint each mip level
		for( int mip = 0; mip < mipCount; ++mip ) {
			Color [] cols = texture.GetPixels( mip );
			for( int i = 0; i < cols.Length; ++i ) 
			{
				cols[i] = Color.Lerp( cols[i], colors[mip], 0.33f );

				//Debug.Log("cols "+cols[i]);
			}
			texture.SetPixels( cols, mip );
		}
		// actually apply all SetPixels, don't recalculate mip levels
		texture.Apply( false );

		*/
		float ratio = (float)image.width / (float)image.height;
		
		float ymh, xmw;
		
		if(ratio >= 1.2f) 
		{
			ymh = 50.0f * (1.0f / ratio);
			xmw = ymh * ratio;
		} 
		else if(ratio < 1.2f && ratio >= 1.0f) 
		{
			ymh = 40.0f * (1.0f / ratio);
			xmw = ymh * ratio;
		}
		else
		{
			ymh = 40.0f;
			xmw = ymh * ratio;
		}
		
		for(int ym = 0; ym < ymh; ym++) 
		{
			for(int xm = 0; xm < xmw; xm++) 
			{
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

				//The pixelColor
				Color c = image.GetPixelBilinear(xm/xmw, ym/ymh);

				//if(c.r > 0.95f && c.g > 0.95f && c.b > 0.95f) 
				//	continue; // Ignore white and very light pixels

				
				cube.transform.position = new Vector3(xm-xmw/2, 0, ym-ymh/2);
				cube.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
				cube.transform.rotation = Random.rotation;
				cube.renderer.material.color = c;
				cube.renderer.castShadows = false;
				cube.renderer.receiveShadows = false;

			}
		}


	}

	void Update () 
	{

	}
}