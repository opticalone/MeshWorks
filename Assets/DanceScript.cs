using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour {

    public Animator anim;
    public bool didSetDance;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        buttonChangeDance();
        if (Input.GetKey(KeyCode.Space))
        {
            setChangeDance();
        }
        else { anim.SetBool("changeDance", false); }
	}

    public void buttonChangeDance()
    {
        if(didSetDance)
        {
            anim.SetBool("changeDance", true);
        }

    }


    public void setChangeDance()
    {
        
        anim.SetBool("changeDance", true);
        Debug.LogError("change bitch");
 
        //StartCoroutine(ChangeBack());
    }

    IEnumerator ChangeBack()
    {
        
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("changeDance", false);
    }

}
