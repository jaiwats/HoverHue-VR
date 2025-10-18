using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn_whisp : MonoBehaviour
{
    public GameObject whisp;
    private GameObject[] AllUncoloredBlocks;
    void Start()
    {
        StartCoroutine(WhipSpwan());

    }

    void Update()
    {
        AllUncoloredBlocks = GameObject.FindGameObjectsWithTag("Not_Colored");
    }

    private IEnumerator WhipSpwan()
    {
        yield return new WaitForSeconds(35.0f + StaticData.Difficulty);
        if (AllUncoloredBlocks.Length > 0)
        {
            Instantiate(whisp);
        }
    }
}
