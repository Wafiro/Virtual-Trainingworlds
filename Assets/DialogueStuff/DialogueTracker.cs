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
        _responseMap = new Dictionary<DialogueObject, List<ulong>>();
        _dialogueOrder = new List<DialogueObject>();
    }
    // Initialize connections.
    void Start()
    {
        _handler.onResponse += OnResponse;
    }
    private void OnResponse(Response response)
    {
        // Exit if invalid or empty dialogue
        if (!response.DialogueObject || !response.DialogueObject.HasResponses)
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
    public IReadOnlyList<DialogueOrder.OrderResult> check(IReadOnlyCollection<DialogueOrder.DialogueOrder> constraints)
    {
        List<DialogueOrder.OrderResult> result = new List<DialogueOrder.OrderResult>();
        foreach (var current in _dialogueOrder)
        {
            if (_responseMap.TryGetValue(current, out var responses))
            {
                result.Add(
                    new DialogueOrder.OrderResult(current,
                        constraints.Where(t => responses.Contains(t.current) && !t.IsCorrect(responses)).ToList()
                    )
                );
            }
        }
        return result;
    }
}

namespace DialogueOrder
{
    public abstract class DialogueOrder
    {
        public readonly ulong current;
        public DialogueOrder(ulong current)
        {
            this.current = current;
        }
        public abstract bool IsCorrect(IReadOnlyList<ulong> order);
    }
    public class RequireBefore : DialogueOrder
    {
        public RequireBefore(ulong current, ulong before) :
            base(current)
        {
            this.before = before;
        }
        public readonly ulong before;
        public override bool IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.SkipWhile(x => x != before).Contains(base.current);
        }
    }
    public class RequireAfter : DialogueOrder
    {
        public RequireAfter(ulong current, ulong after) :
            base(current)
        {
            this.after = after;
        }
        public readonly ulong after;
        public override bool IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.SkipWhile(x => x != base.current).Contains(after);
        }
    }
    public class Require : DialogueOrder
    {
        public Require(ulong current) :
            base(current)
        {
        }
        public override bool IsCorrect(IReadOnlyList<ulong> order)
        {
            return order.Contains(base.current);
        }
    }
    public class Deny : DialogueOrder
    {
        public Deny(ulong current) :
            base(current)
        {
        }
        public override bool IsCorrect(IReadOnlyList<ulong> order)
        {
            return !order.Contains(base.current);
        }
    }
    public class OrderResult
    {
        public DialogueObject dialogueObject;
        public List<DialogueOrder> failed;
        public OrderResult(DialogueObject dialogueObject, IReadOnlyList<DialogueOrder> failed)
        {
            this.dialogueObject = dialogueObject;
            this.failed = failed.ToList();
        }
    }
}