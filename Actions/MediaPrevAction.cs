﻿using System;
using System.Threading.Tasks;
using Windows.Media.Control;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;


// ReSharper disable once CheckNamespace
namespace MediaControls_Plugin; // Don't change because of compatibility

public class MediaPrevAction : PluginAction
{
    private GlobalSystemMediaTransportControlsSessionManager _manager;

    public MediaPrevAction()
    {
        if (PluginManager.Plugins.ContainsKey("Macro Deck Media Controls"))
        {
            MacroDeckPlugin plugin = PluginManager.Plugins["Macro Deck Media Controls"];
            if (plugin.GetType().IsEquivalentTo(typeof(MediaControlsPlugin)))
            {
                _manager = ((MediaControlsPlugin)plugin).Manager;
            }
        }
    }
    public override string Name => "Media Prev";
    public override string Description => "Plays the previous track on a media player.\n\r\n\rConfiguration: no";
    public override void Trigger(string clientId, ActionButton actionButton)
    {
        var session = _manager.GetCurrentSession();
        if (session == null)
        {
            return;
        }
        var test = Task.Run(async () => await session.TrySkipPreviousAsync()).GetAwaiter().GetResult();
    }
}