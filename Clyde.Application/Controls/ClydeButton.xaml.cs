using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Clyde.App.Controls
{
    /// <summary>
    /// Interaktionslogik für ClydeButton.xaml
    /// </summary>
    public partial class ClydeButton : Button
    {
        public ClydeButton()
        {
            InitializeComponent();
        }

        #region Properties

        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public Brush OuterEllipseColor
        {
            get => (Brush)GetValue(OuterEllipseColorProperty);
            set => SetValue(OuterEllipseColorProperty, value);
        }

        public Brush InnerEllipseColor
        {
            get => (Brush)GetValue(InnerEllipseColorProperty);
            set => SetValue(InnerEllipseColorProperty, value);
        }

        #endregion

        #region DependencyProperties

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(ClydeButton),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty OuterEllipseColorProperty =
            DependencyProperty.Register(nameof(OuterEllipseColor), typeof(Brush), typeof(ClydeButton),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty InnerEllipseColorProperty =
            DependencyProperty.Register(nameof(InnerEllipseColor), typeof(Brush), typeof(ClydeButton),
                new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            base.OnMouseLeave(e);
        }
    }
}
