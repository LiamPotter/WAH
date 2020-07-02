using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerationExperments : MonoBehaviour
{
    public int NumberOfRooms = 50;
    public float PlacementRadius;
    public int TileSize;

    public Vector2 MinMaxRoomWidth = new Vector2();
    public Vector2 MinMaxRoomHeight = new Vector2();

    public Room BaseRoom;

    private List<Room> rooms = new List<Room>();

    private Vector3 tempSize;
    private Vector2 tempPosition;
    private Vector3 tempPositionRot= new Vector3();
    private Room tempRoom;
 
    public void Start()
    {
        PlaceRooms();
    }

    public void PlaceRooms()
    {
        for (int i = 0; i < NumberOfRooms; i++)
        {
            tempRoom = Instantiate(BaseRoom);
            tempRoom.transform.localScale = resizeRoom();
            tempPosition = getRandomPointInCircle(PlacementRadius);
            tempPositionRot.x = tempPosition.x;
            tempPositionRot.z = tempPosition.y;
            tempRoom.transform.position = tempPositionRot;
            tempRoom.ID = i;
            rooms.Add(tempRoom);
        }
    }

    public void SeperateRooms()
    {

    }
    
    private Vector3 resizeRoom()
    {
        tempSize.x = roundToTileSize(Random.Range(MinMaxRoomWidth.x, MinMaxRoomWidth.y),TileSize);
        tempSize.y = 1;
        tempSize.z = roundToTileSize(Random.Range(MinMaxRoomHeight.x, MinMaxRoomHeight.y),TileSize);
        return tempSize;
    }

    private int roundToTileSize(float toRound, int tileSize)
    {
        return Mathf.FloorToInt(((toRound + tileSize - 1) / TileSize)) * tileSize;
    }

    private Vector2 getRandomPointInCircle(float radius)
    {
   

        float pi = (float)Mathf.PI;
        float t = 2 * pi * Random.value;
        float u = Random.value + Random.value;
        float r = 0;
        if (u > 1)
            r = 2-u;
        else r = u;
        return new Vector2(
            roundToTileSize(radius * r * Mathf.Cos(t),TileSize),
            roundToTileSize(radius * r * Mathf.Sin(t),TileSize));
    }

}
