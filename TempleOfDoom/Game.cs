using CODE_Frontend;
using CODE_GameLib.Enums;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Entities.Enemy;
using CODE_GameLib.Models.Floors;
using CODE_GameLib.Models.Room;
using CODE_GameLib.Models.Utils;
using CODE_GameLib.ObservableInterfaces;

namespace TempleOfDoom
{
    public class Game : Observer
    {
        private readonly Level _level;
        
        private Player _player;
        
        private Room _playerCurrentRoom;

        public Game(Level level)
        {
            _level = level;
        }


        public void Run()
        {
            _level.Player.Attach(this);
            
            Console.Clear();
            GameView.Render(_level);
            while (!_level.Player.IsDone)
            {
                _player = _level.Player;
                _playerCurrentRoom = _player.CurrentRoom;
                ConsoleKey key = Console.ReadKey().Key;
                if (key is ConsoleKey.Escape or ConsoleKey.Q)
                    break;
                if (key is ConsoleKey.Spacebar)
                {
                   DamageEnemies();
                }
                Direction? direction = DirectionExtension.ToDirectionOrNull(key);
                
                if (direction == null) continue;
                _level.Player.Move(direction.Value);
               
               
            }
        }

        private void DamageEnemies()
        {
            _level.Player.CurrentRoom.Entities.ForEach(entity =>
            {
                if (entity is not EnemyAdapter enemy) return;
                if (enemy.Position.IsAdjacentTo(_player.Position))
                { 
                    _level.Player.Attack(enemy);
                }
            });
        }
        
        public void Update(Observable observable)
        {
            if (observable is not Player player) return;
            if (player.IsDead)
            {
                Console.Clear();
                Console.WriteLine("You died!");
                Thread.Sleep(3000);
            }
            else if (player.HasWon)
            {
                Console.Clear();
                Console.WriteLine("You won!");
                Thread.Sleep(3000);

            }
            else switch (player)
            {
                case { PerformedAction: true, MovedIntoNewRoom: true }:
                    GameView.Render(_level);
                    break;
                case { PerformedAction: true, MovedIntoNewRoom: false }:
                {
                    _level.Player.PossibleSpecialFloorInteractions();

                    foreach (IEntity entity in _playerCurrentRoom.Entities.Where(entity => !entity.IsDead))
                    {
                        //store the old position so we know what tile the entity was on before moving
                        Position oldPosition = entity.Position;
                        
                        //perform the regular move of the entity
                        if (entity is not IAutoMovingEntity autoMovingEntity) continue;
                        autoMovingEntity.AutomaticallyMove();

                        if (autoMovingEntity is not EnemyAdapter enemy) continue;
                        // If enemy attacks the player, it is because the player is on the tile the enemy wants to move to.
                        // Instead of moving the enemy, we move the enemy back to the old position and attack the player.
                        if (enemy.ShouldAttackPlayer(_level.Player))
                        {
                            _level.Player.Damage(EnemyAdapter.DamageAmount);
                            enemy.SetPosition(oldPosition);
                        }
                            
                        // check whether the enemy is on a special floor tile, if so, interact with it
                        foreach (ISpecialFloor specialFloor in _playerCurrentRoom.SpecialFloors.Where(specialFloor => specialFloor.Position.Equals(entity.Position)))
                        {
                            specialFloor.Interact(entity);
                            if (enemy.ShouldAttackPlayer(_player))
                            {
                                _level.Player.Damage(EnemyAdapter.DamageAmount);
                                enemy.SetPosition(oldPosition);
                            }
                        }

                    }

                    GameView.Render(_level);
                    break;
                }
            }
        }
    }
}