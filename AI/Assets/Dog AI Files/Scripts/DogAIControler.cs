using UnityEngine;

public class DogAIControler : MonoBehaviour
{
    [Header("All Body Parts")]

    [SerializeField] private Rigidbody2D hindFrontThighRB; // these will be refrences to all of the body part RBs
    [SerializeField] private Rigidbody2D hindFrontShinRB;

    [SerializeField] private Rigidbody2D hindBackThighRB;
    [SerializeField] private Rigidbody2D hindBackShinRB;


    [SerializeField] private Rigidbody2D frontFrontThighRB;
    [SerializeField] private Rigidbody2D frontFrontShinRB;

    [SerializeField] private Rigidbody2D frontBackThighRB;
    [SerializeField] private Rigidbody2D fronBackShinRB;

    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network of the dog

    [Header("Settings")]
    [SerializeField] private float strength; // the strength of the dog


    void Update()
    {
        if(neuralNetwork.isPreformingEvents()) { // if the neural network is doing all of the events
            AddTorqueToEachBodyPart(); // we add torque to each body part
        }
    }

    private void AddTorqueToEachBodyPart() {
        // this function will add torque to each body part based on the activation of the corresponding node

        OutputLayer outputLayer = neuralNetwork.GetOutputLayer(); // get the output layer

        int nodeIndex = 0;

        // Apply torque to each body part based on output node activations
        hindFrontThighRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));
        hindFrontShinRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));

        hindBackThighRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));
        hindBackShinRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));

        frontFrontThighRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));
        frontFrontShinRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));

        frontBackThighRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));
        fronBackShinRB.AddTorque(Mathf.Clamp(outputLayer.GetNode(nodeIndex++).GetActivation(), -strength, strength));
    }
}