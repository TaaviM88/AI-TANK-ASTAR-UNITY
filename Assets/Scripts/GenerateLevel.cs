using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GenerateLevel : MonoBehaviour
{
    public List<string> map = new List<string>();
    
    public AStar astar;
    public GameObject navtileprefab;
    public GameObject pathNode;
    public GameObject player;
    public GameObject enemy;
    public GameSceneController sceneController;
    public int xOffset = 0;
    public int yOffset = 0;
    //CHARS
    public char leftRight = '-';
    public char upDown = '|';
    public char crossroad4 = '+';
    public char crossTRight3 = 'E';
    public char crossTDown3 = 'T';
    public char crossTLeft3 = '3';
    public char crossTUp3 = 'W';
    public char turnDownRight = 'C';
    public char turnDownLeft = 'Z';
    public char turnUpRight = 'U';
    public char turnUpLeft = 'V';
    [Space]
    [Header("ObjectMap")]
    public List<string> objectMap = new List<string>();
    public char playerStart = 'P';
    public char enemyStart = 'E';
    public List<GameObject> navTiles = new List<GameObject>();
    public List<GameObject> navTilesRow = new List<GameObject>();
    List<GameObject> pathSprites = new List<GameObject>();
   public GameObject createdPlayer = null;
    public GameObject createdEnemy = null;

    private Node[,] nodeMap;
    // Start is called before the first frame update
    void Awake()
    {
       
    }
    private void Start()
    {
        //astar.StartMapping(map);
        StartCoroutine(DelayCreateTile());
    }

    public void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            //  astar.StartMapping(map);
            //astar.FindPath(createdPlayer, createdEnemy);
            //sceneController.StartMove();
        }
        */
    }

    IEnumerator DelayCreateTile()
    {
        nodeMap = new Node[map.Count, map[0].Length];
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                bool tmp = true;
                char currentChar = map[y][x];
                if (currentChar == 'X')
                {
                    tmp = false;
                }
                //Left/right = -
                //Up/Down = |
                //crossroad4 +
                //CrossTRight3 = E
                //CrossTDown3 = T
                //CrossTLeft3 = 3
                //crossTUp3 = W
                //turnDownRight = C
                //turnDownLeft = Z
                //turnUpRight = U
                //turnUpLeft = V

                CreateNavTile(new Vector2(x, -1*y), tmp,currentChar);
                yield return new WaitForSeconds(0.01f);
            }
        }
        CreateObjects();
    }

    public void CreateObjects()
    {
        for (int y = 0; y < objectMap.Count; y++)
        {
            for (int x = 0; x < objectMap[0].Length; x++)
            {
                char currentChar = objectMap[y][x];
                if(currentChar == playerStart)
                {
                    CreatePlayer(new Vector2(x, -1 * y));
                }
                else if(currentChar == enemyStart)
                {
                    CreateEnemy(new Vector2(x, -1 * y));
                }
            }
        }
    }

    private void CreateEnemy(Vector2 pos)
    {
        createdEnemy = Instantiate(enemy, transform.position, Quaternion.identity, this.transform);
        createdEnemy.transform.localPosition = pos;
        
    }

    private void CreatePlayer(Vector2 pos)
    {
        createdPlayer = Instantiate(player, transform.position, Quaternion.identity, this.transform);
        createdPlayer.transform.localPosition = pos;
        sceneController.PlayerCreated();
    }

    public void CreateNavTile(Vector2 pos, bool navigable, char charType)
    {
        GameObject newTile = Instantiate(navtileprefab, transform.position, Quaternion.identity, this.transform);
        newTile.transform.localPosition = pos;
        NavTile tile = newTile.GetComponent<NavTile>();
        tile.identifyXY = new Vector2(pos.x,MathF.Abs(pos.y));
        tile.navigable = navigable;
        tile.TileType = charType;
        tile.x = (int)pos.x;
        tile.y = -1*(int)pos.y;

        Node node = new Node((int)pos.x, -1 * (int)pos.y);
        node.value = tile;
        nodeMap[(int)pos.x, -1 * (int)pos.y] = node;
        
        navTiles.Add(newTile);
    }
    public Node[,] GetNodeArray()
    {
        GenerateNodeMap();
        return nodeMap;
    }

    private void GenerateNodeMap()
    {
        nodeMap = new Node[map.Count, map[0].Length];
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                Node node = new Node(x,y);
                for (int i = 0; i < navTiles.Count; i++)
                {
                    if (navTiles[i].GetComponent<NavTile>().identifyXY == new Vector2(x, y))
                    {
                        node.value = navTiles[i].GetComponent<NavTile>();
                    }
                }
                nodeMap[x, y] = node;

            }
        }
    }

    public void CreatePathNodeSprite(Vector2 pos, bool navigable)
    {

        GameObject pathTile = Instantiate(pathNode, transform.position, Quaternion.identity, this.transform);
        pathTile.transform.localPosition =new Vector2(pos.x,-1* pos.y);
        pathSprites.Add(pathTile);
    }
    
}
