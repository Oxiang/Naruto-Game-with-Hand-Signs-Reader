using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {


    public Material m_material;
    public string input;
    public GameObject cube;

    ArduinoInterface ai;

    // Use this for initialization
    void Start () {
        ai = cube.GetComponent<ArduinoInterface>();
        m_material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        ai.getInputs("PALM");
        input = ai.message;
        if (input.Equals("Palm check"))
        {
            m_material.color = Color.red;
            Debug.Log(ai.message + " from new script");
        }
        else
        {
            m_material.color = Color.black;
        }
        

	}
}
