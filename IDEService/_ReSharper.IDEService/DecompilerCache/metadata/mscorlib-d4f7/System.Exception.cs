// Type: System.Exception
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (_Exception))]
    [Serializable]
    public class Exception : ISerializable, _Exception
    {
        public Exception();
        public Exception(string message);
        public Exception(string message, Exception innerException);

        [SecuritySafeCritical]
        protected Exception(SerializationInfo info, StreamingContext context);

        public virtual IDictionary Data { [SecuritySafeCritical]
        get; }

        protected int HResult { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        #region _Exception Members

        public virtual Exception GetBaseException();

        [SecuritySafeCritical]
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public override string ToString();

        public new Type GetType();

        public virtual string Message { [SecuritySafeCritical]
        get; }

        public Exception InnerException { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public MethodBase TargetSite { [SecuritySafeCritical,
                                        TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public virtual string StackTrace { [SecuritySafeCritical,
                                            TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public virtual string HelpLink { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        public virtual string Source { [SecuritySafeCritical]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        #endregion

        #region ISerializable Members

        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

        protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState;
    }
}
