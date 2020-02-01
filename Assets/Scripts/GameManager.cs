using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject holdingObject;
    public static GameObject customer;
    public List<PersonData> person;
    public GameObject cust;
    public static GameManager instance;
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
        var rand = Random.Range(0, person.Count);
        Dialogue.instance.SetData(person[rand]);
        ChangeGameState(GameStates.WaitCustomer);
        StartCoroutine(MoveCustomer());
    }


    public void RepairGameState()
    {
        UIManager.instance.ChangeToolBox(true);
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
