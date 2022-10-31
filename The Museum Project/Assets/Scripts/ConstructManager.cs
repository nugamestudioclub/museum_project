using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConstructManager : MonoBehaviour
{
    [SerializeField]
    private bool isMaker = true;

    [SerializeField]
    private string resourceName = "";
    private float resource = 0f;
    [SerializeField]
    private float resourceMax = 50f;
    [SerializeField]
    private float resourceRate = 1f;

    private TMP_Text resourceIndicator;

    [SerializeField]
    private float radiusEffect = 5f;
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        resourceIndicator = transform.Find("ResourceIndicator").GetComponent<TMP_Text>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();

        if (isMaker)
        {
            sphereCollider.enabled = false;
        }
        if (!isMaker)
        {
            resourceIndicator.enabled = false;
            sphereCollider.radius = radiusEffect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMaker)
        {
            if (resource != resourceMax)
            {
                resource = Mathf.Clamp(resource + resourceRate * Time.deltaTime, 0f, resourceMax);
                resourceIndicator.SetText(resourceName + ":\n" + Mathf.FloorToInt(resource));
            }
        }
        else
        {

        }
    }

    // called whenever the player collects the resources from this construct
    public float CollectAll()
    {
        if (isMaker)
        {
            float amount = resource;
            resource = 0f;
            return amount;
        }
        return 0f;
    }

    public void Disassemble()
    {
        Destroy(this.gameObject);
    }

    public bool IsMaker()
    {
        return isMaker;
    }

    public string GetResource()
    {
        return resourceName;
    }
}