using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UndyingWill2D.Managers
{
    public class ItemAnimationManager : AnimationManager
    {
        bool _previousFrameIsAttacked = false;
        int _attackCounter = 0;
        //Properties
        public bool IsAttacking { get; set; }
        public Microsoft.Xna.Framework.Rectangle NextFrameRect { get; set; }
        //Contructor
        public ItemAnimationManager(int numberOfFrames, int numberOfColumns, Microsoft.Xna.Framework.Vector2 spriteResolution, int startColumn) : 
        base(numberOfFrames, numberOfColumns, spriteResolution, startColumn)
        { _animationInterval = 15; }
        //Core Methods
        public void Update()
        {
            _frameCount++;
            if ((_frameCount > _animationInterval) || 
                (_previousFrameIsAttacked == false && IsAttacking))
            {
                _frameCount = 0;
                _currentFrame++;
                if (_currentFrame <= _numberOfFrames)
                {
                    if (_previousFrameIsAttacked == false && IsAttacking)
                    {
                        _attackCounter += 1;
                        _previousFrameIsAttacked = true;
                        NewAttackFrame();
                    }
                    else if (_previousFrameIsAttacked)
                    {
                        _previousFrameIsAttacked = false;
                        NewAttackFrame();
                    }
                }
                else
                {
                    _currentFrame = 0;
                    ResetAnimation();
                }
                NextFrameRect = GetFrame();
            }
        }
        private void NewAttackFrame()
        {
            _columnPosition++;
            if (_columnPosition >= _numberOfColumns)
            {
                _columnPosition = 0;
                _rowPosition++;
            }
        }
    }
}
