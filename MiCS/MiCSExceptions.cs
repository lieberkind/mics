using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    [Serializable]
    internal class MemberSignatureArgumentTypeNotMappedException : Exception
    {
        public MemberSignatureArgumentTypeNotMappedException() { }
        public MemberSignatureArgumentTypeNotMappedException(string message) : base(message) { }
        public MemberSignatureArgumentTypeNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberSignatureArgumentTypeNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    internal class MemberSignatureNotMappedException : Exception
    {
        public MemberSignatureNotMappedException() { }
        public MemberSignatureNotMappedException(string message) : base(message) { }
        public MemberSignatureNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberSignatureNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    internal class MemberNotMappedException : Exception
    {
        public MemberNotMappedException() { }
        public MemberNotMappedException(string message) : base(message) { }
        public MemberNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected MemberNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    internal class TypeNotMappedException : Exception
    {
        public TypeNotMappedException() { }
        public TypeNotMappedException(string message) : base(message) { }
        public TypeNotMappedException(string message, Exception inner) : base(message, inner) { }
        protected TypeNotMappedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    internal class MixedSidePrincipleViolatedException : Exception
    {
        public MixedSidePrincipleViolatedException() { }
        public MixedSidePrincipleViolatedException(string message) : base(message) { }
        public MixedSidePrincipleViolatedException(string message, Exception inner) : base(message, inner) { }
        protected MixedSidePrincipleViolatedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    internal class IllegalInvocationException : Exception
    {
        public IllegalInvocationException() { }
        public IllegalInvocationException(string message) : base(message) { }
        public IllegalInvocationException(string message, Exception inner) : base(message, inner) { }
        protected IllegalInvocationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
