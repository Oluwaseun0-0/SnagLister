using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum planeDirection

{
    xPlane = 0,
    yPlane = 1,
    zPlane = 2
}

public class GridLine : MonoBehaviour
{

    public planeDirection planeDirectionDir;
    public LineRenderer lr;
        

    public List<Vector3> xPointList;
    public List<Vector3> yPointList;
    public List<Vector3> zPointList;
        
    [Header("Debuggers")]
    public Vector3 rotDebug;

        
        

    

    
    
    private void Awake()
    {
        lr =  GetComponent<LineRenderer>();
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetLineRendererPoints();
    }

    private void GetLineRendererPoints()
    {
        int layerMask = 1 << LayerMask.NameToLayer("OVRRig");

        switch (planeDirectionDir)
        {
            case planeDirection.xPlane:
                float xOffset = transform.rotation.z;
                xPointList.Clear(); // Clear the list before adding new points
                for (float i = 0; i < 360.5; i += 0.25f)
                {
            
                    transform.eulerAngles = new Vector3(0, 0, xOffset + i);
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position,  transform.right, out hit,Mathf.Infinity, ~layerMask))
                    {
                        xPointList.Add(hit.point);
                    }

                }

                lr.positionCount = xPointList.Count; // Update the position count
                lr.SetPositions(xPointList.ToArray()); // Set the positions of the LineRenderer

                break;
            
                
            case planeDirection.yPlane:
                
                float yOffset = transform.rotation.y;
                yPointList.Clear(); // Clear the list before adding new points
                for (float i = 0; i < 360.5; i += 0.25f)
                {
            
                    transform.eulerAngles = new Vector3(0, yOffset + i, 0);
                    
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position,  transform.forward, out hit,Mathf.Infinity, ~layerMask))
                    {
                        yPointList.Add(hit.point);
                    }

                }

                lr.positionCount = yPointList.Count; // Update the position count
                lr.SetPositions(yPointList.ToArray()); // Set the positions of the LineRenderer

                
                break;
            
            
            case planeDirection.zPlane:
                
                float zOffset = transform.rotation.x;
                zPointList.Clear(); // Clear the list before adding new points
                for (float i = 0; i < 360.5; i += 0.25f)
                {
            
                    transform.eulerAngles = new Vector3(zOffset + i,0, 0);
                    

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position,  transform.forward, out hit,Mathf.Infinity, ~layerMask))
                    {
                        zPointList.Add(hit.point);
                    }

                }

                lr.positionCount = zPointList.Count; // Update the position count
                lr.SetPositions(zPointList.ToArray()); // Set the positions of the LineRenderer

                break;
                
                
        }
    }

    
}
