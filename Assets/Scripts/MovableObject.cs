using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float speed = 0.8f;
    public float rotationSpeed = 5;
    private List<Node> currentPath;
    Node targetNode;
    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        currentPath = new List<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        //Determine the target node.
        if(targetNode == null && currentPath.Count > 0)
        {
             targetNode = currentPath[0];
            currentPath.Remove(targetNode);
        }
        //move towards the target node.
        if(targetNode != null)
        {
            Vector3 direction = (targetNode.value.transform.localPosition - transform.localPosition).normalized;
            transform.localPosition += direction * speed * Time.deltaTime;
            //Rotation
            float angle = Mathf.Atan2(direction.y, direction.x);
            //+90 koska tankki katsoo alaspäin
            targetRotation = Quaternion.Euler(0,0,90+angle * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime * rotationSpeed);
            if(Vector3.Distance(transform.localPosition, targetNode.value.transform.localPosition) < 0.1f)
            {
               
                targetNode = null;
            }
        }
    }
    public void Move(List<Node> path)
    {
        currentPath.Clear();
        foreach (Node node in path)
        {
            currentPath.Add(node);
        }
    }
}
