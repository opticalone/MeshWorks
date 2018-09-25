using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changetime : MonoBehaviour {

    public Light sun;
    private void Start()
    {
        sun = GetComponent<Light>();
    }
    public void ChangeTheLight()
    {
        sun.transform.Rotate(transform.up, 30);
    }
    public void DimTheLight()
    {
        sun.intensity -= 1;
    }
    public void BrightenTheLight()
    {
        sun.intensity += 1;
    }
}
