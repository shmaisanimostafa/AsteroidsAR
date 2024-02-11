using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UFO : MonoBehaviour
{
	[SerializeField] private UFOStates _currentState;
	[SerializeField] private List<Vector3> _trajectoryVectors = new List<Vector3>();
	[SerializeField] private int _trajectoriesPerSpawn = 2;
	[SerializeField] private float _spawnDistanceFromPlayer = 20;
	[SerializeField] private float _xyOffset = 10;
	[SerializeField] private float _movementSpeed = 5;
	[SerializeField] private int _cooldownMinTime = 5;
	[SerializeField] private int _cooldownMaxTime = 15;
	[SerializeField] private GameState _gameState;
	private Transform _player;

	[SerializeField] private UnityEvent OnStopAttacking;
	[SerializeField] private UnityEvent OnStartAttacking;
	[SerializeField] private UnityEvent OnDie;

	[SerializeField] private AudioSfx _ufoOnScene;

	// Idle State
	private IEnumerator IdleRoutine()
	{
		transform.position = new Vector3(1000, 1000, 1000);
		_trajectoryVectors.Clear();
		yield return new WaitForSeconds(Random.Range(_cooldownMinTime, _cooldownMaxTime));
		CurrentState = UFOStates.Attacking;
	}

	// Die Function
	public void Die()
	{
		OnDie?.Invoke();
		OnStopAttacking?.Invoke();
		StopAllCoroutines();
		StartCooldown();
		_ufoOnScene.StopAudio();
	}

	// Attack State
	private Vector3 GetNewPositionVector()
	{
		return new Vector3(Random.Range(-_xyOffset, _xyOffset), Random.Range(-_xyOffset, _xyOffset), Random.Range(_player.position.z, _player.position.z + 15));
	}

	public void StartAttacking()
	{
		// Check if the player is available
		if (_player == null) return;

		// Define the new spawn position
		Vector3 spawnPosition = GetNewPositionVector();
		transform.position = spawnPosition;

		// Define new random trajectory vectors
		for (int i = 0; i < _trajectoriesPerSpawn; i++)
		{
			_trajectoryVectors.Add(GetNewPositionVector());
		}
		_ufoOnScene.PlayAudio(gameObject);
		StartCoroutine(AttackMovement());
	}

	IEnumerator AttackMovement()
	{
		for (int i = 0; i < _trajectoryVectors.Count; i++)
		{
			float distance = Vector3.Distance(transform.position, _trajectoryVectors[i]);

			while (distance > 0.5f && !_gameState.GameOver)
			{
				// wait a frame
				yield return null;

				transform.position = Vector3.MoveTowards(transform.position, _trajectoryVectors[i], Time.deltaTime * _movementSpeed);

				distance = Vector3.Distance(transform.position, _trajectoryVectors[i]);
			}
		}

		CurrentState = UFOStates.Idle;
	}

	public void StartCooldown()
	{
		StartCoroutine(IdleRoutine());
		_ufoOnScene.StopAudio();
	}

	// Get & Set for the Current State
	public UFOStates CurrentState
	{
		get => _currentState;
		set 
		{
			_currentState = value;
			if (_currentState == UFOStates.Attacking)
			{
				OnStartAttacking?.Invoke();
			} else
			{
				OnStopAttacking?.Invoke();
			}
		}
	}

	public enum UFOStates
	{
		Idle,
		Attacking
	}

	// Start Method
	private void Start()
	{
		// Find the player in the scene
		GameObject playerObject = GameObject.Find("Player");

		// If the player is found, then grab its transform object
		if (playerObject)
		{
			_player = playerObject.transform;
		}

		// UFO first state is idle
		CurrentState = UFOStates.Idle;
	}
}
