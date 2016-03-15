using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AlienAttackUniversal.Sprites
{
    class EnemyGroup : Sprite
    {
        // grid of enemies
        private readonly Enemy[,] _enemies;
        // all enemy shots
        private readonly List<EnemyShot> _enemyShots;
        // all enemy explosions
        private readonly List<Explosion> _explosions;
        private readonly Random _random;

        // width of single enemy
        private readonly int _enemyWidth;
        private const int EnemyRows = 4;    // number of rows in grid
        private const int EnemyCols = 8;    // number of cols in grid
        private readonly Vector2 EnemyVerticalJump = new Vector2(0, 10);    // number of pixels to jump vertically after hitting edge
        private const int EnemyStartPosition = 10;  // vertical position of grid
        private const int ScreenEdge = 20;  // virtual edge of screen to change direction
        private Vector2 EnemySpacing = new Vector2(16, 32); // space between sprites
        private readonly Vector2 EnemyVelocity = new Vector2(100 / 1000.0f, 0);	// speed at which grid moves per frame

        public EnemyGroup()
        {
            _random = new Random();
            _enemyShots = new List<EnemyShot>();
            _explosions = new List<Explosion>();
            _enemies = new Enemy[EnemyRows, EnemyCols];
            // create a grid of enemies
            for (int y = 0; y < EnemyRows; y++)
            {
                for (int x = 0; x < EnemyCols; x++)
                {
                    Enemy enemy = new Enemy();
                    enemy.Position = new Vector2(x * enemy.Width + EnemySpacing.X, y * enemy.Height + EnemySpacing.Y);
                    _enemies[y, x] = enemy;
                }
            }
            _enemyWidth = _enemies[0, 0].Width;
            // position the grid centered at the vertical position specified above
            Position = new Vector2(AlienAttackGame.ScreenWidth / 2.0f - ((EnemyCols * (_enemyWidth + EnemySpacing.X)) / 2), EnemyStartPosition);
            Velocity = EnemyVelocity;
        }

        public void Reset()
        {
            _enemyShots.Clear();
        }

        private Enemy FindRightMostEnemy()
        {
            // find the enemy in the right-most position in the grid
            for (int x = EnemyCols - 1; x > -1; x--)
            {
                for (int y = 0; y < EnemyRows; y++)
                {
                    if (_enemies[y, x] != null)
                        return _enemies[y, x];
                }
            }
            return null;
        }

        private Enemy FindLeftMostEnemy()
        {
            // find the enemy in the left-most position in the grid
            for (int x = 0; x < EnemyCols; x++)
            {
                for (int y = 0; y < EnemyRows; y++)
                {
                    if (_enemies[y, x] != null)
                        return _enemies[y, x];
                }
            }
            return null;
        }

        public bool AllDestroyed()
        {
            // we won if we can't find any enemies at all
            return (FindLeftMostEnemy() == null);
        }

        private void MoveEnemies(GameTime gameTime)
        {
            Enemy enemy = FindRightMostEnemy();

            // if the right-most enemy hit the screen edge, change directions
            if (enemy != null)
            {
                if (enemy.Position.X + enemy.Width > AlienAttackGame.ScreenWidth - ScreenEdge)
                {
                    Position += EnemyVerticalJump;
                    Velocity = -EnemyVelocity;
                }
            }

            enemy = FindLeftMostEnemy();

            // if the left-most enemy hit the screen edge, change direction
            if (enemy != null)
            {
                if (enemy.Position.X < ScreenEdge)
                {
                    Position += EnemyVerticalJump;
                    Velocity = EnemyVelocity;
                }
            }

            // update the positions of all enemies
            for (int y = 0; y < EnemyRows; y++)
            {
                for (int x = 0; x < EnemyCols; x++)
                {
                    if (_enemies[y, x] != null)
                    {
                        // X = position of the whole grid + (X grid position * width of enemy) + padding
                        // Y = position of the whole grid + (Y grid position * width of enemy) + padding
                        _enemies[y, x].Position =
                            new Vector2((Position.X + (x * (_enemyWidth + EnemySpacing.X))),
                                        (Position.Y + (y * (_enemyWidth + EnemySpacing.Y))));
                        _enemies[y, x].Update(gameTime);
                    }
                }
            }
        }

        private void EnemyFire(GameTime gameTime)
        {
            if (AllDestroyed())
                return;

            // at random times, drop an enemy shot
            if (_random.NextDouble() > 0.99f)
            {
                int x, y;

                // find an enemy that hasn't been destroyed
                do
                {
                    x = (int)(_random.NextDouble() * EnemyCols);
                    y = (int)(_random.NextDouble() * EnemyRows);
                }
                while (_enemies[y, x] == null);

                // create a shot for that enemy and add it to the list
                EnemyShot enemyShot = new EnemyShot();
                enemyShot.Position = _enemies[y, x].Position;
                enemyShot.Position += new Vector2(0, _enemies[y, x].Height);
                _enemyShots.Add(enemyShot);

                AudioManager.PlayCue(AudioManager.Cue.EnemyShot);
            }

            for (int i = 0; i < _enemyShots.Count; i++)
            {
                // update all shots
                _enemyShots[i].Update(gameTime);

                // remove those that are off the screen
                if (_enemyShots[i].Position.Y > AlienAttackGame.ScreenHeight)
                    _enemyShots.RemoveAt(i);
            }
        }

        public bool CheckCollision(Sprite s1, Sprite s2)
        {
            // simple bounding box collision detection
            return s1.BoundingBox.Intersects(s2.BoundingBox);
        }

        //TODO Uncomment
        //public bool HandlePlayerShotCollision(PlayerShot playerShot)
        //{
        //    for (int y = 0; y < EnemyRows; y++)
        //    {
        //        for (int x = 0; x < EnemyCols; x++)
        //        {
        //            // if a player shot hit an enemy, destroy the enemy
        //            if (_enemies[y, x] != null && CheckCollision(playerShot, _enemies[y, x]))
        //            {
        //                Explosion explosion = new Explosion();
        //                Vector2 center = _enemies[y, x].Position + (_enemies[y, x].Size / 2.0f);
        //                explosion.Position = center - (explosion.Size / 2.0f);
        //                _explosions.Add(explosion);
        //                _enemies[y, x] = null;
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        //
        //public bool HandleEnemyShotCollision(Player player)
        //{
        //    for (int i = 0; i < _enemyShots.Count; i++)
        //    {
        //        // if an enemy shot hit the player, destroy the player
        //        if (CheckCollision(_enemyShots[i], player))
        //        {
        //            _enemyShots.RemoveAt(i);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //              
        //public bool HandleEnemyPlayerCollision(Player player)
        //{
        //    for (int y = 0; y < EnemyRows; y++)
        //    {
        //        for (int x = 0; x < EnemyCols; x++)
        //        {
        //            // if an enemy hit the player, destroy the enemy
        //            if (_enemies[y, x] != null && CheckCollision(_enemies[y, x], player))
        //            {
        //                Explosion explosion = new Explosion();
        //                Vector2 center = _enemies[y, x].Position + (_enemies[y, x].Size / 2.0f);
        //                explosion.Position = center - (explosion.Size / 2.0f);
        //                _explosions.Add(explosion);
        //                _enemies[y, x] = null;
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MoveEnemies(gameTime);
            EnemyFire(gameTime);

            for (int i = 0; i < _explosions.Count; i++)
            {
                // update all explosions, remove those whose animations are over
                if (_explosions[i].Update(gameTime))
                    _explosions.RemoveAt(i);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // draw all active enemies
            foreach (Enemy enemy in _enemies)
            {
                if (enemy != null)
                    enemy.Draw(gameTime, spriteBatch);
            }

            // draw all enemy shots
            foreach (EnemyShot enemyShot in _enemyShots)
                enemyShot.Draw(gameTime, spriteBatch);

            // draw all explosions
            foreach (Explosion explosion in _explosions)
                explosion.Draw(gameTime, spriteBatch);
        }
    }
}
