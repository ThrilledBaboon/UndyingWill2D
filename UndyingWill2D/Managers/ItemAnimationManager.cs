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
        //Properties
        protected bool IsAttacking { get; set; }
        public Microsoft.Xna.Framework.Rectangle NextFrameRect { get; set; }
        //Contructor
        public ItemAnimationManager(int numberOfFrames, int numberOfColumns, Microsoft.Xna.Framework.Vector2 spriteResolution, int startColumn) : 
        base(numberOfFrames, numberOfColumns, spriteResolution, startColumn)
        { _animationInterval = 15; }
        //Core Methods
        public void Update(bool isAttacking)
        {
            _frameCount+=5;
            IsAttacking = isAttacking;
            if (_frameCount > _animationInterval)
            {
                _frameCount = 0;
                if (_previousFrameIsAttacked == false && IsAttacking)
                {
                    //start attack
                    _previousFrameIsAttacked = true;
                    NewAttackFrame();
                }
                else if (_previousFrameIsAttacked)
                {
                    //continue attack
                    _previousFrameIsAttacked = false;
                    NewAttackFrame();
                }
                else
                {
                    NextFrameRect = Microsoft.Xna.Framework.Rectangle.Empty;
                    return;
                }
                NextFrameRect = GetFrame();
            }
        }
        private void NewAttackFrame()
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
    }
}
