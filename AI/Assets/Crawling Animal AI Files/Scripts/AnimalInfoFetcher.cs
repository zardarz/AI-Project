using UnityEngine;

public class AnimalInfoFetcher : MonoBehaviour
{
    // this will get the info for the neural network

    private Transform target; // the target the neural network must follow

    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network of the animal

    private InputLayer inputLayer; // input layer of the neural network


    void Start()
    {
        inputLayer = neuralNetwork.GetInputLayer(); // get the input layer
        target = GameObject.Find("Target").transform; // get the target
    }

    void Update()
    {
        GiveNewInputs(); // give the neural network the new inputs
        neuralNetwork.HasGaveNewInputs(); // notify it that it has new inputs
    }

    private void GiveNewInputs() {
        // this will give the neural network new info

        Vector2 differenceVector = transform.position - target.position; // the difference in positions of the animal and the target

        inputLayer.SetActivationOnNode(differenceVector.x, 0); // give it the difference vector
        inputLayer.SetActivationOnNode(differenceVector.y, 1);

        inputLayer.SetActivationOnNode(transform.rotation.z, 2); // give it the rotation

    }
}