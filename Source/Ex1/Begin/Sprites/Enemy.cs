using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienAttackUniversal.Sprites
{
    class Enemy : Sprite
    {
        public Enemy()
        {
            LoadContent(AlienAttackGame.Instance.Content, "gfx\\enemy1\\enemy1_{0}", 10);
        }

        public override void Update(GameTime gameTime)
        {
            AnimateReverse(gameTime, 60);
        }
    }
}
