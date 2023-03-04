using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField]
    private const int MAP_SIZE = 6;

    private List<string> map;

    private Node[,] nodeMap;
    // Start is called before the first frame update
    void Start()
    {
        map = new List<string>();
        map.Add("G-----");
        map.Add("XXX-XX");
        map.Add("S-X-X-");
        map.Add("--X-X-");
        map.Add("--X-X-");
        map.Add("------");
        nodeMap = new Node[MAP_SIZE,MAP_SIZE];
        Node start = null;
        Node goal = null;

        for (int y = 0; y < MAP_SIZE; y++)
        {
            for (int x = 0; x < MAP_SIZE; x++)
            {
                Node node = new Node(x,y);
                char currentChar = map[y][x];
                if(currentChar == 'X')
                {
                    node.value = Node.Value.BLOCKED;
                }else if(currentChar == 'G')
                {
                    goal = node;
                }
                else if(currentChar == 'S')
                {
                    start = node;
                }

                nodeMap[x, y] = node;
            }
        }

        List<Node> nodePath = ExecuteAStar(start, goal);

        //burn the path in the map
        foreach (Node node in nodePath)
        {
            char[] charArray = map[node.posY].ToCharArray();
            charArray[node.posX] = '@';
            map[node.posY] = new string(charArray);
        }

        string mapString = "";
        foreach(string mapRow in map)
        {
            mapString += mapRow + '\n';
        }

        Debug.Log(mapString);
    }

    private List<Node> ExecuteAStar(Node start, Node goal)
    {
        List<Node> openList = new List<Node>() { start };
        List<Node> closedList = new List<Node>();

        start.g = 0;
        start.f = CalculateHeuristicValue(start, goal);

        while(openList.Count > 0)
        {
            //node  with lowest estimated cost
            Node current = openList[0];
            foreach (Node node in openList)
            {
                if(node.f < current.f)
                {
                    current = node;
                }
            }
            //check if goal
            if(current == goal)
            {
                return BuildPath(goal);
            }
            //not able to visit again same node
            openList.Remove(current);
            closedList.Add(current);

            //calculate neigbours f's
            List<Node> neighbours = GetNeighbourNodes(current);
            foreach (Node neighbour in neighbours)
            {
                if(closedList.Contains(neighbour))
                {
                    continue;
                }
                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
                //calculate a new G value and check if better than whatever is stored in the neighbour 
                int candidateG = current.g + 1;
                if(candidateG >= neighbour.g)
                {
                    //if greater doesn't belong a good path
                    continue;
                }
                else
                {
                    neighbour.parent = current;
                    neighbour.g = candidateG;
                    neighbour.f = neighbour.g + CalculateHeuristicValue(neighbour,goal);
                }
            }

        }
        //no more nodes to search. algorithm failed
        return new List<Node>();
    }

    private List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();
        //right side
        if(node.posX -1 >= 0)
        {
            Node candidate = nodeMap[node.posX - 1, node.posY];
            if(candidate.value != Node.Value.BLOCKED)
            {
                neighbours.Add(candidate);
            }
        }
        //left side
        if (node.posX + 1 <= MAP_SIZE-1)
        {
            Node candidate = nodeMap[node.posX + 1, node.posY];
            if (candidate.value != Node.Value.BLOCKED)
            {
                neighbours.Add(candidate);
            }
        }

        if (node.posY - 1 >= 0)
        {
            Node candidate = nodeMap[node.posX , node.posY - 1];
            if (candidate.value != Node.Value.BLOCKED)
            {
                neighbours.Add(candidate);
            }
        }

        if (node.posY + 1 <= MAP_SIZE - 1)
        {
            Node candidate = nodeMap[node.posX , node.posY + 1];
            if (candidate.value != Node.Value.BLOCKED)
            {
                neighbours.Add(candidate);
            }
        }


        return neighbours;
    }

    private List<Node> BuildPath(Node node)
    {
        List<Node> path = new List<Node>() { node};
        while(node.parent != null)
        {
            node = node.parent;
            path.Add(node);
        }
        return path;
    }

    private int CalculateHeuristicValue(Node node1, Node node2)
    {
        return Mathf.Abs(node1.posX - node2.posX)  + Mathf.Abs(node1.posY - node2.posY);
    }
}
//
class Node
{
    public enum Value
    {
        FREE,
        BLOCKED
    }
    public int posX;
    public int posY;
    //basic cost
    public int g = int.MaxValue;
    //cost of the closest path
    public int f = int.MaxValue;
    public Node parent;
    public Value value;
    public Node(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
        value = Value.FREE;
    }
}