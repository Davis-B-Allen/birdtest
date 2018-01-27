using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndOrbit : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float orbitSpeed = 50f;
    public float desiredOrbitDistance;
    public float attractionSpeed;
    public Transform target;
    public Vector3 rotateSelfAround;
    public Vector3 orbitAround;

    void Start()
    {
        desiredOrbitDistance = Vector3.Distance(target.position, transform.position);
    }

    void Update()
    {
        transform.Rotate(rotateSelfAround, rotationSpeed * Time.deltaTime);
        transform.RotateAround(target.position, orbitAround, orbitSpeed * Time.deltaTime);
        // DesiredMoonDistance += 0.01f;
        //fix possible changes in distance
        float currentMoonDistance = Vector3.Distance(target.position, transform.position);
        Vector3 towardsTarget = transform.position - target.position;
        transform.position += (desiredOrbitDistance - currentMoonDistance) * towardsTarget.normalized * attractionSpeed;
    }
}
