using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Danmaku/Pattern")]
public class Pattern : MonoBehaviour {

    public List<GameObject> emitterGOs = new List<GameObject>();

    public List<PatternStep> steps = new List<PatternStep>();
    
    public bool activated = false;

    int currentStep = -1;

    public void Activate()
    {
        currentStep = 0;
        Debug.Log("Started pattern.");
    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentStep == -1)
        {
            return;
        }
        if(currentStep >= steps.Count)
        {
            return;
        }

        if(!steps[currentStep].started)
        {
            steps[currentStep].Start();
            Debug.Log("Starting step: " + currentStep);
        }
        if (steps[currentStep].ended)
        {
            currentStep += 1;
            Debug.Log("Switching to step: " + currentStep);
        }
    }

    public void PopulateGameObjectsFromChildren()
    {
        List<Emitter> newEmits = new List<Emitter>();
        GetComponentsInChildren<Emitter>(newEmits);

        foreach (Emitter e in newEmits)
        {
            emitterGOs.Add(e.gameObject);
        }
    }
}
