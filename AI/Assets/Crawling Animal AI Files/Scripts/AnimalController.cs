using UnityEngine;

public class AnimalController : MonoBehaviour
{
    // this will let the animal control it self

    [SerializeField] private Rigidbody2D rb; // the animals rb

    private OutputLayer outputLayer; // the output layer of the neural network

    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network

    [SerializeField] private float maxSpeed; // the max speed the animal can go
    [SerializeField] private float maxTurnSpeed; // the max turn speed the animal can go

    void Start()
    {
        outputLayer = neuralNetwork.GetOutputLayer(); // get the output layer
    }


    void Update()
    {
        if(neuralNetwork.isPreformingEvents()) { // if the neural network is preforming the events
            //MoveAnimal(); // we move the animal
        }
    }

    private void MoveAnimal() {
        // this will move the animal based on the outputs of the neural network

        rb.MovePosition(rb.position + (Vector2) transform.up * Mathf.Clamp(outputLayer.GetNode(0).GetActivation(), -maxSpeed, maxSpeed)); // move the animal forward based on the output of the first node

        rb.MoveRotation(rb.rotation + Mathf.Clamp(outputLayer.GetNode(1).GetActivation(), -maxTurnSpeed, maxTurnSpeed)); // rotate the animal based on the output of it
    }

    public void TurnLeft() {
        rb.MoveRotation(rb.rotation - maxTurnSpeed); // make the animal turn left
    }

    public void TurnRight() {
        rb.MoveRotation(rb.rotation + maxTurnSpeed); // make the animal turn right
    }

    public void MoveForward() {
        rb.MovePosition(rb.position + (Vector2) transform.up * maxSpeed); // make the animal move forward
    }
}