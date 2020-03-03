﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillHandler : MonoBehaviour
{
    public ItemSkill[] m_SkillSlots = new ItemSkill[3];
    
    void Start()
    {
        AetherInput.GetPlayerActions().UseSkill.performed += UseSkillAt;
    }

    //key bindings
    public void UseSkillAt(InputAction.CallbackContext ctx)
    {
        int index = UIManager.Instance.GetSkillsIndex();
        if (m_SkillSlots[index] == null)
            return;

        m_SkillSlots[index].UseSkill();
        m_SkillSlots[index].m_NoOfUses--;
        if (m_SkillSlots[index].m_NoOfUses == 0)
        {
            RemoveSkill(index);
            UIManager.Instance.RemoveSkill();
        }
    }

    /*
     * Attempts to add skill to player slots,
     * returns true if successful
     */
    public bool AddSkill(ItemSkill itemSkill)
    {
        if (m_SkillSlots[0] == null)
        {
            m_SkillSlots[0] = itemSkill;
            return true;
        }

        if (m_SkillSlots[1] == null)
        {
            m_SkillSlots[1] = itemSkill;
            return true;
        }
        
        if (m_SkillSlots[2] == null)
        {
            m_SkillSlots[2] = itemSkill;
                return true;
        }

        return false;
    }

    /*
     * Attempts to remove skill from player slots,
     * returns true if successful
     */
    public bool RemoveSkill(int index)
    {
        m_SkillSlots[index] = null;
        return true;
    }
    
}
