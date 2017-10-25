using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshair;
    Rect position;

	// Use this for initialization
	void Start () {
        position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) / 2, crosshair.width, crosshair.height);
    }
	
	// Update is called once per frame
	void OnGUI () {
        GUI.DrawTexture(position, crosshair);
	}
}
