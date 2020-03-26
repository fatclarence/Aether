﻿using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarHandler : MonoBehaviour
{
    [SerializeField]
    private float m_DeltaMultiplier = 0.05f;
    [SerializeField]
    private float m_CriticalValue = 0.2f;

    [SerializeField]
    private Image m_HealthBar;

    [SerializeField]
    private Animator m_Animator;

    private HealthHandler m_PlayerHealth;

    private float m_HealthDelta = 0.0f;

    void Update()
    {
        if (m_HealthBar != null && m_HealthDelta != 0.0f)
            UpdateHealthPercentage();

        if (m_Animator != null)
        {
            if (IsInCriticalHealth())
                m_Animator.SetBool("isCriticalHealth", true);
            else
                m_Animator.SetBool("isCriticalHealth", false);
        }
    }

    public void SetPlayerAttachment(Player player)
    {
        HealthHandler healthHandler = player.GetHealthHandler();
        if (healthHandler == null)
        {
            Debug.LogWarning("Health Handler not found on local player");
            return;
        }
        m_PlayerHealth = healthHandler;
        m_PlayerHealth.HealthChanged += OnHealthChanged;
    }

    public void OnHealthChanged(float deltaHealth) 
    {
        m_HealthDelta += deltaHealth / m_PlayerHealth.m_MaxHealth;
    }

    private void UpdateHealthPercentage()
    {
        // Max delta per frame
        float maxDelta = m_DeltaMultiplier * Time.deltaTime;

        // Update health
        if (m_HealthDelta > maxDelta)
        {
            m_HealthBar.fillAmount += maxDelta;
            m_HealthDelta -= maxDelta;
        }
        else if (m_HealthDelta < -maxDelta)
        {
            m_HealthBar.fillAmount -= maxDelta;
            m_HealthDelta += maxDelta;
        }
        else
        {
            m_HealthBar.fillAmount += m_HealthDelta;
            m_HealthDelta = 0.0f;
        }

        // Clamp health
        m_HealthBar.fillAmount = Mathf.Clamp01(m_HealthBar.fillAmount);
    }

    private bool IsInCriticalHealth()
    {
        return m_HealthBar.fillAmount <= m_CriticalValue;
    }

    private void OnDestroy()
    {
        if (m_PlayerHealth != null)
            m_PlayerHealth.HealthChanged -= OnHealthChanged;
    }
}
