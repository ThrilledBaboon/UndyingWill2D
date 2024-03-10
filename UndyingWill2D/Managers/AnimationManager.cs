using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UndyingWill2D.Managers
{
    public class AnimationManager
    {
        //Animation Information Fields
        protected int _numberOfFrames;
        protected int _numberOfColumns;
        protected Microsoft.Xna.Framework.Vector2 _spriteResolution;
        protected int _startColumn;
        //Animation Frame Fields
        protected int _frameCount;
        protected int _currentFrame;
        protected int _animationInterval;
        protected int _rowPosition;
        protected int _columnPosition;
        //Contructors
        public AnimationManager(int numberOfFrames, int numberOfColumns, Microsoft.Xna.Framework.Vector2 spriteResolution, int startColumn) 
        {
            this._numberOfFrames = numberOfFrames;
            this._numberOfColumns = numberOfColumns;
            this._spriteResolution = spriteResolution;
            this._startColumn = startColumn;

            _frameCount = 0;
            _currentFrame = 0;
            _animationInterval = 15;

            _rowPosition = 0;
            _columnPosition = startColumn;
        }
        //Other Methods
        public void ResetAnimation()
        {
            _currentFrame = 0;
            _columnPosition = _startColumn;
            _rowPosition = 0;
        }
        public Microsoft.Xna.Framework.Rectangle GetFrame()
        {
            return new Microsoft.Xna.Framework.Rectangle(_columnPosition * (int)_spriteResolution.X, _rowPosition * (int)_spriteResolution.Y,
                (int)_spriteResolution.X, (int)_spriteResolution.Y);
        }
    }
}
