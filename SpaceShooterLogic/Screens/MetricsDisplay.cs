using System.Collections.Generic;
using GameEngineCore;
using GuiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShooterUtilities;

namespace SpaceShooterLogic.Screens
{
    public class MetricsDisplay
    {
        private readonly GameState _gameState;
        private readonly Grid _grid;
 
        public MetricsDisplay(GameState gameState)
        {
            _gameState = gameState;

            var name = new GridColumn { Text = "Name", HorizontalAlignment = HorizontalAlignment.Left, X = 50.0f };
            var time = new GridColumn { Text = "Time (ms)", HorizontalAlignment = HorizontalAlignment.Right, X = 285.0f };
            var frames = new GridColumn { Text = "Frames", HorizontalAlignment = HorizontalAlignment.Right, X = 355.0f };
            var avg = new GridColumn { Text = "Avg. (ms)", HorizontalAlignment = HorizontalAlignment.Right, X = 425.0f };

            var gridColumns = new GridColumns(name, time, frames, avg);
            _grid = new Grid(AssetsManager.Instance.GetSpriteFont("arialSmall"), AssetsManager.Instance.GetSpriteFont("arialTiny"), Color.CornflowerBlue, new Vector2(50.0f, 300.0f), true, gridColumns);
        }

        public void Update(GameTime gameTime)
        {
            _grid.ClearRows();
            foreach (KeyValuePair<string, Metric> entry in _gameState.Metrics)
            {
                string name = entry.Key;
                string time = entry.Value.ElapsedTime.ToString("F");
                string frames = entry.Value.Frames.ToString();
                string avg = (entry.Value.ElapsedTime / entry.Value.Frames).ToString("F");

                var row = new GridRow(_grid, AssetsManager.Instance.GetSpriteFont("arialTiny"), Color.LightBlue, name, time, frames, avg);
                _grid.AddRow(row);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _grid.Draw(spriteBatch);
        }
    }
}