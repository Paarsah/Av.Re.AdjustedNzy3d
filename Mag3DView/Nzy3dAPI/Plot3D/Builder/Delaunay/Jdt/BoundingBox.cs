using System;

namespace Mag3DView.Nzy3dAPI.Plot3D.Builder.Delaunay.Jdt
{
	/// <summary>
	/// BoundingBox represents a horizontal bounding rectangle defined by its lower left
	/// and upper right point. This is usually used as a rough approximation of the
	/// bounded geometry
	/// </summary>
	public class BoundingBox
	{
		private double m_minx;
		private double m_maxx;
		private double m_miny;
		private double m_maxy;

		/// <summary>
		/// Creates an empty bounding box
		/// </summary>
		public BoundingBox()
		{
			SetToNull();
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="other">The other boundingbox</param>
		public BoundingBox(BoundingBox other)
		{
			if (other.IsNull)
			{
				SetToNull();
			}
			else
			{
				Init(other.MinX, other.MaxX, other.MinY, other.MaxY);
			}
		}

		/// <summary>
		/// Creates a bounding box given the extent
		/// </summary>
		/// <param name="minx">minimum x coordinate</param>
		/// <param name="maxx">maximum x coordinate</param>
		/// <param name="miny">minimum y coordinate</param>
		/// <param name="maxy">maximum y coordinate</param>
		public BoundingBox(double minx, double maxx, double miny, double maxy)
		{
			Init(minx, maxx, miny, maxy);
		}

		/// <summary>
		/// Create a bounding box between lowerLeft and upperRight
		/// </summary>
		/// <param name="lowerLeft">Lower left point of the box</param>
		/// <param name="upperRight">Upper right point of the box</param>
		public BoundingBox(Point_dt lowerLeft, Point_dt upperRight)
		{
			Init(lowerLeft.X, upperRight.X, lowerLeft.Y, upperRight.Y);
		}

		/// <summary>
		/// Initialize a BoundingBox for a region defined by two points (can be any corner of the region)
		/// </summary>
		/// <param name="x1">The first x-value</param>
		/// <param name="x2">The second x-value</param>
		/// <param name="y1">The first y-value</param>
		/// <param name="y2">The second y-value</param>
		private void Init(double x1, double x2, double y1, double y2)
		{
			if (x1 < x2)
			{
				m_minx = x1;
				m_maxx = x2;
			}
			else
			{
				m_minx = x2;
				m_maxx = x1;
			}

			if (y1 < y2)
			{
				m_miny = y1;
				m_maxy = y2;
			}
			else
			{
				m_miny = y2;
				m_maxy = y1;
			}
		}

		/// <summary>
		/// Makes this BoundingBox a "null" envelope, that is, the envelope
		///  of the empty geometry.
		/// </summary>
		private void SetToNull()
		{
			m_minx = 0;
			m_maxx = -1;
			m_miny = 0;
			m_maxy = -1;
		}

		/// <summary>
		/// true if this BoundingBox is uninitialized
		///      or is the envelope of the empty geometry.
		/// </summary>
		public bool IsNull
		{
			get { return m_maxx < m_minx; }
		}

		/// <summary>
		/// Tests if the other BoundingBox lies wholely inside this BoundingBox
		/// </summary>
		/// <param name="other">The BoundingBox to check</param>
		/// <returns>True if this BoundingBox contains the other BoundingBox, False otherwise</returns>
		public bool Contains(BoundingBox other)
		{
			return (!(this.IsNull || other.IsNull)) && other.MinX >= this.MinX && other.MaxX <= this.MaxX && other.MinY >= this.MinY && other.MaxY <= this.MaxY;
		}

		/// <summary>
		/// Unify the BoundingBoxes of this and the other BoundingBox
		/// </summary>
		/// <param name="other">Another BoundingBox</param>
		/// <returns>The union of the two BoundingBoxes</returns>
		public BoundingBox UnionWith(BoundingBox other)
		{
			if (other.IsNull)
			{
				return new BoundingBox(this);
			}
			else if (this.IsNull)
			{
				return new BoundingBox(other);
			}
			else
			{
				return new BoundingBox(Math.Min(MinX, other.MinX), Math.Max(MaxX, other.MaxX), Math.Min(MinY, other.MinY), Math.Max(MaxY, other.MaxY));
			}
		}

		/// <summary>
		/// Minimum x value of bounding box
		/// </summary>
		/// <returns>Minimum x value of bounding box</returns>
		public double MinX
		{
			get { return m_minx; }
		}

		/// <summary>
		/// Maximum xvalue of bounding box
		/// </summary>
		/// <returns>Maximum x value of bounding box</returns>
		public double MaxX
		{
			get { return m_maxx; }
		}

		/// <summary>
		/// Minimum y value of bounding box
		/// </summary>
		/// <returns>Minimum y value of bounding box</returns>
		public double MinY
		{
			get { return m_miny; }
		}

		/// <summary>
		/// Maximum y value of bounding box
		/// </summary>
		/// <returns>Maximum y value of bounding box</returns>
		public double MaxY
		{
			get { return m_maxy; }
		}

		/// <summary>
		/// Width of bounding box
		/// </summary>
		/// <returns>Width of bounding box</returns>
		public double Width
		{
			get { return MaxX - MinX; }
		}

		/// <summary>
		/// Height of bounding box
		/// </summary>
		/// <returns>Height of bounding box</returns>
		public double Height
		{
			get { return MaxY - MinY; }
		}

		/// <summary>
		/// Minimum coordinate of bounding box
		/// </summary>
		/// <returns>Minimum coordinate of bounding box</returns>
		public Point_dt MinPoint
		{
			get { return new Point_dt(MinX, MinY); }
		}

		/// <summary>
		/// Maximum coordinate of bounding box
		/// </summary>
		/// <returns>Maximum coordinate of bounding box</returns>
		public Point_dt MaxPoint
		{
			get { return new Point_dt(MaxX, MaxY); }
		}
	}
}
