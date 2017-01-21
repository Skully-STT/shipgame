using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player2 : NetworkBehaviour
{
    public float rayLenght = 2f;
    public Camera playerCamera;

    public void Update()
    {
        //if (!isLocalPlayer)
        //    return;

            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*rayLenght,Color.red,2f,false);
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward * rayLenght);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray,out raycastHit, rayLenght))
            {
                Debug.Log(raycastHit.transform.name );
                InteractableObject interactableObject = raycastHit.transform.GetComponent<InteractableObject>();
                if (interactableObject)
                {
                    interactableObject.HitDetected();
                }
            }
    }   
}
