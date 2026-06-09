using System;

namespace BananaParty.WebSocketRelay
{
    public static class Hash
    {
        public static int FromName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 0;

            unchecked
            {
                int hash = 17;
                foreach (char character in name)
                    hash = hash * 31 + character;

                return hash;
            }
        }
    }
}
