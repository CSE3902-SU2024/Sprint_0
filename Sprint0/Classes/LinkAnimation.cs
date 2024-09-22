﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sprint0.Classes
{
    public class LinkAnimation
    {
        private Texture2D _texture;
        private Rectangle[] _sourceRectangles;
        private int _currentFrame;
        private float _frameTime;
        private float _frameTimeCounter;
        private LinkStateMachine _stateMachine;
        private SpriteEffects spriteEffects;

        public LinkAnimation(Rectangle[] sourceRectangles, LinkStateMachine stateMachine, Texture2D texture)
        {
            _sourceRectangles = sourceRectangles;
            _currentFrame = 0;
            _frameTime = 0.2f;
            _frameTimeCounter = 0f;
            _stateMachine = stateMachine;
            _texture = texture;
            spriteEffects = SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameTimeCounter += deltaTime;
            if (_frameTimeCounter >= _frameTime)
            {
                switch (_stateMachine.GetCurrentState())
                {
                    case LinkStateMachine.State.MovingLeft:
                        _currentFrame = 2 + (_currentFrame + 1) % 2; // Left animation (flip horizontally when drawing)
                        break;
                    case LinkStateMachine.State.MovingRight:
                        _currentFrame = 2 + (_currentFrame + 1) % 2; // Right animation
                        break;
                    case LinkStateMachine.State.Idle:
                        switch (_stateMachine.GetPreviousState())
                        {
                            case LinkStateMachine.State.MovingRight:
                                _currentFrame = 2;
                                break;
                            default:
                                break;
                        }
                        break;
                    case LinkStateMachine.State.MovingUp:
                        _currentFrame = 4 + (_currentFrame + 1) % 2; // Up animation
                        break;
                    case LinkStateMachine.State.MovingDown:
                        _currentFrame = (_currentFrame + 1) % 2; // Down animation
                        break;
                    case LinkStateMachine.State.SwordAttackRight:
                        _currentFrame = 8 + (_currentFrame - 8 + 1) % 4;
                        break;
                    case LinkStateMachine.State.SwordAttackLeft:
                        _currentFrame = 8 + (_currentFrame - 8 + 1) % 4;
                        break;
                    case LinkStateMachine.State.SwordAttackUp:
                        _currentFrame = 12 + (_currentFrame - 8 + 1) % 4;
                        break;
                    case LinkStateMachine.State.SwordAttackDown:
                        _currentFrame = 16 + (_currentFrame + 1) % 2; // Down animation
                        break;
                }

                _frameTimeCounter = 0f;
            }


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale)
        {
            if (_stateMachine.GetCurrentState() == LinkStateMachine.State.MovingLeft || 
                _stateMachine.GetPreviousState() == LinkStateMachine.State.MovingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }

            spriteBatch.Draw(_texture, position, _sourceRectangles[_currentFrame], Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0f);
        }

    }

    
}
