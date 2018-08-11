using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject panel_wohnzimmer, panel_kinderzimmer, panel_toilette, panel_settings, panel_dmxTester;
	[HideInInspector] public GameObject activePanel;
	[HideInInspector] public List<GameObject> menu_panels;
	public Dmx_Configurator dmxConfigurator;
	public Slider[] dmxFaders;

	void Start () {
		menu_panels.Add(panel_kinderzimmer);
		menu_panels.Add(panel_wohnzimmer);
		menu_panels.Add(panel_toilette);
		menu_panels.Add(panel_settings);
		menu_panels.Add(panel_dmxTester);

		foreach(GameObject menu_panel in menu_panels){
			menu_panel.SetActive(false);
		}

	}
	
	public void ShowPanel (GameObject currentPanel) {
		foreach(GameObject menu_panel in menu_panels){
			menu_panel.SetActive(false);
		}

		currentPanel.SetActive(true);
		activePanel = currentPanel;
	}

	public void OverrideDmxValues(){
		print("ahoi");

		for(int i = 0; i < dmxFaders.Length; i++){
			dmxConfigurator.DMXData[i] = (byte)dmxFaders[i].value;
		}

		// dmxConfigurator.DMXData[6] = (byte)115;
	}


}
