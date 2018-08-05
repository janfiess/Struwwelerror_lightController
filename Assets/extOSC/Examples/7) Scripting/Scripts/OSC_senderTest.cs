using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSC_senderTest : MonoBehaviour {
	public extOSC.Examples.OSC_send_receive_script oscSender;


	public void Btn_TestOSCsender(string value){
		oscSender.SendOscMsg(value, 0.2f);
	}

	public void changeIntensity(Slider slider){
		oscSender.SendOscMsg("/1/intensity", slider.value);
		print("sliderintensity: " + slider.value);
	}
}
