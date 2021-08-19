// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;
using System.Diagnostics.CodeAnalysis;

namespace HenHen.Framework.MapEditing.Saves
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class MemberDeserializationException : Exception
    {
        public MemberDeserializationException(string memberName, string message) : base($"Failed deserialization of member {memberName}. {message}")
        {
        }

        protected MemberDeserializationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        private MemberDeserializationException()
        {
        }
    }
}