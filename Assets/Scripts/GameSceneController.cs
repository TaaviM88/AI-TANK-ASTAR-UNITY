using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public AStar aStar;
    public GenerateLevel generator;
    public float delay = 0.3f;
    MovableObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator MoveRoutine(List<Node>path)
    {
        foreach (Node node in path)
        {
            generator.createdPlayer.transform.localPosition = node.value.transform.localPosition;
            yield return new WaitForSeconds(delay);
        }
    }
    
    public void StartMove()
    {
        //List<Node> path = aStar.FindPath(generator.createdPlayer,);
       // player = generator.createdPlayer.GetComponent<MovableObject>();
       // player.Move(path);
        //StartCoroutine(MoveRoutine(path));
    }

    public void PlayerCreated()
    {
        player = generator.createdPlayer.GetComponent<MovableObject>();
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            MovePlayer();

        }

        if(Input.GetMouseButtonDown(1))
        {
            ChangeTileType();
        }

    }

    private void ChangeTileType()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            NavTile navTile = hit.collider.gameObject.GetComponent<NavTile>();
            if (navTile != null)
            {
                navTile.ChangeTileType();
                break;
            }
        }
    }

    private void MovePlayer()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            NavTile navTile = hit.collider.gameObject.GetComponent<NavTile>();
            if (navTile != null)
            {
                if (navTile.navigable)
                {
                    List<Node> path = aStar.FindPath(player.gameObject, hit.collider.gameObject);
                    player.Move(path);
                }
                break;
            }
        }
    }
}
