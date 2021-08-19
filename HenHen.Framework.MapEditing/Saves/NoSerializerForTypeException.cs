// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Framework.MapEditing.Saves.PropertySerializers;
using System;

namespace HenHen.Framework.MapEditing.Saves
{
    [Serializable]
    public class NoSerializerForTypeException : Exception
    {
        public NoSerializerForTypeException(Type type) : base($"No {nameof(SaveableMemberSerializer)} specified for type {type.Name}.")
        {
        }

        public NoSerializerForTypeException(Type type, string message) : base($"No {nameof(SaveableMemberSerializer)} specified for type {type.Name}. {message}")
        {
        }

        public NoSerializerForTypeException(Type type, Exception innerException) : base($"No {nameof(SaveableMemberSerializer)} specified for type {type.Name}.", innerException)
        {
        }

        public NoSerializerForTypeException(Type type, string message, Exception innerException) : base($"No {nameof(SaveableMemberSerializer)} specified for type {type.Name}. {message}", innerException)
        {
        }

        protected NoSerializerForTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}