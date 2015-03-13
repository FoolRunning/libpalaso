﻿using System;
using System.Windows.Forms;
using SIL.Keyboarding;
using SIL.Windows.Forms.Keyboarding;
using SIL.Windows.Forms.Keyboarding.Windows;

namespace TestAppKeyboard
{
	public partial class KeyboardForm : Form, IWindowsLanguageProfileSink
	{
		public KeyboardForm()
		{
			InitializeComponent();
			if (DesignMode)
				return;

			KeyboardController.Initialize();
			KeyboardController.RegisterControl(testAreaA, this);
			KeyboardController.RegisterControl(testAreaB, this);
			KeyboardController.RegisterControl(testAreaC, this);
			LoadKeyboards(keyboardsA);
			LoadKeyboards(keyboardsB);
			LoadKeyboards(keyboardsC);
			LoadKeyboards(currentKeyboard);
		}

		public void LoadKeyboards(ComboBox comboBox)
		{
			var keyboards = Keyboard.Controller.AvailableKeyboards;
			foreach (var keyboard in keyboards)
			{
				comboBox.Items.Add(keyboard);
				Console.WriteLine("added keyboard id: {0}, name: {1}", keyboard.Id, keyboard.Name);
			}
			comboBox.SelectedIndex = 0;
		}

		private void testAreaA_Enter(object sender, EventArgs e)
		{
			if (cbOnEnter.Checked)
			{
				var wantKeyboard = (IKeyboardDefinition) keyboardsA.SelectedItem;
				Console.WriteLine("Enter A: Set to {0}", wantKeyboard);
				wantKeyboard.Activate();
			} else {
				Console.WriteLine("Enter A");
			}
		}

		private void testAreaB_Enter(object sender, EventArgs e)
		{
			if (cbOnEnter.Checked)
			{
				var wantKeyboard = (IKeyboardDefinition) keyboardsB.SelectedItem;
				Console.WriteLine("Enter B: Set to {0}", wantKeyboard);
				wantKeyboard.Activate();
			} else {
				Console.WriteLine("Enter B");
			}
		}

		private void testAreaC_Enter(object sender, EventArgs e)
		{
			if (cbOnEnter.Checked)
			{
				var wantKeyboard = (IKeyboardDefinition) keyboardsC.SelectedItem;
				Console.WriteLine("Enter C: Set to {0}", wantKeyboard);
				wantKeyboard.Activate();
			} else {
				Console.WriteLine("Enter C");
			}
		}


		#region IWindowsLanguageProfileSink Members

		public void OnInputLanguageChanged(IKeyboardDefinition previousKeyboard, IKeyboardDefinition newKeyboard)
		{
			Console.WriteLine("TestAppKeyboard.OnLanguageChanged: previous {0}, new {1}",
				previousKeyboard != null ? previousKeyboard.Layout : "<null>",
				newKeyboard != null ? newKeyboard.Layout : "<null>");
			lblCurrentKeyboard.Text = newKeyboard != null ? newKeyboard.Layout : "<null>";
		}

		#endregion
	}
}
