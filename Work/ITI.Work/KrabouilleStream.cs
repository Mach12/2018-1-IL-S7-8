using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITI.Work
{
    public enum KrabouilleMode
    {
        /// <summary>
        /// Used to write krabouilled data to the inner stream.
        /// </summary>
        Krabouille,

        /// <summary>
        /// Used to read krabouilled data from inner stream.
        /// </summary>
        Unkrabouille
    }

    public class KrabouilleStream : Stream
    {
        readonly Stream _inner;
        readonly KrabouilleMode _mode;
        long _position;
        readonly byte[] _workingBuffer;
        readonly byte[] _secretKey;

        public KrabouilleStream( Stream inner, KrabouilleMode mode, string password )
        {
            if( inner == null ) throw new ArgumentNullException( nameof( inner ) );
            if( String.IsNullOrEmpty( password ) ) throw new ArgumentNullException( nameof( password ) );
            if( !inner.CanWrite && mode == KrabouilleMode.Krabouille )
            {
                throw new ArgumentException( "inner must be writable for Krabouille mode.", nameof( inner ) );
            }
            if( !inner.CanRead && mode == KrabouilleMode.Unkrabouille )
            {
                throw new ArgumentException( "inner must be readable for Unkrabouille mode.", nameof( inner ) );
            }
            if( _mode == KrabouilleMode.Krabouille ) _workingBuffer = new byte[256];
            _secretKey = Encoding.UTF8.GetBytes( password );
            _inner = inner;
            _mode = mode;
        }

        public override bool CanRead => _mode == KrabouilleMode.Unkrabouille;

        public override bool CanSeek => false;

        public override bool CanWrite => _mode == KrabouilleMode.Krabouille;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override void Flush() => _inner.Flush();

        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotSupportedException();
        }

        public override void SetLength( long value )
        {
            throw new NotSupportedException();
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            if( !CanRead ) throw new InvalidOperationException();

            int nbRead = _inner.Read( buffer, offset, count );
            for( int i = 0; i < nbRead; ++i )
            {
                buffer[offset+i] = (byte)(buffer[offset + i] + 1);
            }
            return nbRead;
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            if( !CanWrite ) throw new InvalidOperationException();

            while( count > 0 )
            {
                if( count < _workingBuffer.Length )
                {
                    Array.Copy( buffer, offset, _workingBuffer, 0, count );
                    WriteWorkingBuffer( count );
                    count = 0;
                }
                else
                {
                    Array.Copy( buffer, offset, _workingBuffer, 0, _workingBuffer.Length );
                    WriteWorkingBuffer( _workingBuffer.Length );
                    offset += _workingBuffer.Length;
                    count -= _workingBuffer.Length;
                }
            }

        }

        void WriteWorkingBuffer( int count )
        {
            for( int i = 0; i < count; ++i )
            {
                _workingBuffer[i] = (byte)(_workingBuffer[i] - 1);
            }
            _inner.Write( _workingBuffer, 0, count );
        }
    }
}
