using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlock : MonoBehaviour {

	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.tag == "Block")
                hit.collider.gameObject.GetComponent<BreakUp>().Hit();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider.tag == "Block")
                hit.collider.gameObject.GetComponent<StoneBlock>().CreateStoneBlock(hit.transform.position + hit.normal);
        }
    }
}
