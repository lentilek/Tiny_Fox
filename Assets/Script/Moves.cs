using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    public float distance = 1f;
    public List<GameObject> targetsIdle = new List<GameObject>();
    private int counter = 0;

    private int maxStat = 100000;
    private int drink;
    private int food;
    private int sleep;
    private int fun;

    private int drinkN = 25;
    private int foodN = 20;
    private int sleepN = 10;
    private int funN = 5;

    private int isZero = 0;

    public GameObject targetDrink;
    public GameObject targetFood;
    public GameObject targetSleep;
    public GameObject targetFun;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = targetsIdle[counter];
        drink = maxStat;
        food = maxStat;
        sleep = maxStat;
        fun = maxStat;
    }
    void Update()
    {
        switch (isZero) 
        {
            case 0:
                Zero();
                break;
            case 1:
                Drink();
                break;
            case 2:
                Food();
                break;
            case 3:
                Sleep();
                break;
            case 4:
                Fun();
                break;
            default: 
                break;
        }
    }
    public void Zero()
    {
            DropStats();
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < distance && isZero == 0)
            {
                counter++;
                if (counter >= targetsIdle.Count) counter = 0;
                target = targetsIdle[counter];
            }
    }
    public void Drink()
    {
        target = targetDrink;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (drink < maxStat)
            {
                drink += drinkN*10;
            }
            else
            {
                isZero= 0;
            }
        }
    }
    public void Food()
    {
        target = targetFood;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (food < maxStat)
            {
                food += foodN * 10;
            }
            else
            {
                isZero = 0;
            }
        }
    }
    public void Sleep()
    {
        target = targetSleep;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (sleep < maxStat)
            {
                sleep += sleepN * 10;
            }
            else
            {
                isZero = 0;
            }
        }
    }
    public void Fun()
    {
        target = targetFun;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (fun < maxStat)
            {
                fun += funN * 10;
            }
            else
            {
                isZero = 0;
            }
        }
    }
    public void DropStats()
    {
        drink -= drinkN;
        if(drink <= 0) isZero= 1;
        food -= foodN;
        if(food <= 0) isZero= 2;
        sleep -= sleepN;
        if(sleep <= 0) isZero= 3;
        fun -= funN;
        if(fun <= 0) isZero= 4;
    }
}
