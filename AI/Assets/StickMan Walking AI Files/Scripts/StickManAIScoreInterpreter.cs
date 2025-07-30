using UnityEngine;

public class StickManAIScoreInterpreter : MonoBehaviour
{
    // this is the script that will determen the score of the neural network
    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork; // the network of that the score we will change

    [SerializeField] private GameObject torso; // the head of the stick man

    private bool hasHitGround = false; // if he has hit the ground we no longer count his score

    void Update()
    {
        if(hasHitGround == false) { // if he has not hit the ground we reward him with his x velocity
        }
        neuralNetwork.AddToScore(torso.GetComponent<Rigidbody2D>().velocity.x); // we want the AI to lear to go to the right quickly so we reward it is torso x velocity
    }

    public void StickmanHasHitGround() {
        hasHitGround = true;
    }
}