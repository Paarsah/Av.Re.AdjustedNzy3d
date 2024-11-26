using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Mag3DView.Views.Nzy3d.Avalonia.ChartView;


namespace Mag3DView.Views
{
    public class OpenGLCanvasHost : OpenGlControlBase
    {
        private readonly OpenGLCanvas _canvas;

        public OpenGLCanvasHost(OpenGLCanvas canvas)
        {
            _canvas = canvas;
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            _canvas.Render();
        }
    }
}
