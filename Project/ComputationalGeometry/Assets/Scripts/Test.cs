﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Navigation;

public class Test : MonoBehaviour
{
    public Transform Source;
    public Transform Target;

    private INavigator _navigator;

    void Start()
    {
        _navigator = new Navigator();

        var agentsGos = GameObject.FindGameObjectsWithTag("Agent");
        var agents = new List<IAgent>();

        foreach (var agentGo in agentsGos)
        {
            agents.Add(InstantiateAgent(agentGo));
        }

        _navigator.Init(agents.ToArray());
    }

    private Agent InstantiateAgent(GameObject agentGo)
    {
        var vertexPositions = new List<Vector3>();
        foreach (Transform t in agentGo.transform.Find("Vertices"))
        {
            vertexPositions.Add(t.position);
        }

        // Init model first
        var agent = new Agent(vertexPositions.ToArray(), agentGo.transform.position);

        var view = agentGo.GetComponent<AgentView>() ?? agentGo.AddComponent<AgentView>();
        view.Agent = agent;

        return agent;
    }

    void Update()
    {
        _navigator.Update();
        _navigator.Draw();

        var path = _navigator.GetPath(Source.position, Target.position);

        for (int i = 1; i < path.Length; i++)
        {
            Debug.DrawLine(path[i - 1], path[i]);
        }

       
    }

}
