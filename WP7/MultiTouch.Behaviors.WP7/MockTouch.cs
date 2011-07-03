using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MultiTouch.Behaviors.WP7
{
#if DEBUG
    public class MockTouch
    {
        private const int PrimaryId = 10;
        private const int SecondaryId = 12;

        private DateTime _lastAction;
        private UIElement _element;
        private MultiTouchBehavior _behavior;
        private MultiTouchProcessor _processor;
        private bool _attachedToRoot;

        private Point _primaryPoint;
        private Point _primaryPointFromRoot;

        public void Attach(
            UIElement element, 
            MultiTouchBehavior behavior,
            MultiTouchProcessor processor)
        {
            _element = element;
            _element.MouseLeftButtonDown += ElementMouseLeftButtonDown;
            _behavior = behavior;
            _processor = processor;
        }

        void RootVisualMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_mode == Mode.None
                || _mode == Mode.Pin)
            {
                return;
            }

            var list = new List<ITouchPoint>();

            if (_mode == Mode.Pan)
            {
                _primaryPoint = e.GetPosition(_element);
                var primaryTouchPoint = new TouchPointWrapper(
                    _primaryPoint, TouchAction.Up, PrimaryId);
                list.Add(primaryTouchPoint);

                _mode = Mode.None;
            }
            else if (_mode == Mode.Multi)
            {
                var primaryTouchPoint = new TouchPointWrapper(
                    _primaryPoint, TouchAction.Up, PrimaryId);
                list.Add(primaryTouchPoint);

                var secondaryPoint = new TouchPointWrapper(
                    e.GetPosition(_element), TouchAction.Up, SecondaryId);
                list.Add(secondaryPoint);

                _mode = Mode.None;
            }

            _processor.Process(list);
            _behavior.HandleMockDebugInfoAndFingers(list, list[0]);
        }

        void RootVisualMouseMove(object sender, MouseEventArgs e)
        {
            if (_mode == Mode.Pin
                || _mode == Mode.None)
            {
                return;
            }

            var list = new List<ITouchPoint>();
            var listFromRoot = new List<ITouchPoint>();

            if (_mode == Mode.Pan)
            {
                _primaryPoint = e.GetPosition(_element);

                var primaryTouchPoint = new TouchPointWrapper(
                    _primaryPoint, TouchAction.Move, PrimaryId);
                list.Add(primaryTouchPoint);

                _primaryPointFromRoot = e.GetPosition(_behavior.RootVisual);
                var primaryTouchPointFromRoot = new TouchPointWrapper(
                     e.GetPosition(_behavior.RootVisual), TouchAction.Move, PrimaryId);
                listFromRoot.Add(primaryTouchPointFromRoot);
            }
            else if (_mode == Mode.Multi)
            {
                var primaryTouchPoint = new TouchPointWrapper(
                    _primaryPoint, TouchAction.Move, PrimaryId);
                list.Add(primaryTouchPoint);

                var primaryTouchPointFromRoot = new TouchPointWrapper(
                     _primaryPointFromRoot, TouchAction.Move, PrimaryId);
                listFromRoot.Add(primaryTouchPointFromRoot);

                var secondaryPoint = new TouchPointWrapper(
                    e.GetPosition(_element), TouchAction.Move, SecondaryId);
                list.Add(secondaryPoint);

                var secondaryPointFromRoot = new TouchPointWrapper(
                    e.GetPosition(_behavior.RootVisual), TouchAction.Move, SecondaryId);
                listFromRoot.Add(secondaryPointFromRoot);
            }

            _processor.Process(list);
            _behavior.HandleMockDebugInfoAndFingers(listFromRoot, listFromRoot[0]);
        }

        private Mode _mode;

        private enum Mode
        {
            None,
            Pan,
            Multi,
            Pin,
        }

        void ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_attachedToRoot)
            {
                _behavior.RootVisual.MouseLeftButtonUp += RootVisualMouseLeftButtonUp;
                _behavior.RootVisual.MouseMove += RootVisualMouseMove;
                _attachedToRoot = true;
            }

            var list = new List<ITouchPoint>();
            var listFromRoot = new List<ITouchPoint>();

            if (DateTime.Now - _lastAction < TimeSpan.FromMilliseconds(500))
            {
                _mode = Mode.Pin;

                _primaryPoint = e.GetPosition(_element);
                var primaryTouchPoint = new TouchPointWrapper(
                    _primaryPoint, TouchAction.Down, PrimaryId);
                list.Add(primaryTouchPoint);

                _primaryPointFromRoot = e.GetPosition(_behavior.RootVisual);
                var primaryTouchPointFromRoot = new TouchPointWrapper(
                    _primaryPointFromRoot, TouchAction.Down, PrimaryId);
                listFromRoot.Add(primaryTouchPointFromRoot);
            }
            else
            {
                if (_mode == Mode.None)
                {
                    _mode = Mode.Pan;

                    _primaryPoint = e.GetPosition(_element);
                    var primaryTouchPoint = new TouchPointWrapper(
                        _primaryPoint, TouchAction.Down, PrimaryId);
                    list.Add(primaryTouchPoint);

                    _primaryPointFromRoot = e.GetPosition(_behavior.RootVisual);
                    var primaryTouchPointFromRoot = new TouchPointWrapper(
                        _primaryPointFromRoot, TouchAction.Down, PrimaryId);
                    listFromRoot.Add(primaryTouchPointFromRoot);

                    _lastAction = DateTime.Now;
                }
                else if (_mode == Mode.Pin)
                {
                    _mode = Mode.Multi;

                    var primaryTouchPoint = new TouchPointWrapper(
                        _primaryPoint, TouchAction.Down, PrimaryId);
                    list.Add(primaryTouchPoint);

                    var primaryTouchPointFromRoot = new TouchPointWrapper(
                        _primaryPointFromRoot, TouchAction.Down, PrimaryId);
                    listFromRoot.Add(primaryTouchPointFromRoot);

                    var secondaryPoint = new TouchPointWrapper(
                        e.GetPosition(_element), TouchAction.Down, SecondaryId);
                    list.Add(secondaryPoint);

                    var secondaryPointFromRoot = new TouchPointWrapper(
                        e.GetPosition(_behavior.RootVisual), TouchAction.Down, SecondaryId);
                    listFromRoot.Add(secondaryPointFromRoot);
                }
            }

            if (list.Count == 0)
            {
                return;
            }

            _processor.Process(list);
            _behavior.HandleMockDebugInfoAndFingers(listFromRoot, listFromRoot[0]);
        }

        public void Detach(UIElement element)
        {
            _element.MouseLeftButtonDown -= ElementMouseLeftButtonDown;
            _behavior.RootVisual.MouseMove -= RootVisualMouseMove;
            _behavior.RootVisual.MouseLeftButtonUp -= RootVisualMouseLeftButtonUp;
            _element = null;
            _behavior = null;
            _processor = null;
        }
    }
#endif
}
