

public class Connection
{
    private Node fromNode; // the node it is connecting to from the left
    private Node toNode; // the node it is connecting to from the right

    private float weight; // weight of connection

    public Connection(Node fromNode, Node toNode) {
        this.fromNode = fromNode;
        this.toNode = toNode;
    }

    public Node GetFromNode() {
        // returns the from node
        return fromNode;
    }

    public Node GetToNode() {
        // returns the to node
        return toNode;
    }

    public float GetWeight() {
        // returns the weight of connection
        return weight;
    }

    public void SetWeight(float newWeight) {
        // changes wegith to the new weight
        weight = newWeight;
    }

    public void AddToWeight(float weightToAdd) {
        // adds a weight to the current weight
        weight += weightToAdd;
    }
}