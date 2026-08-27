using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vip.ElginTEF.Core
{
    public class VipLPStr : ICustomMarshaler
    {
        #region Fields

        private static VipLPStr marshaler;

        [ThreadStatic] private static byte[] _threadBuffer;

        #endregion Fields

        #region Methods

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return marshaler ??= new VipLPStr();
        }

        /// <inheritdoc />
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
                return null;

            var len = 0;
            while (Marshal.ReadByte(pNativeData, len) != 0)
                len++;

            if (len == 0)
                return string.Empty;

            // reusa buffer por thread para evitar byte[] por retorno nativo (5-15x por transação)
            var buffer = _threadBuffer;
            if (buffer == null || buffer.Length < len)
            {
                buffer = new byte[Math.Max(len, 1024)];
                _threadBuffer = buffer;
            }

            Marshal.Copy(pNativeData, buffer, 0, len);
            return Encoding.UTF8.GetString(buffer, 0, len);
        }

        /// <inheritdoc />
        public void CleanUpNativeData(IntPtr pNativeData) { }

        /// <inheritdoc />
        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            return IntPtr.Zero;
        }

        /// <inheritdoc />
        public void CleanUpManagedData(object ManagedObj) { }

        /// <inheritdoc />
        public int GetNativeDataSize()
        {
            return IntPtr.Size;
        }

        #endregion Methods
    }
}