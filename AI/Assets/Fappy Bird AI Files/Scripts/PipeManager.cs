using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject pipe; // the 2 pipes
    [SerializeField] private Transform spawnTransformOfPipes; // where the pipes will be spawning
    [SerializeField] private Transform deathTransformOfPipes; // where the pipe will despawn

    [SerializeField] private Transform pipeParent; // this is the parent transform of where the pipes will spawn so it is organized;

    [Header("Settings")]
    [SerializeField] private float pipeSpeed; // how fast the pipes will be moving
    [SerializeField] private float timeBetweenPipes; // how much time between each pipe spawn
    [SerializeField] private float pipeHeightVariation; // how much each pipe will change its y position

    [Header("Settings")]
    [SerializeField] private List<GameObject> currentPipes = new List<GameObject>(); // this will hold each pipe that is currently in the world

    // private info
    private float timeUntilSpawnPipe; //how long ago was the last pipe spawned

    void Update()
    {
        timeUntilSpawnPipe -= Time.deltaTime; // minus time from the last pipe spawn to make a timer

        if(timeUntilSpawnPipe <= 0) { // if the time until we spawn a pipe is 0 or less 
            SpawnPipe(); // we spawn the pipe
            timeUntilSpawnPipe = timeBetweenPipes; // and set the time until we spawn the next pipe to the time between pipes
        }

        DestroyPipesIfNeeded(); // destroy all the pipes that go past the death point
    }

    private void SpawnPipe() {
        GameObject pipeCopy = Instantiate(pipe, pipeParent); // copy the new pipe and set its parent to the pipe parent
        currentPipes.Add(pipeCopy); // add the pipe copy to the list of current pipes;

        pipeCopy.transform.position = new(spawnTransformOfPipes.position.x, Random.Range(-pipeHeightVariation, pipeHeightVariation), 0); // set the new pipe x pos to the spawn point of it and set the y pos to how ever much varience there is 

        pipeCopy.GetComponent<Rigidbody2D>().AddForce(new(-pipeSpeed, 0), ForceMode2D.Impulse); // add force so it moves to the left
    }

    private void DestroyPipesIfNeeded() {
        // this will destroy all of the pipes that go past the despawn pos of the pipes

        for(int i = 0; i < currentPipes.Count; i++) { // go foreach current pipe
            GameObject pipe = currentPipes[i]; // get the pipe

            if(pipe.transform.position.x <= deathTransformOfPipes.position.x) { // if the pipes x pos is less or equal to the despawn pos 
                currentPipes.Remove(pipe); // we remove it from the list
                i--; // minus one from i because we just removed an object from that list
                Destroy(pipe); // and destroy the pipe
            }
        }
    }

    public List<GameObject> GetCurrentPipes() {
        // returns a list of all of the pipe that currently exsist;
        return currentPipes;
    }
}