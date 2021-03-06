﻿using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework;
using UnityEngine.UI;
using DG.Tweening;

namespace MineS
{
    /// <summary>
    /// スタッフロールのテキストの表示を管理する
    /// </summary>
    public class StaffRollElement : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private float fadeInDuration;

        [SerializeField]
        private float fadeOutDelay;

        [SerializeField]
        private float fadeOutDuration;

        public void Setup(StaffRollManager manager, string message, float visibleDelay, float visibleDuration)
        {
            this.text.text = message;
            this.StartFadeIn(manager, visibleDelay, visibleDuration);
        }

        private void StartFadeIn(StaffRollManager manager, float visibleDelay, float visibleDuration)
        {
            this.SetAlpha(0.0f);
            DOTween.ToAlpha(() => this.text.color, c => this.text.color = c, 1.0f, this.fadeInDuration)
            .SetDelay(visibleDelay)
            .OnComplete(() => this.StartFadeOut(manager, visibleDuration));
        }

        private void StartFadeOut(StaffRollManager manager, float delay)
        {
            this.SetAlpha(1.0f);
            DOTween.ToAlpha(() => this.text.color, c => this.text.color = c, 0.0f, this.fadeOutDuration)
            .SetDelay(delay)
            .OnComplete(() => manager.Complete(this));
        }

        private void SetAlpha(float alpha)
        {
            var color = this.text.color;
            color.a = alpha;
            this.text.color = color;
        }
    }
}
