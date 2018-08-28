using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDisplacementMapDeformer : MonoBehaviour {

    private bool isUpdateRequired;
    //public float collideRadius = .5f;
    public Texture2D deformTexture;
    public Material deformedMat;

    [Range(32,2048)]
    public int size;

    private void Start()
    {
        deformTexture = new Texture2D(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                deformTexture.SetPixel(i, j, Color.black);
            }
        }
        deformTexture.Apply();
        deformedMat.SetTexture("_DisplacementMap", deformTexture);
        //deformedMat.SetTexture("_MainTex", deformTexture);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            Vector3 posDiff = this.transform.position - contact.point;
            //posDiff = new Vector3(posDiff.x / this.transform.localScale.x, posDiff.y / this.transform.localScale.y, posDiff.z / this.transform.localScale.z);
            float u = Vector3.Dot(posDiff, this.transform.right);
            float v = Vector3.Dot(posDiff, this.transform.up);

            u = .5f + u / this.transform.localScale.x; 
            v = .5f * v / this.transform.localScale.y;

            int x = size - Mathf.RoundToInt(size * u);
            int y = size - Mathf.RoundToInt(size * v);

            deformTexture.SetPixel(x, y, Color.green);
            isUpdateRequired = true;
            Debug.Log("hit at " + x + " and " + y);

            //Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 2))
            //{
                
            //}
       
        }

    }
    private void Update()
    {
        if (isUpdateRequired)
        {
            deformTexture.Apply();
            isUpdateRequired = false;
        }
    }
}
