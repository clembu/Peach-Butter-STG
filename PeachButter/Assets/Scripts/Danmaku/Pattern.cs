using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pattern : MonoBehaviour {

    public List<Emmiter> emitters = new List<Emmiter>();
    
    public bool activated = false;
    

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < emitters.Count; i++)
        {
            emitters[i].isEmitting = activated;
        }
    }
}
