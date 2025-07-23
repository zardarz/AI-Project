using System.Collections.Generic;

public class Node
{
    private List<Connection> fromConnections = new List<Connection>(); // all of the connections from the left
    private List<Connection> toConnections = new List<Connection>(); // all of the connections from the right

    private float bias; // the nodes bias

    private float activation; // how activated the node is

    public List<Connection> GetFromConnections() {
        // returns the from connections
        return fromConnections;
    }

    public List<Connection> GetToConnections() {
        // returns the to connections
        return toConnections;
    }

    public void AddConnectionToFromConnections(Connection connectionToAdd) {
        // adds a connection to the from connections
        fromConnections.Add(connectionToAdd);
    }

    public void AddConnectionToToConnections(Connection connectionToAdd) {
        // adds a connection to the to connections
        toConnections.Add(connectionToAdd);
    }

    public float GetBias() {
        // returns the nodes bias
        return bias;
    }

    public void SetBias(float newBias) {
        // sets a new bias
        bias = newBias;
    }

    public void AddToBias(float biasToAdd) {
        // this adds a number to the bias
        
        bias += biasToAdd;
    }

    public void SetActivation(float newActivation) {
        // sets the activation to somthing new
        activation = newActivation;
    }

    public void AddToActivation(float addedActivation) {
        // adds some activation to the node
        activation += addedActivation;
    }

    public float GetActivation() {
        // returns the activation of the node
        return activation;
    }
}