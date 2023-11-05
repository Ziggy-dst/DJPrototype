using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EditManager : MonoBehaviour
{
    public static bool isEditing;
    public PostProcessVolume postProcessVolume;

    void Start()
    {
        isEditing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isNotDragging = true;
            foreach (var draggable in GameObject.FindObjectsByType<Draggable>(FindObjectsSortMode.None))
            {
                if (draggable.isDragging) isNotDragging = false;
            }

            if (isNotDragging) isEditing = !isEditing;
        }

        postProcessVolume.weight = isEditing ? 1 : 0;
    }
}
