using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject panel_wohnzimmer, panel_kinderzimmer, panel_toilette, panel_settings;

	[HideInInspector] public List<GameObject> menu_panels;

	void Start () {
		menu_panels.Add(panel_kinderzimmer);
		menu_panels.Add(panel_wohnzimmer);
		menu_panels.Add(panel_toilette);
		menu_panels.Add(panel_settings);

		foreach(GameObject menu_panel in menu_panels){
			menu_panel.SetActive(false);
		}

	}
	
	public void ShowPanel (GameObject currentPanel) {
		foreach(GameObject menu_panel in menu_panels){
			menu_panel.SetActive(false);
		}

		currentPanel.SetActive(true);
	}
}
