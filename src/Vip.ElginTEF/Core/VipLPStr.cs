using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vip.ElginTEF.Core
{
    public class VipLPStr : ICustomMarshaler
    {
        #region Fields

        private static VipLPStr marshaler;

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

            var buffer = new byte[len];
            Marshal.Copy(pNativeData, buffer, 0, len);

            return Encoding.UTF8.GetString(buffer);
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