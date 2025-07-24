using UnityEngine;

public class StickManAIInfoFetcher : MonoBehaviour
{
    // this is how the stick man will get all of his info so he can balence

    [Header("Body Parts")]
    [SerializeField] private GameObject torso; // you can probably guess the rest
    [SerializeField] private GameObject waist;
    [SerializeField] private GameObject rightThigh;
    [SerializeField] private GameObject rightShin;
    [SerializeField] private GameObject leftThigh;
    [SerializeField] private GameObject leftShin;

    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the network that will be given all of this data

    // private info
    private Rigidbody2D torsoRb; // all of the rbs of the body parts we need
    private Rigidbody2D waistRb;

    private Rigidbody2D rightThighRb;
    private Rigidbody2D rightShinRb;

    private Rigidbody2D leftThighRb;
    private Rigidbody2D leftShinRb;

    void Start()
    {
        AssignAllRBs(); // assign all of the rbs
    }

    private void AssignAllRBs() {
        torsoRb = torso.GetComponent<Rigidbody2D>(); // assign all of the rigidbodies
        waistRb = waist.GetComponent<Rigidbody2D>();
        rightThighRb = rightThigh.GetComponent<Rigidbody2D>();
        rightShinRb = rightShin.GetComponent<Rigidbody2D>();
        leftThighRb = leftThigh.GetComponent<Rigidbody2D>();
        leftShinRb = leftShin.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GiveNeuralNetworkData(); // give the neural net work all of the data it needs
        neuralNetwork.HasGaveNewInputs(); // notify the network that we have given it new inputs
    }

    private void GiveNeuralNetworkData() {
        // this will give all of the data the neural network needs 

        InputLayer inputLayer = neuralNetwork.GetInputLayer(); // get the input layer

        inputLayer.SetActivationOnNode(torso.transform.rotation.z, 0); // input the rotation of the torso
        inputLayer.SetActivationOnNode(torsoRb.velocity.magnitude, 1); // input the speed of the torso

        inputLayer.SetActivationOnNode(waist.transform.rotation.z, 2); // input the rotation of the waist
        inputLayer.SetActivationOnNode(waistRb.velocity.magnitude, 3); // input the speed of the waist

        inputLayer.SetActivationOnNode(rightThigh.transform.rotation.z, 4); // input the rotation of the right thigh
        inputLayer.SetActivationOnNode(rightThighRb.velocity.magnitude, 5); // input the speed of the right thigh

        inputLayer.SetActivationOnNode(rightShin.transform.rotation.z, 6); // input the rotation of the right shin
        inputLayer.SetActivationOnNode(rightShinRb.velocity.magnitude, 7); // input the speed of the right shin

        inputLayer.SetActivationOnNode(leftThigh.transform.rotation.z, 8); // input the rotation of the left thing
        inputLayer.SetActivationOnNode(leftThighRb.velocity.magnitude, 9); // input the speed of the left thigh

        inputLayer.SetActivationOnNode(leftShin.transform.rotation.z, 10); // input the rotation of the left shin
        inputLayer.SetActivationOnNode(leftShinRb.velocity.magnitude, 11); // input the speed of the left shin
    }
}