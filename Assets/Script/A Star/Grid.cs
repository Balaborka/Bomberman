using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{ 
    Node[,] NodeArray;
    List<Node> FinalPath;

    float fNodeDiameter;
    public int iGridSizeX, iGridSizeY;

    public void CreateGrid()
    {
        NodeArray = new Node[iGridSizeX, iGridSizeY];
        for (int x = 0; x < iGridSizeX; x++)
        {
            for (int y = 0; y < iGridSizeY; y++)
            {
                Vector3 worldPoint = new Vector3(x, 0.5f, y);
                bool Wall = false;
                

                int layerMask = LayerMask.GetMask("Block", "Obstacle");
                if (Physics.CheckSphere(worldPoint, 0.1f, layerMask))
                {
                    Wall = true;
                }

                NodeArray[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public void ResetCosts() {
        foreach (var node in NodeArray)
        {
            node.igCost = 0;
            node.ihCost = 0;
        }
    }
    
    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();
        int icheckX;
        int icheckY;
        
        icheckX = a_NeighborNode.iGridX + 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        icheckX = a_NeighborNode.iGridX - 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY + 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY - 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);
            }
        }

        return NeighborList;
    }

    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        int ix = Mathf.RoundToInt(a_vWorldPos.x);
        int iy = Mathf.RoundToInt(a_vWorldPos.z);

        return NodeArray[ix, iy];
    }
}
