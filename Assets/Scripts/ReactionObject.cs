using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Reaction", menuName = "ScriptableObjects/New Reaction Object", order = 2)]
public class ReactionObject : ScriptableObject
{
    [SerializeField] private string reactionName;
    [SerializeField] private ItemObject[] inputs;
    [SerializeField] private ItemObject[] outputs;
    [SerializeField] private float reactionTime;

    public ItemObject[] Inputs { get => inputs; }
    public ItemObject[] Outputs { get => outputs; }
    public float ReactionTime { get => reactionTime; }
}
