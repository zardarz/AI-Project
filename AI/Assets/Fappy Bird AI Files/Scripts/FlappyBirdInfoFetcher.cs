using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdInfoFetcher : MonoBehaviour
{
    [SerializeField] private NeuralNetwork neuralNetwork; // and this is the neural network

    private InputLayer inputLayer; // this is the input layer of the neural network

    private PipeManager pipeManager; // this is where we are going to get information about the positions of the pipes

    void Start()
    {
        neuralNetwork = gameObject.GetComponent<NeuralNetwork>(); // get the neural network
        inputLayer = neuralNetwork.GetInputLayer(); // get the input layer
        pipeManager = GameObject.Find("Pipes Manager").GetComponent<PipeManager>(); // get the pipe manager
    }

    void Update()
    {
        GameObject closestPipe = GetClosestPipePosition(); // get the closest pipe
        if(closestPipe == null) return; // if we dont have the closest pipe we dont do any of the stuff

        float differenceInX = closestPipe.transform.position.x - transform.position.x; // find the difference in y and x
        float differenceInY = closestPipe.transform.position.y - transform.position.y;

        inputLayer.GetNodes()[0].SetActivation(differenceInX); // set the input nodes to be those differences
        inputLayer.GetNodes()[1].SetActivation(differenceInY);

        neuralNetwork.HasGaveNewInputs(); // notify the neural network that it has gotten new inputs
    }

    private GameObject GetClosestPipePosition() {
        // this will return the pipe that is the closest to the player and that is to the right of the player

        List<GameObject> pipes = pipeManager.GetCurrentPipes(); // get all of the pipes
        GameObject closestPipe = null; // make a container for the closest pipe

        for (int i = 0; i < pipes.Count; i++) { // go for each pipe
            GameObject pipe = pipes[i]; // get the pipe
            float pipeX = pipe.transform.position.x; // get the pipes x position
            float playerX = transform.position.x; // get the player x position

            if (pipeX > playerX) { // if the pipes x position is greater than the players that means it is to the right
                if (closestPipe == null || pipeX < closestPipe.transform.position.x) { // and if the closest pipe = null or the closest pipe is farther than this pipe
                    closestPipe = pipe; // the closest pipe is this pipe
                }
            }
        }

        return closestPipe; // return the closest pipe
    }

}