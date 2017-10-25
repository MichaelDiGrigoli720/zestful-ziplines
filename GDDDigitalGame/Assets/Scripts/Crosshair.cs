using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshair;
    Rect position;
    public Camera cam;

	// Use this for initialization
	void Start () {
        position = new Rect((cam.pixelWidth - crosshair.width)/2 + (cam.pixelWidth * (cam.rect.x * 2)), (cam.pixelHeight - crosshair.height) / 2, crosshair.width, crosshair.height);
    }
	
	// Update is called once per frame
	void OnGUI () {
        GUI.DrawTexture(position, crosshair);
	}
}
