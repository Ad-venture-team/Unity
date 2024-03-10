using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : SingletonBehaviour<Pathfinding>
{
    private HashSet<Node> grid = new HashSet<Node>();
    private List<Vector2Int> path = new List<Vector2Int>();
    private Heap<Node> open;
    HashSet<Node> close = new HashSet<Node>();
    private int[,] map;
    Node origine;
    Node target;
    int roomSize; // equal to the number of tile in the room (width * height)

    private void OnEnable()
    {
        EventWatcher.onNewRoom += RefreshGrid;
    }

    private void OnDisable()
    {
        EventWatcher.onNewRoom -= RefreshGrid;
    }

    private void RefreshGrid(Room _room)
    {
        roomSize = _room.height * _room.width;
        map = _room.map;
        grid.Clear();
        for (int x = 0; x < _room.width; x++)
            for (int y = 0; y < _room.height; y++)
            {
                Node newNode = new Node(new Vector2Int(x, y));
                grid.Add(newNode);
            }
    }

    public List<Vector2Int> GetPath(Vector2 _origine, Vector2 _target)
    {
        path = new List<Vector2Int>();
        origine = GetNodeFromPosition(ConvertToVector2Int(_origine));
        target = GetNodeFromPosition(ConvertToVector2Int(_target));
        open = new Heap<Node>(roomSize);
        open.Queue(origine);
        origine.comeFrom = origine;
        origine.cost = 0;
        close = new HashSet<Node>();
        close.Add(origine);

        while (open.Count > 0)
        {
            Node current = open.Dequeue();

            if (current.position == target.position)
                break;

            foreach (Node N in GetNeighbourSet(current))
            {
                int cost = current.cost + Heuristic(current,N); 

                if (!close.Contains(N) || (close.Contains(N) && cost < N.cost))
                {
                    close.Add(N);
                    N.cost = cost;
                    N.priority = cost + Heuristic(target, N);
                    N.comeFrom = current;
                    open.Queue(N);
                }
            }
        }

        Node currentNode = target;
        while(currentNode.position != origine.position)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.comeFrom;
        }
        path.Reverse();
        return path;
    }

    public List<Vector2Int> Flee(Vector2 _origine, Vector2 _target)
    {
        path = new List<Vector2Int>();
        origine = GetNodeFromPosition(ConvertToVector2Int(_origine));
        target = GetNodeFromPosition(ConvertToVector2Int(_target));
        int dist = Heuristic(origine, target) + 14;
        open = new Heap<Node>(roomSize);
        open.Queue(origine);
        origine.comeFrom = origine;
        origine.cost = 0;
        close = new HashSet<Node>();
        close.Add(origine);

        while (open.Count > 0)
        {
            Node current = open.Dequeue();

            if (Heuristic(current, origine) > dist)
                continue;
          

            foreach (Node N in GetNeighbourSet(current))
            {
                int cost = current.cost + Heuristic(current, N);


                if (!close.Contains(N) || (close.Contains(N) && cost < N.cost))
                {
                    close.Add(N);
                    N.cost = cost;
                    N.priority = cost;
                    N.comeFrom = current;
                    open.Queue(N);
                }
            }
        }

        Node currentNode = origine;
        int fleeCost = 0;
        foreach (Node N in close)
        {
            int fCost = Heuristic(N, target);
            if (fCost > fleeCost)
            {
                fleeCost = fCost;
                currentNode = N;
            }
        }
        //return GetPath(_origine, currentNode.position);
        while (currentNode.position != origine.position)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.comeFrom;
        }
        path.Reverse();
        return path;
    }

    private int Heuristic(Node a, Node b)
    {
        int x = Mathf.Abs(a.position.x - b.position.x);
        int y = Mathf.Abs(a.position.y - b.position.y);

        if (x > y)
            return y * 14 + (x - y) * 10;

        return x * 14 + (y - x) * 10;
    }

    private HashSet<Node> GetNeighbourSet(Node currentNode)
    {
        HashSet<Node> neighbourList = new HashSet<Node>();

        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                Vector2Int checkPos = new Vector2Int(currentNode.position.x + x, currentNode.position.y + y);

                //TODO
                //if (DataBase.Instance.tileData[map[checkPos.x, checkPos.y]].isBlocking)
                //    continue;
                Node checkNode = GetNodeFromPosition(checkPos);
                if (checkNode != null)
                    neighbourList.Add(checkNode);
            }

        return neighbourList;
    }

    private Node GetNodeFromPosition(Vector2Int _position)
    {
        foreach (Node N in grid)
        {
            if (N.position == _position)
                return N;
        }
        return null;
    }
    private Vector2Int ConvertToVector2Int(Vector3 _toConvert)
    {
        return new Vector2Int((int)Math.Round(_toConvert.x), (int)Math.Round(_toConvert.y));
    }
    private Vector3 ConvertToVector3(Vector2Int _toConvert)
    {
        return new Vector3(_toConvert.x, _toConvert.y, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Node N in close)
        {
            Vector3 pos = ConvertToVector3(N.position);
            Gizmos.DrawSphere(pos, .5f);
        } 
        Gizmos.color = Color.green;
        foreach (Vector2Int V in path)
        {
            Vector3 pos = ConvertToVector3(V);
            Gizmos.DrawSphere(pos, .5f);
        }
    }

    private class Node : IHeapItem<Node>
    {
        public Vector2Int position;
        public Node comeFrom;
        public int cost; // gCost
        public int priority; // fCost
        int heapIndex;

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public Node(Vector2Int _position)
        {
            position = _position;
        }

        public int CompareTo(Node other)
        {
            int compare = priority.CompareTo(other.priority);
            if (compare == 0)
                compare = cost.CompareTo(other.cost);
            if (compare == 0)
                compare = 1;

            return -compare;
        }
    }
}
