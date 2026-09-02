using Library;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SE_Platformer_unlocker.Scenes;
using SE_Platformer_unlocker.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Platformer_unlocker.Factories
{
    public class UIFactory
    {
        private const int shadowDepth = 10;


        public UIButton CreateButton(string textString, SpriteFont font, Sprite buttonSprite, int x, int y, int width, int height, Action action)
        {
            Text text = new Text(font, textString);

            UIButton button = new UIButton(text, buttonSprite, new Rectangle(Game1.Scaler.ScaledWidth(x), Game1.Scaler.ScaledHeight(y), Game1.Scaler.ScaledWidth(width), Game1.Scaler.ScaledHeight(height)), action);
            button.CenterButton();
            return button;
        }

        public UIText CreateText(string textString, SpriteFont font, float x, float y)
        {
            Text text = new Text(font, textString);
            text.CenterText();
            UIText uiText = new UIText(text, new Vector2(x, y));
            return uiText;
        }

        public UIText CreateShadow(UIText uiText)
        {
            Text shadow = new Text(uiText.Text.Font, uiText.Text.InnerText);
            shadow.CenterText();
            shadow.Color = Color.Black * 0.5f;
            shadow.LayerDepth = 1;
            return new UIText(shadow, uiText.Position + new Vector2(shadowDepth, shadowDepth));
        }
        /*
        public UIIcon CreateIcon()
        {

        }*/
    }
}
