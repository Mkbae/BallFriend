using UnityEngine;
using System.Collections;

public class CameraSettings2D : MonoBehaviour{
	public float zPos=-500;
	void Awake()
	{
		Camera cam = GetComponent<Camera> ();
		cam.orthographic = true;
		cam.orthographicSize = Screen.height*0.5f;
		cam.farClipPlane = 1200;
		transform.position = new Vector3(0,0,zPos);
	}
}
