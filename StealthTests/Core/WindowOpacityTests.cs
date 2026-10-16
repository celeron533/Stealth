using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Stealth.Core;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Stealth.Core.Tests
{
    [TestClass]
    public class WindowOpacityTests
    {
        private const int LayeredStyle = (int)WINDOW_EX_STYLE.WS_EX_LAYERED;
        private const uint AlphaFlag = (uint)LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_ALPHA;
        private const uint ColorKeyFlag = (uint)LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_COLORKEY;

        [TestMethod]
        public void TrySetOpacity_EnablesLayeringForOrdinaryWindow()
        {
            var api = new FakeOpacityApi();
            var writer = new WindowOpacityWriter(api);

            var succeeded = writer.TrySetOpacity(new IntPtr(42), 128, out var error);

            Assert.IsTrue(succeeded, error);
            Assert.IsNull(error);
            Assert.AreEqual(LayeredStyle, api.Style);
            Assert.AreEqual((byte)128, api.AppliedAlpha);
            Assert.AreEqual(AlphaFlag, api.AppliedFlags);
        }

        [TestMethod]
        public void TrySetOpacity_PreservesColorKeyOnLayeredWindow()
        {
            var api = new FakeOpacityApi
            {
                Style = LayeredStyle,
                CanReadAttributes = true,
                ExistingFlags = ColorKeyFlag
            };
            var writer = new WindowOpacityWriter(api);

            var succeeded = writer.TrySetOpacity(new IntPtr(42), 128, out var error);

            Assert.IsTrue(succeeded, error);
            Assert.AreEqual(AlphaFlag | ColorKeyFlag, api.AppliedFlags);
        }

        [TestMethod]
        public void TrySetOpacity_RejectsLayeredWindowWithUnreadableAttributes()
        {
            var api = new FakeOpacityApi { Style = LayeredStyle };
            var writer = new WindowOpacityWriter(api);

            var succeeded = writer.TrySetOpacity(new IntPtr(42), 128, out var error);

            Assert.IsFalse(succeeded);
            StringAssert.Contains(error, "per-pixel transparency");
            Assert.IsFalse(api.SetAttributesCalled);
        }

        [TestMethod]
        public void TrySetOpacity_RollsBackStyleAndCanRetryAfterFailure()
        {
            var api = new FakeOpacityApi { SetAttributesSucceeds = false };
            var writer = new WindowOpacityWriter(api);

            var firstAttempt = writer.TrySetOpacity(new IntPtr(42), 128, out var firstError);

            Assert.IsFalse(firstAttempt);
            Assert.IsNotNull(firstError);
            Assert.AreEqual(0, api.Style);

            api.SetAttributesSucceeds = true;
            var retry = writer.TrySetOpacity(new IntPtr(42), 128, out var retryError);

            Assert.IsTrue(retry, retryError);
            Assert.AreEqual(LayeredStyle, api.Style);
            Assert.AreEqual(AlphaFlag, api.AppliedFlags);
        }

        private sealed class FakeOpacityApi : IWindowOpacityApi
        {
            public int Style { get; set; }
            public bool CanReadAttributes { get; set; }
            public uint ExistingFlags { get; set; }
            public bool SetAttributesSucceeds { get; set; } = true;
            public bool SetAttributesCalled { get; private set; }
            public byte AppliedAlpha { get; private set; }
            public uint AppliedFlags { get; private set; }

            public int GetExtendedStyle(IntPtr handle)
            {
                return Style;
            }

            public bool TryGetLayeredAttributes(IntPtr handle, out uint colorKey, out byte alpha, out uint flags, out int errorCode)
            {
                colorKey = 0;
                alpha = byte.MaxValue;
                flags = ExistingFlags;
                errorCode = CanReadAttributes ? 0 : 5;
                return CanReadAttributes;
            }

            public bool TrySetExtendedStyle(IntPtr handle, int style, out int errorCode)
            {
                Style = style;
                errorCode = 0;
                return true;
            }

            public bool TrySetLayeredAttributes(IntPtr handle, uint colorKey, byte alpha, uint flags, out int errorCode)
            {
                SetAttributesCalled = true;
                AppliedAlpha = alpha;
                AppliedFlags = flags;
                errorCode = SetAttributesSucceeds ? 0 : 5;
                return SetAttributesSucceeds;
            }
        }
    }
}