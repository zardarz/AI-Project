using UnityEngine;

public class DogAIController : MonoBehaviour
{
    [Header("All of the Rigidbodies")]
    [SerializeField] private Rigidbody2D hindFrontThigh; // all of the rigidbodies of the legs the dog can controll
    [SerializeField] private Rigidbody2D hindFrontShin;

    [SerializeField] private Rigidbody2D hindBackThigh;
    [SerializeField] private Rigidbody2D hindBackShin;


    [SerializeField] private Rigidbody2D frontFrontThigh;
    [SerializeField] private Rigidbody2D frontFrontShin;

    [SerializeField] private Rigidbody2D frontBackThigh;
    [SerializeField] private Rigidbody2D fronBackShin;

    [Header("Settings")]
    [SerializeField] private float maxStrength;

    [Header("Refrences")]
    [SerializeField] private NeuralNetwork neuralNetwork;


    void Start()
    {
        if(neuralNetwork.isPreformingEvents()) { // if the neural network is preforming the events
            ApplyTorqueToLimbs();  // we apply torque to the limbs
        }
    }

    private void ApplyTorqueToLimbs() {
        // this will add torque to each limb according to the coressponding nodes activation

        OutputLayer outputLayer = neuralNetwork.GetOutputLayer(); // get the output layer

        ApplyTorqueHindFrontThigh(outputLayer.GetNode(0).GetActivation()); // apply torque to each limb
        ApplyTorqueHindFrontShin(outputLayer.GetNode(1).GetActivation());

        ApplyTorqueHindBackThigh(outputLayer.GetNode(2).GetActivation());
        ApplyTorqueHindBackShin(outputLayer.GetNode(3).GetActivation());

        ApplyTorqueFrontFrontThigh(outputLayer.GetNode(4).GetActivation());
        ApplyTorqueFrontFrontShin(outputLayer.GetNode(5).GetActivation());

        ApplyTorqueFrontBackThigh(outputLayer.GetNode(6).GetActivation());
        ApplyTorqueFrontBackShin(outputLayer.GetNode(7).GetActivation());

    }


    public void ApplyTorqueHindFrontThigh(float torque) // all of the functions for adding torque to each limb
    {
        hindFrontThigh.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueHindFrontShin(float torque)
    {
        hindFrontShin.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueHindBackThigh(float torque)
    {
        hindBackThigh.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueHindBackShin(float torque)
    {
        hindBackShin.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueFrontFrontThigh(float torque)
    {
        frontFrontThigh.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueFrontFrontShin(float torque)
    {
        frontFrontShin.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueFrontBackThigh(float torque)
    {
        frontBackThigh.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

    public void ApplyTorqueFrontBackShin(float torque)
    {
        fronBackShin.AddTorque(Mathf.Clamp(torque, -maxStrength, maxStrength));
    }

}