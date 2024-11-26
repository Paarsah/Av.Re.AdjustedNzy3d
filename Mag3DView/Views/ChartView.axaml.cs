using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Mag3DView.Nzy3dAPI.Plot3D.Primitives;
using Mag3DView.Nzy3dAPI.Maths;
using Mag3DView.Nzy3dAPI.Chart;
using ChartNamespace = Mag3DView.Nzy3dAPI.Chart;
using Mag3DView.Views.Nzy3d.Avalonia.ChartView;

namespace Mag3DView.Views
{
    public partial class ChartView : UserControl
    {
        private Mag3DView.Nzy3dAPI.Chart.Chart _chart;

        public ChartView()
        {
            InitializeComponent();
            InitializeChart();
            SetupInteractions();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeChart()
        {
            var canvas = new OpenGLCanvas();
            _chart = new Mag3DView.Nzy3dAPI.Chart.Chart(canvas);

            var drawable = new Surface((x, y) => x * x + y * y, gridSize: 100, scale: 1.0f);
            _chart.AddDrawable(drawable);

            var openGlHost = this.FindControl<ContentControl>("OpenGlHost");
            if (openGlHost != null)
            {
                openGlHost.Content = canvas;
            }
        }

        private void SetupInteractions()
        {
            this.PointerPressed += OnPointerPressed;
            this.PointerMoved += OnPointerMoved;
            this.PointerReleased += OnPointerReleased;
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _chart.StartMouseDrag(new Coord3d(e.GetPosition(this).X, e.GetPosition(this).Y, 0));
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                _chart.MouseDrag(new Coord3d(e.GetPosition(this).X, e.GetPosition(this).Y, 0));
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _chart.EndMouseDrag();
        }
    }
}

