﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gang1057.Ludiwuri.Game.UI
{

    /// <summary>
    /// Used to detect the players sliding action
    /// </summary>
    public class MatchSlider : MonoBehaviour, IPointerDownHandler
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float minSliderDelta;
        [SerializeField] private float strikeDistance;
        /// <summary>
        /// The slider used to update the value
        /// </summary>
        [SerializeField] private Slider slider;

#pragma warning restore 649

        /// <summary>
        /// The value the slider started at
        /// </summary>
        private float startValue = 0;
        /// <summary>
        /// The previous value
        /// </summary>
        private float prevValue;
        private float prevDistance;

        #endregion

        #region Methods

        /// <summary>
        /// Called when the slider is clicked
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            StartAtCurrentValue();
        }

        /// <summary>
        /// Called when the sliders value changes
        /// </summary>
        /// <param name="value">The sliders new value</param>
        public void OnSliderValueChanged(float value)
        {
            float delta = Mathf.Abs(value - prevValue);
            float distance = Mathf.Abs(value - startValue);

            // If the cursor moved too little

            if (delta < minSliderDelta)
            {
                // Start again

                StartAtCurrentValue();
            }

            // If the cursor moved in the wrong direction

            else if (distance < prevDistance)
            {
                // Start again

                StartAtCurrentValue();
            }

            // If the cursor moved enough for a strike

            else if (distance >= strikeDistance)
            {
                // TODO: Trigger strike
            }

            // If the cursor moved correctly, but not enough for a strike

            else
            {
                prevValue = value;
                prevDistance = distance;
            }
        }


        private void StartAtCurrentValue()
        {
            startValue = slider.value;
            prevValue = startValue;
            prevDistance = 0;
        }

        #endregion

    }

}