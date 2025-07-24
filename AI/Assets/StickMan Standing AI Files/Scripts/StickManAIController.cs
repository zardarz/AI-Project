using UnityEngine;

public class StickManAIControler : MonoBehaviour
{
    // this script will let the neural network control the body of the stick man

    [Header("Body Part Rigidbodies")] // all of the rigidbodies of the body parts
    [SerializeField] private Rigidbody2D torsoRb;
    [SerializeField] private Rigidbody2D waistRb;

    [SerializeField] private Rigidbody2D rightThighRb;
    [SerializeField] private Rigidbody2D rightShinRb;
    
    [SerializeField] private Rigidbody2D leftThighRb;
    [SerializeField] private Rigidbody2D leftShinRb;

    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network that will controll these rbs

    // private info
    private OutputLayer outputLayer; // the output layer of the network

    [Header("Settings")]
    [SerializeField] private float minTorque; // the min and max torque the ai can add to its body parts
    [SerializeField] private float maxTorque;

    void Start()
    {
        outputLayer = neuralNetwork.GetOutputLayer(); // get the output layer
    }

    void Update()
    {
        if(neuralNetwork.isPreformingEvents()) { // if the network is preforming its events we add torque to the body parts based on the activation of the output layer nodes
            AddTorqueToBodyParts();
        }
    }


    private void AddTorqueToBodyParts() {
        // this will add torque to each body part based on the activation of the output layer

        AddTorqueToTorso(Mathf.Clamp(outputLayer.GetNode(0).GetActivation(), minTorque, maxTorque)); // add torque to each body part while clamping it based on the activation of the corresponding node in the output layer
        AddTorqueToWaist(Mathf.Clamp(outputLayer.GetNode(1).GetActivation(), minTorque, maxTorque));

        AddTorqueToRightThigh(Mathf.Clamp(outputLayer.GetNode(2).GetActivation(), minTorque, maxTorque));
        AddTorqueToRightShin(Mathf.Clamp(outputLayer.GetNode(3).GetActivation(), minTorque, maxTorque));

        AddTorqueToLeftThigh(Mathf.Clamp(outputLayer.GetNode(4).GetActivation(), minTorque, maxTorque));
        AddTorqueToLeftShin(Mathf.Clamp(outputLayer.GetNode(5).GetActivation(), minTorque, maxTorque));
    }


    private void AddTorqueToTorso(float torqueStrength) { // all of the functions to add torque to a body part
        torsoRb.AddTorque(torqueStrength);
    }

    private void AddTorqueToWaist(float torqueStrength) {
        waistRb.AddTorque(torqueStrength);
    }

    private void AddTorqueToRightThigh(float torqueStrength) {
        rightThighRb.AddTorque(torqueStrength);
    }

    private void AddTorqueToRightShin(float torqueStrength) {
        rightShinRb.AddTorque(torqueStrength);
    }

    private void AddTorqueToLeftThigh(float torqueStrength) {
        leftThighRb.AddTorque(torqueStrength);
    }

    private void AddTorqueToLeftShin(float torqueStrength) {
        leftShinRb.AddTorque(torqueStrength);
    }
}