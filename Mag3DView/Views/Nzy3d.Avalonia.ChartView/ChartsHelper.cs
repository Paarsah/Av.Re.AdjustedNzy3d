using Mag3DView.Nzy3dAPI.Plot3D.Primitives;
using Mag3DView.Nzy3dAPI.Chart;
using Mag3DView.Nzy3dAPI.Colors.ColorMaps;
using Mag3DView.Nzy3dAPI.Maths;
using Mag3DView.Nzy3dAPI.Plot3D.Builder.Concrete;
using Mag3DView.Nzy3dAPI.Plot3D.Primitives.Axes.Layout.Renderers;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.Canvas;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.View.Modes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Color = Mag3DView.Nzy3dAPI.Colors.Color;
using ColorMapper = Mag3DView.Nzy3dAPI.Colors.ColorMapper;
using Mag3DView.Nzy3d.Avalonia;

namespace Mag3DView.Views.Nzy3d.Avalonia.ChartView
{

    internal static class ChartsHelper
    {
        public static Chart GetIssue16(Renderer3D renderer3D)
        {
            var chart = InitializeChart(renderer3D);

            var data = new List<Coord3d>
            {
                new Coord3d(-4.000000, -4.000000, -0.586176),
                new Coord3d(-2.000000, -4.000000, -0.971278),
                new Coord3d(0.000000, -4.000000, -0.756803),
                // More points here...
            };

            // Assuming BuildDelaunay returns a Surface object, use Surface
            var surface = new Surface((x, y) =>
            {
                // Custom function to create surface, e.g. based on your data
                return 0f;  // Just an example, use your surface function here
            }, 10); // Example grid size

            CustomizeSurface(surface);

            chart.Scene.Graph.Add(surface);

            return chart;
        }

        public static Chart GetMapperSurface(Renderer3D renderer3D)
        {
            var chart = InitializeChart(renderer3D);

            var range = new Nzy3dAPI.Maths.Range(-150, 150);
            const int steps = 50;

            // Assuming BuildDelaunay returns a Surface object, use Surface
            var surface = new Surface((x, y) =>
            {
                // Custom function to create surface, e.g. based on your data
                return 0f;  // Just an example, use your surface function here
            }, 10); // Example grid size
            CustomizeSurface(surface);

            chart.Scene.Graph.Add(surface);

            return chart;
        }

        public static Chart GetFRB_H15_dec_2021(Renderer3D renderer3D, string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
                throw new FileNotFoundException($"The file '{csvFilePath}' does not exist.");

            var labels = new TickLabelMap();
            var coords = new List<Coord3d>();
            var isHeader = true;

            foreach (var line in File.ReadLines(csvFilePath))
            {
                var data = line.Split(',');

                if (isHeader)
                {
                    // Add maturity labels (e.g., "1-month", "3-month") for the X-axis
                    for (int i = 1; i < data.Length; i++)
                    {
                        labels.Register(i, data[i]);
                    }
                    isHeader = false;
                }
                else
                {
                    try
                    {
                        // Parse the date and interest rate values
                        var date = DateTime.ParseExact(data[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var dateL = date.ToOADate(); // Convert date to numeric for Y-axis

                        for (int i = 1; i < data.Length; i++)
                        {
                            if (double.TryParse(data[i], out double rate))
                            {
                                coords.Add(new Coord3d(i, dateL, rate)); // X: maturity index, Y: date, Z: rate
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing line '{line}': {ex.Message}");
                    }
                }
            }

            var chart = InitializeChart(renderer3D);

            // Configure axis labels
            chart.AxeLayout.YTickRenderer = new DateTickRenderer("dd/MM/yyyy"); // Format Y-axis as dates
            chart.AxeLayout.YAxeLabel = "Date";
            chart.AxeLayout.XTickRenderer = labels; // Use maturity labels on the X-axis
            chart.AxeLayout.XAxeLabel = "Maturity";
            chart.AxeLayout.ZAxeLabel = "Rate (%)";

            // Build surface and add to the chart
            // Assuming BuildDelaunay returns a Surface object, use Surface
            var surface = new Surface((x, y) =>
            {
                // Custom function to create surface, e.g. based on your data
                return 0f;  // Just an example, use your surface function here
            }, 10); // Example grid size

            CustomizeSurface(surface);

            chart.Scene.Graph.Add(surface);

            return chart;
        }


        private static Chart InitializeChart(Renderer3D renderer3D)
        {
            var chart = new Chart(renderer3D, Quality.Nicest)
            {
                View =
                {
                    Maximized = false,
                    CameraMode = CameraMode.PERSPECTIVE,
                    IncludingTextLabels = true
                }
            };
            return chart;
        }

        private static void CustomizeSurface(Surface surface)
        {
            surface.ColorMapper = new ColorMapper(new ColorMapRainbow(), surface.Bounds.ZMin, surface.Bounds.ZMax, new Color(1, 1, 1, 0.8));
            surface.FaceDisplayed = true;
            surface.WireframeDisplayed = true;
            surface.WireframeColor = Color.CYAN;
            surface.WireframeColor.Mul(new Color(1, 1, 1, 0.5));
        }
    }

}