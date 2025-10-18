using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIManger : MonoBehaviour
{
    public TMP_Text RainbowState;
    public TMP_Text Difficulty;
    public TMP_Text Number_of_blocks;
    private GameObject[] AllUncoloredBlocks;
    private int number;


    void Start()
    {
        if (StaticData.Difficulty == 0)
        {
            Difficulty.text = "<color=yellow>Difficulty: Medium</color>";
        }
        if (StaticData.Difficulty > 0)
        {
            Difficulty.text = "<color=green>Difficulty: Easy</color>";
        }
        if (StaticData.Difficulty < 0)
        {
            Difficulty.text = "<color=red>Difficulty: Hard</color>";
        }
    }

    void Update()
    {
        AllUncoloredBlocks = GameObject.FindGameObjectsWithTag("Not_Colored");
        number = AllUncoloredBlocks.Length;
        Number_of_blocks.text = "Number of blocks: " + number.ToString();

        RainbowState.text = "Rainbow: " + StaticData.StatesOfWhisps;
    }
}
