using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObject : MonoBehaviour
{

    // public Transform planeDebug;
    // public GameObject target;
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
<<<<<<< HEAD
    AudioSource sound;
    public Material crossSectionMaterial;
    
=======

    public Material crossSectionMaterialRedMeat;
    public Material crossSectionMaterialYellowPotato;
    public Material crossSectionMaterialGreenLettuce;
>>>>>>> 13a463490714526d82a248a46deb7aa5cca6a06f

    public float cutForce = 2000;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     Slice(target);
        // }
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);

        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        // SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        

        if (hull != null)
        {
            string name = "Algo";
            Material materiall = crossSectionMaterialRedMeat;
            if (target.name == "Carne")
            {
                name = "Carne";
                materiall = crossSectionMaterialRedMeat;
            }
            else if (target.name == "Potato")
            {
                name = "Potato";
                materiall = crossSectionMaterialYellowPotato;
            }
            else if (target.name == "Lettuce")
            {
                name = "Lettuce";
                materiall = crossSectionMaterialGreenLettuce;
            }

            GameObject upperHull = hull.CreateUpperHull(target, materiall);
            SetupSlicedComponent(upperHull, name);


            GameObject loverHull = hull.CreateLowerHull(target, materiall);

            SetupSlicedComponent(loverHull, name);
            Destroy(target);

            sound.Play(0);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject, string name)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);


        slicedObject.tag = "Ingredient";
        slicedObject.name = name;

        Grabbable gr = slicedObject.AddComponent<Grabbable>();
        gr.InjectOptionalRigidbody(rb);

        HandGrabInteractable hgi = slicedObject.AddComponent<HandGrabInteractable>();
        hgi.InjectOptionalPointableElement(gr);
        hgi.InjectRigidbody(rb);

        PhysicsGrabbable pg = slicedObject.AddComponent<PhysicsGrabbable>();
        pg.InjectPointable(gr);
        pg.InjectRigidbody(rb);

        GrabInteractable gi = slicedObject.AddComponent<GrabInteractable>();
        gi.InjectOptionalPointableElement(gr);
        gi.InjectRigidbody(rb);


        // JUST FOR
        // XR SIMULATOR :)
        slicedObject.AddComponent<XRGrabInteractable>();
    }
}
