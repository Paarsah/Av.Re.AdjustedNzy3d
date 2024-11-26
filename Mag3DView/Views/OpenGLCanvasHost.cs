using Avalonia.Controls;
using Avalonia.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;

namespace Mag3DView.Views
{
    public class OpenGLCanvasHost : Control
    {
        private AvaloniaTkContext glContext; // Context to manage OpenGL interface
        private GlInterface glInterface; // Direct interface to interact with OpenGL

        public OpenGLCanvasHost()
        {
            this.Loaded += (sender, e) => InitializeOpenGL();
        }

        private void InitializeOpenGL()
        {
            GlProfileType profile = GlProfileType.OpenGL; // Use OpenGL profile
            GlVersion glVersion = new GlVersion(profile, 4, 5); // OpenGL version 4.5

            glInterface = new GlInterface(glVersion, proc => IntPtr.Zero); // Replace with your actual ProcAddress lookup
            glContext = new AvaloniaTkContext(glInterface);

            // Load OpenGL bindings
            GL.LoadBindings(glContext);

            // Query OpenGL version to confirm
            string version = GL.GetString(StringName.Version);
            Console.WriteLine("OpenGL Version: " + version);
        }

    }
}
