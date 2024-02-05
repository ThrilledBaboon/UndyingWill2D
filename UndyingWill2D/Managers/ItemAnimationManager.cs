using System;
using System.Collections.Generic;
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
        protected bool IsAttacking { get; set; }
        public ItemAnimationManager(int numberOfFrames, int numberOfColumns, Microsoft.Xna.Framework.Vector2 spriteResolution, int startColumn) : base(numberOfFrames, numberOfColumns, spriteResolution, startColumn)
        { }
        public void Update(bool isAttacking)
        {
            IsAttacking = isAttacking;
            _frameCount++;
            if (_frameCount > _animationInterval)
            {
                _frameCount = 0;
                NewAttackFrame();
            }
        }
        public Microsoft.Xna.Framework.Rectangle Attack()
        {
            NewAttackFrame();
            Microsoft.Xna.Framework.Rectangle frameRect = GetFrame();
            return frameRect;
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
