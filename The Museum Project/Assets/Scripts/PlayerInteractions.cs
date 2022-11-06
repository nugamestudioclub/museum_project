using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1f;

    private PlayerStats playerStats;

    private GameObject selectedConstruct;
    public GameObject foodMakerPrefab;
    public GameObject waterMakerPrefab;
    public GameObject healthAreaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();

        selectedConstruct = foodMakerPrefab;
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
    }

    public void InteractWithObject()
    {
        RaycastHit hit;
        Debug.Log("interact");
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            Debug.Log(hit.collider.gameObject);
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
}