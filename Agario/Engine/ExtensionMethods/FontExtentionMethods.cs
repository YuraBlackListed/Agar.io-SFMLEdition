using SFML.Graphics;
using System.IO;

namespace Agario.Engine.ExtensionMethods.FontExtentionMethods
{
    public static class FontExtentionMethods
    {
        private static string thisDirectoryPath = GetFontDirectory();
        private static string GetFontDirectory()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
            return path;
        }
        public static Font LoadFromFile(this Font font, string input)
        {
            string fontPath = Path.Combine(thisDirectoryPath, "Fonts", input);
            Font newFont = new Font(fontPath);
            return newFont;
        }
    }
}
