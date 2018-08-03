// script attached to Canvas/Panel_Kinderzimmer/Lichterkette/Pixel
// try graphs on graphsketch.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingShapes : MonoBehaviour
{
    float defaultBrightness = 0;
	// public Slider masterfader;


   

    public byte Linear(float x)
    {
        float y=defaultBrightness * 255;
        if (x <= 0 || x >= 1)     y =               defaultBrightness * 255;
        if (x > 0 && x <= 0.5f)   y =  x * (2 - 2 * defaultBrightness) * 255 + defaultBrightness * 255;
        if (x > 0.5f && x <= 1f)  y =  ((-x + 0.5f) * 255) * (2 - 2 * defaultBrightness) + 255 ;
        // y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
        return (byte)Mathf.Round(y);
    }

    
    public byte LinearSteepStart(float x)
    {
        float y = defaultBrightness * 255;
        if (x <= 0 || x >= 0.5f) y = defaultBrightness * 255;
		if (x > 0 && x <= 0.06225f) y = (16 - 16 * defaultBrightness) * (x * 255) + defaultBrightness * 255;
		if (x > 0.06225f && x <= 0.5f) y = ((-x + 0.06225f) * 255) * (2.3f - 2.3f * defaultBrightness) + 255;
        // y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
        return (byte)Mathf.Round(y);
    }

	public byte LinearSteepStart2(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0 || x >= 0.5f) y = defaultBrightness * 255;
		if (x > 0 && x <= 0.06225f) y = (16 - 16 * defaultBrightness) * (x * 255) + defaultBrightness * 255;
		if (x > 0.06225f && x <= 0.5f) y = ((-x + 0.06225f) * 255) * (4.2f - 4.2f * defaultBrightness) + 255;
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}

    

    public byte Linear2(float x)
    {
		float y = defaultBrightness * 255;
		if (x <= 0.25f || x >= 0.75f) y = defaultBrightness * 255;
		if (x > 0.25f && x <= 0.5f) y = defaultBrightness *255 + (x-0.25f)*(4-4*defaultBrightness)*255;
		if (x > 0.5f && x <= 0.75f) y = ((-x + 0.5f) * 255) * (4 - 4 * defaultBrightness) + 255;
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
        return (byte)Mathf.Round(y);
    }

	public byte Linear3(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0.375f || x >= 0.625f) y = defaultBrightness * 255;
		if (x > 0.375f && x <= 0.5f) y = defaultBrightness *255 + (x-0.375f)*(8-8*defaultBrightness)*255;
		if (x > 0.5f && x <= 0.625f) y = ((-x + 0.5f) * 255) * (8 - 8 * defaultBrightness) + 255;
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}

	public byte Linear4(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0.4375f || x >= 0.5625f) y = defaultBrightness * 255;
		if (x > 0.4375f && x <= 0.5f) y = defaultBrightness *255 + (x-0.4375f)*(16-16*defaultBrightness)*255; // 0.3*255 + (x-0.4375)*(16-16*0.3)*255
		if (x > 0.5f && x <= 0.5625f) y = ((-x + 0.5f) * 255) * (16 - 16 * defaultBrightness) + 255;   // ((-x + 0.5) * 255) * (16-16*0.3) + 255
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}

	public byte Linear5(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0.46875f || x >= 0.53125f) y = defaultBrightness * 255;
		if (x > 0.46875f && x <= 0.5f) y = defaultBrightness *255 + (x-0.46875f)*(32-32*defaultBrightness)*255; // 0.3*255 + (x-0.4375)*(16-16*0.3)*255
		if (x > 0.5f && x <= 0.53125f) y = ((-x + 0.5f) * 255) * (32 - 32 * defaultBrightness) + 255;   // ((-x + 0.5) * 255) * (16-16*0.3) + 255
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}

	public byte Linear6(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0.484375f || x >= 0.515625f) y = defaultBrightness * 255;
		if (x > 0.484375f && x <= 0.5f) y = defaultBrightness *255 + (x-0.484375f)*(64-64*defaultBrightness)*255; // 0.3*255 + (x-0.4375)*(16-16*0.3)*255
		if (x > 0.5f && x <= 0.515625f) y = ((-x + 0.5f) * 255) * (64 - 64 * defaultBrightness) + 255;   // ((-x + 0.5) * 255) * (16-16*0.3) + 255
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}



    public byte GammaCorrection(float x)
    {
        float y = defaultBrightness * 255;
        if (x <= 0 || x >= 0.5f) y = defaultBrightness * 255;
		if (x > 0 && x <= 0.25f) y = Mathf.Pow(x, 2.2f) * 255 * 4.5f * (4.7f-4.7f*defaultBrightness) + defaultBrightness * 255; // x^2.2 * 255 * 4.5* (4.7-4.7*0.3) + 0.3* 255 | try on graphsketch.com
		if (x > 0.25f && x <= 0.5f) y = Mathf.Pow((0.5f - x), 2.2f) * 255 * 4.5f * (4.7f-4.7f*defaultBrightness) + defaultBrightness * 255; // (0.5-x)^2.2 * 255 * 4.5* (4.7-4.7*0.3) + 0.3* 255
        // y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
        return (byte)Mathf.Round(y);
    }

	/*
	public byte GammaCorrection2(float x)
	{
		float y = defaultBrightness.val * 255;
		if (x <= 0 || x >= 0.3f) y = defaultBrightness.val * 255;
		if (x > 0 && x <= 0.15f) y = Mathf.Pow(x,2.2f) * 255 * 4.5f * (22-22*defaultBrightness.val) + defaultBrightness.val* 255; // x^2.2 * 255 * 4.5* (4.7-4.7*0.3) + 0.3* 255 | try on graphsketch.com
		if (x > 0.15f && x <= 0.3f) y = Mathf.Pow((0.5f - x), 2.2f) * 255 * 4.5f * (22-22*defaultBrightness.val) + defaultBrightness.val * 255; // (0.5-x)^2.2 * 255 * 4.5* (4.7-4.7*0.3) + 0.3* 255
		y *= masterfader.val;
		if(y < (defaultBrightness.val * 255)) y = defaultBrightness.val * 255;
		return (byte)Mathf.Round(y);
	}
	*/

	public byte Ducks(float x)
	{
		float y = defaultBrightness * 255;
		if (x <= 0 || x >= 0.5f) y = defaultBrightness * 255;
		if (x > 0 && x <= 0.06225f) y = (16 - 16 * defaultBrightness) * (x * 255) + defaultBrightness * 255;
		if (x > 0.06225f && x <= 0.25f) y = ((-x + 0.06225f) * 255) * (5.3f - 5.3f * defaultBrightness) + 255;
		if (x > 0.25f && x <= 0.3f) y = (8.2f- 8.2f* defaultBrightness) * (x-0.25f) * 255 + defaultBrightness * 255; // (0.5-x)^2.2 * 255 * 4.5* (4.7-4.7*0.3) + 0.3* 255
		if (x > 0.3f && x <= 0.5f) y = ((-x + 0.03f) * 255) * (2.12f - 2.12f * defaultBrightness) + 255;
		// y *= masterfader.value;
		if(y < (defaultBrightness * 255)) y = defaultBrightness * 255;
		return (byte)Mathf.Round(y);
	}
}
