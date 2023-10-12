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
    public class CinemachineKnobListener : KnobListener
    {
        [System.Serializable]
        private enum VariableTypes
        {
            FOV
        }
        
        [Tooltip("The game object that the knob will affect")]
        [SerializeField] private CinemachineVirtualCamera cinemachineToManipulate = default;

        [Tooltip("The part of the variable to affect positively")]
        [SerializeField] private VariableTypes cinemachineVariableTypes = VariableTypes.FOV;
        
        [Tooltip("Minimum value to set the value to")]
        [SerializeField] private float MinValue = 0;

        [Tooltip("Maximum value to set the value to")]
        [SerializeField] private float MaxValue = 0;

        [Tooltip("Whether or not the min and max values are adding to the initial values of the variable")]
        [SerializeField] private bool RelativeToInitialValue = true;

        private float initialFOV;

        void Awake()
        {
            if (cinemachineToManipulate == null)
            {
                Debug.LogException(new MissingReferenceException("A cinemachine to manipulate is required"), this);
                return;
            }

            initialFOV = cinemachineToManipulate.m_Lens.FieldOfView;
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
            switch (cinemachineVariableTypes)
            {
                case VariableTypes.FOV:
                    cinemachineToManipulate.m_Lens.FieldOfView = variableValue + (RelativeToInitialValue ? initialFOV : 0);
                    break;
                default:
                    Debug.LogException(new System.InvalidOperationException("Invalid PositiveVariableTypes value " + cinemachineVariableTypes), this);
                    return;
            }
        }
    }
}
