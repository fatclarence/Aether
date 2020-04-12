﻿using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class LobbyPlayer : LobbyPlayerBehavior
{
    [SerializeField]
    private Text m_PlayerName;

    private RawImage m_Container;

    private bool m_IsReady = false;

    private ulong m_Customization;

    private void Start()
    {
        m_Container = GetComponent<RawImage>();
    }

    public string GetName()
    {
        return m_PlayerName.text;
    }

    public bool GetIsReady()
    {
        return m_IsReady;
    }

    public ulong GetCustomization()
    {
        return m_Customization;
    }

    public void SetReadyStatus(bool isReady)
    {
        networkObject.SendRpc(RPC_SET_READY, Receivers.All, isReady);
    }

    public void UpdateName(string name)
    {
        m_PlayerName.text = name;

        if (networkObject != null)
            networkObject.SendRpc(RPC_SET_NAME, Receivers.All, name);
    }

    public void UpdateDataFor(NetworkingPlayer player)
    {
        networkObject.SendRpc(player, RPC_SET_NAME, m_PlayerName.text);
    }

    public void SetCustomization(ulong data)
    {
        m_Customization = data;
    }

    public override void SetName(RpcArgs args)
    {
        m_PlayerName.text = args.GetNext<string>();
    }

    public override void SetReady(RpcArgs args)
    {
        m_IsReady = args.GetNext<bool>();

        if (m_IsReady)
            m_Container.color = Color.green;
        else
            m_Container.color = Color.white;
    }
}
