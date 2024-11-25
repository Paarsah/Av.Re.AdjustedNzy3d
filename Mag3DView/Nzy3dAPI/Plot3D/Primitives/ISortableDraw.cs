using Mag3DView.Nzy3dAPI.Plot3D.Rendering.View;

namespace Mag3DView.Nzy3dAPI.Plot3D.Primitives
{
    public interface ISortableDraw
	{
		double GetDistance(Camera camera);

		double GetShortestDistance(Camera camera);

		double GetLongestDistance(Camera camera);
	}
}
