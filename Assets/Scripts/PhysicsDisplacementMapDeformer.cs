using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDisplacementMapDeformer : MonoBehaviour {

    private bool isUpdateRequired;
    public float collideRadius = .5f;
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
        deformedMat.SetTexture("_MainTex", deformTexture);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                int x = Mathf.RoundToInt(size * hit.textureCoord.x);
                int y = Mathf.RoundToInt(size * hit.textureCoord.y);
                deformTexture.SetPixel(x, y, Color.green);
                isUpdateRequired = true;
                Debug.Log("hit at " + x + " and " + y);
            }
       
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
