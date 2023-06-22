using SFML.Graphics;
using Agario.Engine.ExtensionMethods.PathExtentionMethods;
using System.IO;
using System.Collections.Generic;
using Agario.Engine.Interfaces;

namespace Agario.Engine
{
    public class Animation : IUpdatable
    {
        public List<Texture> frames;
        public int curretFrame = 0;
        
        public Animation(string keyword) 
        {
            frames = new List<Texture>();
            GetTextures(keyword);
            GameLoop.updatableObjects.Add(this);
        }
        private void GetTextures(string key)
        {
            int index = 1;

            string currentDirectory = PathExtentionMethods.GetCurrentDirectory();

            string folderPath = currentDirectory + "/Game/" + "Animations/" + key;

            string filePath = folderPath + "/" + index + ".png";

            while (File.Exists(filePath))
            {
                frames.Add(new Texture(filePath));
                index++;
                filePath = folderPath + "/" + index + ".png";
            }
        }
        public void Update()
        {
            if (curretFrame < frames.Count - 1)
            {
                curretFrame++;
                return;
            }
                
            curretFrame = 0;
        }
        public void Destroy()
        {
            GameLoop.updatableObjects.Remove(this);
        }
    }
}
