using UnityEngine;

public class DogAIScoreInterpreter : MonoBehaviour
{
    // this script will determen the score of this dog

    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network on this dog

    [SerializeField] private Transform headTransform; // the transform of the dogs head

    void Update()
    {
        neuralNetwork.SetScore(headTransform.position.x); // we want the dog to go to the left so it get more points the more it is to the right
    }
}