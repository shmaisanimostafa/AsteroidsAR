using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialRotationTowardsTarget : MonoBehaviour
{
    [SerializeField] private string _targetName;
    [SerializeField] private Vector3 _randomOffset;

    void Start()
    {
        // Look for the target in the hierarchy
        GameObject targetObject = GameObject.Find(_targetName);

        // Approve the existence of the target, if not, do nothing
        if (targetObject == null) return;

        // Get the target's position to get the direction
        Vector3 direction = targetObject.transform.position - transform.position;

        // Randomize using the Offset and add it to the direction
        direction += new Vector3(
            Random.Range(-_randomOffset.x, _randomOffset.x),
            Random.Range(-_randomOffset.y, _randomOffset.y),
            Random.Range(-_randomOffset.z, _randomOffset.z));

        // Set the target's initial rotation based on the direction
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

}
