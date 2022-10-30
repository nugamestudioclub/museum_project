using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float healthDepleteRate = 1;

    private float hunger;
    [SerializeField]
    private float maxHunger = 20f;

    private float hydration;
    [SerializeField]
    private float maxHydration = 10f;

    private GameObject statUI;
    private Image healthContent;
    private Image hungerContent;
    private Image hydrationContent;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        hydration = maxHydration;

        statUI = GameObject.Find("Survival Stats");
        healthContent = statUI.transform.Find("Health Bar/Health Value").gameObject.GetComponent<Image>();
        hungerContent = statUI.transform.Find("Hunger Bar/Hunger Value").gameObject.GetComponent<Image>();
        hydrationContent = statUI.transform.Find("Hydration Bar/Hydration Value").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            TickStats();
        }
    }

    //called each frame to update stat values
    public void TickStats()
    {
        TickHunger();
        TickHydration();
        if (hunger == 0f || hydration == 0f)
        {
            ChangeHealth(-healthDepleteRate * Time.deltaTime);
        }
        if (health == 0)
        {
            Debug.Log("player died");
            isAlive = false;
        }
    }

    public void ChangeStat(ref float stat, float maxStat, float change, ref Image statContent)
    {
        stat = Mathf.Clamp(stat + change, 0f, maxStat);
        statContent.fillAmount = Mathf.Clamp(stat / maxStat, 0f, 1f);
    }

    public void TickHunger()
    {
        ChangeStat(ref hunger, maxHunger, -Time.deltaTime, ref hungerContent);
    }

    public void TickHydration()
    {
        ChangeStat(ref hydration, maxHydration, -Time.deltaTime, ref hydrationContent);
    }

    public void ChangeHealth(float change)
    {
        ChangeStat(ref health, maxHealth, change, ref healthContent);
    }
}
