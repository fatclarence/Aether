﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemSkill m_ItemSkill;
    public void Interact(ICanInteract interactor, InteractionType interactionType)
    {
        if (interactor != null && interactor is Player player)
        {
            switch (interactionType)
            {
                case InteractionType.INTERACTION_TRIGGER_ENTER:
                    PlayPickUpSound();
                    // Debug.Log("Current transform of Items: " + transform.position);
                    // Debug.Log("Current player transform: " + PlayerManager.Instance.GetLocalPlayer().transform.position);
                    HandleItemSkill(player);
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
    
    private void PlayPickUpSound()
    {
        AudioManager.m_Instance.PlaySound("MAGIC_Powerup", 1.0f, 1.2f);
    }

    public void HandleItemSkill(Player player)
    {

        m_ItemSkill.InitializeSkill();
        player.GetSkillHandler().AddSkill(m_ItemSkill); //must be after or else will be deleted

    }
}
