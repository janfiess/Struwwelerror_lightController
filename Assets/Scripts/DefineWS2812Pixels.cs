// script attached to the Manager GameObject
// defining colors of WS2812 pixels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{


public class DefineWS2812Pixels : MonoBehaviour {
    int debugLights_count;
	public Dmx_Configurator dmxConfigurator;
	int dmxStartAddress = 25;
	private IEnumerator coroutine_AnimateShapeSlider;
	public Transform lichterkette_pixels;
	public RawImage imageToAnalyse;
	public Texture defaultTexture;
	int textureWidth;
	int uiLights_count;
	bool toggleAnimBtn = false;
	public extOSC.Examples.OSC_send_receive_script oscSender;

	float prevTime;
	float currentTime;
	









   
	public Slider slider;
    float prev_sliderVal, sliderVal = 0;


	public Slider speedslider;
	float speed;
	public Slider masterfader, redfader, greenfader, bluefader;



	void OnEnable(){
		print("imageToAnalyse.texture.width: " + imageToAnalyse.texture.width);
		debugLights_count = lichterkette_pixels.childCount;
		uiLights_count = lichterkette_pixels.GetComponent<Transform>().childCount;
		print("uiLights_count: " + uiLights_count);

		dmxStartAddress = dmxConfigurator.dmxStartAddress_lichterkette;
		
	}

	void Start(){
		Btn_GetShape1(defaultTexture); 
	}

	
	
	void Update () {

		// prev_sliderVal = sliderVal;
		// sliderVal = slider.value;
		// if(sliderVal == prev_sliderVal) return;

		currentTime = Time.time;
		if((currentTime - prevTime)< 0.3f) return;
		prevTime = Time.time;

		
		Texture2D imgTexture = (imageToAnalyse.texture as Texture2D);
		var oscMessage = new OSCMessage("/1/fader1");
		// oscMessage.AddValue(OSCValue.Int(4711));
		// oscMessage.AddValue(OSCValue.Int(4712));
		// oscMessage.AddValue(OSCValue.Int(4713));
			

		// for each light
		for (int i = uiLights_count - 1; i >= 0; i--)
        {
			        // 5f:           10px image width per led, 5 ist die Mitte, 
					// slider.value: range [o..1] -> * textureWidth: slider range covers the whole width of the image	
			var pixelColor = imgTexture.GetPixel(Mathf.FloorToInt(( 5f - slider.value * textureWidth +  i * 10f) % textureWidth),Mathf.FloorToInt(2));
			var lightIntensity = pixelColor.r;

			
			
            // send value to light
			// 6 weil immer 2 lichter doppelt gelegt werden
            dmxConfigurator.DMXData[dmxStartAddress + 6 * i] = (byte)(lightIntensity * masterfader.value * redfader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * redfader.value * 255)));
			dmxConfigurator.DMXData[dmxStartAddress + 6 * i + 1] = (byte)(lightIntensity * masterfader.value * greenfader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * greenfader.value * 255)));
			dmxConfigurator.DMXData[dmxStartAddress + 6 * i + 2] = (byte)(lightIntensity * masterfader.value * bluefader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * bluefader.value * 255)));


			dmxConfigurator.DMXData[dmxStartAddress + 6 * i + 3] = (byte)(lightIntensity * masterfader.value * redfader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * redfader.value * 255)));
			dmxConfigurator.DMXData[dmxStartAddress + 6 * i + 4] = (byte)(lightIntensity * masterfader.value * greenfader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * greenfader.value * 255)));
			dmxConfigurator.DMXData[dmxStartAddress + 6 * i + 5] = (byte)(lightIntensity * masterfader.value * bluefader.value * 255);
			oscMessage.AddValue(OSCValue.Int((int)(lightIntensity * masterfader.value * bluefader.value * 255)));

            // send value to software light
            Color debugLightColor = new Color(
				(float)lightIntensity * redfader.value, 
				(float)lightIntensity * greenfader.value, 
				(float)lightIntensity * bluefader.value, 
				masterfader.value);
			lichterkette_pixels.GetComponent<Transform>().GetChild(i).GetComponent<Image>().color = debugLightColor;   
		}

		oscSender._transmitter_lichterkette.Send(oscMessage);

// print(slider.value);
		if(slider.value == 0f || slider.value == 1f){
			toggleAnimBtn = false;
		}

		
	}

	// UI Button
	public void Btn_AnimateShapeSlider(){
		// print("pressed");
		if(toggleAnimBtn == false){
			toggleAnimBtn = true;

			float startVal = slider.value;
			float endVal = 1.0f;
			if(slider.value > 0.5f)endVal = 0.0f;
			if(slider.value <= 0.5f)endVal = 1.0f;
			coroutine_AnimateShapeSlider = AnimateShapeSliderCor(slider, startVal, endVal);
			StartCoroutine(coroutine_AnimateShapeSlider);
			// btn.image.color = new Color(210,0,61, 255);
		}
		else if(toggleAnimBtn == true){
			toggleAnimBtn = false;
			StopCoroutine(coroutine_AnimateShapeSlider);
			// btn.image.color = new Color(65,65,65, 255);
		}
	}

	// UI Button
	// public void Btn_StopShapeSliderAnimation(){
	// 	StopCoroutine(coroutine_AnimateShapeSlider);
	// }

	IEnumerator AnimateShapeSliderCor(Slider shapeSlider, float startVal, float endVal){

		float journey = 0f;
		float duration = 50.0f - 49.99f * speedslider.value;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            shapeSlider.value = Mathf.Lerp(startVal, endVal, percent);
            yield return null;
        }

	}

	public void Btn_GetShape1(Texture imgTexture){
		imageToAnalyse.texture = imgTexture;
		textureWidth = imageToAnalyse.texture.width;
		// print("texture width: " + textureWidth);
	}
}
}
