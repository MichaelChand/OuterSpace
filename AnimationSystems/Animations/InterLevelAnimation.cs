using Chronometers;
using CommonRelay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OuterSpace.AnimationSystems.Animations
{
    public class InterLevelAnimation : IAnimation
    {
        private TextureReader _textureReader;
        private readonly int _maxFrames;
        private AnimationDataBinder _animationDataBinder;
        private GameTimer _gameTimer;
        private bool AnimationUpdatePaused = true;
        private double _viewportWidth;
        private double _viewportHeight;
        protected Image _gate1;

        public InterLevelAnimation(int SystemFrameLimit, double viewportWidth, double viewportHeight)
        {
            _maxFrames = SystemFrameLimit;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _animationDataBinder = new AnimationDataBinder();
            LoadTexture();
            _gameTimer = new GameTimer(_maxFrames, Callback);
            InitialiseBindingVariables();
            SetBinding(); //Added 5/04/2018.  Not sure if I should put this here. Just guessing.
        }

        private void InitialiseBindingVariables()
        {
            _animationDataBinder.X = (_viewportWidth - 800) / 2.0f;
            _animationDataBinder.Y = 600 + (_viewportHeight - 600) / 2.0f;
        }

        private void SetBinding()
        {
            Binding TextureLODBinding = new Binding("_animationDataBinder.CurrentSlide");
            Binding DrawPositionBinding = new Binding("DrawPosition");
            TextureLODBinding.Source = this;
            DrawPositionBinding.Source = this;
            _gate1.SetBinding(Image.SourceProperty, TextureLODBinding);
            _gate1.SetBinding(Image.MarginProperty, DrawPositionBinding);
        }

        private void LoadTexture()
        {
            string texturePath = string.Format("Assets//Images//{0}", "HangerDoor.png");
            _textureReader = new TextureReader();
            _animationDataBinder.CurrentSlide = _textureReader.LoadTextureFromAssemblyPath(texturePath, 800, 600);
        }

        public void Callback(object sender, ElapsedEventArgs eea)
        {
            Update();
        }

        public void Start()
        {
            _gameTimer.Start();
        }

        public void Stop()
        {
            if(_gameTimer.TimerRunning)
                _gameTimer.Stop();
            AnimationUpdatePaused = true;
        }

        public void Pause()
        {
            AnimationUpdatePaused = !AnimationUpdatePaused;
        }

        public UIElement GetUIElement()
        {
            return _gate1;
        }

        public void Update()
        {
            if(!AnimationUpdatePaused)
            {
                //Update animation
            }
        }

        public void DeInitialise()
        {
            _textureReader = null;
        }
    }
}
