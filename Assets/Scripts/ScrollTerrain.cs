using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTerrain : MonoBehaviour
{

    public GameObject TerrainPrefab;

    public float ScrollSpeed = 5;

    GameObject[,] TerrainTiles;

    float TerrainTileSize;

    void Start() {
        TerrainTileSize = 1f;
        TerrainTiles = new GameObject[3,3]; 
        for (int x = 0; x < 3; x++){
            for (int z = 0; z < 3; z++){
                TerrainTiles[x,z] = Instantiate(TerrainPrefab, new Vector3(x*TerrainTileSize,0,z*TerrainTileSize), Quaternion.identity) as GameObject;
                TerrainTiles[x,z].transform.Rotate(0, 0, 0);
                if (TerrainTileSize == 1f)
                    TerrainTileSize = TerrainTiles[x,z].GetComponent<Renderer>().bounds.size.x;
            }
        }  
        Debug.Log(TerrainTileSize);
    }

    // Update is called once per frame
    void Update()
    {
        var xDirection = 0;
        var zDirection = 0;
        if(Input.GetKey(KeyCode.A))
        {
            xDirection -= 1;
            //TerrainTiles.transform.Translate(-Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if(Input.GetKey(KeyCode.D))
        {
            xDirection += 1;
            //TerrainTiles.transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if(Input.GetKey(KeyCode.S))
        {
            zDirection -= 1;
            //TerrainTiles.transform.Translate(-Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
        }

        if(Input.GetKey(KeyCode.W))
        {
            zDirection +=1;
            //TerrainTiles.transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
        }
        
        for (int x = 0; x < 3; x++){
            for (int z = 0; z < 3; z++){
                TerrainTiles[x,z].transform.Translate(Vector3.forward * zDirection * Time.deltaTime * ScrollSpeed, Space.World);
                TerrainTiles[x,z].transform.Translate(Vector3.right * xDirection * Time.deltaTime * ScrollSpeed, Space.World);
                if (TerrainTiles[x,z].transform.position.x < transform.position.x - TerrainTileSize){
                    TerrainTiles[x,z].transform.Translate(Vector3.right * TerrainTileSize * 2, Space.World);
                }
                else if (TerrainTiles[x,z].transform.position.x > transform.position.x + TerrainTileSize){
                    TerrainTiles[x,z].transform.Translate(Vector3.right * -TerrainTileSize * 2, Space.World);
                }
                else if (TerrainTiles[x,z].transform.position.z < transform.position.z - TerrainTileSize){
                    TerrainTiles[x,z].transform.Translate(Vector3.forward * TerrainTileSize * 2, Space.World);
                }
                else if (TerrainTiles[x,z].transform.position.z > transform.position.z + TerrainTileSize){
                    TerrainTiles[x,z].transform.Translate(Vector3.forward * -TerrainTileSize * 2, Space.World);
                }
            }
        }
    }
}
