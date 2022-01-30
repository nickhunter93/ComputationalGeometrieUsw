using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMinkowski : MonoBehaviour
{
    private List<Vector3> _verticiesList;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        Preparation();
        Generation();
    }

    private void Update()
    {
        if (once)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Test>().enabled = true;
            once = false;
        }
    }

    private void Generation()
    {
        List<Vector3> objectVerticies = new List<Vector3>();
        var player = GameObject.FindGameObjectWithTag("Player");

        objectVerticies.Add(new Vector3(_verticiesList[0].x + (player.transform.localScale.x * 0.5f), 0, _verticiesList[0].z + (player.transform.localScale.z * 0.5f)));
        objectVerticies.Add(new Vector3(_verticiesList[1].x - (player.transform.localScale.x * 0.5f), 0, _verticiesList[1].z + (player.transform.localScale.z * 0.5f)));
        objectVerticies.Add(new Vector3(_verticiesList[2].x + (player.transform.localScale.x * 0.5f), 0, _verticiesList[2].z - (player.transform.localScale.z * 0.5f)));
        objectVerticies.Add(new Vector3(_verticiesList[3].x - (player.transform.localScale.x * 0.5f), 0, _verticiesList[3].z - (player.transform.localScale.z * 0.5f)));

        var vertice1 = new GameObject();
        var vertice2 = new GameObject();
        var vertice3 = new GameObject();
        var vertice4 = new GameObject();

        vertice1.transform.position = objectVerticies[1];
        vertice2.transform.position = objectVerticies[0];
        vertice3.transform.position = objectVerticies[2];
        vertice4.transform.position = objectVerticies[3];

        var verticies = new GameObject();
        verticies.name = "Vertices";

        vertice1.transform.SetParent(verticies.transform);
        vertice2.transform.SetParent(verticies.transform);
        vertice3.transform.SetParent(verticies.transform);
        vertice4.transform.SetParent(verticies.transform);


        var minkowObject = new GameObject();
        minkowObject.name = "MinkowObject " + this.name;
        verticies.transform.SetParent(minkowObject.transform);
        minkowObject.transform.position = transform.position;

        minkowObject.tag = "Agent";

        /*var test = minkowObject.AddComponent<Test>();
        test.Target = GameObject.FindGameObjectWithTag("Finish").transform;
        test.Source = GameObject.FindGameObjectWithTag("Player").transform;
        */
    }

    private void Preparation()
    {
        _verticiesList = new List<Vector3>();

        var upperRightTop = new Vector3(0.5f * transform.localScale.x, 0, 0.5f * transform.localScale.z);
        var upperLeftTop =  new Vector3(-0.5f * transform.localScale.x, 0, 0.5f * transform.localScale.z);
        var downRightTop =  new Vector3(0.5f * transform.localScale.x, 0, -0.5f * transform.localScale.z);
        var downLeftTop =  new Vector3(-0.5f * transform.localScale.x, 0, -0.5f * transform.localScale.z);

        _verticiesList.Add(upperRightTop);
        _verticiesList.Add(upperLeftTop);
        _verticiesList.Add(downRightTop);
        _verticiesList.Add(downLeftTop);
        
        GetComponent<BoxCollider>().enabled = false;
    }
}
