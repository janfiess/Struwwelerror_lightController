// script attached to Canvas/Panel_Header/UI-Lights/Lichterkette/Pixel
// defining colors of WS2812 pixels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefineWS2812Pixels : MonoBehaviour {
    int debugLights_count;
	public Dmx_Configurator dmxConfigurator;
	int dmxStartAddress = 15;
	FadingShapes fadingShapes;
	public Toggle graph_linear, graph_gammaCorrection, graph_linearSteepStart;
  
	public delegate byte FadingShape(float x);
    FadingShape fadingShape_function;
	private IEnumerator coroutine_AnimateShapeSlider;
	


   
	public Slider slider;
    float prev_sliderVal;




	public Slider speedslider;
	float speed;
	public Slider masterfader, redfader, greenfader, bluefader;



	void Start () {
		debugLights_count = transform.childCount;
		fadingShapes = GetComponent<FadingShapes>();
	}
	
	void Update () {

		float sliderVal = slider.value * 1.8f; // adapt slider
        prev_sliderVal = sliderVal;



		// specify the fading function (fadingShape_function) depending on the selected radio button)
        if (graph_linear.isOn) fadingShape_function = fadingShapes.Linear;
        else if (graph_gammaCorrection.isOn) fadingShape_function = fadingShapes.GammaCorrection;
		else if (graph_linearSteepStart.isOn) fadingShape_function = fadingShapes.LinearSteepStart;
		

		// for each light
        // for (int i = 0; i < debugLights_count; i++)

		for (int i = debugLights_count-1; i >= 0; i--)
        {
            // execute the fading function fadingShape_function
            byte y = fadingShape_function(sliderVal-0.025f *i);
		

            // print("y-Value: " + y);

            // send value to light
            dmxConfigurator.DMXData[dmxStartAddress + 3 * i] = (byte)(y * masterfader.value * redfader.value);
			dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 1] = (byte)(y * masterfader.value * greenfader.value);
			dmxConfigurator.DMXData[dmxStartAddress + 3 * i + 2] = (byte)(y * masterfader.value * bluefader.value);

            // send value to software light
            Color debugLightColor = new Color(
				(float)y * redfader.value / 255.0f, 
				(float)y * greenfader.value / 255.0f, 
				(float)y * bluefader.value / 255.0f, 
				masterfader.value);

			transform.GetChild(i).GetComponent<Image>().color = debugLightColor;   
		}		
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
}
