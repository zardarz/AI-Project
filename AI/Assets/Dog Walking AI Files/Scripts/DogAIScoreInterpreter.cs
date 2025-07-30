using UnityEngine;

public class DogAIScoreInterpreter : MonoBehaviour
{
    // this script is the thing the will give the neural network its score
    [SerializeField] private NeuralNetwork neuralNetwork;

    void Update()
    {
        neuralNetwork.SetScore(transform.GetChild(0).position.x); // set the score to the x position of the heqd
    }
}