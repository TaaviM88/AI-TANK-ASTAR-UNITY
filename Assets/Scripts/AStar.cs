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

    public GenerateLevel generator;
    public float delay = 0.5f;
    int mapWidth = 0;
    int mapHeight = 0;

    public List<Node> FindPath(GameObject obj1, GameObject obj2)
    {

        mapWidth = generator.map.Count;
        mapHeight = generator.map[0].Length;
        nodeMap = new Node[mapWidth, mapHeight];
        Node start = null;
        Node goal = null;
        nodeMap = generator.GetNodeArray();
      /* for (int y = 0;y< mapHeight; y++)
          {
              for (int x= 0; x < mapWidth; x++)
              {
                  NavTile tile = tiles[x].gameObject.GetComponent<NavTile>();
                  Node node = new Node(tile.x, tile.y);
                  node.value = tile;

                  nodeMap[tile.x, tile.y] = node;
              }

          }
      */


      /*  for (int i = 0; i < tiles.Count; i++)
        {
            
            NavTile tile = tiles[i].gameObject.GetComponent<NavTile>();
            Node node = new Node(tile.x,tile.y);
            node.value = tile;
            nodeMap[node.posX, node.posY] = node;
            Debug.Log($"Node: {node.posX}, {node.posY}");
        }
      */
        start = FindNode(obj1);
        goal = FindNode(obj2);


        List<Node> nodePath = ExecuteAStar(start,goal);
        nodePath.Reverse();
        return nodePath;
    }

    Node FindNode(GameObject obj)
    {
        Collider2D[] collidingObjects = Physics2D.OverlapCircleAll(obj.transform.position, 0.2f);
       /* for (int y = 0; y < nodeMap.GetLength(1); y++)
        {
            for (int x = 0; x < nodeMap.GetLength(0); x++)
            {
                Debug.Log($"Counting: {x}, {y} : Node {nodeMap[x, y].value}");

            }
        }
       */
        foreach (Collider2D collidingObject in collidingObjects)
        {
            //this is the tile the obj is on.
            if (collidingObject.gameObject.GetComponent<NavTile>() != null)
            {
                //find the node witch contains the tile.
                NavTile tile = collidingObject.gameObject.GetComponent<NavTile>();
                for (int y = 0; y < nodeMap.GetLength(1); y++)
                {
                    for (int x = 0; x < nodeMap.GetLength(0); x++)
                    {
                        Node node = nodeMap[x, y];
                        
                        if (node.value == tile)
                        {
                            return node;
                        }
                    }
                }
           
            }
        }
       return null;
    }


    public void StartMapping(List<String> newMap)
    {
        /*map = new List<string>();
        map.Add("G-----");
        map.Add("XXX-XX");
        map.Add("S-X-X-");
        map.Add("--X-X-");
        map.Add("--X-X-");
        map.Add("------");
        */

        //StartCoroutine(DelayAStar(newMap));
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
        Debug.Log("Can't reach!");
        return new List<Node>();
    }

    private List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();

        if(node.value.canMoveLeft && node.posX -1 >= 0)
        {
            Node candidate = nodeMap[node.posX - 1, node.posY];
            if(candidate.value.navigable)
                neighbours.Add(candidate);
        }

        if (node.value.canMoveRight && node.posX + 1 <= mapWidth-1)
        {
            Node candidate = nodeMap[node.posX + 1, node.posY];
            if (candidate.value.navigable)
                neighbours.Add(candidate);
        }

        if (node.value.canMoveUp && node.posY - 1 >= 0)
        {
            Node candidate = nodeMap[node.posX , node.posY - 1];
            if (candidate.value.navigable)
                neighbours.Add(candidate);
        }

        if (node.value.canMoveDown && node.posY + 1 <= mapHeight - 1)
        {
            Node candidate = nodeMap[node.posX , node.posY + 1];
            if (candidate.value.navigable)
                neighbours.Add(candidate);
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
public class Node
{

    public int posX;
    public int posY;
    //basic cost
    public int g = int.MaxValue;
    //cost of the closest path
    public int f = int.MaxValue;
    public Node parent = null;

    public NavTile value = null;
    public Node(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
}