using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private InputLayer inputLayer; // all of the inputs the network will get
    [SerializeField] private List<Layer> hiddenLayers; // all of the hidden layer the network has
    [SerializeField] private OutputLayer outputLayer; // the events the network can preform

    private bool gotNewInputs = false; // this tell the neural network if it can propagate

    void Start()
    {
        MakeAllNodes(); // make all of the nodes for the network
        MakeAllConnections(); // make all of the network connections

        AddWeightsAndBias(5f);

    }

    private void MakeAllNodes() {
        inputLayer.MakeNodes(); // make the input layer nodes

        for(int i = 0; i < hiddenLayers.Count; i++) { // make the hidden layer nodes
            hiddenLayers[i].MakeNodes();
        }

        outputLayer.MakeNodes(); // make the output layer nodes
    }

    private void MakeAllConnections() {
        // this will make all of connections for each layer 

        // starting with the input layer
        ConnectLayers(inputLayer, hiddenLayers[0]);

        // now time for the connections inbetween the hidden layers
        // we will do all of the hidden layers exept for the last one because that is connected to the output layer
        
        for(int hiddenLayerIndex = 0; hiddenLayerIndex < hiddenLayers.Count - 1; hiddenLayerIndex++) { // go foreach hidden layer
            Layer hiddenLayer = hiddenLayers[hiddenLayerIndex]; // get the hidden layer
            Layer nextHiddenLayer = hiddenLayers[hiddenLayerIndex + 1]; // the layer we need to connect to

            ConnectLayers(hiddenLayer, nextHiddenLayer);
        }

        // lastly time for the last connection between the last hidden layer and the output layer
        ConnectLayers(hiddenLayers[hiddenLayers.Count - 1], outputLayer);
    }

    private void MakeConnectionBetween(Node fromNode, Node toNode) {
        Connection newConnection = new Connection(fromNode, toNode); // make a new connection

        fromNode.AddConnectionToToConnections(newConnection); // add that connection to the from node
        toNode.AddConnectionToFromConnections(newConnection); // and the to node
    }

    private void ConnectLayers(Layer fromLayer, Layer toLayer) {
        // this will just connect 2 layers with all of the connections they need

        for(int fromLayerNodeIndex = 0; fromLayerNodeIndex < fromLayer.GetAmountOfNodes(); fromLayerNodeIndex++) { // go foreach from layer node
            Node fromLayerNode = fromLayer.GetNodes()[fromLayerNodeIndex]; // get the node

            for(int toLayerNodeIndex = 0; toLayerNodeIndex < toLayer.GetAmountOfNodes(); toLayerNodeIndex++) { // now go for each to layer node
                Node toLayerNode = toLayer.GetNodes()[toLayerNodeIndex]; // get teh node

                MakeConnectionBetween(fromLayerNode, toLayerNode); // make the connection
            }
        }
    }

    void Update()
    {
        if(gotNewInputs) { // if we have new inputs
            PropagateAllLayers(); // we propagte the activations
            outputLayer.PreformMostActivationEvent(); // and preform the action with the most activation
        }
    }

    public void PropagateAllLayers() {
        // propagating is just sending all of the activation value in the correct way through the network

        // So we will first propagate the input layer first
        Propagate(inputLayer);

        // then the hidden layers
        for(int i = 0; i < hiddenLayers.Count; i++) {
            Propagate(hiddenLayers[i]);
        }

        // we dont propagete the ouput layer

        gotNewInputs = false; // but now we need new inputs
    }

    private void Propagate(Layer layerToPropate) {
        for(int i = 0; i < layerToPropate.GetNodes().Length; i++) { // go foreach input layer node
            Node node = layerToPropate.GetNodes()[i]; // get the node
            List<Connection> connections = node.GetToConnections(); // get the nodes connections

            for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++) { // go for each connection
                Connection connection = connections[connectionIndex]; // get the connection

                float nodeActivation = node.GetActivation(); // get the node activation
                float newActivation = nodeActivation * connection.GetWeight(); // get how much activation we should add to the to node

                Node toNode = connection.GetToNode(); // get the to node
                toNode.SetActivation(newActivation); // add the activation
                toNode.AddToActivation(toNode.GetBias()); // add the toNodes bias
            }
        }
    }

    public InputLayer GetInputLayer() {
        // returns the input layer

        // this is for the info fetcher
        return inputLayer;
    }

    public void AddWeightsAndBias(float maxAdded) {
        // this will add some random number to each weight and bias

        // first the input layer
        // we only add it to the connections because the input layer nodes dont affect the activation

        for(int inputLayerNodeIndex = 0; inputLayerNodeIndex < inputLayer.GetAmountOfNodes(); inputLayerNodeIndex++) { // go for each input layer node
            List<Connection> connections = inputLayer.GetNodes()[inputLayerNodeIndex].GetToConnections(); // get the nodes connections

            AddWeights(connections, maxAdded);
        }


        // now the hidden layers

        for(int hiddenLayerIndex = 0; hiddenLayerIndex < hiddenLayers.Count; hiddenLayerIndex++) { // go for each hidden layer
            Node[] nodes = hiddenLayers[hiddenLayerIndex].GetNodes(); // get the nodes

            AddBiases(nodes, maxAdded); // add the biases to the nodes

            for(int hiddenLayerNodeIndex = 0; hiddenLayerNodeIndex < nodes.Length; hiddenLayerNodeIndex++) { // go for each hidden node
                List<Connection> connections = nodes[hiddenLayerNodeIndex].GetToConnections(); // get all of the connections

                AddWeights(connections, maxAdded); // add the weights
            }
        }

    }

    private void AddBiases(Node[] nodes, float maxAdded) {

        for(int nodeIndex = 0; nodeIndex < nodes.Length; nodeIndex++) { // go for each connection
            Node node = nodes[nodeIndex]; // get the connection

            node.AddToBias(Random.Range(0f, maxAdded)); // add the weight
        }
    }

    private void AddWeights(List<Connection> connections, float maxAdded) {
        // this will add weights to the list of connections

        for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++) { // go for each connection
            Connection connection = connections[connectionIndex]; // get the connection

            connection.AddToWeight(Random.Range(0f, maxAdded)); // add the weight
        }
    }

    public void HasGaveNewInputs() {
        gotNewInputs = true; // this runs when we have gotten new inputs
    }

}