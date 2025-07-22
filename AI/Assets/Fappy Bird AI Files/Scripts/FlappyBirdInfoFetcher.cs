using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdInfoFetcher : MonoBehaviour
{
    [SerializeField] private PipeManager pipeManager; // this is where we will get the info for the neural network
    [SerializeField] private NeuralNetwork neuralNetwork; // and this is the neural network

    private InputLayer inputLayer; // this is the input layer of the neural network

    void Start()
    {
        neuralNetwork = gameObject.GetComponent<NeuralNetwork>(); // get the neural network
        inputLayer = neuralNetwork.GetInputLayer(); // get the input layer
    }

    void Update()
    {
        GameObject closestPipe = GetClosestPipePosition(); // get the closest pipe

        float differenceInX = closestPipe.transform.position.x - transform.position.x; // find the difference in y and x
        float differenceInY = closestPipe.transform.position.y - transform.position.y;

        inputLayer.GetNodes()[0].SetActivation(differenceInX); // set the input nodes to be those differences
        inputLayer.GetNodes()[1].SetActivation(differenceInY);

        neuralNetwork.HasGaveNewInputs(); // notify the neural network that it has gotten new inputs
        print(closestPipe.transform.position);
    }

    private GameObject GetClosestPipePosition() {
        List<GameObject> pipes = pipeManager.GetCurrentPipes(); 
        GameObject closestPipe = null;

        for (int i = 0; i < pipes.Count; i++) {
            GameObject pipe = pipes[i];
            float pipeX = pipe.transform.position.x;
            float playerX = transform.position.x;

            if (pipeX > playerX) {
                if (closestPipe == null || pipeX < closestPipe.transform.position.x) {
                    closestPipe = pipe;
                }
            }
        }

        return closestPipe;
    }

}