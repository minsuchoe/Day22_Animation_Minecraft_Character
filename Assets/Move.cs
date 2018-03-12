using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float slideSpeed = 3f;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;

    float jumpSpeed = 8f;
    float gravity = 20f;

    bool isOnSlope = false;
    Vector3 hitNormal;
    Vector3 hitPoint;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        StartCoroutine(GetIdenticonFromURL());
    }

    private IEnumerator GetIdenticonFromURL()
    {
        string url = "http://www.gravatar.com/avatar/mschoe?s=256&d=identicon&r=PG";

        using (WWW www = new WWW(url))
        {
            yield return www;
            var head = transform.Find("/player/neck/Cube");
            head.GetComponent<Renderer>().material.mainTexture = www.texture;
        }
    }

    void Update()
    {
        if (con.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDirection = (new Vector3(h, 0f, v)).normalized;
            transform.LookAt(transform.position + moveDirection);

            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) > con.slopeLimit;
        Vector3 slideDirection = Vector3.zero;
        if (isOnSlope)
        {
            // 1. approximate sliding vector
            // slideDirection = (new Vector3(hitNormal.x, 0f, hitNormal.z)) * slideSpeed;

            // 2. accurate slideing vector
            Vector3 c = Vector3.Cross(hitNormal, Vector3.up);
            slideDirection = -Vector3.Cross(c, hitNormal) * slideSpeed;

            Debug.DrawRay(hitPoint, slideDirection, Color.blue, 3f);
        }

        anim.SetFloat("Speed", con.velocity.magnitude);
        con.Move((moveDirection + slideDirection) * Time.deltaTime);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Wall")
        {
            hitNormal = hit.normal;
            hitPoint = hit.point;
        }
    }

}
