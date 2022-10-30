using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructManager : MonoBehaviour
{
    private float water;
    [SerializeField]
    private float waterMax = 50f;
    [SerializeField]
    private float waterRate = 1f;
    private float food;
    [SerializeField]
    private float foodMax = 50f;
    [SerializeField]
    private float foodRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        water = 0f;
        food = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        water = Mathf.Clamp(water + waterRate * Time.deltaTime, 0f, waterMax);
        food = Mathf.Clamp(food + foodRate * Time.deltaTime, 0f, foodMax);
    }

    // called whenever the player collects the resources from this construct
    public (float waterAmt, float foodAmt) CollectAll()
    {
        water = 0f;
        food = 0f;
        return (water, food);
    }
}