using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3;
    [SerializeField]
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        cam  = GetComponent<PlayerLook>().cam; 
    }

    // Update is called once per frame
    void Update()
    {
        //this will create a ray at the centre of the camera, shooting outward.
        //it as 2 components 1.origin and 2. a direction.
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction*distance);
        RaycastHit hitInfo; // variable to store our collision information.
        if(Physics.Raycast(ray,out hitInfo,distance,mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
            }
        }
    }
}
