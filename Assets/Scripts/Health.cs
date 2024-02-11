using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;
    [SerializeField] private UnityEvent<int> OnRecieveDamage;
    [SerializeField] private UnityEvent OnZeroHealth;
    [SerializeField] private UnityEvent<int> OnRecieveHealth;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public void RecieveDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        OnRecieveDamage?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            OnZeroHealth?.Invoke();
        }
    }
    public void GainHealth(int gainAmount)
    {
        //_currentHealth += gainAmount;
        //_currentHealth -= Mathf.Clamp(_currentHealth, 0, _maxHealth);
        
        if(_currentHealth + gainAmount > _maxHealth)
        {
            _currentHealth = _maxHealth;
        } else
        {
            _currentHealth += gainAmount;
        }
		OnRecieveHealth?.Invoke(_currentHealth);
	}

}
