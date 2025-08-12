using UnityEngine;

public class DogAIInfoFetcher : MonoBehaviour
{
    // this will give the neural network all of the info it needs

    [Header("All Body Part Refrences")]
    [SerializeField] private Rigidbody2D hindFrontThighRB; // these will be refrences to all of the body part RBs
    [SerializeField] private Rigidbody2D hindFrontShinRB;

    [SerializeField] private Rigidbody2D hindBackThighRB;
    [SerializeField] private Rigidbody2D hindBackShinRB;


    [SerializeField] private Rigidbody2D frontFrontThighRB;
    [SerializeField] private Rigidbody2D frontFrontShinRB;

    [SerializeField] private Rigidbody2D frontBackThighRB;
    [SerializeField] private Rigidbody2D fronBackShinRB;

    [Header("Other Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the neural network we will be giving all of this info to


    void Update()
    {
        GiveNeuralNetworkInfo(); // give the neural network all of the infomation
        neuralNetwork.HasGaveNewInputs(); // notify it that it has got all of the info
    }

    private void GiveNeuralNetworkInfo() {
        // this will assign each input layer node with the corresponding info

        InputLayer inputLayer = neuralNetwork.GetInputLayer(); // get the input layer

        int nodeIndex = 0;

        // Add rotation and speed for each body part
        inputLayer.SetActivationOnNode(hindFrontThighRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(hindFrontThighRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(hindFrontShinRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(hindFrontShinRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(hindBackThighRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(hindBackThighRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(hindBackShinRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(hindBackShinRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(frontFrontThighRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(frontFrontThighRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(frontFrontShinRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(frontFrontShinRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(frontBackThighRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(frontBackThighRB.velocity.magnitude, nodeIndex++);

        inputLayer.SetActivationOnNode(fronBackShinRB.rotation, nodeIndex++);
        inputLayer.SetActivationOnNode(fronBackShinRB.velocity.magnitude, nodeIndex++);
    }

}