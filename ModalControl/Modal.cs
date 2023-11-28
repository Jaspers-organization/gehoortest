using System.Windows;
using System.Windows.Controls;

namespace ModalControl;

public class Modal : ContentControl
{
    //Using a DependencyProperty as the backing store for IsOpen.This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register("IsOpen", typeof(bool), typeof(Modal), new PropertyMetadata(false));


    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    static Modal()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(typeof(Modal)));
    }
}