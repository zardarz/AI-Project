[System.Serializable]
public class InputLayer : Layer 
{
    public void SetActivationOnNode(float activation, int index) {
        GetNodes()[index].SetActivation(activation);
    }
}