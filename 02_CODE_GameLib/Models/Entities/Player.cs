using CODE_GameLib.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Floors;
using CODE_GameLib.Models.Room;
using CODE_GameLib.Models.Utils;
using CODE_GameLib.ObservableInterfaces;

namespace CODE_GameLib.Models.Entities
{
		public class Player : IEntity, Observable
		{
			private const int PlayerDamage = 1;
			
			public Inventory Inventory { get; }
			public Room.Room CurrentRoom { get; private set; }
			public Position Position { get; private set; }
			private Direction LastDirection { get; set; }
			public int Lives { get; private set; }
			
			private int SankaraStones => Inventory.GetSankaraStonesCount();
			public bool HasWon => SankaraStones >= 5;
			public bool IsDead => Lives <= 0;
			public bool IsDone => HasWon || IsDead;
			public bool PerformedAction { get; private set; }
			
			public bool MovedIntoNewRoom { get; private set; }

			private readonly List<Observer> _observers = new();

		public Player(Room.Room startRoom, Position position, int lives) {
			CurrentRoom = startRoom;
			Position = position;
			Lives = lives;
			Inventory = new Inventory();
		}
		
		public void AddItemToInventory(IItem item)
		{
			Inventory.AddItem(item);
			Notify();
		}
		
		public bool HasKey(Color color)
		{
			return Inventory.HasKey(color);
		}

		public void Damage(int amount)
		{
			Lives = Math.Max(Lives - amount, 0);
			Notify();
		}
		
		public void Attack(IEntity entity)
		{
			PerformedAction = true;
			entity.Damage(PlayerDamage);
			Notify();
			PerformedAction = false;
		}

		public void Move(Direction direction) {
			
			Position directionValues = direction.GetDirectionValues();
			Position newPosition = Position.Add(directionValues);

			// if the move isn't valid, we dont want to do anything
			if (!CurrentRoom.IsValidMove(this, newPosition)) return;

			LastDirection = direction;
			Position = newPosition;
			PerformedAction = true;
				
			// If the player moves into a room using a connection, set the property so the observer can act on it.
			MovedIntoNewRoom = PossibleMoveToNewRoom();
			
			IItem item = CurrentRoom.Items.FirstOrDefault(item => item.Exists && item.Position.Equals(Position));
			item?.Interact(this);
			Notify();
			PerformedAction = false;
			MovedIntoNewRoom = false;


		}

		public void PossibleSpecialFloorInteractions()
		{
			foreach (ISpecialFloor specialFloor in CurrentRoom.SpecialFloors.Where(specialFloor => specialFloor.IsSpecialFloor(Position)))
			{
				specialFloor.Interact(this);
			}
		}

		private bool PossibleMoveToNewRoom()
		{
			Direction? entranceDirection = CurrentRoom.PositionToDirectionOrNull(Position);
			if (entranceDirection != null && CurrentRoom.IsConnectedRoom(Position))
			{
				MoveToNewRoomUsingDoor(entranceDirection.Value);
				return true;
			}
			
			return false;
		}

		private void MoveToNewRoomUsingDoor(Direction entranceDirection)
		{
			Connection entrance = CurrentRoom.ConnectedRooms[entranceDirection];
			CurrentRoom = entrance.Room;

			//set the player position to the entrance of the new room
			Position position = CurrentRoom.GetEntrancePosition(entranceDirection.Opposite());
			Position = position;
		}
		
		public void Attach(Observer observer)
		{
			_observers.Add(observer);
		}

		public void Detach(Observer observer)
		{
			_observers.Remove(observer);
		}

		public void Notify()
		{
			foreach (Observer observer in _observers)
			{
				observer.Update(this);
			}
		}
		
		public Direction GetLastDirection()
		{
			return LastDirection;
		}
	}
}