using System.Windows.Interactivity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FacetedWorlds.MyCon.Presentation
{
    public class ButtonCommandBehavior : TriggerAction<DependencyObject>
    {
        public void Invoke()
        {
            Invoke(null);
        }

        protected override void Invoke(object parameter)
        {
            if (AssociatedElementIsDisabled())
            {
                return;
            }

            var command = GetCommand();

            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private static void OnCommandChanged(
            ButtonCommandBehavior element,
            DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= element.OnCommandCanExecuteChanged;
            }

            var command = (ICommand)e.NewValue;

            if (command != null)
            {
                command.CanExecuteChanged += element.OnCommandCanExecuteChanged;
            }

            element.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {
            var element = GetAssociatedObject();
            return AssociatedObject == null
                || (element != null
                   && !element.IsEnabled);
        }

        private void EnableDisableElement()
        {
            var element = GetAssociatedObject();

            if (element == null)
            {
                return;
            }

            var command = this.GetCommand();

            if (command != null)
            {
                element.IsEnabled = command.CanExecute(null);
            }
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            EnableDisableElement();
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(Binding),
            typeof(ButtonCommandBehavior),
            new PropertyMetadata(
                null,
                (s, e) =>
                {
                    var sender = s as ButtonCommandBehavior;
                    if (sender != null)
                    {
                        sender._listenerCommand.Binding = e.NewValue as Binding;
                    }
                }));

        private readonly BindingListener _listenerCommand;

        public ButtonCommandBehavior()
        {
            _listenerCommand = new BindingListener(
                this,
                (s, e) =>
                {
                    var sender = s as BindingListener;
                    if (sender != null)
                    {
                        OnCommandChanged(
                            sender.Context as ButtonCommandBehavior,
                            e.EventArgs);
                    }
                });
        }

        public Binding Command
        {
            get
            {
                return (Binding) GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            _listenerCommand.Element = this.AssociatedObject;

            EnableDisableElement();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _listenerCommand.Element = null;
        }

        private static void CheckEnableDisable(BindingListener listener)
        {
            if (listener == null)
            {
                return;
            }

            var thisObject = listener.Context as ButtonCommandBehavior;
            if (thisObject != null)
            {
                thisObject.EnableDisableElement();
            }
        }

        private Control GetAssociatedObject()
        {
            return AssociatedObject as Control;
        }

        private ICommand GetCommand()
        {
            return _listenerCommand.Value as ICommand;
        }
    }
}