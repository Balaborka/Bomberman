using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmartEnemyMovement : EnemyMovement
{
    Grid GridReference;

    private void Awake()
    {
        GridReference = GetComponent<Grid>();        
    }

    protected override Vector3 GeneratePosition()
    {
        GridReference.CreateGrid();
        var path = FindPath(transform.position, PlayerMovement._destination);
        if (path != null && path.Any()) {
            var vec = new Vector3(path[0].vPosition.x, 0, path[0].vPosition.z);
            
            return vec - transform.position;
        }
        return Vector3.zero;
    }

    List<Node> FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        GridReference.ResetCosts();
        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos);
        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                return GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))
            {
                if (NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.igCost = MoveCost;
                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.ParentNode = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }

        }

        return null;
    }

    List<Node> GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while (CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.ParentNode;
        }

        FinalPath.Reverse();
        
        return FinalPath;
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);

        return ix + iy;
    }
}
