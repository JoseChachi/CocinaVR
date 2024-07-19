using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    //AudioSource sound;
    bool isPressed;
    public GameObject obj;
    public Transform spawn_position;


    // Start is called before the first frame update
    void Start()
    {
        //sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition += new Vector3 (0, -0.1f, 0);
            presser = other.gameObject;
            onPress.Invoke ();
            //sound.Play ();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition += new Vector3(0, 0.1f, 0);
            onRelease.Invoke ();
            isPressed=false;
        }
    }

    public void SpawnSphere()
    {
        GameObject new_obj = Instantiate(obj, spawn_position.position, spawn_position.rotation);
        Rigidbody rb = new_obj.GetComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Grabbable gr = new_obj.AddComponent<Grabbable>();
        gr.InjectOptionalRigidbody(rb);

        HandGrabInteractable hgi = new_obj.AddComponent<HandGrabInteractable>();
        hgi.InjectOptionalPointableElement(gr);
        hgi.InjectRigidbody(rb);

        PhysicsGrabbable pg = new_obj.AddComponent<PhysicsGrabbable>();
        pg.InjectPointable(gr);
        pg.InjectRigidbody(rb);

        GrabInteractable gi = new_obj.AddComponent<GrabInteractable>();
        gi.InjectOptionalPointableElement(gr);
        gi.InjectRigidbody(rb);


        // JUST FOR
        // XR SIMULATOR :)
        new_obj.AddComponent<XRGrabInteractable>();
    }

}
