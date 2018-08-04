﻿// script attached to the Manager GameObject
// defining colors of WS2812 pixels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefineWS2812Pixels : MonoBehaviour {
    int debugLights_count;
	public Dmx_Configurator dmxConfigurator;
	int dmxStartAddress = 5;
	private IEnumerator coroutine_AnimateShapeSlider;
	public Transform lichterkette_pixels;
	public RawImage imageToAnalyse;
	public Texture imgTexture1, imgTexture2;
	









   
	public Slider slider;
    // float prev_sliderVal;


	public Slider speedslider;
	float speed;
	public Slider masterfader, redfader, greenfader, bluefader;



	void Start () {
		imageToAnalyse.texture = imgTexture1;
		// print("Image to analyse: " + imageToAnalyse.texture.width);
		debugLights_count = lichterkette_pixels.childCount;
		lichterkette_pixels.GetChild(1).GetComponent<Image>().color = Color.green;   	
	}
	
	void Update () {

		float sliderVal = slider.value; // adapt slider
        // prev_sliderVal = sliderVal;



		

		Texture2D imgTexture = (imageToAnalyse.texture as Texture2D);

		// for each light
		for (int i = lichterkette_pixels.GetComponent<Transform>().childCount-1; i >= 0; i--)
        {
			var pixelColor = imgTexture.GetPixel(Mathf.FloorToInt(i*10 + 5),Mathf.FloorToInt(2));
			var lightIntensity = pixelColor.r;
			print( i + ": " + lightIntensity + " (GetMultiplePixelsFromImage");


            // send value to light
            dmxConfigurator.DMXData[dmxStartAddress + 3 * i] = (byte)(lightIntensity * masterfader.value * redfader.value * 255);
			dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 1] = (byte)(lightIntensity * masterfader.value * greenfader.value * 255);
			dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 2] = (byte)(lightIntensity * masterfader.value * bluefader.value * 255);

            // send value to software light
            Color debugLightColor = new Color(
				(float)lightIntensity * redfader.value, 
				(float)lightIntensity * greenfader.value, 
				(float)lightIntensity * bluefader.value, 
				masterfader.value);





			lichterkette_pixels.GetComponent<Transform>().GetChild(i).GetComponent<Image>().color = debugLightColor;   
		}
		





















		// for each light
        // for (int i = 0; i < debugLights_count; i++)

		// for (int i = debugLights_count-1; i >= 0; i--)
        // {
        //    float y = 1;

        //     // send value to light
        //     dmxConfigurator.DMXData[dmxStartAddress + 3 * i] = (byte)(y * masterfader.value * redfader.value);
		// 	dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 1] = (byte)(y * masterfader.value * greenfader.value);
		// 	dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 2] = (byte)(y * masterfader.value * bluefader.value);

        //     // send value to software light
        //     Color debugLightColor = new Color(
		// 		(float)y * redfader.value / 255.0f, 
		// 		(float)y * greenfader.value / 255.0f, 
		// 		(float)y * bluefader.value / 255.0f, 
		// 		masterfader.value);

		// 	// lichterkette_pixels.transform.GetChild(i).GetComponent<Image>().color = debugLightColor;  
		// }		
	}

	// UI Button
	public void Btn_AnimateShapeSlider(){
		float startVal = slider.value;
		float endVal = 1.0f;
		if(slider.value > 0.5f)endVal = 0.0f;
		if(slider.value <= 0.5f)endVal = 1.0f;
		coroutine_AnimateShapeSlider = AnimateShapeSliderCor(slider, startVal, endVal);
		StartCoroutine(coroutine_AnimateShapeSlider);
	}

	// UI Button
	public void Btn_StopShapeSliderAnimation(){
		StopCoroutine(coroutine_AnimateShapeSlider);
	}

	IEnumerator AnimateShapeSliderCor(Slider shapeSlider, float startVal, float endVal){

		float journey = 0f;
		float duration = 30.0f * (1 - Mathf.Pow(speedslider.value,3));
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            shapeSlider.value = Mathf.Lerp(startVal, endVal, percent);
            yield return null;
        }

	}

	public void Btn_GetShape1(Texture imgTexture){
		print("hit Btn_GetShape1");
		imageToAnalyse.texture = imgTexture;
		print("Image to analyse: " + imageToAnalyse.texture.width);
	}

	public void Btn_GetShape2(Texture imgTexture){
		print("hit Btn_GetShape2");
		imageToAnalyse.texture = imgTexture;
		print("Image to analyse: " + imageToAnalyse.texture.width);
	}
}
