﻿using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private BattleStateManager _stateManager;

	[SerializeField] private UIFacade _uiFacade;
	[SerializeField] private AudioSource _audioSource;

	private void Start()
	{
		_stateManager = new BattleStateManager(this, _uiFacade, _audioSource);
		_stateManager.Init();
	}

	private void Update() => _stateManager.Tick();

	private void OnDestroy()
	{
		_stateManager.OnDestroy();
	}
}
