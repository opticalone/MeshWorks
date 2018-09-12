using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDisplacementMapDeformer : MonoBehaviour {

    private bool isUpdateRequired;
    //public float collideRadius = .5f;
    public Texture2D deformTexture;
    public Material deformedMat;

    public Collider coll;

    [Range(8,2048)]
    public int size;

    private void Start()
    {
        coll = GetComponent<Collider>();
        deformTexture = new Texture2D(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                deformTexture.SetPixel(i, j, Color.black);
            }
        }
        //deformTexture.wrapMode = TextureWrapMode.Clamp;
        deformTexture.Apply();
        deformedMat.SetTexture("_DisplacementMap", deformTexture);
        //deformedMat.SetTexture("_MainTex", deformTexture);
    }
    private void OnCollisionEnter(Collision collision)
    {
        DeformIt(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        DeformIt(collision);

    }
    void DeformIt(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            // figure out where to raycast from
            Vector3 raycastDirection = (transform.position - contact.otherCollider.transform.position).normalized;
            Vector3 raycastOrigin = contact.point + -raycastDirection;
            Ray ray = new Ray(raycastOrigin, raycastDirection);

            Debug.LogFormat("Origin: {0} + Direction: {1}", raycastOrigin, raycastDirection);

            var hits = Physics.RaycastAll(ray, 2.0f);
            foreach (var hit in hits)
            {
                // ignore anything that isn't me
                if (hit.collider != coll) { continue; }

                var roughLoc = (hit.textureCoord * size);

                int x = 1 - (size - Mathf.RoundToInt(roughLoc.x) + 1);
                int y = 1 - (size - Mathf.RoundToInt(roughLoc.y) + 1);



                deformTexture.SetPixel(x, y, Color.green);
                isUpdateRequired = true;
                Debug.LogError("hit at " + x + " and " + y);
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
