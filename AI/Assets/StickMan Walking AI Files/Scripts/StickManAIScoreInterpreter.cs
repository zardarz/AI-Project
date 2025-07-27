using UnityEngine;

public class StickManAIScoreInterpreter : MonoBehaviour
{
    // this is the script that will determen the score of the neural network
    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the network of that the score we will change

    private GameObject head; // the head of the stick man

    void Start()
    {
        head = transform.GetChild(0).gameObject; // get the head of the stick man
    }

    void Update()
    {
        neuralNetwork.AddToScore(head.transform.position.x); // we want the network to learn to walk so we reward it if it is going to the right
    }
}