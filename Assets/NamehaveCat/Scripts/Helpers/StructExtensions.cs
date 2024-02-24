namespace NamehaveCat.Scripts.Helpers
{
    using System.Runtime.InteropServices;

    public static class StructExtensions
    {
        public static T BytesAsStruct<T>(this byte[] bytes) where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            nint ptr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.Copy(bytes, 0, ptr, size);
                return Marshal.PtrToStructure<T>(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static byte[] StructAsBytes<T>(this T str) where T : struct
        {
            var size = Marshal.SizeOf(str);
            var arr = new byte[size];

            nint ptr = 0;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return arr;
        }
    }
}