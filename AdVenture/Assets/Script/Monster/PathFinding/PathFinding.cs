using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int MOVE_COST = 1;

    private HashSet<Vector3> grid = new HashSet<Vector3>();
    Vector3 position = Vector3.zero;
    Vector3 destination = Vector3.zero;
    int moovPoint;

    public PathFinding(HashSet<Vector3> ValidePlace, Vector3 startPoint, Vector3 finalDestination)
    {
        grid = ValidePlace;
        position = startPoint;
        destination = finalDestination;
        moovPoint = 1;
    }

    public List<Vector3> FindLowestPath()
    {
        Vector3 startingPoint = position;
        Vector3 endPoint = destination;

        List<Vector3> opendList = new List<Vector3>();
        List<Vector3> closedList = new List<Vector3>();

        opendList.Add(startingPoint);

        while (opendList.Count > 0)
        {
            Vector3 currentNode = GetLowestCostTile(opendList);
            if (currentNode == endPoint || closedList.Count == moovPoint)
            {
                closedList.Add(currentNode);
                return CalculatePath(closedList);
            }

            opendList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Vector3 neighbourt in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(Vector3Int.FloorToInt(neighbourt))) continue;

                int tentativeGCalculate = CalculateDistanceCost(neighbourt, destination);
                int finalDestiTry = CalculateDistanceCost(currentNode, destination);
                if (tentativeGCalculate <= finalDestiTry)
                {
                    if (!opendList.Contains(Vector3Int.FloorToInt(neighbourt)))
                    {
                        opendList.Add(neighbourt);
                    }
                }
            }
        }

        return CalculatePath(closedList);
    }

    public List<Vector3> FindHighestPath()
    {
        Vector3 startingPoint = position;
        Vector3 endPoint = destination;

        List<Vector3> opendList = GetNeighbourList(startingPoint);
        Vector3 currentNode = GetHighestCostTile(opendList);
        return new List<Vector3> { currentNode };
        List<Vector3> closedList = new List<Vector3>();

        opendList.Add(startingPoint);

        while (opendList.Count > 0)
        {
            if (currentNode == endPoint || closedList.Count == moovPoint)
            {
                closedList.Add(currentNode);
                return CalculatePath(closedList);
            }

            opendList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Vector3 neighbourt in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(Vector3Int.FloorToInt(neighbourt))) continue;

                int tentativeGCalculate = CalculateDistanceCost(neighbourt, destination);
                int finalDestiTry = CalculateDistanceCost(currentNode, destination);
                if (tentativeGCalculate <= finalDestiTry)
                {
                    if (!opendList.Contains(Vector3Int.FloorToInt(neighbourt)))
                    {
                        opendList.Add(neighbourt);
                    }
                }
            }
        }

        return CalculatePath(closedList);
    }

    private List<Vector3> CalculatePath(List<Vector3> ListEnd)
    {
        //foreach(Vector3 vecto in ListEnd)
        //{
        //    Debug.Log(vecto);
        //}

        return ListEnd;
    }

    private List<Vector3> GetNeighbourList(Vector3 currentNode)
    {
        List<Vector3> neighbourList = new List<Vector3>();
        currentNode = new Vector3((int)currentNode.x, (int)currentNode.y, (int)currentNode.z);

        if (grid.Contains(new Vector3(currentNode.x - 1, currentNode.y, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x - 1, currentNode.y, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x + 1, currentNode.y, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x + 1, currentNode.y, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x, currentNode.y - 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x, currentNode.y - 1, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x, currentNode.y + 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x, currentNode.y + 1, 0));
        }


        if (grid.Contains(new Vector3(currentNode.x + 1, currentNode.y + 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x+1, currentNode.y + 1, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x - 1, currentNode.y + 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x -1, currentNode.y + 1, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x + 1, currentNode.y - 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x +1, currentNode.y - 1, 0));
        }
        if (grid.Contains(new Vector3(currentNode.x - 1, currentNode.y - 1, 0)))
        {
            neighbourList.Add(new Vector3(currentNode.x -1, currentNode.y - 1, 0));
        }

        return neighbourList;
    }

    private int CalculateDistanceCost(Vector3 start, Vector3 destination)
    {
        int xDistance = (int)Mathf.Abs(start.x - destination.x);
        int yDistance = (int)Mathf.Abs(start.y - destination.y);
        int remaining = (int)Mathf.Abs(xDistance - yDistance);
        int goReturned = MOVE_COST * (int)Mathf.Min(xDistance, yDistance) + MOVE_COST * remaining;
        return goReturned;
    }

    private int HeuristicTileCost(Vector3 _tile)
    {
        int gCost = (int)Vector2.Distance(position, _tile);
        int hCost = (int)Vector2.Distance(destination, _tile);
        int fCost = gCost + hCost;
        return fCost;
    }

    private List<int> CalculateListFDistance(List<Vector3> openListDetected, Vector3 destination)
    {
        List<int> listReturn = new List<int>();
        foreach (Vector3 vector in openListDetected)
        {
            listReturn.Add(CalculateDistanceCost(vector, destination));
        }

        return listReturn;
    }

    private Vector3 GetLowestCostTile(List<Vector3> tileList)
    {
        Vector3 lowestCstTile = tileList[0];

        for (int i = 1; i < tileList.Count; i++)
        {
            if (CalculateDistanceCost(tileList[i], destination) < CalculateDistanceCost(lowestCstTile, destination))
            {
                lowestCstTile = tileList[i];
            }
        }

        return lowestCstTile;
    }

    private Vector3 GetHighestCostTile(List<Vector3> tileList)
    {
        Vector3 highestCstTile = tileList[0];

        for (int i = 1; i < tileList.Count; i++)
        {
            if (CalculateDistanceCost(tileList[i], destination) > CalculateDistanceCost(highestCstTile, destination))
            {
                highestCstTile = tileList[i];
            }
        }

        return highestCstTile;
    }
}
