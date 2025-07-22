using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OutputLayer : Layer
{
    [SerializeField] private UnityEvent[] nodeEvents; // this is what functions are run based on the activation on the corresponding node

    public void PreformMostActivationEvent() {
        // this will preform the most activated nodes event

        Node[] nodes = GetNodes();
        int amountOfNodes = GetAmountOfNodes();

        float mostActivation = nodes[0].GetActivation(); // stores the highest activation
        int mostActivationIndex = 0; //  stores the index of the highest activation

        for(int i = 0; i < amountOfNodes; i++) { // go for each nod
            if(nodes[i].GetActivation() > mostActivation) { // if the nodes activation is more than the previous most
                mostActivation = nodes[i].GetActivation(); // set the most activation to this nodes activation
                mostActivationIndex = i; // set the most activation index to the current index
            }
        }

        // by the end we have the most activaion index so we preform that event
        nodeEvents[mostActivationIndex].Invoke();
    }
}