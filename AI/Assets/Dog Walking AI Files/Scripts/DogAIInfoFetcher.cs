using UnityEngine;

public class DogAIInfoFetcher : MonoBehaviour
{
    // this is how the dog will get all of its info so it can balance

    [Header("Body Parts")]
    [SerializeField] private GameObject hindFrontThigh;
    [SerializeField] private GameObject hindFrontShin;

    [SerializeField] private GameObject hindBackThigh;
    [SerializeField] private GameObject hindBackShin;

    [SerializeField] private GameObject frontFrontThigh;
    [SerializeField] private GameObject frontFrontShin;

    [SerializeField] private GameObject frontBackThigh;
    [SerializeField] private GameObject frontBackShin;

    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork;

    // Private Rigidbodies
    private Rigidbody2D hindFrontThighRb;
    private Rigidbody2D hindFrontShinRb;

    private Rigidbody2D hindBackThighRb;
    private Rigidbody2D hindBackShinRb;

    private Rigidbody2D frontFrontThighRb;
    private Rigidbody2D frontFrontShinRb;

    private Rigidbody2D frontBackThighRb;
    private Rigidbody2D frontBackShinRb;

    void Start()
    {
        AssignAllRBs(); // assign all of the rigidbodies
    }

    private void AssignAllRBs()
    {
        hindFrontThighRb = hindFrontThigh.GetComponent<Rigidbody2D>();
        hindFrontShinRb = hindFrontShin.GetComponent<Rigidbody2D>();

        hindBackThighRb = hindBackThigh.GetComponent<Rigidbody2D>();
        hindBackShinRb = hindBackShin.GetComponent<Rigidbody2D>();

        frontFrontThighRb = frontFrontThigh.GetComponent<Rigidbody2D>();
        frontFrontShinRb = frontFrontShin.GetComponent<Rigidbody2D>();

        frontBackThighRb = frontBackThigh.GetComponent<Rigidbody2D>();
        frontBackShinRb = frontBackShin.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GiveNeuralNetworkData();
        neuralNetwork.HasGaveNewInputs();
    }

    private void GiveNeuralNetworkData() {
        InputLayer inputLayer = neuralNetwork.GetInputLayer(); // get the input layer from the neural network

        // Hind Front Thigh
        inputLayer.SetActivationOnNode(hindFrontThigh.transform.rotation.z, 0); // rotation of hind front thigh
        inputLayer.SetActivationOnNode(hindFrontThighRb.velocity.magnitude, 1); // speed of hind front thigh

        // Hind Front Shin
        inputLayer.SetActivationOnNode(hindFrontShin.transform.rotation.z, 2); // rotation of hind front shin
        inputLayer.SetActivationOnNode(hindFrontShinRb.velocity.magnitude, 3); // speed of hind front shin

        // Hind Back Thigh
        inputLayer.SetActivationOnNode(hindBackThigh.transform.rotation.z, 4); // rotation of hind back thigh
        inputLayer.SetActivationOnNode(hindBackThighRb.velocity.magnitude, 5); // speed of hind back thigh

        // Hind Back Shin
        inputLayer.SetActivationOnNode(hindBackShin.transform.rotation.z, 6); // rotation of hind back shin
        inputLayer.SetActivationOnNode(hindBackShinRb.velocity.magnitude, 7); // speed of hind back shin

        // Front Front Thigh
        inputLayer.SetActivationOnNode(frontFrontThigh.transform.rotation.z, 8); // rotation of front front thigh
        inputLayer.SetActivationOnNode(frontFrontThighRb.velocity.magnitude, 9); // speed of front front thigh

        // Front Front Shin
        inputLayer.SetActivationOnNode(frontFrontShin.transform.rotation.z, 10); // rotation of front front shin
        inputLayer.SetActivationOnNode(frontFrontShinRb.velocity.magnitude, 11); // speed of front front shin

        // Front Back Thigh
        inputLayer.SetActivationOnNode(frontBackThigh.transform.rotation.z, 12); // rotation of front back thigh
        inputLayer.SetActivationOnNode(frontBackThighRb.velocity.magnitude, 13); // speed of front back thigh

        // Front Back Shin
        inputLayer.SetActivationOnNode(frontBackShin.transform.rotation.z, 14); // rotation of front back shin
        inputLayer.SetActivationOnNode(frontBackShinRb.velocity.magnitude, 15); // speed of front back shin
    }
}