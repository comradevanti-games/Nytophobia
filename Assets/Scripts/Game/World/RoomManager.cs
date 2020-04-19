﻿using Gang1057.Ludiwuri.Game.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gang1057.Ludiwuri.Game.World
{

    /// <summary>
    /// Manages the loading of rooms
    /// </summary>
    public class RoomManager : MonoBehaviour
    {

        #region Fields

        /// <summary>
        /// Called when a room is entered
        /// </summary>
        public RoomEvent onRoomEntered;

#pragma warning disable 649

        /// <summary>
        /// The initial rooms name
        /// </summary>
        [SerializeField] private string initialRoomName;
        /// <summary>
        /// The player
        /// </summary>
        [SerializeField] private PlayerController player;

#pragma warning restore 649

        /// <summary>
        /// A dictionary of cached rooms, indexed by their name
        /// </summary>
        private Dictionary<string, Room> cachedRooms = new Dictionary<string, Room>();
        /// <summary>
        /// The currently active room
        /// </summary>
        private Room currentRoom;

        #endregion

        #region Properties

        /// <summary>
        /// The singleton instance of this class
        /// </summary>
        public static RoomManager Instance { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Enters the room with the given name
        /// </summary>
        /// <param name="roomName">The rooms name</param>
        public void EnterRoom(string roomName)
        {
            // Get the room with that name

            Room room = GetRoom(roomName);

            // Enter the room

            EnterRoom(room);
        }


        /// <summary>
        /// Enters the given room
        /// </summary>
        /// <param name="room">The room</param>
        private void EnterRoom(Room room)
        {
            // Get the current rooms name

            string lastRoomName = currentRoom.Name;

            // Exit the current room

            currentRoom.OnExit();

            // Enter the new room

            room.OnEnter();

            // Get the door that connects to the previous room

            Door enterDoor = room.GetDoorToRoom(lastRoomName);

            // Teleport the player to it

            player.TeleportTo(enterDoor.Position);

            // Set the room to be the current one

            currentRoom = room;

            // Fire enter event

            onRoomEntered.Invoke(room);
        }

        /// <summary>
        /// Loads the room the game starts in
        /// </summary>
        private void EnterInitialRoom()
        {
            // Get the room with the initial rooms name

            Room room = GetRoom(initialRoomName);

            // Enter the new room

            room.OnEnter();

            // Get the bed spawn-point

            SpawnPoint bed = room.GetSpawnPoint("Bed");

            // Teleport the player to the bed

            player.TeleportTo(bed.Position);

            // Set the room as the current room

            currentRoom = room;
        }

        /// <summary>
        /// Gets the room with the given name
        /// </summary>
        /// <param name="roomName">The rooms name</param>
        /// <returns>The room</returns>
        private Room GetRoom(string roomName)
        {
            // Declare variable for the room

            Room room = null;

            // If the room is already cached

            if (cachedRooms.ContainsKey(roomName))

                // Get the room from the cache

                room = cachedRooms[roomName];

            // If it is not yet cached

            else
            {
                // Load the room

                room = LoadRoomFromAsset(roomName);
            }

            // Return the room

            return room;
        }

        /// <summary>
        /// Loads a room 
        /// </summary>
        /// <param name="roomAsset">The rooms asset</param>
        /// <returns>The loaded room</returns>
        private Room LoadRoomFromAsset(string roomName)
        {
            // Instantiate the prefab

            GameObject roomGameObject = Instantiate(
                Resources.Load<GameObject>($"Rooms/{roomName}")
                , transform);

            // Create a room

            Room room = new Room(roomName, roomGameObject);

            // Cache the room

            cachedRooms.Add(room.Name, room);

            // Return the loaded room

            return room;
        }

        private void Awake()
        {
            Instance = this;

            // Enter the initial room

            EnterInitialRoom();
        }

        #endregion

        #region SubClasses

        /// <summary>
        /// Event with <see cref="Room"/> parameter
        /// </summary>
        [System.Serializable]
        public class RoomEvent : UnityEvent<Room> { }

        #endregion

    }

}