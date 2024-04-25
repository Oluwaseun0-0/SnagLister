using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GridController : MonoBehaviour
{
    
    public enum increments
    {
        point0 = 0,
        point1 = 1,
        point2 = 2,
        point3 = 3,
        noIncrement = 4
    }

    [Header("Geometry Controller")] 
    
    public increments offsetAmount = increments.noIncrement;
    public float gridOffset;
    public float gridOffsetX;
    public float gridOffsetY;
    public float gridOffsetZ;
    private float gridOffsetAdjusterValue;
    public float heightOffset = 1.5f;

    public int gridXCount;
    public int gridYCount;
    public int gridZCount;

    public bool gridSymmetry;
    public bool gridOn;

    [Header("Assets")] 
    [SerializeField] private GameObject gridLineX;
    [SerializeField] private GameObject gridLineY;
    [SerializeField] private GameObject gridLineZ;
    [SerializeField] private TMP_Text gridOffsetXText;
    [SerializeField] private TMP_Text gridOffsetYText;
    [SerializeField] private TMP_Text gridOffsetZText;
    [SerializeField] private TMP_Text gridXCountText;
    [SerializeField] private TMP_Text gridYCountText;
    [SerializeField] private TMP_Text gridZCountText;
    [SerializeField] private Camera _camera;

    
    public List<GameObject> gridLineXList;
    public List<GameObject> gridLineYList;
    public List<GameObject> gridLineZList;
    [SerializeField] private Transform _rayOrigin;

    /*private void Awake()
    {
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }*/


    // Start is called before the first frame update
    void Start()
    {
        /*gridLineXList = new List<GameObject>();
        gridLineYList = new List<GameObject>();
        gridLineZList = new List<GameObject>();*/
        
        offsetAmount = increments.noIncrement;
        
        
        // Update GridOffset Amount Text
        UpdateGridAmountText(gridOffsetXText, gridOffsetX);
        UpdateGridAmountText(gridOffsetYText, gridOffsetY);
        UpdateGridAmountText(gridOffsetZText, gridOffsetZ);
        
        
        // Update GridCount Text
        UpdateGridCountText(gridXCountText, gridXCount);
        UpdateGridCountText(gridYCountText, gridYCount);
        UpdateGridCountText(gridZCountText, gridZCount);
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("I am Running");
        
        //Set grid offset increment value
        
        
        switch (offsetAmount)
        {
            
            case increments.point0:
                gridOffsetAdjusterValue = 1f;
                break;
            case increments.point1:
                gridOffsetAdjusterValue = 0.1f;
                break;
            case increments.point2:
                gridOffsetAdjusterValue = 0.01f;
                break;
            case increments.point3:
                gridOffsetAdjusterValue = 0.001f;
                break;
            case increments.noIncrement:
                gridOffsetAdjusterValue = 0f;
                break;
                
        }
        
        
        
        //Set Grid symmetry
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
        UpdateGridCountText(gridXCountText, gridXCount);
        SpawnGridX();
    }
    public void RemoveGridX()
    {
        gridXCount--;
        gridXCount--;
        UpdateGridCountText(gridXCountText, gridXCount);
        SpawnGridX();
    }

    private void GenerateLine(int NameCount, string Axis, Vector3 spawn_Pos, GameObject gridLine, List<GameObject> list)
    {
        var lineName = GetLineName(NameCount, Axis);
        
        // Add Y offset to variable spawnPos.
        var spawningPosition = new Vector3(spawn_Pos.x, spawn_Pos.y + heightOffset, spawn_Pos.z);
        var newLine = Instantiate(gridLine, spawningPosition, Quaternion.identity);
        newLine.name = lineName;            //Change Name of object.
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
            var spawnPosY = new Vector3(_rayOrigin.position.x, _rayOrigin.position.y + (i * gridOffsetY), _rayOrigin.position.z );
            var spawnPosYb = new Vector3(_rayOrigin.position.x, _rayOrigin.position.y - (i * gridOffsetY), _rayOrigin.position.z );
            
            //increase Grid by factor of two
            GenerateLine(i, "Y", spawnPosY, gridLineY, gridLineYList);
            if (i != 0)
            {
                GenerateLine(i, "Z", spawnPosYb, gridLineY, gridLineYList);
            }
            
            
            
        }
    }
    public void AddGridY()
    {
        gridYCount++;
        gridYCount++;
        UpdateGridCountText(gridYCountText, gridYCount);
        SpawnGridY();
    }
    
    public void RemoveGridY()
    {
        gridYCount--;
        gridYCount--;
        UpdateGridCountText(gridYCountText, gridYCount);
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
            var spawnPosZ = new Vector3(_rayOrigin.position.x + (i * gridOffsetZ), _rayOrigin.position.y, _rayOrigin.position.z );
            var spawnPosZb = new Vector3(_rayOrigin.position.x + (i * -gridOffsetZ), _rayOrigin.position.y, _rayOrigin.position.z );
            
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
        UpdateGridCountText(gridZCountText, gridZCount);
        SpawnGridZ();
    }
    
    public void RemoveGridZ()
    {
        gridZCount--;
        gridZCount--;
        UpdateGridCountText(gridZCountText, gridZCount);
        SpawnGridZ();
    }

    public void GridSymmetryToggole()
    {
        gridSymmetry = !gridSymmetry;
        UpdateGridAmountText(gridOffsetXText, gridOffsetX);
        UpdateGridAmountText(gridOffsetYText, gridOffsetY);
        UpdateGridAmountText(gridOffsetZText, gridOffsetZ);
    }
    
    //Increment Setter For GridOffsets

    public void SetIncrementPoint1(int incrementType)
    {
        offsetAmount = (increments)incrementType;
    }

    public void IncreaseXGridOffset()
    {
        
        PositiveGridOffsetAdjuster(ref gridOffsetX);
        UpdateGridAmountText(gridOffsetXText, gridOffsetX);
        SpawnGridX();
    }
    
    public void DecreaseXGridOffset()
    {
        NegativeeGridOffsetAdjuster(ref gridOffsetX);
        UpdateGridAmountText(gridOffsetXText, gridOffsetX);
        SpawnGridX();
    }
    
    public void IncreaseYGridOffset()
    {
        PositiveGridOffsetAdjuster(ref gridOffsetY);
        UpdateGridAmountText(gridOffsetYText, gridOffsetY);
        SpawnGridY();
    }
    
    public void DecreaseYGridOffset()
    {
        NegativeeGridOffsetAdjuster(ref gridOffsetY);
        UpdateGridAmountText(gridOffsetYText, gridOffsetY);
        SpawnGridY();
    }
    public void IncreaseZGridOffset()
    {
        PositiveGridOffsetAdjuster(ref gridOffsetZ);
        UpdateGridAmountText(gridOffsetZText, gridOffsetZ);
        SpawnGridZ();
    }
    
    public void DecreaseZGridOffset()
    {
        NegativeeGridOffsetAdjuster(ref gridOffsetZ);
        UpdateGridAmountText(gridOffsetZText, gridOffsetZ);
        SpawnGridZ();
    }
    
    private void PositiveGridOffsetAdjuster(ref float offset)
    {
        Debug.Log("I am Increasing the offset");
        offset += gridOffsetAdjusterValue;
    }
    
    private void NegativeeGridOffsetAdjuster(ref float planeToOffset)
    {
        Debug.Log("I am Reducing the offset");
        planeToOffset -= gridOffsetAdjusterValue;
    }

    private void UpdateGridAmountText(TMP_Text textToUpdate, float amount)
    {
        
        //Update the OffsetAmount Text based on incremental value.
        switch (offsetAmount)
        {
            case increments.point0:
                textToUpdate.text = $"{amount:F3}";
                break;
            case increments.point1:
                textToUpdate.text = $"{amount:F3}";
                break;
            case increments.point2:
                textToUpdate.text = $"{amount:F3}";
                break;
            case increments.point3:
                textToUpdate.text = $"{amount:F3}";
                break;
            case increments.noIncrement:
                textToUpdate.text = $"{amount:F3}";
                break;
                
        }
        
    }

    private void UpdateGridCountText(TMP_Text textToUpdate, int gridCount)
    {
        textToUpdate.text = $"{gridCount}";
    }
}
