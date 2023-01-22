﻿namespace Athena.NET.Athena.NET.Parser.LexicalAnalyzer
{
    internal readonly struct PrimitiveType
    {
        public TokenIndentificator TokenType { get; }
        public Type Type { get; }

        public PrimitiveType(TokenIndentificator tokenType, Type type) 
        {
            TokenType = tokenType;
            Type = type;
        }
    }
}
