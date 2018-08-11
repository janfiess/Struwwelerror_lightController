using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmxTesterSlider : MonoBehaviour {

	public Slider slider;

	public void OnSliderChanged(Text label){
		label.text = slider.value.ToString();
	}
}
