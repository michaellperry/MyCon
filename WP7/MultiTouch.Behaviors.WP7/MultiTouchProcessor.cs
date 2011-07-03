using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MultiTouch.Behaviors.WP7
{
    public class MultiTouchProcessor
    {
        private enum TouchMode
        {
            None,
            Pan,
            PanZoomRotate,
        };

        private int _primaryDeviceId = -1;
        private int _secondaryDeviceId = -1;

        private List<ITouchPoint> _mockPoints;
        private TouchPointCollection _points;
        private Point _panStart;
        private Point _multiOneStart;
        private Point _multiTwoStart;

        private readonly TranslateTransform _translation = new TranslateTransform();
        private readonly ScaleTransform _scale = new ScaleTransform();
        private readonly RotateTransform _rotation = new RotateTransform();
        private readonly TransformGroup _transform = new TransformGroup();

        private TouchMode Mode { get; set; }

        public event EventHandler BeginCapture;
        public event EventHandler ReleaseCapture;
        public event EventHandler Delta;

        public MultiTouchProcessor()
        {
            _transform.Children.Add(_rotation);
            _transform.Children.Add(_scale);
            _transform.Children.Add(_translation);
        }

        public Point Offset
        {
            get { return new Point(_translation.X, _translation.Y); }
        }

        public double Scale
        {
            get { return _scale.ScaleX; }
        }

        public double RotationDegrees
        {
            get { return _rotation.Angle; }
        }

        public double MinimumScale { get; set; }

        public double MaximumScale { get; set; }

        public bool IsScaleEnabled { get; set; }

        public bool IsRotateEnabled { get; set; }

        public bool IsTranslateXEnabled { get; set; }

        public bool IsTranslateYEnabled { get; set; }

        public void Process(List<ITouchPoint> points)
        {
            _mockPoints = points;

            UpdateMode();
            Update();
        }

        public void Process(TouchPointCollection points)
        {
            _points = points;

            UpdateMode();
            Update();
        }

        private void UpdateMode()
        {
            var newPrimaryDeviceId = -1;
            var newSecondaryDeviceId = -1;

            if (_primaryDeviceId != -1)
            {
                var point = GetTouchPoint(_primaryDeviceId);
                if (point != null)
                {
                    if (point.Action != TouchAction.Up)
                    {
                        newPrimaryDeviceId = point.TouchDevice.Id;
                    }
                }
                else
                {
                    var mockPoint = GetMockTouchPoint(_primaryDeviceId);
                    if (mockPoint != null && mockPoint.Action != TouchAction.Up)
                    {
                        newPrimaryDeviceId = mockPoint.TouchDevice.Id;
                    }
                }
            }
            else if (_points != null && _points.Count > 0)
            {
                newPrimaryDeviceId = _points[0].TouchDevice.Id;
            }
            else if (_mockPoints.Count > 0)
            {
                newPrimaryDeviceId = _mockPoints[0].TouchDevice.Id;
            }

            if (_secondaryDeviceId != -1)
            {
                var point = GetTouchPoint(_secondaryDeviceId);
                if (point != null)
                {
                    if (point.Action != TouchAction.Up)
                    {
                        newSecondaryDeviceId = point.TouchDevice.Id;
                    }
                }
                else
                {
                    var mockPoint = GetMockTouchPoint(_secondaryDeviceId);
                    if (mockPoint != null && mockPoint.Action != TouchAction.Up)
                    {
                        newSecondaryDeviceId = mockPoint.TouchDevice.Id;
                    }
                }
            }
            else if (_points != null && _points.Count > 1)
            {
                newSecondaryDeviceId = _points[1].TouchDevice.Id;
            }
            else if (_mockPoints != null && _mockPoints.Count > 1)
            {
                newSecondaryDeviceId = _mockPoints[1].TouchDevice.Id;
            }
            
            if (newPrimaryDeviceId == -1 && newSecondaryDeviceId != -1)
            {
                newPrimaryDeviceId = newSecondaryDeviceId;
                newSecondaryDeviceId = -1;
            }

            if (_primaryDeviceId != newPrimaryDeviceId || _secondaryDeviceId != newSecondaryDeviceId)
            {
                EndCurrentAction();

                _primaryDeviceId = newPrimaryDeviceId;
                _secondaryDeviceId = newSecondaryDeviceId;

                if (_primaryDeviceId != -1 
                    && _secondaryDeviceId == -1
                    && (IsTranslateXEnabled
                        || IsTranslateYEnabled))
                {
                    BeginPan();
                }
                else if (_primaryDeviceId != -1 
                        && _secondaryDeviceId != -1)
                {
                    BeginMulti();
                }
            }
        }

        private void EndCurrentAction()
        {
            switch (Mode)
            {
                case TouchMode.Pan:
                    EndPan();
                    break;
                case TouchMode.PanZoomRotate:
                    EndMulti();
                    break;
            }

            Mode = TouchMode.None;
        }

        private void Update()
        {
            switch (Mode)
            {
                case TouchMode.Pan:
                    UpdatePan();
                    break;
                case TouchMode.PanZoomRotate:
                    UpdateMulti();
                    break;
            }
        }

        private void BeginPan()
        {
            Mode = TouchMode.Pan;

            if (BeginCapture != null)
            {
                BeginCapture(this, EventArgs.Empty);
            }

            _panStart = GetTouchPosition(_primaryDeviceId);
        }

        private void UpdatePan()
        {
            var position = GetTouchPosition(_primaryDeviceId);
            Translate(new Point(
                IsTranslateXEnabled ? position.X - _panStart.X : 0,
                IsTranslateYEnabled ? position.Y - _panStart.Y : 0));

            FireDelta(_panStart != position);

            _panStart = position;
        }

        private void EndPan()
        {
            if (ReleaseCapture != null)
            {
                ReleaseCapture(this, EventArgs.Empty);
            }
        }

        private void BeginMulti()
        {
            Mode = TouchMode.PanZoomRotate;

            if (BeginCapture != null)
            {
                BeginCapture(this, EventArgs.Empty);
            }

            _multiOneStart = GetTouchPosition(_primaryDeviceId);
            _multiTwoStart = GetTouchPosition(_secondaryDeviceId);
        }

        private void UpdateMulti()
        {
            var originalDistance = _multiOneStart.DistanceTo(_multiTwoStart);

            var multiOnePos = GetTouchPosition(_primaryDeviceId);
            var multiTwoPos = GetTouchPosition(_secondaryDeviceId);

            var newDistance = multiOnePos.DistanceTo(multiTwoPos);

            double scale = 1;
            if (originalDistance != 0)
            {
                scale = newDistance / originalDistance;
            }

            var angleStart = Math.Atan2(_multiOneStart.Y - _multiTwoStart.Y, _multiOneStart.X - _multiTwoStart.X);
            var newAngle = Math.Atan2(multiOnePos.Y - multiTwoPos.Y, multiOnePos.X - multiTwoPos.X);

            var deltaAngle = newAngle - angleStart;

            if (multiOnePos != multiTwoPos || scale != 1 || deltaAngle != 0)
            {
                Translate(new Point(
                    multiOnePos.X - _multiOneStart.X, 
                    multiOnePos.Y - _multiOneStart.Y));
                Zoom(scale, multiOnePos);
                Rotate(deltaAngle, multiOnePos);

                FireDelta(_multiOneStart != multiOnePos
                    || scale != 1.0
                    || deltaAngle != 0);
            }

            _multiOneStart = multiOnePos;
            _multiTwoStart = multiTwoPos;
        }

        private void EndMulti()
        {
            if (ReleaseCapture != null)
            {
                ReleaseCapture(this, EventArgs.Empty);
            }
        }

        private void Zoom(double zoom, Point offset)
        {
            if (!IsScaleEnabled)
            {
                return;
            }

            var scale = Scale + Scale * (zoom - 1);
            scale = Math.Max(scale, MinimumScale);
            scale = Math.Min(scale, MaximumScale);

            zoom = scale / Scale;

            _scale.ScaleX 
                = _scale.ScaleY 
                = scale;

            _translation.X += offset.X * (1 - zoom) + _translation.X * (zoom - 1);
            _translation.Y += offset.Y * (1 - zoom) + _translation.Y * (zoom - 1);
        }

        private void Translate(Point amount)
        {
            if (IsTranslateXEnabled)
            {
                _translation.X += amount.X;
            }

            if (IsTranslateYEnabled)
            {
                _translation.Y += amount.Y;
            }
        }

        private void Rotate(double radians, Point offset)
        {
            if (!IsRotateEnabled)
            {
                return;
            }

            var degrees = radians / Math.PI * 180;
            _rotation.Angle += degrees;

            var delta = new Point(
                offset.X - _translation.X, 
                offset.Y - _translation.Y);
            var rotated = new Point(
                Math.Cos(radians) * delta.X - Math.Sin(radians) * delta.Y,
                Math.Sin(radians) * delta.X + Math.Cos(radians) * delta.Y);

            _translation.X += delta.X - rotated.X;
            _translation.Y += delta.Y - rotated.Y;
        }

        private TouchPoint GetTouchPoint(int deviceId)
        {
            if (_points == null)
            {
                return null;
            }

            return _points.FirstOrDefault(p => p.TouchDevice.Id == deviceId);
        }

        private ITouchPoint GetMockTouchPoint(int deviceId)
        {
            if (_mockPoints == null)
            {
                return null;
            }

            return _mockPoints.FirstOrDefault(p => p.TouchDevice.Id == deviceId);
        }

        private Point GetTouchPosition(int deviceId)
        {
            if (_points != null)
            {
                return _transform.Transform(
                    _points.FirstOrDefault(p => p.TouchDevice.Id == deviceId).Position);
            }

            if (_mockPoints != null)
            {
                return _transform.Transform(
                    _mockPoints.FirstOrDefault(p => p.TouchDevice.Id == deviceId).Position);
            }

            return new Point();
        }

        private void FireDelta(bool hasMoved)
        {
            if (Delta != null
                && hasMoved)
            {
                Delta(this, EventArgs.Empty);
            }
        }

        internal void Reset()
        {
            _rotation.Angle = 0;
            _scale.ScaleX = 1;
            _scale.ScaleY = 1;
            _translation.X = 0;
            _translation.Y = 0;
            FireDelta(true);
        }

        public void ZoomOut(Size imageSize, Size containerSize)
        {
            double scale = Math.Min(
                GetScaleX(imageSize, containerSize),
                GetScaleY(imageSize, containerSize));

            _rotation.Angle = 0;
            _scale.ScaleX = scale;
            _scale.ScaleY = scale;
            _translation.X = (containerSize.Width - imageSize.Width) / 2.0;
            _translation.Y = (containerSize.Height - imageSize.Height) / 2.0;
            FireDelta(true);
        }

        private static double GetScaleX(Size imageSize, Size containerSize)
        {
            if (imageSize.Width > containerSize.Width)
                return containerSize.Width / imageSize.Width;
            else
                return 1.0;
        }

        private static double GetScaleY(Size imageSize, Size containerSize)
        {
            if (imageSize.Height > containerSize.Height)
                return containerSize.Height / imageSize.Height;
            else
                return 1.0;
        }
    }
}
