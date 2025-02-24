using System;
using System.Collections.Generic;

public class GameEventController
{
    private static readonly 
        Dictionary<string, List<Action<String,object>>> listeners = new();

    public static void EmitEvent(String type, object payload)
    {
        if (listeners.ContainsKey(type))
        {
            foreach (var action in listeners[type])
            {
                action(type, payload);
            }
        }            
    }

    public static void AddListener(String type, Action<String, object> action)
    {
        if ( ! listeners.ContainsKey(type))
        {
            listeners[type] = new List<Action<String, object>>();
        }
        listeners[type].Add(action);
    }

    public static void RemoveListener(String type, Action<String, object> action)
    {
        if (listeners.ContainsKey(type))
        {
            listeners[type].Remove(action);
        }
    }
}