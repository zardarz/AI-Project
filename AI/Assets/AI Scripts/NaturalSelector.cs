using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaturalSelector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject creatureToEvolve; // the creature we want to evolve
    [Range(0,50)] [SerializeField] private int creaturesPerGeneration; // how many creatures there will be each generation
    [Range(0,100)] [SerializeField] private float lengthOfEachGeneration;
    [SerializeField] private static int generation; // the current generation
    [SerializeField] private float timeUntilNextGeneration = 1f; // the time until the next generation is made

    [SerializeField] private string pathToWeightAndBiases; // this is the file path to the txt file that contains all of the weight and biases

    [Range(0,1)] [SerializeField] private float varience; // the amount of varience in the sim

    // private info
    private GameObject[] creatures; // all of the creatures that are alive

    [Header("Refrences")]
    [SerializeField] private TMP_Text generationText;


    void Start()
    {
        SetGenerationText(); // set the generation text
        timeUntilNextGeneration = lengthOfEachGeneration; // the time until the next generation is the time for the next generation

        creatures = new GameObject[creaturesPerGeneration]; // fill the creature list with nulls

        // we will save the best creatures weight and biases in a text file so we first check if it actually exist
        if(!File.Exists(pathToWeightAndBiases)) { // if it doesn't 
            File.WriteAllText(pathToWeightAndBiases, ""); // we make the file to the path and fill it with nothing

            MakeNewGenerationBasedOn(Instantiate(creatureToEvolve)); // and we make the new generation based on the untrained creature
            return; // and we dont do the rest
        }
        
        string weightAndBiases = File.ReadAllText(pathToWeightAndBiases); // we load the text from the file

        GameObject startingCreature = Instantiate(creatureToEvolve); // make a starting creature so we don't effect the asset
        startingCreature.GetComponent<NeuralNetwork>().LoadWeightsAndBiases(weightAndBiases); // load in the weights and biases for the starting creature
        MakeNewGenerationBasedOn(startingCreature); // make the generation with the starting creature
    }

    private void SetGenerationText() {
        generationText.text = "Gen: " + generation;
    }

    void Update()
    {
        timeUntilNextGeneration -= Time.deltaTime; // make the count down until the next generation

        if(timeUntilNextGeneration <= 0) { // if the time until the next generation is or less 0
            File.WriteAllText(pathToWeightAndBiases, GetFitestCreature().GetComponent<NeuralNetwork>().GetWeightsAndBiases()); // we write all of the weight and biases to the file
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reset the scene
        }
    }


    private void MakeNewGenerationBasedOn(GameObject selectedCreature) {
        // this will make new creatures that mutaded from the selected creature

        for(int i = 0; i < creatures.Length; i++) { // first we destroy all of the previous creatures
            DestroyImmediate(creatures[i]);
        }

        float weightAndBiasToAdd = 0f; // this will keep track of the weight and biases we should add to each creature

        for(int i = 0; i < creatures.Length; i++) { // go for each creature
            creatures[i] = Instantiate(selectedCreature); // make the element be a copy of the selected creature
            GameObject creature = creatures[i]; // get the selected creature

            creature.GetComponent<NeuralNetwork>().AddWeightsAndBias(weightAndBiasToAdd); // mutate the creature
            weightAndBiasToAdd += varience; // increment the weight and bias to add 
        }

        DestroyImmediate(selectedCreature); // then we destroy the select creature

        generation += 1; // this is now a new generation
    }

    private GameObject GetFitestCreature() {
        // this will return the creature that had the most score

        GameObject fitestCreature = creatures[0]; // container for the fitest creature
        float fitestCreatureScore = fitestCreature.GetComponent<NeuralNetwork>().GetScore(); // the score of the fitest creature

        for(int i = 0; i < creatures.Length; i++) { // go for each creature
            GameObject creature = creatures[i]; // get the creature
            float creatureScore = creature.GetComponent<NeuralNetwork>().GetScore(); // get the creatures score

            if(creatureScore > fitestCreatureScore) { // if this creatures score is higher than the fitest creatures score
                fitestCreature = creature; // then the fitest creature is this creature
            }
        }

        return fitestCreature; // return the fitest creature
    }
}