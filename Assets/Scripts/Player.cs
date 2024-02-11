using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent OnShoot;
    [SerializeField] private float _fireRate = 0.3f;

    private bool _canShoot = true;
    private bool _shoot;

    void Update()
    {
        // Get input from the player
        _shoot = Input.GetMouseButtonDown(0);

        // Check if player can shoot, when he clicks shoot
        if (_shoot && _canShoot)
        {
            // Invoke Shooting
            OnShoot?.Invoke();

            // Disable the ability to shoot
            _canShoot = false;

            // Allow reshooting after fireRate
            StartCoroutine(EnableShooting());
        }
    }

    IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

}
