using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Whisp_States : MonoBehaviour
{
    public enum WhispStates { Find_Block, OvrrideBlock, HelpPlayer, Dissaper };
    public enum WhispEvents { Looking, FoundBlock, PlayerChoseYou, NoMoreBlocks };

    public float speed;
    private StateMachine stateMachine;
    private Renderer renderer;


    private GameObject[] AllUncoloredBlocks;
    private GameObject closestBlock;

    private GameObject[] playerObject;
    private GameObject closestPlayer;

    void OnEnable()
    {
        stateMachine = new StateMachine(new WhispStates(), new WhispEvents(), WhispStates.Find_Block);

        // States from find block
        stateMachine.AddEvent(WhispStates.Find_Block, WhispStates.OvrrideBlock, WhispEvents.FoundBlock);
        stateMachine.AddEvent(WhispStates.Find_Block, WhispStates.HelpPlayer, WhispEvents.PlayerChoseYou);
        stateMachine.AddEvent(WhispStates.Find_Block, WhispStates.Dissaper, WhispEvents.NoMoreBlocks);

        // States from OvrrideBlock
        stateMachine.AddEvent(WhispStates.OvrrideBlock, WhispStates.Find_Block, WhispEvents.Looking);
        stateMachine.AddEvent(WhispStates.OvrrideBlock, WhispStates.Dissaper, WhispEvents.NoMoreBlocks);

        // States from HelpPlayer
        stateMachine.AddEvent(WhispStates.HelpPlayer, WhispStates.Find_Block, WhispEvents.Looking);

        speed -= StaticData.Difficulty;
        if (speed < 0)
        {
            speed = 0;
        }
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        AllUncoloredBlocks = GameObject.FindGameObjectsWithTag("Not_Colored");
        playerObject = GameObject.FindGameObjectsWithTag("Player");

        switch (stateMachine.getState())
        {
            case WhispStates.Find_Block:
                if (AllUncoloredBlocks.Length == 0)
                {
                    stateMachine.HandleEvent(WhispEvents.NoMoreBlocks);
                }
                else
                {
                    closestBlock = ClosestUncolleredBlock();
                    this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, closestBlock.transform.position, step);
                }

                break;

            case WhispStates.OvrrideBlock:
                StartCoroutine(Stop_Continue());
                break;

            case WhispStates.HelpPlayer:
                closestPlayer = ClosestPlayer();
                renderer.sharedMaterial = closestPlayer.GetComponent<Renderer>().material;
                this.gameObject.tag = "Player";
                stateMachine.HandleEvent(WhispEvents.Looking);
                break;

            case WhispStates.Dissaper:
                this.gameObject.SetActive(false);
                break;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Not_Colored")
        {
            stateMachine.HandleEvent(WhispEvents.FoundBlock);
        }
        if (other.tag == "Player")
        {
            stateMachine.HandleEvent(WhispEvents.PlayerChoseYou);
        }
    }

    GameObject ClosestUncolleredBlock()
    {
        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;

        foreach (var block in AllUncoloredBlocks)
        {
            float distanceHere = Vector3.Distance(this.gameObject.transform.position, block.transform.position);
            if (block.activeSelf)
            {
                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestHere = block;
                }
            }
        }
        return closestHere;
    }

    GameObject ClosestPlayer()
    {
        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;

        foreach (var Player in playerObject)
        {
            float distanceHere = Vector3.Distance(this.gameObject.transform.position, Player.transform.position);
            if (Player.activeSelf)
            {
                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestHere = Player;
                }
            }
        }
        return closestHere;
    }

    private IEnumerator Stop_Continue()
    {
        yield return new WaitForSeconds(2.5f);
        stateMachine.HandleEvent(WhispEvents.Looking);
    }

}
