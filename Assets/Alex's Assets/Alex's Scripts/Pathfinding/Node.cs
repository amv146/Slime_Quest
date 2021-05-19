using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Node : MonoBehaviour, IComparable {
    // Use this for initialization
    public int X;
    public int Y;
    public int Z { get
        {
            return Y;
        }
        set
        {
            Y = value;
        }
    }
    private float cost;
    private float distance;
    public float weight = 1;
    public Node parentNode;
    public float defaultWeight = 1;

    public float FCost() {
        return cost + distance;
    }

    public void SetGCost(float cost) {
        this.cost = cost;
    }

    public float GCost() {
        return cost;
    }

    public void SetHCost(float distance) {
        this.distance = distance;
    }

    public float ManhattanDistance(Node targetNode) {
        return Mathf.Abs(targetNode.X - X) + Mathf.Abs(targetNode.Y - Y);
    }

    public int DiagonalDistance(Node targetNode) {
        int XDistance = Mathf.Abs(this.X - targetNode.X);
        int YDistance = Mathf.Abs(this.Y - targetNode.Y);

        if (XDistance <= YDistance) {
            return YDistance;
        }
        else {
            return XDistance;
        }
    }

    public Vector2 GetDirectionVector(Node targetNode) {
        return new Vector2(targetNode.X - this.X, targetNode.Y - this.Y);
    }

    public float HCost() {
        return distance;
    }

    public void ResetWeight() {
        weight = defaultWeight;
    }


    public List<(int, int)> GetAllAdjacentTileCoords() {
        List<(int, int)> allTileCoords = new List<(int, int)>();
        allTileCoords.Add((X - 1, Y));
        allTileCoords.Add((X + 1, Y));
        allTileCoords.Add((X, Y - 1));
        allTileCoords.Add((X, Y + 1));

        return allTileCoords;
    }

    public int CompareTo(object obj) {
        if (obj == null || obj.GetType() != this.GetType()) {
            return -1;
        }
        return (this.FCost()).CompareTo((obj as Node).FCost());
    }

    override public bool Equals(object obj) {
        if (obj == null || obj.GetType() != this.GetType()) {
            return false;
        }

        Node other = (obj as Node);
        if (this.X == other.X && this.Y == other.Y) {
            return true;
        }

        return false;
    }
}
