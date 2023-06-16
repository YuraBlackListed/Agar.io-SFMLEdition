using SFML.Graphics;
using System.IO;

namespace Agario.Engine.ExtensionMethods.PathExtentionMethods
{
    public static class PathExtentionMethods
    {
        private static string thisDirectoryPath = GetCurrentDirectory();
        public static string GetCurrentDirectory()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
            return path;
        }
    }
}
