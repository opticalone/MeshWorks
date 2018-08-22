using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChangeyBoi : MonoBehaviour {

    Light light;
    public float rangeFind = 1;
	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        Mathf.Clamp(light.spotAngle, 0 , 100);
        Mathf.Clamp(light.intensity, 0, 100);
	}
	
	// Update is called once per frame
	void Update () {
        light.spotAngle = rangeFind;
        light.intensity = 100 / rangeFind;
	}
}
