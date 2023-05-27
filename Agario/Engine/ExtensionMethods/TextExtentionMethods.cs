using SFML.System;
using SFML.Graphics;

namespace Agario.Engine.ExtensionMethods.TextExtentionMethods
{
    public static class TextExtentionMethods
    {
        public static Text SetupText(this Text text, string displayedText, Font font, Color color, uint size, Vector2f position)
        {
            Text newText = new Text(displayedText, font);
            newText.Position = position;
            newText.FillColor = color;
            newText.CharacterSize = size;
            return newText;
        }
    }
}