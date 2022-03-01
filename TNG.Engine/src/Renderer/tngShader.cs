using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine.Renderer {

    internal class tngShader : IDisposable {
        private uint _handle;
        private GL _gl;
        private bool disposedValue;

        public tngShader(GL gl, string vertexPath, string fragmentPath) {
            _gl = gl;
            uint vertexID = LoadShader(ShaderType.VertexShader, vertexPath);
            uint fragmentID = LoadShader(ShaderType.FragmentShader, fragmentPath);
            _handle = _gl.CreateProgram();
            _gl.AttachShader(_handle, vertexID);
            _gl.AttachShader(_handle, fragmentID);
            _gl.LinkProgram(_handle);
            _gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);
            if (status == 0) {
                throw new Exception($"Shaders failed to link with error: {_gl.GetProgramInfoLog(_handle)}");
            }
            _gl.DetachShader(_handle, vertexID);
            _gl.DetachShader(_handle, fragmentID);
            _gl.DeleteShader(vertexID);
            _gl.DeleteShader(fragmentID);
        }

        public void Use() {
            _gl.UseProgram(_handle);
        }

        public void SetUniform(string name, int value) {
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1) {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        public void SetUniform(string name, float value) {
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1) {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        private uint LoadShader(ShaderType type, string path) {
            //To load a single shader we need to:
            //1) Load the shader from a file.
            //2) Create the handle.
            //3) Upload the source to opengl.
            //4) Compile the shader.
            //5) Check for errors.
            string src = File.ReadAllText(path);
            uint handle = _gl.CreateShader(type);
            _gl.ShaderSource(handle, src);
            _gl.CompileShader(handle);
            string infoLog = _gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog)) {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }
            return handle;
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                    _gl.DeleteProgram(_handle);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Shader()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}