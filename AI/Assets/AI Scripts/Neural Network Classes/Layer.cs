using UnityEngine;

[System.Serializable]
public class Layer
{
    [SerializeField] private int amountOfNodes; // how many nodes are in this layer

    private Node[] nodes; // all of the nodes

    public Node[] GetNodes() {
        // returns all of the nodes in this layer
        return nodes;
    }

    public int GetAmountOfNodes() {
        // returns the amount of nodes in the layer
        return amountOfNodes;
    }

    public void MakeNodes() {
        nodes = new Node[amountOfNodes]; // fill the array with nulls

        for(int i = 0; i < amountOfNodes; i++) { // fill the array with nodes
            nodes[i] = new Node();
        }
    }
}