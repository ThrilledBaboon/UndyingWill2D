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
    internal class AnimationManager
    {
        int _numberOfFrames;
        int _numberOfColumns;
        Microsoft.Xna.Framework.Vector2 _spriteResolution;

        int _frameCount;
        int _currentFrame;
        int _animationInterval;

        int _rowPosition;
        int _columnPosition;

        public AnimationManager(int numberOfFrames, int numberOfColumns, Microsoft.Xna.Framework.Vector2 spriteResolution) 
        {
            this._numberOfFrames = numberOfFrames;
            this._numberOfColumns = numberOfColumns;
            this._spriteResolution = spriteResolution;

            _frameCount = 0;
            _currentFrame = 0;
            _animationInterval = 15;

            _rowPosition = 0;
            _columnPosition = 1;
        }
        public void Update()
        {
            _frameCount++;
            if (_frameCount > _animationInterval) 
            {
                _frameCount = 0;
                NewFrame();
            }
        }

        private void NewFrame()
        {
            _currentFrame++;
            _columnPosition++;
            if (_currentFrame >= _numberOfFrames)
            {
                ResetAnimation();
            }
            if (_columnPosition >= _numberOfColumns)
            {
                _columnPosition = 0;
                _rowPosition++;
            }
        }

        private void ResetAnimation()
        {
            _currentFrame = 0;
            _columnPosition = 1;
            _rowPosition = 0;
        }

        public Microsoft.Xna.Framework.Rectangle GetFrame()
        {
            return new Microsoft.Xna.Framework.Rectangle(_columnPosition * (int)_spriteResolution.X, _rowPosition * (int)_spriteResolution.Y,
                (int)_spriteResolution.X, (int)_spriteResolution.Y);
        }

    }
}
