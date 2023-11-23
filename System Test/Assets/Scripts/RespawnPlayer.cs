using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnobsAsset;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private Health _health;
    private LevelManager _levelManager;
    public float respawnDelay;
    private float respawnTimer;
    private List<SliderPairKnob> pairedSliders;

    public static Action OnRespawn;

    void Start()
    {
        _health = GetComponent<Health>();
        _levelManager = FindObjectOfType<LevelManager>();
        respawnTimer = respawnDelay;
        pairedSliders = FindObjectsByType<SliderPairKnob>(FindObjectsSortMode.None).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                RespawnReset();
                respawnTimer = respawnDelay;
            }
        }
    }

    public void RespawnReset()
    {
        GetComponent<MMPath>().Initialization();
        GetComponent<AIActionMovePatrol3D>().ResetPatrol(transform.position);
        GetComponent<AIBrain>().ResetBrain();
        _levelManager.Respawn();
        Invoke("SetKnob", 0.4f);
        OnRespawn();
    }

    private void SetKnob()
    {
        foreach (var slider in pairedSliders)
        {
            slider.SetKnobOnRespawn();
        }
    }
}
