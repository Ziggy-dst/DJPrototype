using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KnobsAsset;
using UnityEngine;

public class Connecter : MonoBehaviour
{
    private Draggable draggable;
    private int knobCount;
    private List<SliderPairKnob> allPairedModule;
    private List<Connecter> allConnecters;
    public SliderPairKnob pairKnob1;
    public SliderPairKnob pairKnob2;

    void Start()
    {
        allPairedModule = GameObject.FindObjectsOfType<SliderPairKnob>().ToList();
        draggable = GetComponent<Draggable>();
        allConnecters = FindObjectsOfType<Connecter>().ToList();
    }


    void Update()
    {
        if (!EditManager.isEditing)
        {
            float closestDistance = 99f;
            float secondToClosestDistance = 100f;
            
            foreach (var module in allPairedModule)
            {
                if (!module.gameObject.GetComponent<Draggable>().isDragging)
                {
                    // print("Distance to " + module + " = " + (transform.localPosition - module.transform.localPosition).magnitude);
                    float distance = Vector3.Distance(transform.localPosition, module.transform.localPosition);

                    if (distance < closestDistance)
                    {
                        secondToClosestDistance = closestDistance;
                        pairKnob2 = pairKnob1;
                        closestDistance = distance;
                        if (closestDistance <= 0.75f) pairKnob1 = module;
                    }
                    else if (distance < secondToClosestDistance)
                    {
                        secondToClosestDistance = distance;
                        if (secondToClosestDistance <= 0.75f) pairKnob2 = module;
                    }

                    List<SliderPairKnob> currentPairedKnob = new List<SliderPairKnob>();
                    foreach (var connecter in allConnecters)
                    {
                        currentPairedKnob.Add(connecter.pairKnob1); 
                        currentPairedKnob.Add(connecter.pairKnob2);
                    }

                    if (!currentPairedKnob.Contains(module)) module.pairedKnob = null;
                    // if ((transform.localPosition - module.transform.localPosition).magnitude <=0.75f)
                    // {
                    //     if (pairKnob1 == null)
                    //     {
                    //         pairKnob1 = module;
                    //     }
                    //     else if (pairKnob2 == null)
                    //     {
                    //         pairKnob2 = module;
                    //     }
                    //
                    //     if (pairKnob1 != null && pairKnob2 != null && pairKnob1 == pairKnob2) pairKnob2 = null;
                    // }
                }
            }

            if (pairKnob1 != null && pairKnob2 != null && pairKnob1 != pairKnob2) 
            {
                SliderPairKnob lastPairedKnob1 = pairKnob1.pairedKnob;
                SliderPairKnob lastPairedKnob2 = pairKnob2.pairedKnob;
                
                pairKnob1.pairedKnob = pairKnob2;
                pairKnob2.pairedKnob = pairKnob1;

                if (lastPairedKnob1 != pairKnob1.pairedKnob)
                {
                    pairKnob1.ResetKnob();
                }
                
                if (lastPairedKnob2 != pairKnob2.pairedKnob)
                {
                    pairKnob2.ResetKnob();
                }

                lastPairedKnob1 = pairKnob1.pairedKnob;
                lastPairedKnob2 = pairKnob2.pairedKnob;

                // print("paired!");
            }

            if (pairKnob1 == pairKnob2)
            {
                pairKnob1.pairedKnob = null;
            }
        }
        
        
        // if (draggable.isDragging)
        // {
        //     if (pairKnob1 != null) pairKnob1.pairedKnob = null;
        //     if (pairKnob2 != null) pairKnob2.pairedKnob = null;
        //     pairKnob1 = pairKnob2 = null;
        // }
    }
}
