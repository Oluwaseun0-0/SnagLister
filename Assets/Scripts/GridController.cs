using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GridController : MonoBehaviour
{
    
    [Header("Geometry Controller")] 
    
    
    public float gridOffset;
    public float gridOffsetX;
    public float gridOffsetY;
    public float gridOffsetZ;

    public int gridXCount;
    public int gridYCount;
    public int gridZCount;

    public bool gridSymmetry;
    public bool gridOn;

    [Header("Assets")] 
    [SerializeField] private GameObject gridLineX;
    [SerializeField] private GameObject gridLineY;
    [SerializeField] private GameObject gridLineZ;
    [SerializeField] private Camera _camera;

    
    public List<GameObject> gridLineXList;
    public List<GameObject> gridLineYList;
    public List<GameObject> gridLineZList;
    [SerializeField] private Transform _rayOrigin;

    private void Awake()
    {
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }


    // Start is called before the first frame update
    void Start()
    {
        gridLineXList = new List<GameObject>();
        gridLineYList = new List<GameObject>();
        gridLineZList = new List<GameObject>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gridSymmetry)
        {
            gridOffsetX = gridOffsetY = gridOffsetZ = gridOffset;
        }

        /*if (gridOn)
        {
            SpawnGrid();
        }*/
        
    }

    public void SpawnRayOrigin()
    {
        
    }

    private string GetLineName(int id, string axis)
    {
         string lineName = $"gridLine {axis} {id}" ;

         return lineName;

    }

    public void SpawnGrid()
    {
        Debug.Log($"SpawnGrid");
        
        SpawnGridX();
        SpawnGridY();
        SpawnGridZ();
    }
    
    public void RemoveGrid()
    {
        RemoveGridX();
        RemoveGridY();
        RemoveGridZ();
    }
    public void SpawnGridX()
    {
        _rayOrigin = GameObject.FindObjectOfType<RayOrigin>().transform;
            
        
        if (gridXCount <= 0) return;
        //Destroy any thing in the list.
       
        foreach (var gridLine in gridLineXList)
        {
            Destroy(gridLine);
            
        }
        gridLineXList.Clear();
        
        for (int i = 0; i < gridXCount; i++)
        {
            // Get position for new Grid lines
            var spawnPosX = new Vector3(_rayOrigin.position.x, _rayOrigin.position.y, _rayOrigin.position.z + (i * gridOffsetX));
            var spawnPosXb = new Vector3(_rayOrigin.position.x, _rayOrigin.position.y, _rayOrigin.position.z + (i * -gridOffsetX));
            
            //increase Grid by factor of two
            GenerateLine(i, "X", spawnPosX, gridLineX, gridLineXList);
            if (i != 0)
            {
                GenerateLine(i, "X", spawnPosXb, gridLineX, gridLineXList);
            }
            
            
        }
    }

    public void AddGridX()
    {
        gridXCount++;
        gridXCount++;
        SpawnGridX();
    }
    public void RemoveGridX()
    {
        gridXCount--;
        gridXCount--;
        SpawnGridX();
    }

    private void GenerateLine(int NameCount, string Axis, Vector3 spawn_Pos, GameObject gridLine, List<GameObject> list)
    {
        var lineName = GetLineName(NameCount, Axis);
        var newLine = Instantiate(gridLine, spawn_Pos, Quaternion.identity);
        newLine.name = lineName;
        newLine.transform.SetParent(_rayOrigin);
        list.Add(newLine);
    }

    public void SpawnGridY()
    {
        _rayOrigin = GameObject.FindObjectOfType<RayOrigin>().transform;

        if (gridYCount <= 0) return;
        //Destroy any thing in the list.
       
        foreach (var gridLine in gridLineYList)
        {
            Debug.Log($"SpawnGrid");
            Destroy(gridLine);
        }
        gridLineYList.Clear();
        
        for (int i = 1; i < gridYCount; i++)
        {
            // Get position for new Grid lines
            var spawnPosY = new Vector3(_rayOrigin.position.x, _rayOrigin.position.y + (i * gridOffsetX), _rayOrigin.position.z );
            
            //increase Grid by factor of two
            GenerateLine(i, "Y", spawnPosY, gridLineY, gridLineYList);
            
            
            
        }
    }
    public void AddGridY()
    {
        gridYCount++;
        SpawnGridY();
    }
    
    public void RemoveGridY()
    {
        gridYCount--;
        SpawnGridY();
    }
    
    public void SpawnGridZ()
    {
        _rayOrigin = GameObject.FindObjectOfType<RayOrigin>().transform;

        if (gridZCount <= 0) return;
        //Destroy any thing in the list.
       
        foreach (var gridLine in gridLineZList)
        {
            Destroy(gridLine);
            
        }
        gridLineZList.Clear();
        
        for (int i = 0; i < gridZCount; i++)
        {
            // Get position for new Grid lines
            var spawnPosZ = new Vector3(_rayOrigin.position.x + (i * gridOffsetX), _rayOrigin.position.y, _rayOrigin.position.z );
            var spawnPosZb = new Vector3(_rayOrigin.position.x + (i * -gridOffsetX), _rayOrigin.position.y, _rayOrigin.position.z );
            
            //increase Grid by factor of two
            GenerateLine(i, "Z", spawnPosZ, gridLineZ, gridLineZList);
            if (i != 0)
            {
                GenerateLine(i, "Z", spawnPosZb, gridLineZ, gridLineZList);
            }
            
            
        }
    }
    
    public void AddGridZ()
    {
        gridZCount++;
        gridZCount++;
        SpawnGridZ();
    }
    
    public void RemoveGridZ()
    {
        gridZCount--;
        gridZCount--;
        SpawnGridZ();
    }
}
