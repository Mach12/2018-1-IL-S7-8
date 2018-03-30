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
        public KrabouilleStream( Stream inner, KrabouilleMode mode, string password )
        {
            if( String.IsNullOrEmpty( password ) ) throw new ArgumentException();

            _inner = inner ?? throw new ArgumentNullException();
            _mode = mode;
            _password = password;
        }

        private Stream _inner;
        private KrabouilleMode _mode;
        private string _password;

        public override bool CanRead => _mode == KrabouilleMode.Unkrabouille && _inner.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => _mode == KrabouilleMode.Krabouille && _inner.CanWrite;

        public override long Length => _inner.Length;

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        private byte PasswordAt( int index )
        {
            return (byte)_password[ index % _password.Length ];
        }

        public override void Flush()
        {
            _inner.Flush();
        }

        public override int Read( byte[] buffer, int offset, int count )
        {
            int actualCount = _inner.Read( buffer, offset, count );

            for( int i = 0; i < actualCount; ++i )
            {
                byte keyByte = PasswordAt( offset + i );
                buffer[ i ] ^= keyByte;
            }

            return actualCount;
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotSupportedException();
        }

        public override void SetLength( long value )
        {
            _inner.SetLength( value );
        }

        public override void Write( byte[] buffer, int offset, int count )
        {
            byte[] newBuf = new byte[count];
            Array.Copy( buffer, newBuf, count );

            for( int i = 0; i < count; ++i )
            {
                byte keyByte = PasswordAt( offset + i );
                newBuf[ i ] = (byte)(keyByte^buffer[i]);
            }

            _inner.Write( buffer, offset, count );
        }
    }
}
