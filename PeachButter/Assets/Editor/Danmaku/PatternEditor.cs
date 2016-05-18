using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Pattern))]
public class PatternEditor : Editor {

    Pattern p;
    
    void OnEnable()
    {
        p = (Pattern)target;
    }

    public override void OnInspectorGUI()
    {
        p.activated = EditorGUILayout.ToggleLeft("Active", p.activated);
        
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();
        
        for (int i = 0; i < p.gos.Count; i++)
        {
            GameObject go = p.gos[i];
            Transform t = go.transform;
            Emitter e = go.GetComponent<Emitter>();
            

                EditorGUI.indentLevel += 1;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                t.position = EditorGUILayout.Vector3Field("position", t.position);
                t.eulerAngles = EditorGUILayout.Vector3Field("rotation", t.eulerAngles);
                EditorGUILayout.Space();
                e.bulletPool = EditorGUILayout.ObjectField("Bullet Pool", e.bulletPool, typeof(BulletsPool), true) as BulletsPool;
                e.speed = EditorGUILayout.FloatField("Speed", e.speed);
                e.lifeTime = EditorGUILayout.FloatField("Life Time", e.lifeTime);
                e.fireRate = EditorGUILayout.FloatField("Fire Rate", e.fireRate);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();

                if (GUILayout.Button(new GUIContent("X")))
                {
                    DeleteEmitter(i);
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel -= 1;
                    break;
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel -= 1;
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("Add New")))
        {
            CreateEmitter(p.gos.Count);
        }
        
        if (GUILayout.Button(new GUIContent("Update List")))
        {
            p.PopulateGameObjects();
        }

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button(new GUIContent("Clear all")))
        {
            ClearEmitters();
        }

        EditorGUILayout.EndVertical();

        EditorUtility.SetDirty(p);
    }

    void OnSceneGUI()
    {
        for (int i = 0; i < p.gos.Count; i++)
        {
            GameObject go = p.gos[i];
            Transform t = go.transform;
            EditorGUI.BeginChangeCheck();
            Vector3 pos = Handles.PositionHandle(t.position, t.rotation);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move Emitter "+go.name);
                t.position = pos;
            }
            EditorGUI.BeginChangeCheck();
            Quaternion rot = Handles.RotationHandle(t.rotation, t.position);
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Rotate Emitter " + go.name);
                t.rotation = rot;
            }
        }
    }

    void CreateEmitter(int number)
    {
        GameObject go = new GameObject("emit "+number);
        go.AddComponent(typeof(Emitter));
        go.transform.SetParent(p.transform);

        p.gos.Add(go);
    }

    void DeleteEmitter(int i)
    {
        if (i > -1)
        {
            GameObject go = p.gos[i];
            p.gos.RemoveAt(i);
            
            go.transform.SetParent(null);
            DestroyImmediate(go);
        }
    }

    void ClearEmitters()
    {
        while(p.gos.Count > 0)
        {
            DeleteEmitter(p.gos.Count - 1);
        }
    }
}
