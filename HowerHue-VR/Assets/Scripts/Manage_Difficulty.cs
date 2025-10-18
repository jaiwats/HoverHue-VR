using UnityEngine;

public class Manage_Difficulty : MonoBehaviour
{
    private GameObject[] RedCubes;
    private GameObject[] BlueCubes;
    void Update()
    {
        RedCubes = GameObject.FindGameObjectsWithTag("Red");
        BlueCubes = GameObject.FindGameObjectsWithTag("Blue");
        StaticData.Difficulty = BlueCubes.Length - RedCubes.Length;
    }
}
