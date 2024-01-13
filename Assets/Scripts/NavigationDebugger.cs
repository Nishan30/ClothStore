using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]

public class NavigationDebugger : MonoBehaviour
{
    [SerializeField] NavMeshAgent agentToDebug;
    LineRenderer linerRenderer;
    void Start()
    {
        linerRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (agentToDebug.hasPath)
        {
            linerRenderer.positionCount = agentToDebug.path.corners.Length;
            linerRenderer.SetPositions(agentToDebug.path.corners);
            linerRenderer.enabled = true;
        }
        else
        {
            linerRenderer.enabled = false;
        }
    }
}
