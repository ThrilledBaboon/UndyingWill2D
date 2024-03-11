using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UndyingWill2D.Managers;

namespace UndyingWill2D.Controllers
{
    public class MobController : EntityController
    {
        //Property
        public Rectangle SeeAbleArea {  get; set; }
        public Rectangle AttackAbleArea { get; set; }
        //Contructor
        public MobController(Texture2D texture, int scale, Vector2 roomPosition, ContentManager contentManager) : base(texture, scale, roomPosition, contentManager) 
        {
            _moveSpeed = 0.5f;
        }
        public override void Update(int roomHeight, int roomLength)
        {
            _roomHeight = roomHeight;
            _roomLength = roomLength;
            SeeAbleArea = new Rectangle((int)(RoomPosition.X - 2), (int)(RoomPosition.Y - 2), 6, 6);
            AttackAbleArea = new Rectangle((int)(RoomPosition.X - 1), (int)(RoomPosition.Y - 1), 2, 2);
            _heldItem.Update(RoomPosition, _isAttacking, MouseDirection);
        }
        public void MobMovement(Rectangle playerCollider, Vector2 playerPosition)
        {
            if (SeeAbleArea.Contains(playerCollider)) 
            {
                _moveDirection = playerPosition - RoomPosition;
                IsMoving = true;
            }
            else 
            {
                _moveDirection = Vector2.Zero;
                IsMoving = false; 
            }
            WalkAnimationManager.Update(IsMoving);
            if (IsMoving) 
            {
                if (_moveDirection != Vector2.Zero)
                {
                    _moveDirection.Normalize();
                }
                Vector2 moveVelocity = _moveDirection / 14 * _moveSpeed;
                Vector2 currentRoomPosition = RoomPosition;
                Vector2 roomArea = new Vector2(_roomLength, _roomHeight);
                Vector2 positionToMoveTo = currentRoomPosition + moveVelocity;
                bool isMovePossible = (0 <= positionToMoveTo.X &&
                    positionToMoveTo.X <= roomArea.X)
                    && (0 <= positionToMoveTo.Y &&
                    positionToMoveTo.Y <= roomArea.Y);
                if (isMovePossible)
                {
                    RoomPosition += moveVelocity;
                }
            }
        }
    }
}
