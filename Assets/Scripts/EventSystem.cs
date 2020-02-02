using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventSystem : MonoBehaviour
{
    // Overrides for event types
    [System.Serializable]
    public class Collision2DEvent : UnityEvent<Collision2D> {}


    // Dictionaries for subscriptions by event type
    private Dictionary<string, UnityEvent<Collision2D>> collision2DEvents;


    // Collision2D callbacks
    public static void StartListening(string eventName, UnityAction<Collision2D> listener) {
        if (Instance.collision2DEvents.TryGetValue(eventName, out UnityEvent<Collision2D> thisEvent)) {
            thisEvent.AddListener(listener);
        } else {    // First time we've encountered this event name
            thisEvent = new Collision2DEvent();
            thisEvent.AddListener(listener);
            Instance.collision2DEvents.Add(eventName, thisEvent);

        }
    }

    public static void StopListening(string eventName, UnityAction<Collision2D> listener) {
        if (eventSystem == null) return;
        if (Instance.collision2DEvents.TryGetValue(eventName, out UnityEvent<Collision2D> thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Collision2D arg) {
        if (Instance.collision2DEvents.TryGetValue(eventName, out UnityEvent<Collision2D> thisEvent)) {
            thisEvent.Invoke(arg);
        }
    }

    // Enforcement stuff
    private static EventSystem eventSystem;
    public static EventSystem Instance {
        get {
            if (!eventSystem) {
                eventSystem = FindObjectOfType(typeof(EventSystem)) as EventSystem;
                if (!eventSystem) {
                    Debug.LogError("EventSystem requires at least one active EventSystem component in the scene.");
                } else {
                    eventSystem.Init();
                }
            }
            return eventSystem;
        }
    }

    // Initialize subscription dictionaries
    void Init() {
        if (collision2DEvents == null) {
            collision2DEvents = new Dictionary<string, UnityEvent<Collision2D>>();
        }
    }
}
