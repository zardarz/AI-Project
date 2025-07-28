using UnityEngine;

public class GroundDiscipliner : MonoBehaviour
{
    // this script will decrease the score of any stick man that touches the ground with a body part that is not his legs

    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if(collision.gameObject.name == "Right Shin" || collision.gameObject.name == "Left Shin") { // if the colliders name is right or left shin
            return; // we return since that is ok
        }

        
        GameObject stickmanGO = collision.transform.root.gameObject;

        if (stickmanGO.name == "Stick Man(Clone)(Clone)") { // if it is not the right or left shin and the top parent is called stick man
            stickmanGO.GetComponent<NeuralNetwork>().SetScore(float.NegativeInfinity); // we punish him a lot
        }
    }
}