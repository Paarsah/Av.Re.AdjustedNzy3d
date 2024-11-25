using Mag3DView.Nzy3dAPI.Chart;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.Canvas;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.View;

namespace Mag3DView.Nzy3dAPI.Factories
{
	public class ViewFactory
	{
		public static View GetInstance(Scene scene, ICanvas canvas, Quality quality)
		{
			return new ChartView(scene, canvas, quality);
		}
	}
}
