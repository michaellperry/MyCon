using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace FacetedWorlds.MyCon.Presentation
{
    public class BindingListener
    {
        public delegate void ChangedHandler(object sender, BindingChangedEventArgs e);

        private static readonly List<DependencyPropertyListener> freeListeners = new List<DependencyPropertyListener>();

        private readonly ChangedHandler changedHandler;

        private Binding binding;

        private DependencyPropertyListener listener;

        private DependencyObject target;

        private object value;

        public object Context
        {
            get;
            private set;
        }

        public BindingListener(object context, ChangedHandler changedHandler)
        {
            Context = context;
            this.changedHandler = changedHandler;
        }

        public BindingListener()
        {
        }

        public Binding Binding
        {
            get
            {
                return this.binding;
            }
            set
            {
                this.binding = value;
                this.Attach();
            }
        }

        public DependencyObject Element
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
                this.Attach();
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.listener != null)
                {
                    this.listener.SetValue(value);
                }
            }
        }

        private void Attach()
        {
            this.Detach();

            if (this.target != null
                && this.binding != null)
            {
                this.listener = this.GetListener();
                this.listener.Attach(target, binding);
            }
        }

        private void Detach()
        {
            if (this.listener != null)
            {
                this.ReturnListener();
            }
        }

        private DependencyPropertyListener GetListener()
        {
            DependencyPropertyListener listener;

            if (freeListeners.Count != 0)
            {
                listener = freeListeners[freeListeners.Count - 1];
                freeListeners.RemoveAt(freeListeners.Count - 1);

                return listener;
            }
            else
            {
                listener = new DependencyPropertyListener();
            }

            listener.Changed += this.HandleValueChanged;

            return listener;
        }

        private void HandleValueChanged(object sender, BindingChangedEventArgs e)
        {
            this.value = e.EventArgs.NewValue;

            if (this.changedHandler != null)
            {
                this.changedHandler(this, e);
            }
        }

        private void ReturnListener()
        {
            this.listener.Changed -= this.HandleValueChanged;
            this.listener.Detach();

            freeListeners.Add(this.listener);

            this.listener = null;
        }

        private class DependencyPropertyListener
        {
            private readonly DependencyProperty property;

            private static int index;

            private DependencyObject target;

            public DependencyPropertyListener()
            {
                this.property = DependencyProperty.RegisterAttached("DependencyPropertyListener" + index++,
                                                                    typeof(object),
                                                                    typeof(DependencyPropertyListener),
                                                                    new PropertyMetadata(null, this.HandleValueChanged));
            }

            public event EventHandler<BindingChangedEventArgs> Changed;

            public void Attach(DependencyObject element, Binding binding)
            {
                if (this.target != null)
                {
                    throw new Exception("Cannot attach an already attached listener");
                }

                this.target = element;

                BindingOperations.SetBinding(target, this.property, binding);
            }

            public void Detach()
            {
                this.target.ClearValue(this.property);
                this.target = null;
            }

            public void SetValue(object value)
            {
                this.target.SetValue(this.property, value);
            }

            private void HandleValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
            {
                if (this.Changed != null)
                {
                    this.Changed(this, new BindingChangedEventArgs(e));
                }
            }
        }
    }

    public class BindingChangedEventArgs : EventArgs
    {
        public BindingChangedEventArgs(DependencyPropertyChangedEventArgs e)
        {
            this.EventArgs = e;
        }

        public DependencyPropertyChangedEventArgs EventArgs
        {
            get;
            private set;
        }
    }
}