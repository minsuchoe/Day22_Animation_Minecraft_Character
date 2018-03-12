using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : MonoBehaviour {
    public GameObject block;

    public void CreateStoneBlock(Vector3 pos)
    {
        Instantiate(block, pos, Quaternion.identity);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
