using System;
using System.Collections;
using System.Collections.Generic;
using SpaceHunter.Scripts.Models.Dialog;
using SpaceHunter.Scripts.Modules.Dialog;
using SpaceHunter.Scripts.Modules.Dialog.View;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private TriggerDialogModule _triggerDialogModule;
    [SerializeField] private Scenarios _scenario;
    
    private void OnValidate()
    {
        _triggerDialogModule = FindObjectOfType<TriggerDialogModule>() as TriggerDialogModule;
        if (_triggerDialogModule == null)
        {
            Debug.LogError("TriggerDialogModule not found on scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerDialogModule.OnTrigger(_scenario);
            gameObject.SetActive(false);
        }
    }
}