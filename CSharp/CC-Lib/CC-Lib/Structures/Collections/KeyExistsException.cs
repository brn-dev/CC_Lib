using System;

namespace CC_Lib.Structures.Collections
{
    class KeyExistsException : Exception
    {
        public KeyExistsException(object key) : base($"Key '{key}' already exists!")
        {
        }
    }
}
