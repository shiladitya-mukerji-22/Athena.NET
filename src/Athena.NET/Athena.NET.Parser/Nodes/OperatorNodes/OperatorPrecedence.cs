﻿namespace Athena.NET.Athena.NET.Parser.Nodes.OperatorNodes
{
    internal enum OperatorPrecedence : uint
    {
        Multiplicative = 1,
        Additive = 2,
        Logical= 3,
        Brace = 100
    }
}
