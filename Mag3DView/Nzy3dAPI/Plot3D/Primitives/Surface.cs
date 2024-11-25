using Mag3DView.Nzy3dAPI.Colors;
using Mag3DView.Nzy3dAPI.Plot3D.Primitives;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.View;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Mag3DView.Nzy3dAPI.Plot3D.Primitives
{
    public class Surface : AbstractDrawable
    {
        private List<Vector3> vertices;
        private List<int> indices;
        private int vertexArrayObject;
        private int vertexBufferObject;
        private int indexBufferObject;

        // Properties mimicking the ones expected
        public bool FaceDisplayed { get; set; } = true;
        public bool WireframeDisplayed { get; set; } = true;
        public Color WireframeColor { get; set; } = new Color(1, 1, 1, 1); // Default to white
        public ColorMapper ColorMapper { get; set; }
        public Bounds Bounds { get; private set; }

        // Constructor to create the surface based on the provided mathematical function
        public Surface(Func<float, float, float> surfaceFunction, int gridSize, float scale = 1f)
        {
            vertices = new List<Vector3>();
            indices = new List<int>();

            // Create the grid of vertices (points)
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    float x = i * scale;
                    float y = j * scale;
                    float z = surfaceFunction(x, y); // Calculate Z value from the function
                    vertices.Add(new Vector3(x, y, z));
                }
            }

            // Generate indices to connect the vertices into triangles
            for (int i = 0; i < gridSize - 1; i++)
            {
                for (int j = 0; j < gridSize - 1; j++)
                {
                    int topLeft = i * gridSize + j;
                    int topRight = topLeft + 1;
                    int bottomLeft = (i + 1) * gridSize + j;
                    int bottomRight = bottomLeft + 1;

                    // First triangle (top-left, bottom-left, top-right)
                    indices.Add(topLeft);
                    indices.Add(bottomLeft);
                    indices.Add(topRight);

                    // Second triangle (top-right, bottom-left, bottom-right)
                    indices.Add(topRight);
                    indices.Add(bottomLeft);
                    indices.Add(bottomRight);
                }
            }

            // Initialize OpenGL buffers for rendering
            InitializeBuffers();

            // Calculate Bounds after vertices are populated
            CalculateBounds();
        }

        // Initialize OpenGL buffers for vertices and indices
        private void InitializeBuffers()
        {
            vertexArrayObject = GL.GenVertexArray();
            vertexBufferObject = GL.GenBuffer();
            indexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(vertexArrayObject);

            // Set up vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (nint)(vertices.Count * Vector3.SizeInBytes), vertices.ToArray(), BufferUsageHint.StaticDraw);

            // Set up index buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (nint)(indices.Count * sizeof(int)), indices.ToArray(), BufferUsageHint.StaticDraw);

            // Enable vertex attribute
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);
        }

        // Implement the abstract Draw method from AbstractDrawable
        public override void Draw(Camera cam)
        {
            // You can implement any specific drawing behavior based on the camera's view here if needed.
            // For now, we'll call the Render method as part of the drawing logic.
            Render();
        }

        // Render the surface using OpenGL
        public void Render()
        {
            GL.BindVertexArray(vertexArrayObject);

            if (FaceDisplayed)
            {
                GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, nint.Zero);
            }

            if (WireframeDisplayed)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, nint.Zero);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill); // Reset to fill mode
            }

            GL.BindVertexArray(0);
        }

        // Calculate the bounds of the surface (min/max Z values)
        private void CalculateBounds()
        {
            float minZ = float.MaxValue;
            float maxZ = float.MinValue;

            foreach (var vertex in vertices)
            {
                if (vertex.Z < minZ) minZ = vertex.Z;
                if (vertex.Z > maxZ) maxZ = vertex.Z;
            }

            Bounds = new Bounds(minZ, maxZ);
        }
    }

    // Helper classes
    public class Bounds
    {
        public float ZMin { get; }
        public float ZMax { get; }

        public Bounds(float zMin, float zMax)
        {
            ZMin = zMin;
            ZMax = zMax;
        }
    }
}
