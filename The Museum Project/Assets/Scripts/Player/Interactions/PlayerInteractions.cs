using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1f;

    private PlayerStats playerStats;

    private static GameObject selectedConstruct;
    /*
    public GameObject foodMakerPrefab;
    public GameObject waterMakerPrefab;
    public GameObject healthAreaPrefab;
    */

    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();

        //selectedConstruct = foodMakerPrefab;
        selectedConstruct = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R Pressed");
            InteractWithObject();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisassembleObject();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlaceObject();
        }

        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedConstruct = foodMakerPrefab;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedConstruct = waterMakerPrefab;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedConstruct = healthAreaPrefab;
        }
        */
        //Debug.DrawRay(transform.position + new Vector3(0f, 0.6f, 0f), Camera.main.transform.forward * 10f, Color.green, 5f);
    }

    public void InteractWithObject()
    {
        RaycastHit hit;
        Debug.Log("interact");
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green, 3f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            Debug.Log(hit.collider.gameObject);
            //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.green, 5f);
            if (hit.collider.gameObject.tag == "Construct")
            {
                ConstructMaker maker = hit.collider.gameObject.GetComponent<ConstructMaker>();
                if (maker != null)
                {
                    float change = maker.Interact();
                    playerStats.ConsumeResource(change, maker.GetResource());
                }
            }
        }
    }

    public void DisassembleObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            if (hit.collider.gameObject.tag == "Construct")
            {
                int reward = hit.collider.gameObject.GetComponent<ConstructBase>().Disassemble();
                playerStats.ChangeNanites(reward);
            }
        }
    }

    public void PlaceObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            if (Vector3.Normalize(hit.normal) == Vector3.up && hit.collider.gameObject.tag != "Construct")
            {
                int cost = selectedConstruct.GetComponent<ConstructBase>().GetCost();
                if (cost <= playerStats.GetNanites())
                {
                    GameObject construct = Instantiate(selectedConstruct, hit.point + Vector3.up * 0.3f, Quaternion.identity);
                    playerStats.ChangeNanites(-cost);
                }
            }
        }
    }

    public static void SelectConstruct(GameObject s)
    {
        selectedConstruct = s;
    }
}