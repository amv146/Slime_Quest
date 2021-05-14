using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class AStarAlgorithm {
    public static Tile[,] TileMap;
    private static PriorityQueue<Node> openList = new PriorityQueue<Node>();
    private static List<Node> closedList = new List<Node>();

    public static List<Node> GetShortestPossiblePath(Tile startTile, Tile targetTile, int maxNodes = -1) {
        closedList.Clear();
        openList.Clear();
        openList.Enqueue(startTile);

        while (openList.Count > 0) {
            Node currentNode = openList.Dequeue();
            closedList.Add(currentNode);
            if (currentNode == targetTile) {
                break;
            }
            List<Node> neighbors = GetWalkableAdjacentTiles(currentNode);

            SetHCosts(neighbors, startTile, targetTile);

            foreach (Node neighbor in neighbors) {
                float newCost = currentNode.GCost() + neighbor.weight;
                neighbor.parentNode = currentNode;

                if (newCost > neighbor.GCost()) {
                    neighbor.SetGCost(newCost);
                    if (!openList.Contains(neighbor)) {
                        openList.Enqueue(neighbor);
                    }
                }
            }
        }
        return RetracePath(targetTile);
    }


    public static float GetCrossProduct(Node current, Node start, Node target) {
        int dx1 = current.X - target.X;
        int dy1 = current.Y - target.Y;
        int dx2 = start.X - target.X;
        int dy2 = start.Y - target.Y;

        return Mathf.Abs(dx1 * dy2 - dx2 * dy1);
    }

    private static List<Node> RetracePath(Node targetNode) {
        List<Node> path = new List<Node>();
        path.Insert(0, targetNode);
        Node parent = targetNode.parentNode;

        while (parent != null) {
            path.Insert(0, parent);
            parent = parent.parentNode;
        }

        return path;
    }

    private static List<Node> GetWalkableAdjacentTiles(Node currentNode) {
        List<(int, int)> possibleCoords = currentNode.GetAllAdjacentTileCoords();
        List<Node> walkableTiles = new List<Node>();

        foreach ((int, int) coords in possibleCoords) {
            if (coords.Item1 >= 0 && coords.Item1 < TileMap.GetLongLength(0) && coords.Item2 >= 0 && coords.Item2 < TileMap.GetLongLength(1)) {
                Tile tile = TileMap[coords.Item1, coords.Item2];
                if (!closedList.Contains(tile)) {
                    walkableTiles.Add(tile);
                }
            }
        }

        return walkableTiles;
    }

    private static void SetHCosts(List<Node> tiles, Node startTile, Node targetTile) {
        foreach (Node node in tiles) {
            float HCost = node.ManhattanDistance(targetTile) + GetCrossProduct(node, startTile, targetTile) * 0.001f;
            node.SetHCost(HCost);
        }
    }

    public static void ResetTiles(TileGrid tileGrid) {;
        foreach (Tile tile in TileMap) {
            tile.ResetWeight();
            tile.parentNode = null;
            tile.SetHCost(0);
            tile.SetGCost(0);
        }
    }

    public static void SetWeights(List<CharacterController> characters) {
        foreach (CharacterController character in characters) {
            character.currentTile.weight = 10000;
        }
    }
}
