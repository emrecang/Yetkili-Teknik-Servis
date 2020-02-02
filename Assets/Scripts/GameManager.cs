﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject holdingObject;
    public static GameObject customer;
    public List<PersonData> person;
    public GameObject cust;
    public static GameManager instance;
    public DeviceManager brokenDevice;

    Transform startCamTransform;
    Transform repairCamTransform;

    private void Awake()
    {
        instance = this;
        holdingObject = null;
    }
    public enum GameStates
    {
        WaitCustomer,
        Dialogue,
        Repair
    }
    public GameStates states;
    public void Start()
    {
        startCamTransform = Camera.main.transform;
        customer = cust;
        WaitCustomerGameState();
        DayManager.instance.StartIncreaseMinCor(10,1);
    }
    public void ChangeGameState(GameStates myStates)
    {
        states = myStates;
    }
    public void WaitCustomerGameState()
    {
        Camera.main.transform.position = startCamTransform.position;
        Camera.main.transform.rotation = startCamTransform.rotation;

        var rand = Random.Range(0, person.Count);
        Dialogue.instance.SetData(person[rand]);
        ChangeGameState(GameStates.WaitCustomer);
        StartCoroutine(MoveCustomer());
    }


    public void RepairGameState()
    {
        //repairCamTransform = brokenDevice.cameraTransform;
        //Camera.main.transform.position = repairCamTransform.position;
        //Camera.main.transform.rotation = repairCamTransform.rotation;
        //Camera zoom
        //brokenDevice.GetComponent<BoxCollider>().enabled = false;
    }

    public IEnumerator MoveCustomer()
    {
        while(customer.transform.position.x > 0)
        {
            customer.transform.position += Vector3.left * Time.deltaTime* 3;
            yield return new WaitForSeconds(0.01f);
        }
        DialogueTrigger.instance.TriggerDialogue();
    }
    public IEnumerator BackCustomer()
    {

        while (customer.transform.position.x < 9)
        {
            customer.transform.position += Vector3.right * Time.deltaTime* 3;
            yield return new WaitForSeconds(0.01f);
        }

        RepairGameState(); // automatic;
    }
    public void BackCustomerCor()
    {
        StartCoroutine(BackCustomer());
    }
}