using System;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Utils;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Models.Entities.Enemy;

 public class EnemyAdapter: IAutoMovingEntity
 {
     private readonly CODE_TempleOfDoom_DownloadableContent.Enemy _adaptee;

     public Position Position => new(_adaptee.CurrentXLocation, _adaptee.CurrentYLocation);

     public Position StartPosition { get; }
     private Direction LastDirection { get; set; }

     public bool IsDead { get; private set; }
     public int Lives => _adaptee.NumberOfLives;
     public const int DamageAmount = 1;

     public EnemyAdapter(string type, int x, int y, int minX, int maxX, int minY, int maxY)
     {
         MovableDirection movableDirection = Enum.Parse<MovableDirection>(type, true);
         _adaptee = movableDirection switch
         {
             MovableDirection.Horizontal => new HorizontallyMovingEnemy(3, x, y, minX, maxX),
             MovableDirection.Vertical => new VerticallyMovingEnemy(3, x, y, minY, maxY),
             _ => throw new ArgumentException("Unknown type of enemy")
         };

         StartPosition = new Position(x, y);
         _adaptee.OnDeath += c_OnDeath;
         _adaptee.CurrentField = new Field();
     }

     public void Damage(int amount)
     {
         _adaptee.DoDamage(amount);
     }

     public void SetPosition(Position position)
     {
         _adaptee.CurrentXLocation = position.X;
         _adaptee.CurrentYLocation = position.Y;
     }

     public void AutomaticallyMove()
     {
         _adaptee.Move();
     }

     public void Move(Direction direction)
     {
         Position directionValues = direction.GetDirectionValues();
         _adaptee.CurrentXLocation += directionValues.X;
         _adaptee.CurrentYLocation += directionValues.X;

         LastDirection = direction;
     }

     private void c_OnDeath(object sender, EventArgs e)
     {
         if (sender is CODE_TempleOfDoom_DownloadableContent.Enemy)
         {
             IsDead = true;
         }     
     }

     public bool ShouldAttackPlayer( Player player)
     {
         return Position.Equals(player.Position);
     }
     
     public Direction GetLastDirection()
     {
         return LastDirection;
     }

 } 