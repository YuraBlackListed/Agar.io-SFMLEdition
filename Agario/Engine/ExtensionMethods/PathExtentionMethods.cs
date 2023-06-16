using SFML.Graphics;
using System.IO;

namespace Agario.Engine.ExtensionMethods.PathExtentionMethods
{
    public static class PathExtentionMethods
    {
        private static string thisDirectoryPath = GetFontDirectory();
        public static string GetFontDirectory()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
            return path;
        }
    }
}
