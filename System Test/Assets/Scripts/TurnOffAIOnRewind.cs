using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chronos;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class TurnOffAIOnRewind : MonoBehaviour
{
    private Timeline timeline;
    private AIBrain aiBrain;
    void Start()
    {
        timeline = GetComponent<Timeline>();
        aiBrain = GetComponent<AIBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeline.timeScale <= 0) aiBrain.CurrentState = aiBrain.States.Last();
        // else aiBrain.CurrentState = aiBrain.States[0];

    }
}
