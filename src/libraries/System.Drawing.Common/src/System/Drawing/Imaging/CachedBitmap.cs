using System.Runtime.InteropServices;
using Gdip = System.Drawing.SafeNativeMethods.Gdip;

namespace System.Drawing.Imaging
{
    public sealed class CachedBitmap : MarshalByRefObject, IDisposable
    {
        internal IntPtr nativeCachedBitmap;

        public CachedBitmap(Bitmap bitmap, Graphics graphics)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }

            int status = Gdip.GdipCreateCachedBitmap(new HandleRef(bitmap, bitmap.nativeImage),
                new HandleRef(graphics, graphics.NativeGraphics),
                out nativeCachedBitmap);

            Gdip.CheckStatus(status);
        }

        public void Dispose()
        {
            if (nativeCachedBitmap != IntPtr.Zero)
            {
                int status = Gdip.GdipDeleteCachedBitmap(new HandleRef(this, nativeCachedBitmap));
                nativeCachedBitmap = IntPtr.Zero;
                Gdip.CheckStatus(status);
            }
        }
    }
}
