using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CanvasSpawner : MonoBehaviour
{
    public GameObject Block;
    private Vector3 spawnPosition;
    public int BlockLengthX;
    public int BlockLengthY;
    public int BlockLengthZ;
    public float Closefactor;
    private List<GameObject> spawnedBlocks = new List<GameObject>();

    void Start()
    {
        StartCoroutine(Canvas());
    }

    private IEnumerator Canvas()
    {
        spawnPosition = this.gameObject.transform.position;

        for (int x = 0; x <= BlockLengthX; x++)
        {
            for (int y = 0; y <= BlockLengthY; y++)
            {
                for (int z = 0; z <= BlockLengthZ; z++)
                {
                    GameObject obj = Instantiate(Block, spawnPosition + new Vector3(x * Closefactor, y * Closefactor, z * Closefactor), Quaternion.identity);
                    spawnedBlocks.Add(obj);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}
