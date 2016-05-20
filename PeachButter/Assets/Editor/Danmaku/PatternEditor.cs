using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Pattern))]
public class PatternEditor : Editor {

    Pattern p;
    GameObject inspectedEmitter;
    PatternStep inspectedStep;

    int inspectedStepEmitter = -1;

    Vector2 emitScrollPos;
    Vector2 stepScrollPos;
    Vector2 stepEmitScrollPos;

    bool showEmitters = true, showSteps = false;
    
    void OnEnable()
    {
        p = (Pattern)target;
        if(p.steps.Count==0)
        {
            CreateStep(0);
        }
        inspectedStepEmitter = -1;
        inspectedEmitter = null;
        inspectedStep = null;
    }

    public override void OnInspectorGUI()
    {
        p.activated = EditorGUILayout.ToggleLeft("Active", p.activated);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (showEmitters) GUI.color = Color.grey;
        if(GUILayout.Button("Emitters ("+p.emitterGOs.Count+")"))
        {
            showEmitters = true;
            showSteps = false;
        }
        GUI.color = Color.white;
        if (showSteps) GUI.color = Color.grey;
        if(GUILayout.Button("Steps (" + p.steps.Count + ")"))
        {
            showSteps = true;
            showEmitters = false;

            if(inspectedStep == null)
            {
                inspectedEmitter = null;
                inspectedStepEmitter = -1;
            } else
            {
                if(inspectedStepEmitter == -1)
                {
                    inspectedEmitter = null;
                } else
                {
                    inspectedEmitter = inspectedStep.emitters[inspectedStepEmitter].gameObject;
                }
            }
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();
        if (showEmitters)
        {

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Add New")))
            {
                CreateEmitter(p.emitterGOs.Count);
            }

            if (GUILayout.Button(new GUIContent("Update List")))
            {
                p.PopulateGameObjectsFromChildren();
            }

            if (GUILayout.Button(new GUIContent("Clear all")))
            {
                ClearEmitters();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            emitScrollPos = EditorGUILayout.BeginScrollView(emitScrollPos, EditorStyles.helpBox);
            for (int i = 0; i < p.emitterGOs.Count; i++)
            {
                GameObject go = p.emitterGOs[i];

                if (inspectedEmitter == go) GUI.color = Color.cyan;

                if (GUILayout.Button(new GUIContent(go.name)))
                {
                    inspectedEmitter = go;
                }
                GUI.color = Color.white;

            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            if (inspectedEmitter == null)
            {
                EditorGUILayout.HelpBox("No emitter selected to edit.",MessageType.Info, true);
            }
            else
            {

                Transform t = inspectedEmitter.transform;
                Emitter e = inspectedEmitter.GetComponent<Emitter>();

                EditorGUILayout.LabelField("Currently editing: " + inspectedEmitter.name);

                EditorGUILayout.Space();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.Space();

                t.position = EditorGUILayout.Vector3Field("Position", t.position);
                t.eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.eulerAngles);

                EditorGUILayout.Space();

                e.bulletPool = EditorGUILayout.ObjectField("Bullet Pool", e.bulletPool, typeof(BulletsPool), true) as BulletsPool;
                e.speed = EditorGUILayout.FloatField("Bullet Speed", e.speed);
                e.lifeTime = EditorGUILayout.FloatField("Bullet Life Time", e.lifeTime);
                e.fireRate = EditorGUILayout.FloatField("Fire Rate", e.fireRate);

                EditorGUILayout.Space();

                if (GUILayout.Button(new GUIContent("Delete")))
                {
                    int i = p.emitterGOs.IndexOf(inspectedEmitter);
                    DeleteEmitter(i);
                }
                EditorUtility.SetDirty(t);
                EditorUtility.SetDirty(e);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
        if(showSteps)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Add New")))
            {
                CreateStep(p.steps.Count);
            }

            if (GUILayout.Button(new GUIContent("Clear all")))
            {
                ClearSteps();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            stepScrollPos = EditorGUILayout.BeginScrollView(stepScrollPos, EditorStyles.helpBox);
            for (int i = 0; i < p.steps.Count; i++)
            {
                PatternStep step = p.steps[i];

                if (inspectedStep == step)
                {
                    GUI.color = Color.cyan;
                }

                if (GUILayout.Button("Step "+(step.order)))
                {
                    inspectedStep = step;
                }
                GUI.color = Color.white;

            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            

            if (inspectedStep == null)
            {
                EditorGUILayout.HelpBox("No step selected to edit.",MessageType.Info, true);
            }
            else
            {

                EditorGUILayout.LabelField("Currently editing: Step " + inspectedStep.order);

                List<Emitter> stepEmits = inspectedStep.emitters;
                List<MonoBehaviour> stepMoves = inspectedStep.moves;
                List<bool> stepFireOnces = inspectedStep.fireOnces;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                bool hasDuration = EditorGUILayout.Toggle("Fixed Duration", inspectedStep.duration > 0.0f);
                if (hasDuration)
                {
                    if(inspectedStep.duration <= 0.0f) inspectedStep.duration = 1.0f;
                    inspectedStep.duration = EditorGUILayout.FloatField("Duration", inspectedStep.duration);
                    for (int i = 0; i < inspectedStep.moves.Count; i++)
                    {
                        inspectedStep.moves[i] = null;
                    }
                } else
                {
                    inspectedStep.duration = 0.0f;
                }
                
                EditorGUILayout.Space();
                
                stepEmitScrollPos = EditorGUILayout.BeginScrollView(stepEmitScrollPos, EditorStyles.helpBox);
                for (int i = 0; i < stepEmits.Count; i++)
                {
                    Emitter emit = stepEmits[i];

                    if (inspectedStepEmitter == i) GUI.color = Color.cyan;

                    if (GUILayout.Button(emit.gameObject.name))
                    {
                        inspectedStepEmitter = i;
                        inspectedEmitter = emit.gameObject;
                    }
                    GUI.color = Color.white;

                }
                EditorGUILayout.EndScrollView();

                EditorGUILayout.Space();

                if(inspectedStepEmitter == -1)
                {
                    EditorGUILayout.HelpBox("No emitter selected to edit.",MessageType.Info,true);
                }
                else
                {
                    EditorGUILayout.LabelField("Currently editing: " + stepEmits[inspectedStepEmitter].gameObject.name);

                    if (!hasDuration)
                    {
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        stepFireOnces[inspectedStepEmitter] = EditorGUILayout.ToggleLeft("Fire Once", stepFireOnces[inspectedStepEmitter]);
                        AutoMove oldMove = AutoMoves.GetEnum(stepMoves[inspectedStepEmitter]);
                        AutoMove move = (AutoMove) EditorGUILayout.EnumPopup("Motion",oldMove);
                        if(oldMove!=move)
                        {
                            ChangeMove(inspectedStep,inspectedStepEmitter, oldMove, move);
                        }
                        if(!stepFireOnces[inspectedStepEmitter] && stepMoves[inspectedStepEmitter]==null)
                        {
                            EditorGUILayout.HelpBox("This emitter will emit infintely!\nTo prevent this, set it to fire once, give it a duration, or a motion.", MessageType.Warning, true);
                        }
                        if(stepMoves[inspectedStepEmitter] != null)
                        {
                            EditorGUILayout.LabelField("Current Motion: " + stepMoves[inspectedStepEmitter].GetType().Name);
                            if(move == AutoMove.Straight)
                            {
                                Straight m = stepMoves[inspectedStepEmitter] as Straight;

                                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                                m.lookAtTarget = EditorGUILayout.ObjectField("Always aim at (opt.): ", m.lookAtTarget , typeof(Transform),true) as Transform;

                                m.distanceToGo = EditorGUILayout.FloatField("Distance", m.distanceToGo);

                                m.speed = EditorGUILayout.FloatField("Movement speed", m.speed);

                                m.direction = EditorGUILayout.Vector3Field("Direction", m.direction);

                                m.doReturn = EditorGUILayout.Toggle("Return",m.doReturn);

                                m.loops = EditorGUILayout.IntField("Repeats",m.loops);

                                if(m.direction == Vector3.zero)
                                {
                                    EditorGUILayout.HelpBox("Direction is zero, emitter won't move.", MessageType.Error);
                                }
                                if(m.speed == 0)
                                {
                                    EditorGUILayout.HelpBox("Speed is zero, emitter won't move.", MessageType.Error);
                                }
                                if(m.distanceToGo <= 0)
                                {
                                    EditorGUILayout.HelpBox("Distance is zero or below, emitter won't behave correctly.", MessageType.Error);
                                }

                                EditorUtility.SetDirty(m);
                                EditorGUILayout.EndVertical();
                            } else if(move == AutoMove.MoveAround)
                            {
                                MoveAround m = stepMoves[inspectedStepEmitter] as MoveAround;

                                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                                m.lookAtTarget = EditorGUILayout.ObjectField("Always aim at (opt.): ", m.lookAtTarget, typeof(Transform), true) as Transform;

                                m.moveAroundTarget = EditorGUILayout.ObjectField("Move around: ", m.moveAroundTarget, typeof(Transform), true) as Transform;

                                m.angleToGo = EditorGUILayout.FloatField("Angle to go", m.angleToGo);
                                
                                m.speed = EditorGUILayout.FloatField("Arc speed", m.speed);

                                EditorGUILayout.BeginHorizontal();

                                m.cw = EditorGUILayout.ToggleLeft("Clockwise", m.cw);

                                m.cw = !EditorGUILayout.ToggleLeft("Counterclockwise", !m.cw);

                                EditorGUILayout.EndHorizontal();

                                m.doReturn = EditorGUILayout.Toggle("Return", m.doReturn);

                                m.loops = EditorGUILayout.IntField("Repeats", m.loops);

                                if(m.moveAroundTarget == null)
                                {
                                    EditorGUILayout.HelpBox("Emitter is moving around nothing. Set the target!", MessageType.Error);
                                }

                                if (m.speed == 0)
                                {
                                    EditorGUILayout.HelpBox("Speed is zero, emitter won't move.", MessageType.Error);
                                }
                                if (m.angleToGo <= 0)
                                {
                                    EditorGUILayout.HelpBox("Distance is zero or below, emitter won't behave correctly.", MessageType.Error);
                                }

                                EditorUtility.SetDirty(m);
                                EditorGUILayout.EndVertical();
                            }
                            else if(move == AutoMove.Rotate)
                            {

                                Rotate m = stepMoves[inspectedStepEmitter] as Rotate;

                                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                                m.angleToGo = EditorGUILayout.FloatField("Angle to go", m.angleToGo);

                                m.angSpeed = EditorGUILayout.FloatField("Arc speed", m.angSpeed);

                                EditorGUILayout.BeginHorizontal();

                                m.cw = EditorGUILayout.ToggleLeft("Clockwise", m.cw);

                                m.cw = !EditorGUILayout.ToggleLeft("Counterclockwise", !m.cw);

                                EditorGUILayout.EndHorizontal();

                                m.doReturn = EditorGUILayout.Toggle("Return", m.doReturn);

                                m.loops = EditorGUILayout.IntField("Repeats", m.loops);
                                
                                if (m.angSpeed == 0)
                                {
                                    EditorGUILayout.HelpBox("Speed is zero, emitter won't move.", MessageType.Error);
                                }
                                if (m.angleToGo <= 0)
                                {
                                    EditorGUILayout.HelpBox("Distance is zero or below, emitter won't behave correctly.", MessageType.Error);
                                }

                                EditorUtility.SetDirty(m);
                                EditorGUILayout.EndVertical();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                }

                if (p.steps.Count > 1)
                {
                    if (GUILayout.Button(new GUIContent("Delete Step")))
                    {
                        int i = p.steps.IndexOf(inspectedStep);
                        DeleteStep(i);
                    }
                }

                

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }

        EditorUtility.SetDirty(p);
    }

    void OnSceneGUI()
    {
        for (int i = 0; i < p.emitterGOs.Count; i++)
        {
            GameObject go = p.emitterGOs[i];
            Transform t = go.transform;
            Emitter e = go.GetComponent<Emitter>();

            Color oldC = Handles.color;
            Handles.color = new Color(0, 0, 0, 0.3f);
            Handles.DrawSolidDisc(t.position, -t.forward, 0.2f);
            Handles.color = oldC;

            Handles.ConeCap(0, t.position + t.up * 0.4f, t.transform.rotation*Quaternion.AngleAxis(-90,t.InverseTransformDirection(t.right)), 0.2f);

            if (go == inspectedEmitter)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 pos = Handles.PositionHandle(t.position, t.rotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(t, "Move Emitter " + go.name);
                    t.position = pos;
                }
                EditorGUI.BeginChangeCheck();
                Quaternion rot = Handles.RotationHandle(t.rotation, t.position);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(t, "Rotate Emitter " + go.name);
                    t.rotation = rot;
                }
            }

            GUIStyle style = new GUIStyle();
            style.padding = GUI.skin.button.padding;
            Rect labelrect = HandleUtility.WorldPointToSizedRect(t.position + t.up*1.8f, new GUIContent(go.name),style);
            labelrect.x -= labelrect.width / 2.0f -5;
            labelrect.y -= labelrect.height / 2.0f -3;
            Handles.BeginGUI();
            GUILayout.BeginArea(labelrect);
            GUI.contentColor = Color.white;
            GUI.color = new Color(0.3f, 0.5f, 0.8f);
            if (GUILayout.Button(go.name))
            {
                inspectedEmitter = go;
            }
            GUILayout.EndArea();
            if(inspectedStep != null)
            {
                labelrect = HandleUtility.WorldPointToSizedRect(t.position + t.up * 1.8f, new GUIContent("+"), style);
                labelrect.x -= labelrect.width / 2.0f - 5;
                labelrect.y -= labelrect.height / 2.0f - 3 - 16;
                GUILayout.BeginArea(labelrect);
                if (inspectedStep.emitters.Contains(e))
                {
                    GUI.color = new Color(0.8f, 0.5f, 0.3f);
                    if(GUILayout.Button("-"))
                    {
                        RemoveEmitterFromStep(inspectedStep,e);
                    }
                }
                else {
                    GUI.color = new Color(0.5f, 0.8f, 0.3f);
                    if (GUILayout.Button("+"))
                    {
                        AddEmitterToStep(inspectedStep, e);
                    }
                }
                GUILayout.EndArea();
            }
            Handles.EndGUI();

        }
    }

    void CreateEmitter(int number)
    {
        GameObject go = new GameObject("emit "+number);
        go.AddComponent(typeof(Emitter));
        go.transform.SetParent(p.transform);
        go.GetComponent<Emitter>().enabled = false;
        p.emitterGOs.Add(go);
    }


    void DeleteEmitter(int i)
    {
        if (i > -1)
        {
            GameObject go = p.emitterGOs[i];
            p.emitterGOs.RemoveAt(i);
            
            go.transform.SetParent(null);
            DestroyImmediate(go);
            inspectedEmitter = null;
        }
    }

    void ClearEmitters()
    {
        while(p.emitterGOs.Count > 0)
        {
            DeleteEmitter(p.emitterGOs.Count - 1);
        }
    }


    void CreateStep(int number)
    {
        PatternStep s = new PatternStep();
        s.order = number+1;
        p.steps.Add(s);
    }

    void DeleteStep(int i)
    {
        if(i>-1)
        {
            PatternStep s = p.steps[i];
            p.steps.RemoveAt(i);
            for (int m = s.moves.Count - 1; m >= 0; m--)
            {
                MonoBehaviour mb = s.moves[m];
                s.moves.RemoveAt(m);
                DestroyImmediate(mb);
            }
        }
        inspectedStep = null;
        inspectedStepEmitter = -1;
    }

    void ClearSteps()
    {
        while(p.steps.Count>0)
        {
            DeleteStep(p.steps.Count - 1);
        }
    }

    void AddEmitterToStep(PatternStep step, Emitter emitter)
    {
        step.emitters.Add(emitter);
        step.fireOnces.Add(false);
        step.moves.Add(null);
        inspectedStepEmitter = step.emitters.IndexOf(emitter);
    }

    void RemoveEmitterFromStep(PatternStep step, Emitter emitter)
    {
        int i = step.emitters.IndexOf(emitter);
        if(i >= 0)
        {
            ChangeMove(step, i, AutoMoves.GetEnum(step.moves[i]), AutoMove.NONE);
            step.emitters.RemoveAt(i);
            step.fireOnces.RemoveAt(i);
            step.moves.RemoveAt(i);
        }
        inspectedStepEmitter = -1;
    }



    public void ChangeMove(PatternStep step, int i, AutoMove oldMove, AutoMove newMove)
    {
        GameObject go = step.emitters[i].gameObject;

        if (oldMove != AutoMove.NONE)
        {

            MonoBehaviour oldComp = (MonoBehaviour)go.GetComponent(AutoMoves.GetScriptType(oldMove));
            DestroyImmediate(oldComp);
        }

        if (newMove != AutoMove.NONE)
        {
            go.AddComponent(AutoMoves.GetScriptType(newMove));
            (go.GetComponent(AutoMoves.GetScriptType(newMove)) as MonoBehaviour).enabled = false;
            step.moves[i] = (MonoBehaviour) go.GetComponent(AutoMoves.GetScriptType(newMove));
        }
        else
            step.moves[i] = null;
    }

}
