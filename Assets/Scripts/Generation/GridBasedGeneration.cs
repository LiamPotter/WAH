using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridBasedGeneration : MonoBehaviour
{
    public Vector2 GridDimensions;

    public Vector2 MinMaxRoomWidth = new Vector2();
    public Vector2 MinMaxRoomHeight = new Vector2();

    public Dictionary<Vector2, bool> Grid;

    public List<Room> Rooms;

    public int TileSize = 4;

    public int NumberOfRooms = 5;

    public Room RoomPrefab;

    private Room tempRoom;

    private Room previousRoom;

    private Vector3 tempSize = new Vector3();
    private Vector2 tempPosition = new Vector2();
    bool foundPosition = false;
    void Start()
    {
        CreateGrid();
        CreateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        Grid = new Dictionary<Vector2, bool>();
        for (int x = 0; x < GridDimensions.x; x++)
        {
            for (int y = 0; y < GridDimensions.y; y++)
            {
                Grid.Add(new Vector2(x, y), false);
                //Debug.Log("Added Grid Space at x: " + x + " y: " + y);
            }
        }
    }

    private void CreateRooms()
    {
        Vector2 initialPosition = new Vector2(
            Random.Range(0, GridDimensions.x),
            Random.Range(0, GridDimensions.y));
        for (int i = 0; i < NumberOfRooms; i++)
        {
            tempRoom = Instantiate(RoomPrefab);
            if (i == 0)
            {
                tempRoom.GridPosition = initialPosition;
            }
            else
            {
                tempRoom.GridPosition = findFreeGridSpace(previousRoom);
            }
            Grid[tempRoom.GridPosition] = true;
            tempRoom.transform.position = tempRoom.GridPosition;
            previousRoom = tempRoom;
        }
    }

    private Vector2 findFreeGridSpace(Room prevRoom)
    {
        foundPosition = false;
        while (!foundPosition)
        {
            Vector2 randomGridSpace = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            tempPosition = previousRoom.GridPosition + randomGridSpace;
            Debug.Log(tempPosition);
            if (tempPosition.x >= 0 && tempPosition.x <= GridDimensions.x)              
                if (tempPosition.y >= 0 && tempPosition.y <= GridDimensions.y)   
                    if (!Grid[tempPosition])
                        foundPosition = true;
        }
        return tempPosition;
    }

    private Vector3 setRoomSize()
    {
        tempSize.x = roundToTileSize(Random.Range(MinMaxRoomWidth.x, MinMaxRoomWidth.y), TileSize);
        tempSize.y = 1;
        tempSize.z = roundToTileSize(Random.Range(MinMaxRoomHeight.x, MinMaxRoomHeight.y), TileSize);
        return tempSize;
    }
    private int roundToTileSize(float toRound, int tileSize)
    {
        return Mathf.FloorToInt(((toRound + tileSize - 1) / TileSize)) * tileSize;
    }
}
