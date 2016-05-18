using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pattern : MonoBehaviour {

    public List<GameObject> gos = new List<GameObject>();
    List<Emitter> emitters = new List<Emitter>();
    
    public bool activated = false;
    
    void OnEnable()
    {
        for (int i = 0; i < gos.Count; i++)
        {
            emitters[i] = gos[i].GetComponent<Emitter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < emitters.Count; i++)
        {
            emitters[i].isEmitting = activated;
        }
    }

    public void PopulateGameObjects()
    {
        List<Emitter> newEmits = new List<Emitter>();
        GetComponentsInChildren<Emitter>(newEmits);

        foreach (Emitter e in newEmits)
        {
            gos.Add(e.gameObject);
        }
    }
}
