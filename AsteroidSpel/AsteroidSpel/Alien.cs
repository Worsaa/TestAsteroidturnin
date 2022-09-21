using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AsteroidSpel
{
    public class Alien
    {
        public Texture2D texAlien;
        public Vector2 posAlien;
        public float sizeAlien;

        public Alien(Texture2D texAlien, Vector2 posAlien, float sizeAlien)
        {
            this.texAlien = texAlien;
            this.posAlien = posAlien;
            this.sizeAlien = sizeAlien;    
        }
        public void Draw(SpriteBatch alienSpriteBatch)
        {
            alienSpriteBatch.Draw(texAlien, posAlien, null, Color.White, 0, Vector2.Zero, sizeAlien, SpriteEffects.None,0);  
        }
    }
}
