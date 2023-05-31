using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class AI : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    public float distance = 1f;
    public List<GameObject> targetsIdle = new List<GameObject>();
    private int counter = 0;

    public float maxStat = 100;
    private float drink;
    private float food;
    private float sleep;
    private float fun;

    private float drinkN = 0.35f;
    private float foodN = 0.5f;
    private float sleepN = 0.7f;
    private float funN = 1f;

    private int isZero = 0;

    public GameObject targetDrink;
    public GameObject targetFood;
    public GameObject targetSleep;
    public GameObject targetFun;

    private bool waiting = false;
    Animator m_Animator;
    bool m_Walk = true;
    bool m_Eat = false;
    bool m_Sleep = false;
    bool m_Fun = false;

    public Image drinkBar;
    public Image foodBar;
    public Image sleepBar;
    public Image funBar;

    [SerializeField] private AudioSource playSoundEffect;
    [SerializeField] private AudioSource eatSoundEffect;
    [SerializeField] private AudioSource drinkSoundEffect;
    [SerializeField] private AudioSource sleepSoundEffect;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        target = targetsIdle[counter];
        drink = maxStat;
        food = maxStat;
        sleep = maxStat;
        fun = maxStat;

        drinkBar.fillAmount = (float)drink/(float)maxStat;
        foodBar.fillAmount = (float)food / (float)maxStat;
        sleepBar.fillAmount = (float)sleep / (float)maxStat;
        funBar.fillAmount = (float)fun / (float)maxStat;

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
            if (drink < maxStat && !waiting)
            {
                drinkSoundEffect.Play();
                waiting = true;
                StartCoroutine(DrinkMore());

                if (m_Walk == true)
                {
                    m_Animator.SetBool("m_Walk", false);
                    m_Walk = false;
                }
                if (m_Eat == false)
                {
                    m_Animator.SetBool("m_Eat", true);
                    m_Eat = true;
                }
            }
            else if (!waiting)
            {
                isZero= 0;
                StartCoroutine(DrinkLess());

                if (m_Walk == false)
                {
                    m_Animator.SetBool("m_Walk", true);
                    m_Walk = true;
                }
                if (m_Eat == true)
                {
                    m_Animator.SetBool("m_Eat", false);
                    m_Eat = false;
                }
            }
        }
    }

    IEnumerator DrinkMore()
    {
        do
        {
            drink++;
            drinkBar.fillAmount = (float)drink / (float)maxStat;
            if (drink%5 == 0)
            {
                yield return new WaitForSeconds(0.3f);
            }
        }while(drink < maxStat);
        waiting= false;
    }

    public void Food()
    {
        target = targetFood;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (food < maxStat && !waiting)
            {
                eatSoundEffect.Play();
                waiting = true;
                StartCoroutine(FoodMore());

                if (m_Walk == true)
                {
                    m_Animator.SetBool("m_Walk", false);
                    m_Walk = false;
                }
                if (m_Eat == false)
                {
                    m_Animator.SetBool("m_Eat", true);
                    m_Eat = true;
                }
            }
            else if(!waiting)
            {
                isZero = 0;
                StartCoroutine(FoodLess());

                if (m_Walk == false)
                {
                    m_Animator.SetBool("m_Walk", true);
                    m_Walk = true;
                }
                if (m_Eat == true)
                {
                    m_Animator.SetBool("m_Eat", false);
                    m_Eat = false;
                }
            }
        }
    }
    IEnumerator FoodMore()
    {
        do
        {
            food++;
            foodBar.fillAmount = (float)food / (float)maxStat;
            if (food % 5 == 0)
            {
                yield return new WaitForSeconds(0.3f);
            }
        } while (food < maxStat);
        waiting = false;
    }
    public void Sleep()
    {
        target = targetSleep;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (sleep < maxStat && !waiting)
            {
                sleepSoundEffect.Play();
                waiting = true;
                StartCoroutine(SleepMore());

                if (m_Walk == true)
                {
                    m_Animator.SetBool("m_Walk", false);
                    m_Walk = false;
                }
                if (m_Sleep == false)
                {
                    m_Animator.SetBool("m_Sleep", true);
                    m_Sleep = true;
                }
            }
            else if(!waiting)
            {
                isZero = 0;
                StartCoroutine(SleepLess());

                if (m_Walk == false)
                {
                    m_Animator.SetBool("m_Walk", true);
                    m_Walk = true;
                }
                if (m_Sleep == true)
                {
                    m_Animator.SetBool("m_Sleep", false);
                    m_Sleep = false;
                }

            }
        }
    }
    IEnumerator SleepMore()
    {
        do
        {
            sleep++;
            sleepBar.fillAmount = (float)sleep / (float)maxStat;
            if (sleep % 5 == 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
        } while (sleep < maxStat);
        waiting = false;
    }
    public void Fun()
    {
        target = targetFun;
        agent.SetDestination(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            if (fun < maxStat && !waiting)
            {
                playSoundEffect.Play();
                waiting = true;
                StartCoroutine(FunMore());

                if (m_Walk == true)
                {
                    m_Animator.SetBool("m_Walk", false);
                    m_Walk = false;
                }
                if (m_Fun == false)
                {
                    m_Animator.SetBool("m_Fun", true);
                    m_Fun = true;
                }
            }
            else if(!waiting)
            {
                isZero = 0;
                StartCoroutine(FunLess());


                if (m_Walk == false)
                {
                    m_Animator.SetBool("m_Walk", true);
                    m_Walk = true;
                }
                if (m_Fun == true)
                {
                    m_Animator.SetBool("m_Fun", false);
                    m_Fun = false;
                }
            }
        }
    }
    IEnumerator FunMore()
    {
        do
        {
            fun++;
            funBar.fillAmount = (float)fun / (float)maxStat;
            if (fun % 5 == 0)
            {
                yield return new WaitForSeconds(0.3f);
            }
        } while (fun < maxStat);
        waiting = false;
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
            drinkBar.fillAmount = (float)drink / (float)maxStat;

        } while(drink > 0);
        //if (drink <= 0) isZero = 1;
    }
    IEnumerator FoodLess()
    {
        do
        {
            yield return new WaitForSeconds(foodN);
            food--;
            foodBar.fillAmount = (float)food / (float)maxStat;

        } while (food > 0);
        //if (food <= 0) isZero = 2;
    }
    IEnumerator SleepLess()
    {
        do
        {
            yield return new WaitForSeconds(sleepN);
            sleep --;
            sleepBar.fillAmount = (float)sleep / (float)maxStat;

        } while (sleep > 0);
        //if (sleep <= 0) isZero = 3;
    }
    IEnumerator FunLess()
    {
        do
        {
            yield return new WaitForSeconds(funN);
            fun --;
            funBar.fillAmount = (float)fun / (float)maxStat;

        } while (fun > 0);
        //if (fun <= 0) isZero = 4;
    }
}
