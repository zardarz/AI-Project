using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NeuralNetwork : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private InputLayer inputLayer; // all of the inputs the network will get
    [SerializeField] private List<Layer> hiddenLayers; // all of the hidden layer the network has
    [SerializeField] private OutputLayer outputLayer; // the events the network can preform

    private bool gotNewInputs = false; // this tell the neural network if it can propagate

    [Header("Extra Info")]
    [SerializeField] private float score;

    private bool preformingEvents = false; // this will tell if the network is currently preform the events in the output layer

    void Awake()
    {
        MakeAllNodes(); // make all of the nodes for the network
        MakeAllConnections(); // make all of the network connections
    }

    private void MakeAllNodes() {
        inputLayer.MakeNodes(); // make the input layer nodes

        for(int i = 0; i < hiddenLayers.Count; i++) { // make the hidden layer node 
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
            Node fromLayerNode = fromLayer.GetNode(fromLayerNodeIndex); // get the node

            for(int toLayerNodeIndex = 0; toLayerNodeIndex < toLayer.GetAmountOfNodes(); toLayerNodeIndex++) { // now go for each to layer node
                Node toLayerNode = toLayer.GetNode(toLayerNodeIndex); // get teh node

                MakeConnectionBetween(fromLayerNode, toLayerNode); // make the connection
            }
        }
    }

    void Update()
    {
        preformingEvents = false; // we are now not preforming events

        if(gotNewInputs) { // if we have new inputs
            PropagateAllLayers(); // we propagte the activations
            outputLayer.PreformMostActivationEvent(); // and preform the action with the most activation
            preformingEvents = true; // we are now preforming events
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
            Node node = layerToPropate.GetNode(i); // get the node
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
            List<Connection> connections = inputLayer.GetNode(inputLayerNodeIndex).GetToConnections(); // get the nodes connections

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

            float biasToAdd = Random.Range(-maxAdded, maxAdded);

            node.AddToBias(biasToAdd); // add the weight
        }
    }

    private void AddWeights(List<Connection> connections, float maxAdded) {
        // this will add weights to the list of connections

        for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++) { // go for each connection
            Connection connection = connections[connectionIndex]; // get the connection

            float weightToAdd = Random.Range(-maxAdded, maxAdded);

            connection.AddToWeight(weightToAdd); // add the weight
        }
    }

    public void LoadWeightsAndBiases(string weightAndBiasesString) {
        // this will take a string that contains all of the weights and biases and load then into this network

        string[] listOfWeightsAndBiasesString = weightAndBiasesString.Split("\n"); // split the weights and biases based on enters because that is how the file is formated
        float[] weightsAndBiaes = new float[listOfWeightsAndBiasesString.Length]; // now we need a new array to hold all of weights and biases

        for(int i = 0; i < weightsAndBiaes.Length - 1; i++) { // go for each weight and bias. -1 because the last line is just a "\n"
            string weightOrBiasString = listOfWeightsAndBiasesString[i]; // get the string float

            weightsAndBiaes[i] = float.Parse(weightOrBiasString); // parse it into a float and put it in the list
        }

        int weightsAndBiaesIndex = 0; // this will keep track of what line of the weights and biases we are on

        // now we have a list of all of the weights and biases for the network
        // now we load them

        // starting with the connections from the input layer to the first hidden layer

        for(int inputLayerNodeIndex = 0; inputLayerNodeIndex < inputLayer.GetAmountOfNodes(); inputLayerNodeIndex++) { // go for each input layer node
            Node inputLayerNode = inputLayer.GetNode(inputLayerNodeIndex); // get the node
            List<Connection> inputLayerNodeConnections = inputLayerNode.GetToConnections(); // get the nodes connections

            for(int inputLayerNodeConnectionIndex = 0; inputLayerNodeConnectionIndex < inputLayerNodeConnections.Count; inputLayerNodeConnectionIndex++) { // go for each connection
                Connection inputLayerNodeConnection = inputLayerNodeConnections[inputLayerNodeConnectionIndex]; // get the connection

                inputLayerNodeConnection.SetWeight(weightsAndBiaes[weightsAndBiaesIndex]); // set the connections weight to the weight in the list
                weightsAndBiaesIndex++; // increment the weights and biases index
            }
        }

        // now time for the hidden layers nodes and connections

        for(int hiddenLayerIndex = 0; hiddenLayerIndex < hiddenLayers.Count; hiddenLayerIndex++) { // go for each hidden layer
            Layer hiddenLayer = hiddenLayers[hiddenLayerIndex]; // get the layer
            Node[] hiddenLayerNodes = hiddenLayer.GetNodes(); // get all of the nodes

            for(int hiddenLayerNodeIndex = 0; hiddenLayerNodeIndex < hiddenLayerNodes.Length; hiddenLayerNodeIndex++) { // go for each node
                Node hiddenLayerNode = hiddenLayerNodes[hiddenLayerNodeIndex]; // get the node

                hiddenLayerNode.SetBias(weightsAndBiaes[weightsAndBiaesIndex]); // set the bias
                weightsAndBiaesIndex++; // increment the weights and biases index
            }

            // the way to weights and biases are organized are in a way that it goes -> Connections , nodes , connections , nodes
            // so that means we have to do all of the nodes first before doing the connections

            for(int hiddenLayerNodeIndex = 0; hiddenLayerNodeIndex < hiddenLayerNodes.Length; hiddenLayerNodeIndex++) { // go for each node
                Node hiddenLayerNode = hiddenLayerNodes[hiddenLayerNodeIndex]; // get the node
                List<Connection> hiddenLayerNodeConnections = hiddenLayerNode.GetToConnections(); // get all of the connections

                for(int hiddenLayerNodeConnectionIndex = 0; hiddenLayerNodeConnectionIndex < hiddenLayerNodeConnections.Count; hiddenLayerNodeConnectionIndex++) { // go for each hidden layer node connection
                    Connection hiddenLayerNodeConnection = hiddenLayerNodeConnections[hiddenLayerNodeConnectionIndex]; // get the connection

                    hiddenLayerNodeConnection.SetWeight(weightsAndBiaes[weightsAndBiaesIndex]); // set the weight
                    weightsAndBiaesIndex++; // increment the weights and biases index
                }

            }
        }

    }

    public string GetWeightsAndBiases() {
        // this will return a string of all of the networks weights and biases 

        // this is how the weights and biases will be formated
        // it will first have all of the weights of the connections from the input layer to the first hidden layer
        // then all of the nodes and connections between and hidden layers 
        // then the connections from the last hidden layer to the output layer

        string weightsAndBiases = "";

        // first get all of the weights of the connections from the input layer to the first hidden layer

        for(int inputLayerNodeIndex = 0; inputLayerNodeIndex < inputLayer.GetAmountOfNodes(); inputLayerNodeIndex++) { // go foreach input layer node
            List<Connection> connections = inputLayer.GetNode(inputLayerNodeIndex).GetToConnections(); // get all of the connections

            weightsAndBiases += GetAllWeightsFromConnections(connections); // add all of the weights to the weights and biases
        }

        // now for all of the hidden layers nodes and connections
        for(int hiddenLayerIndex = 0; hiddenLayerIndex < hiddenLayers.Count; hiddenLayerIndex++) { // go for each hidden layer
            Layer hiddenLayer = hiddenLayers[hiddenLayerIndex]; // get the layer
            Node[] hiddenLayerNodes = hiddenLayer.GetNodes(); // get all of the nodes

            weightsAndBiases += GetAllBiasesFromNodes(hiddenLayerNodes); // add all of the biases to the weights and biases string

            for(int hiddenLayerNodeIndex = 0; hiddenLayerNodeIndex < hiddenLayerNodes.Length; hiddenLayerNodeIndex++) { // go for each hidden layer node
                Node hiddenLayerNode = hiddenLayerNodes[hiddenLayerNodeIndex]; // get the node
                List<Connection> hiddenLayerNodeConnections = hiddenLayerNode.GetToConnections(); // get all of the connections

                weightsAndBiases += GetAllWeightsFromConnections(hiddenLayerNodeConnections); // add the weights to the weights and biases
            }
        }

        // after doing all of the hidden layers we have already done the connections from the last hidden layer to the output layer
        // so we are done

        return weightsAndBiases; // return all of the weights and biases
    }

    private string GetAllWeightsFromConnections(List<Connection> connections) {
        // this is a helper function for GetWeightsAndBiases() that will return a string of all the weights of the connections

        string weights = ""; // this will contain all of the weights

        for(int connectionIndex = 0; connectionIndex < connections.Count; connectionIndex++) {  // go for each connection
            Connection connection = connections[connectionIndex]; // get the connection

            weights += connection.GetWeight().ToString() + "\n"; // add the weight to the weights
        }

        return weights; // return all of the weights
    }

    private string GetAllBiasesFromNodes(Node[] nodes) {
        // this is the same as the GetAllWeightsFromConnections() but will return the biases of the nodes

        string biases = ""; // this will contain all of the biases

        for(int nodeIndex = 0; nodeIndex < nodes.Length; nodeIndex++) {  // go for each node
            Node node = nodes[nodeIndex]; // get the node

            biases += node.GetBias().ToString() + "\n"; // add the bias to the biases
        }

        return biases; // return all of the biases
    }


    public void HasGaveNewInputs() {
        gotNewInputs = true; // this runs when we have gotten new inputs
    }

    public void SetScore(float score) {
        // this is run from the controller of the neural networ
        this.score = score;
    }

    public float GetScore() {
        // this is for the natural slector
        return score;
    }

    public void AddToScore(float scoreToAdd) {
        // adds score based on the number provided
        score += scoreToAdd;
    }

    public bool isPreformingEvents() {
        // returns a bool based on if the network is preforming an event
        return preformingEvents;
    }

    public OutputLayer GetOutputLayer() {
        // returns the output layer of this network
        return outputLayer;
    }
}