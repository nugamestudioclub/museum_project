using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1f;

    private PlayerStats playerStats;

    public GameObject foodMakerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
            {
                if (hit.collider.gameObject.tag == "Construct")
                {
                    ConstructManager manager = hit.collider.gameObject.GetComponent<ConstructManager>();
                    float change = manager.CollectAll();
                    playerStats.ConsumeResource(change, manager.GetResource());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
            {
                if (hit.collider.gameObject.tag == "Construct")
                {
                    hit.collider.gameObject.GetComponent<ConstructManager>().Disassemble();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
            {
                if (Vector3.Normalize(hit.normal) == Vector3.up)
                {
                    GameObject construct = Instantiate(foodMakerPrefab, hit.point + Vector3.up * 0.3f, Quaternion.identity);
                    //construct.AddComponent<ConstructManager>();
                }    
            }
        }
    }
}