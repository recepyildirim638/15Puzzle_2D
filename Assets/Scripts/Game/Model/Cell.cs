using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Game.UI;
using Game.Manager;
using System;

namespace Game.Model
{
    public class Cell : MonoBehaviour
    {
       
        [SerializeField] private int index;

        [SerializeField] private int value;

        [SerializeField] private TMP_Text valueText;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            ActionManager.ChangeCellColor += ChangeCellColorFunc;
            ChangeCellColorFunc(SettingsMenu.ins.GetCurrentCellColor());
        }

        private void OnDisable()
        {
            ActionManager.ChangeCellColor -= ChangeCellColorFunc;
        }

        private void ChangeCellColorFunc(Color color)
        {
            spriteRenderer.color = color;
        }

        public int GetValue() => value;
        public int GetIndex() => index;
        public void SetIndex(int val) => index = val;

        public void SetValue(int val)
        {
            this.value = val;
            SetText(value);
        }

        public void SetResumeValue() => SetText(this.value);

        public void SetText(int val)
        {
            valueText.text = val.ToString();
        }
       
    }
}

