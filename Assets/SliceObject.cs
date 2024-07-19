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

    public Material crossSectionMaterial;

    public float cutForce = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);

            SetupSlicedComponent(upperHull);

            GameObject loverHull = hull.CreateLowerHull(target, crossSectionMaterial);

            SetupSlicedComponent(loverHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);


        slicedObject.tag = "Ingredient";

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
