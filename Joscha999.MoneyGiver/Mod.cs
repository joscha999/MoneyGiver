using SimAirport.Modding.Base;
using SimAirport.Modding.Settings;
using UnityEngine;
using SimAirport.Modding.Data;

namespace Joscha999.MoneyGiver {
	public class Mod : BaseMod {
		public override string Name => "MoneyGiver";

		public override string InternalName => "joscha999.MoneyGiver";

		public override string Description => "Gives money, can be used to buy happy little trees";

		public override string Author => "joscha999";

		public override SettingManager SettingManager { get; set; } //will be filled by game

		private double amountGiven;

		public override void OnSettingsLoaded() {
			var moneyAmount = new SliderSetting {
				Minimum = 0,
				Maximum = 100000000,
				Name = "Amount given",
				Value = 1000000
			};

			moneyAmount.OnValueChanged += val => amountGiven = val;
			SettingManager.AddDefault("moneyAmount", moneyAmount);

			if (!SettingManager.TryGetValue("moneyAmount", out amountGiven))
				amountGiven = 1000000;
		}

		public override void OnTick() {
			var gameState = Game.Instance.GetState();

			if ((gameState == GameState.Career || gameState == GameState.Sandbox)
				&& Input.GetKey(KeyCode.LeftControl)
				&& (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))) {
				Game.Instance.MoneyBalance += amountGiven;
				Debug.Log(gameState);
			}
		}
	}
}