﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovePlayer : MonoBehaviour
{
    public int speed = 7;
    const float gravity = 9.8f;
    CharacterController characterController;
    // Start is called before the first frame update

    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        MoveCharacter();
    }
    Vector3 moveVector;
    private void MoveCharacter()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);
        moveVector = transform.TransformDirection(moveVector);
        if (!characterController.isGrounded)
        {
            moveVector.y -= Time.deltaTime * gravity;
        }
        characterController.Move(moveVector);
    }
}
