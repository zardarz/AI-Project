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
        neuralNetwork.AddToScore(head.transform.position.y); // we want the network to learn to balence so we want its y position to be high

        string colliderLayer = LayerMask.LayerToName(Physics2D.OverlapCircle(head.transform.position, 1).gameObject.layer); // conver the collided object to the layer name

        if(colliderLayer != "Body Part") { // if the head collided with something that is not a body part
            neuralNetwork.AddToScore(-10000); // it has lost
        }
    }
}