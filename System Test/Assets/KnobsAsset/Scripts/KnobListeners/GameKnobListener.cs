using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for assigning a knob value to change the transform values of an object.
    /// </summary>
    public class GameKnobListener : KnobListener
    {
        [System.Serializable]
        private enum VariableTypes
        {
            TimeScale, 
        }
        
        // [Tooltip("The game object that the knob will affect")]
        // [SerializeField] private CinemachineVirtualCamera cinemachineToManipulate = default;

        [Tooltip("The part of the variable to affect positively")]
        [SerializeField] private VariableTypes gameVariableTypes = VariableTypes.TimeScale;
        
        [Tooltip("Minimum value to set the value to")]
        [SerializeField] private float MinValue = 0;

        [Tooltip("Maximum value to set the value to")]
        [SerializeField] private float MaxValue = 0;

        [Tooltip("Whether or not the min and max values are adding to the initial values of the variable")]
        [SerializeField] private bool RelativeToInitialValue = true;

        private float initialTimeScale;

        void Awake()
        {
            initialTimeScale = Time.timeScale;
        }

        private void Start()
        {

        }

        private void Update()
        {
           
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            float variableValue = Mathf.Lerp(MinValue, MaxValue, knobPercentValue);
            switch (gameVariableTypes)
            {
                case VariableTypes.TimeScale:
                    Time.timeScale = variableValue + (RelativeToInitialValue ? initialTimeScale : 0);
                    break;
                default:
                    Debug.LogException(new System.InvalidOperationException("Invalid PositiveVariableTypes value " + gameVariableTypes), this);
                    return;
            }
        }
    }
}
