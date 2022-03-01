using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG.Engine.Renderer {

    internal class BufferObject<TDataType> : IDisposable where TDataType : unmanaged {
        private uint _handle;
        private BufferTargetARB _bufferType;
        private GL _gl;
        private bool disposedValue;

        public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType) {
            _gl = gl;
            _bufferType = bufferType;
            _handle = _gl.GenBuffer();
            Bind();
            fixed (void* d = data) {
                _gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
            }
        }

        public void Bind() {
            _gl.BindBuffer(_bufferType, _handle);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                    _gl.DeleteBuffer(_handle);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BufferObjects()
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