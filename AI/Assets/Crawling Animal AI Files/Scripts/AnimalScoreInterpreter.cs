using UnityEngine;

public class AnimalScoreInterpreter : MonoBehaviour
{
    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network of the animal

    private Transform target; // the target the animal want to go to

    void Start()
    {
        target = GameObject.Find("Target").transform;
    }

    void Update()
    {
        neuralNetwork.AddToScore(-Vector2.Distance(target.position, transform.position)); // add to the anials score its distance to the target
        // we add here because we want it to be close to the target for the longest amount of time
    }
}