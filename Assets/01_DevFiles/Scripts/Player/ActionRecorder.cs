using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRecorder : MonoBehaviour
{

    private Dictionary<float, RecordData> playerInputData;
    private float horizontalValue;
    private InputManager input;

    void Start()
    {
        input = InputManager.Instance;
        playerInputData = new Dictionary<float, RecordData>();

    }

    //you can get recorded inputs
    public void GetInputs()
    {
        horizontalValue = input.RotValue;
    }

    //if you want to record player's movement, you can use this function
    public void Recording(float timer)
    {
        GetInputs();

        AddToDictionary(timer, GetInputData());
    }

    public RecordData GetInputData()
    {
        RecordData playerInputs = new RecordData(horizontalValue);
        return playerInputs;
    }

    public void ResetInput()
    {
        horizontalValue = 0;
    }

    
    // Record every frame to the RecordData
    private void AddToDictionary(float time, RecordData inputs)
    {
        playerInputData.Add(time, inputs);
    }


    public void ClearHistory()
    {
        playerInputData.Clear();
    }

    public bool KeyExists(float key)
    {
        return playerInputData.ContainsKey(key);
    }

    // You can use every frame of recorded
    public RecordData GetRecordedInputs(float timeStamp)
    {
        return playerInputData[timeStamp];
    }
}
