using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchSO", menuName = "SO/Research/ResearchSO")]
public class ResearchSO : ScriptableObject
{
    [SerializeField] public ResearchTree researchTree;
    public bool ActivateResearchNode(string name)
    {
        Debug.Log("Activate Research Node Called");
        Debug.Log("Research Tree: " + researchTree);
        ResearchNodeSO node = ResearchTree.GetValidResearchNode(researchTree, name);
        if (node != null && !node.activated)
        {
            node.activated = true;
            return true;
        }
        else
        {
            Debug.Log("Given research name is not valide");
        }
        return false;

    }

    public void ResetRound()
    {
        ResetRound(researchTree);
        researchTree.node.activated = true;
    }
    private void ResetRound(ResearchTree point)
    {
        point.node.activated = false;
        foreach(ResearchTree child in point.children)
        {
            ResetRound(child);
        }
    }

    [System.Serializable]
    public class ResearchTree
    {
        public ResearchNodeSO node;
        public ResearchTree[] children;

        public static ResearchNodeSO GetResearchNode(ResearchTree startingTreeNode, string name)
        {
            Debug.Log("Searching for " + name + "|this node is " + startingTreeNode.node.name + " | " + startingTreeNode.node.name.Equals(name));
            if (startingTreeNode.node.name.Equals(name)) return startingTreeNode.node;
            else
            {
                ResearchNodeSO ret = null;
                foreach (ResearchTree child in startingTreeNode.children)
                {
                    //print("Looking at childe");
                    if (ret == null)
                    {
                        ret = GetResearchNode(child, name);
                    }
                    else
                    {
                        return ret;
                    }
                }
                if (ret != null) return ret;
            }
            return null;
        }

        public static ResearchNodeSO GetValidResearchNode(ResearchTree startingTreeNode, string name)
        {
            Debug.Log("StartingTreeNode.node: " + startingTreeNode.node);
            Debug.Log("Searching for " + name + "|this node is " + startingTreeNode.node.name + " | " + startingTreeNode.node.name.Equals(name));
            if (startingTreeNode.node.name.Equals(name)) return startingTreeNode.node;
            else if (startingTreeNode.node.activated)
            {
                ResearchNodeSO ret = null;
                foreach (ResearchTree child in startingTreeNode.children)
                {
                    //print("Looking at childe");
                    if (ret == null)
                    {
                        ret = GetResearchNode(child, name);
                    }
                    else
                    {
                        return ret;
                    }
                }
                if (ret != null) return ret;
            }
            return null;
        }
    }
}
