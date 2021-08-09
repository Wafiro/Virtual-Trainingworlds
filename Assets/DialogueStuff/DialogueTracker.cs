using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The dialogueTracker tracks all data associated with one dialogue.
[RequireComponent(typeof(ResponseHandler))] // The responseHandler of the tracked dialogue.
public class DialogueTracker : MonoBehaviour
{
    // The responseHandler of the tracked dialogue.
    private ResponseHandler _handler;

    // The data structure to hold all responses indexed by their dialogue object.
    private Dictionary<DialogueObject, List<ulong>> _responseMap;
    // The order of the dialogue objects, for displaying in order
    private List<DialogueObject> _dialogueOrder;

    // Initialize this object.
    void Awake()
    {
        // Set reference to responseHandler.
        _handler = this.GetComponent<ResponseHandler>();
    }
    // Initialize connections.
    void Start()
    {
        _handler.onResponse += OnResponse;
    }
    private void OnResponse(Response response)
    {
        // Exit if invalid or empty dialogue
        if (!response.DialogueObject || !response.DialogueObject.HasResponses())
        {
            return;
        }
        // The key in the responseMap
        var key = response.DialogueObject;
        // If not yet present
        if (!_responseMap.ContainsKey(key))
        {
            // Create new List for key
            _responseMap.Add(key, new List<ulong>(3));
        }
        // Append the new response to its corresponding dialogue object's List
        _responseMap[key].Add((ulong)response.id);

        // Append the dialogue object to the order, if it is new
        if (!_dialogueOrder.Contains(key))
        {
            _dialogueOrder.Add(key);
        }
    }
}

namespace DialogueOrder
{
    abstract class DialogueOrder
    {
        public readonly ulong current;
        public DialogueOrder(ulong current)
        {
            this.current = current;
        }
        public bool IsCorrect(IReadOnlyList<ulong> order);
    }
    class RequireBefore : DialogueOrder
    {
        public RequireBefore(ulong before)
        {
            this.before = before;
        }
        public readonly ulong before;
        override IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.SkipWhile(x => x != before).Contains(current);
        }
    }
    class RequireAfter : DialogueOrder
    {
        public RequireAfter(ulong current, ulong after)
        {
            base(current);
            this.after = after;
        }
        public readonly ulong after;
        override IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.SkipWhile(x => x != current).Contains(after);
        }
    }
    class Require : DialogueOrder
    {
        override IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.Contains(current);
        }
    }
    class Deny : DialogueOrder
    {
        override IsCorrect(IReadOnlyList<ulong> order)
        {
            return !order.Contains(current);
        }
    }
}