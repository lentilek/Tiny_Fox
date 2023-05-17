using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    public float distance = 1f;
    public List<GameObject> targetsIdle = new List<GameObject>();
    private int counter = 0;

    private float maxStat = 100;
    private float drink;
    private float food;
    private float sleep;
    private float fun;

    private float drinkN = 0.3f;
    private float foodN = 0.5f;
    private float sleepN = 0.7f;
    private float funN = 1f;

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

        StartCoroutine(DrinkLess());
        StartCoroutine(FoodLess());
        StartCoroutine(SleepLess());
        StartCoroutine(FunLess());
    }
    void Update()
    {
        switch (isZero) 
        {
            case 0:
                Zero();
                ZeroNumber();
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
            //DropStats();
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
                drink ++;
            }
            else
            {
                isZero= 0;
                StartCoroutine(DrinkLess());
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
                food ++;
            }
            else
            {
                isZero = 0;
                StartCoroutine(FoodLess());
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
                sleep ++;
            }
            else
            {
                isZero = 0;
                StartCoroutine(SleepLess());
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
                fun ++;
            }
            else
            {
                isZero = 0;
                StartCoroutine(FunLess());
            }
        }
    }

    public void ZeroNumber()
    {
        if(drink <= 0) isZero= 1;
        else if(food<= 0) isZero= 2;
        else if(sleep<= 0) isZero= 3;
        else if(fun<= 0) isZero= 4;
        else isZero= 0;
    }

    IEnumerator DrinkLess()
    {
        do
        {
            yield return new WaitForSeconds(drinkN);
            drink--;

        }while(drink > 0);
        //if (drink <= 0) isZero = 1;
    }
    IEnumerator FoodLess()
    {
        do
        {
            yield return new WaitForSeconds(foodN);
            food--;

        } while (food > 0);
        //if (food <= 0) isZero = 2;
    }
    IEnumerator SleepLess()
    {
        do
        {
            yield return new WaitForSeconds(sleepN);
            sleep --;

        } while (sleep > 0);
        //if (sleep <= 0) isZero = 3;
    }
    IEnumerator FunLess()
    {
        do
        {
            yield return new WaitForSeconds(funN);
            fun --;

        } while (fun > 0);
        //if (fun <= 0) isZero = 4;
    }
}
