using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformTerrain : MonoBehaviour
{
    Camera cam;

    int circleRadius = 2;

    void Start()
    {
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0.5F));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            var deformingMesh = hit.transform.gameObject.GetComponent<MeshFilter>().mesh;
            var originalVertices = deformingMesh.vertices;
            var displacedVertices = new Vector3[originalVertices.Length];
            for (int i = 0; i < originalVertices.Length; i++) {
                displacedVertices[i] = originalVertices[i];
                if (Math.Pow(Math.Pow(displacedVertices[i].x-hit.point.x,2) + Math.Pow(displacedVertices[i].z-hit.point.z,2), 0.5f) < circleRadius){
                    displacedVertices[i].y += 1;
                }
            }
            

            deformingMesh.vertices = displacedVertices;
            deformingMesh.RecalculateNormals();
        
            print("I'm looking at " + hit.transform.name);
        }
       
    }
}
