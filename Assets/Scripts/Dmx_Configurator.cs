/*
	This script reads the color values of Unity software lights in realtime (Update function) 
	and translates the RGB values to DMX devices
	The Unity software lights' colors are driven by the Animator
 */


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ArtNet;
using UnityEngine.UI;

public class Dmx_Configurator : MonoBehaviour
{
    public byte[] DMXData = new byte[512];
    public InputField ip_textfield;

    ArtNet.Engine ArtEngine;
	// Animator animator;
	float animSmoother = 6f; // lower value: slower and smoother

	// Skypanel 1
	// public Light softwareLight_skypanel1; // this color will be transmitted to LED 1 (and 2)
	public Color color_skypanel1;
	public GameObject softwareLight_skypanel1_gui;
	public Slider masterfader_skypanel1_ui;
	int port_led_skypanel1 = 5; // DMX Channel - 1 (DMX6)
	Color prevLed_skypanel1_Color;


	// Skypanel 2
	public Color color_skypanel2;
	public GameObject softwareLight_skypanel2_gui;
	float masterfader_skypanel2 = 1;
	public Slider masterfader_skypanel2_ui;
	int port_led_skypanel2 = 15;            // DMX 16 -> // DMX Channel - 1
	Color prevLed_skypanel2_Color;



	// LED stripe wohnzimmer
	public Color color_stripe; 
	public GameObject softwareLight_stripe_gui;
	float masterfader_stripe;
	public Slider masterfader_stripe_ui;
	int port_led_stripe = 0;            // DMX 1 -> // DMX Channel - 1
	Color prevLed_stripe_Color;


	// LEDs Lichterkette_kinderzimmer // DMX in DefineWS2812Pixels.cs
	public Color color_lichterkette;
	// public GameObject softwareLight_lichterkette_gui;
	float masterfader_lichterkette;
	public Slider masterfader_lichterkette_ui;
	[HideInInspector] public int dmxStartAddress_lichterkette = 25;            // DMX 26 -> // DMX Channel - 1
	Color prevLed_lichterkette_Color;


	// LEDs Toilette -> OSC in OSC_sender.cs
	public Color color_toilette;
	public GameObject softwareLight_toilette_gui;
	float masterfader_toilette;
	public Slider masterfader_toilette_ui;
	Color prevLed_toilette_Color;





    void Start()
    {
        for (int i = 0; i < DMXData.Length; i++)
        {
            DMXData[i] = (byte)(0);
        }

        // Artnet sender / client
        string ipAddress = ip_textfield.text;
        if(ip_textfield.text == "") ipAddress = ip_textfield.placeholder.GetComponent<Text>().text;
        print(ipAddress);
        ArtEngine = new ArtNet.Engine("Open DMX Etheret", ipAddress);
        ArtEngine.Start();
    }



	// called via GUI Button
	public void Skypanel1_toggle(){
		if (masterfader_skypanel1_ui.value <= 0.5f){
			StartCoroutine(SmoothValue(masterfader_skypanel1_ui, 1f));
		} 
		else if (masterfader_skypanel1_ui.value > 0.5f){
			StartCoroutine(SmoothValue(masterfader_skypanel1_ui, 0f));
		} 
	}

	// called via GUI Button
	public void Skypanel2_toggle(){
		if (masterfader_skypanel2_ui.value <= 0.5f){
			StartCoroutine(SmoothValue(masterfader_skypanel2_ui, 1f));
		} 
		else if (masterfader_skypanel2_ui.value > 0.5f){
			StartCoroutine(SmoothValue(masterfader_skypanel2_ui, 0f));
		} 
	}

	// called via GUI Button
	public void Stripe_toggle(){
		if (masterfader_stripe_ui.value <= 0.5f){
			StartCoroutine(SmoothValue(masterfader_stripe_ui, 1f));
		} 
		else if (masterfader_stripe_ui.value > 0.5f){
			StartCoroutine(SmoothValue(masterfader_stripe_ui, 0f));
		} 
	}

	// called via GUI Button
	public void Toilette_toggle(){
		if (masterfader_toilette_ui.value <= 0.5f){
			StartCoroutine(SmoothValue(masterfader_toilette_ui, 1f));
		} 
		else if (masterfader_toilette_ui.value > 0.5f){
			StartCoroutine(SmoothValue(masterfader_toilette_ui, 0f));
		} 
	}

	// called via GUI Button
	public void Lichterkette_toggle(){
		if (masterfader_lichterkette_ui.value <= 0.5f){
			StartCoroutine(SmoothValue(masterfader_lichterkette_ui, 1f));
		} 
		else if (masterfader_lichterkette_ui.value > 0.5f){
			StartCoroutine(SmoothValue(masterfader_lichterkette_ui, 0f));
		} 
	}

	IEnumerator SmoothValue(Slider slider, float target)
    {
        float journey = 0f;
		float origin = slider.value;
		float duration = 1.0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            // description: float interpolated result between startValue and endValue = Vector3.Lerp(float startValue, float endValue, float interpolation value between the two floats)
            slider.value = Mathf.Lerp(origin, target, percent);
            yield return null;
        }
    }





	void Update(){
	// First change DMX array values (DMX Port, value [0..255]));
	// Then send it away.
	// Pick the current color of the light in the scene and send it to real LEDs via Artnet

		if (color_skypanel1 != prevLed_skypanel1_Color) {

			DMXData[port_led_skypanel1] = (byte)(color_skypanel1.r * masterfader_skypanel1_ui.value * 255);
			
			// see color also on an UI element in scene
			softwareLight_skypanel1_gui.GetComponent<Image>().color = new Color(
				color_skypanel1.r, 
				color_skypanel1.r, 
				color_skypanel1.r, 
				masterfader_skypanel1_ui.value);
			prevLed_skypanel1_Color = color_skypanel1;
		}


		if (color_skypanel2 != prevLed_skypanel2_Color) {
			DMXData[port_led_skypanel2] = (byte)(color_skypanel2.r * masterfader_skypanel2_ui.value * 255);

			// see color also on an UI element in scene
			softwareLight_skypanel2_gui.GetComponent<Image>().color = new Color(
				color_skypanel2.r, 
				color_skypanel2.r, 
				color_skypanel2.r, 
				masterfader_skypanel2_ui.value);
			
			prevLed_skypanel2_Color = color_skypanel2;
		}

		if (color_stripe != prevLed_stripe_Color) {
			DMXData[port_led_stripe] = (byte)(color_stripe.r * masterfader_stripe_ui.value * 255);

			// see color also on an UI element in scene
			softwareLight_stripe_gui.GetComponent<Image>().color = new Color(
				color_stripe.r, 
				color_stripe.r, 
				color_stripe.r, 
				masterfader_stripe_ui.value);
			
			prevLed_stripe_Color = color_stripe;
		}


		// OSC in OSC_Sender.cs
		if (color_toilette != prevLed_toilette_Color) {
			// DMXData[port_led_toilette] = (byte)(color_toilette.r * masterfader_toilette_ui.value * 255);

			// see color also on an UI element in scene
			softwareLight_toilette_gui.GetComponent<Image>().color = new Color(
				color_toilette.r, 
				color_toilette.r, 
				color_toilette.r, 
				masterfader_toilette_ui.value);
			
			prevLed_toilette_Color = color_toilette;
		}

		// Settings für Lichterkette in DefineWS2812Pixels.cs -> Canvas/Panel_Header/UI-Lights/Lichterkette/Pixel




		// if (softwareLight_lichterkette.color != prevLed_lichterkette_Color) {
		// 	DMXData[port_led_lichterkette] = (byte)(softwareLight_lichterkette.color.r * masterfader_lichterkette_ui.value * 255);

		// 	// see color also on an UI element in scene
		// 	softwareLight_lichterkette_gui.GetComponent<Image>().color = new Color(
		// 		softwareLight_lichterkette.color.r, 
		// 		softwareLight_lichterkette.color.r, 
		// 		softwareLight_lichterkette.color.r, 
		// 		masterfader_lichterkette_ui.value);
			
		// 	prevLed_lichterkette_Color = softwareLight_lichterkette.color;
		// }


		DMXData[4] = (byte)255; // DMX 5
		DMXData[14] = (byte)255; // DMX 15
		DMXData[24] = (byte)255; // DMX 25

		

		ArtEngine.SendDMX(0, DMXData, DMXData.Length);
	}
}