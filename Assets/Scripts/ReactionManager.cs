using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    [SerializeField] private ReactionObject[] reactions;

    public ReactionObject FindReactionWithName(string name)
    {
        foreach (ReactionObject reaction in reactions)
        {
            if (reaction.name == name) return reaction;
        }
        return null;
    }
}
