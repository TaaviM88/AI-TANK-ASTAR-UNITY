using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public AStar aStar;
    public GenerateLevel generator;
    public float delay = 0.3f;
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
        List<Node> path = aStar.FindPath();
        StartCoroutine(MoveRoutine(path));
    }
}
