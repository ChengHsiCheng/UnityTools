using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> children = new List<Node>();

    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }

    protected override void OnStopNode()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].StopNode();
        }
    }
}
