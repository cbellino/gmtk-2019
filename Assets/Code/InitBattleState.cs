﻿using System.Collections.Generic;
using UnityEngine;

namespace OneSecond
{
	public class InitBattleState : IBattleState
	{
		private readonly BattleStateManager _manager;

		public InitBattleState(BattleStateManager manager)
		{
			_manager = manager;
		}

		public void EnterState()
		{
			_manager.Allies = new List<UnitFacade>
			{
				SpawnUnit(new Unit("Ally1", Alliances.Ally), 0),
				SpawnUnit(new Unit("Ally1", Alliances.Ally), 1)
			};
			_manager.Foes = GenerateRandomFoes();

			_manager.ChangeState(BattleStates.CpuTurn);
		}

		private List<UnitFacade> GenerateRandomFoes()
		{
			var foes = new List<UnitFacade>();
			var randomCount = Random.Range(1, 3);

			for (int i = 0; i <= randomCount; i++)
			{
				var randomEnemy = Random.Range(1, 3);
				var unit = SpawnUnit(new Unit($"Foe{randomEnemy.ToString()}", Alliances.Foe), i);
				foes.Add(unit);
			}

			return foes;
		}

		public static Vector3 GetPosition(Alliances alliance, int index)
		{
			var offset = alliance == Alliances.Ally ? 0f : 7f;
			return new Vector3(2f * index + offset, -2.936f, 0f);
		}

		private UnitFacade SpawnUnit(Unit unit, int index)
		{
			var instance = Object.Instantiate(Resources.Load<GameObject>(unit.Name));
			instance.transform.position = GetPosition(unit.Alliance, index);
			instance.name = unit.Name;

			var facade = instance.GetComponent<UnitFacade>();
			if (facade == null)
			{
				throw new UnityException("Missing UnitFacade on unit prefab.");
			}

			facade.Init(unit);

			return facade;
		}

		public void Update() { }

		public void ExitState() { }
	}
}
