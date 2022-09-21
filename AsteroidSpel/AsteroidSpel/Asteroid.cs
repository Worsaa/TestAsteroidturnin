using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;


namespace AsteroidSpel
{
    public class Asteroid
    {
        public Texture2D texAstClass;
        public Vector2 posAstClass;
        public Vector2 velocityAstClass;
        public int widthAstClass;

        public bool aliveAstClass;
        public Rectangle hitboxAstClass;
        
        public Random rnd = new Random();
        public int minTimer = 500;
        public int maxTimer = 1000;
        public int spawnTime = 0;       

        public void resetSpawnTime()
        {
            spawnTime = rnd.Next(minTimer, maxTimer);
        }

        public Asteroid(Texture2D texAstClass, Vector2 posAstClass, Vector2 velocityAstClass, int widthAstClass)
        {
            this.texAstClass = texAstClass;
            this.posAstClass = posAstClass;
            this.velocityAstClass = velocityAstClass;
            this.widthAstClass = widthAstClass;   
            
            aliveAstClass = true;
        }

        public void Update() 
        {
            posAstClass = posAstClass + velocityAstClass;         
            hitboxAstClass = new Rectangle((int)posAstClass.X, (int)posAstClass.Y, texAstClass.Width, texAstClass.Height);        
        }

        public void Draw(SpriteBatch spritebatch)
        {           
            spritebatch.Draw(texAstClass, posAstClass, Color.White);             
        }

        public bool isShootBool(int x, int y)
        {
            bool isShootBool = false;
            Rectangle hitboxAsteroid = new Rectangle((int)posAstClass.X, (int)posAstClass.Y, texAstClass.Width, texAstClass.Height);

            if (hitboxAsteroid.Contains(x, y))
            {
                isShootBool = true;
                aliveAstClass = false;
            }

            return isShootBool;
        }
    }
}