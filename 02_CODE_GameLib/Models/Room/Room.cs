using System;
using System.Collections.Generic;
using System.ComponentModel;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Floors;
using CODE_GameLib.Models.Items;

namespace CODE_GameLib.Models.Room
{
    public class Room
    {
        public int Id { get; }
        private string Type { get; }
        private int Width { get; }
        private int Height { get; }
        
        public int LeftX() => 0;
        public int RightX() => Width - 1;
        public int TopY() => Height - 1;
        public int BottomY() => 0;

        public Dictionary<Direction, Connection> ConnectedRooms { get; }

        public List<IEntity> Entities { get; }

        public List<ISpecialFloor> SpecialFloors { get; }
        public List<IItem> Items { get; }


        public Room(int id, int width, int height, string type)
        {
            Id = id;
            Width = width;
            Height = height;
            Type = type;
            ConnectedRooms = new Dictionary<Direction, Connection>();
            Entities = new List<IEntity>();
            SpecialFloors = new List<ISpecialFloor>();
            Items = new List<IItem>();
        }

        private int GetMiddleOfRoom(int roomSize)
        {
            // if a room is even, we want to round up to the next int
            double approxMiddle = roomSize / 2.0;

            return Convert.ToInt32(Math.Ceiling(approxMiddle));
        }

        private bool IsMiddle(int roomSize, int position)
        {
            double approxMiddle = roomSize / 2.0;

            int middle = Convert.ToInt32(Math.Ceiling(approxMiddle));

            return position == middle - 1;
        }

        public Position GetEntrancePosition(Direction entranceDirection)
        {
            switch (entranceDirection)
            {
                case Direction.East:
                    return new Position(RightX(), GetMiddleOfRoom(TopY()));
                case Direction.West:
                    return new Position(LeftX(), GetMiddleOfRoom(TopY()));
                case Direction.South:
                    return new Position(GetMiddleOfRoom(RightX()), TopY());
                case Direction.North:
                    return new Position(GetMiddleOfRoom(RightX()), BottomY());
                default:
                    throw new InvalidEnumArgumentException("Invalid direction");
            }
        }

        public bool IsInRoom(Position pos)
            => pos.X > LeftX() && pos.X < RightX() && pos.Y > BottomY() && pos.Y < TopY();


        public bool IsConnectedRoom(Position pos)
        {
            Direction? entranceDirection = PositionToDirectionOrNull(pos);
            return entranceDirection.HasValue && ConnectedRooms.ContainsKey(entranceDirection.Value);
        }

        public Direction? PositionToDirectionOrNull(Position pos)
        {
            if (IsPossibleEntrance(pos))
            {
                if (pos.Y == TopY()) return Direction.South;
                if (pos.X == LeftX()) return Direction.West;
                if (pos.Y == BottomY()) return Direction.North;
                if (pos.X == RightX()) return Direction.East;
            }

            return null;
        }

        private bool IsWall(Position pos)
            => pos.X == LeftX() || pos.X == RightX() || pos.Y == BottomY() || pos.Y == TopY();

        private bool IsPossibleEntrance(Position pos)
            => IsWall(pos) && (IsMiddle(Width, pos.X) || IsMiddle(Height, pos.Y));
        public bool IsValidMove(Player player, Position newPosition)
        {
            if (IsInRoom(newPosition))
            {
                return true;
            }

            // check whether it is just a wall the player wants to walk to, or if there is a room behind it.
            if (!IsConnectedRoom(newPosition)) return false;
            Direction? entranceDirection = PositionToDirectionOrNull(newPosition);
				
            if (entranceDirection == null)
            {
                return false;
            }

            Connection connection = ConnectedRooms[entranceDirection.Value];
            IDoor door = connection.Door;
            door.Interact(player);
					
            return door.IsOpen;
        }
        
        
    }
}