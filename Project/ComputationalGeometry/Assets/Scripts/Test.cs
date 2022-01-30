using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Navigation;

public class Test : MonoBehaviour
{
    public Transform Source;
    public Transform Target;
    public GameObject Node;
    public Graph Graph;

    private INavigator _navigator;
    private bool once = true;

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

        if (once)
        {
            once = false;
            SetupDjikstra();
        }
    }

    private void SetupDjikstra()
    {
        var path = _navigator.GetPath(Source.position, Target.position);

        var player = GameObject.FindGameObjectWithTag("Player");

        GameObject last = null;
        bool first = true;
        foreach (Vector3 t in path)
        {
            var now = Instantiate(Node);
            now.transform.SetParent(Graph.transform);
            now.transform.position = t;
            var nowNode = now.GetComponent<Node>();

            if (last != null) last.GetComponent<Node>().connections.Add(nowNode);
            if (first)
            {
                first = false;
                player.GetComponent<Follower>().m_Start = nowNode;
                player.GetComponent<Follower>().m_Graph = Graph;
            }
            Graph.nodes.Add(nowNode);
            last = now;
        }
        player.GetComponent<Follower>().m_End = last.GetComponent<Node>();
        player.GetComponent<Follower>().enabled = true;
    }

}
