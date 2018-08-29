using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillME : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KillMePlz());	
	}

    IEnumerator KillMePlz()
    {

        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
