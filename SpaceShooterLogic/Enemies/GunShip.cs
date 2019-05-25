using Microsoft.Xna.Framework;

namespace SpaceShooterLogic.Enemies
{
    public class GunShip : Enemy
    {
        private const int LASER_VELOCITY = 300; // in pixels per second

        private float _timeElapsedSinceLastShot;
        private readonly int _durationInMillisecondsBetweenEachShot = 1000;

        public bool CanShoot { get; private set; }

        public GunShip(string textureName, Vector2 position, Vector2 velocity) : base(textureName, position, velocity)
        {
        }

        public override int Score => 20;

        public override void Update(GameTime gameTime)
        {
            if (!CanShoot)
            {
                if (_timeElapsedSinceLastShot < _durationInMillisecondsBetweenEachShot)
                {
                    _timeElapsedSinceLastShot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    CanShoot = true;
                }
            }

            base.Update(gameTime);
        }

        public override void UseSpecialPower(GameTime gameTime)
        {
            if (CanShoot)
            {
                var projectile = new Projectile("sprLaserEnemy0", new Vector2(Position.X, Position.Y + 30.0f), new Vector2(0.0f, LASER_VELOCITY));
                GameEntitiesManager.Instance.EnemyProjectiles.Add(projectile);
                ResetCanShoot();
            }
        }

        private void ResetCanShoot()
        {
            CanShoot = false;
            _timeElapsedSinceLastShot = 0;
        }
    }
}