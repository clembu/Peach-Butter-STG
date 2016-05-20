using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PatternStep {

    public List<Emitter> emitters = new List<Emitter>();
    public List<MonoBehaviour> moves = new List<MonoBehaviour>();
    public List<bool> fireOnces = new List<bool>();

    public float duration = 0.0f;

    public int order;

    float startTime = -1.0f;

    public void Start()
    {
        startTime = Time.time;

        foreach (Emitter e in emitters)
        {
            e.enabled = true;
            Debug.Log("Enabled " + e.name);
        }

        if(duration <= 0)
        {
            Debug.Log("no duration");
            foreach (MonoBehaviour m in moves)
            {
                m.enabled = true;
                Debug.Log("Enabled " + m.name);
            }
        }
    }

    public bool started
    {
        get
        {
            return startTime > 0.0f;
        }
    }

    public bool ended
    {
        get
        {
            if (duration > 0)
            {
                bool end = startTime + duration >= Time.time;
                if(end)
                {
                    Stop();
                }
                return end;
            }
            else
            {
                bool end = true;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].enabled)
                    {
                        end = false;
                    } else
                    {
                        emitters[i].enabled = false;
                        end = end && true;
                    }
                }
                if(end)
                {
                    Stop();
                }
                return end;
            }
        }
    }

    public void Stop()
    {
        foreach (Emitter e in emitters)
        {
            e.enabled = false;
            Debug.Log("Disabling " + e.name);
        }
        foreach (MonoBehaviour m in moves)
        {
            m.enabled = false;
            Debug.Log("Disabling " + m.name);
        }
        startTime = 0.0f;
    }
}
