using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FR
{
    public class PinchPanImage : FFImageLoading.Forms.CachedImage
    {
        double _currentScale;
        double _startPinchScale;
        double _lastTransX;
        double _lastTransY;

        /// <summary>
        /// constructor
        /// </summary>
        public PinchPanImage()
        {
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OnTapped;
            tapGesture.NumberOfTapsRequired = 1;
            GestureRecognizers.Add(tapGesture);

            ResetView();
        }

        /// <summary>
        /// resets the picture to its origin scale and position
        /// </summary>
        void ResetView()
        {
            TranslationY = 0;
            TranslationX = 0;
            Scale = 1;
            AnchorX = 0.5;
            AnchorY = 0.5;
            _currentScale = 1;
            _lastTransX = 0;
            _lastTransY = 0;
        }

        /// <summary>
        /// gets triggered before the new picture gets drawn
        /// </summary>
        /// <returns>The measure.</returns>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            ResetView();
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        /// <summary>
        /// user tapped to reset view
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnTapped(object sender, EventArgs e)
        {
            ResetView();
        }

        /// <summary>
        /// moves the picture on pan gesture
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    this.TranslationX =
                        Math.Max(Math.Min(0, _lastTransX + e.TotalX), -Math.Abs(this.Width * _currentScale - this.Width));
                    this.TranslationY =
                               Math.Max(Math.Min(0, _lastTransY + e.TotalY), -Math.Abs(this.Height * _currentScale - this.Height));
                    Debug.WriteLine("TransX={0}, transY={1}", this.TranslationX, this.TranslationY);
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    _lastTransX = this.TranslationX;
                    _lastTransY = this.TranslationY;
                    break;
            }
        }

        /// <summary>
        /// pinche zooms the picture on gesture
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                _startPinchScale = this.Scale;
                this.AnchorX = 0;
                this.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                _currentScale += (e.Scale - 1) * _startPinchScale;
                _currentScale = Math.Max(1, _currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = this.X + _lastTransX;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (this.Width * _startPinchScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = this.Y + _lastTransY;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (this.Height * _startPinchScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = _lastTransX - (originX * this.Width) * (_currentScale - _startPinchScale);
                double targetY = _lastTransY - (originY * this.Height) * (_currentScale - _startPinchScale);

                // Apply translation based on the change in origin.
                this.TranslationX = targetX.Clamp(-this.Width * (_currentScale - 1), 0);
                this.TranslationY = targetY.Clamp(-this.Height * (_currentScale - 1), 0);

                // Apply scale factor.
                this.Scale = _currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                _lastTransX = this.TranslationX;
                _lastTransY = this.TranslationY;
            }
        }
    }
}
