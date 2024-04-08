using System.Text;

namespace Pingo.Java.Protocol;

internal ref struct MemoryReader(ReadOnlyMemory<byte> memory)
{
    public int Position { get; private set; }

    private readonly ReadOnlySpan<byte> span = memory.Span;

    public int ReadVariableInteger()
    {
        var numbersRead = 0;
        var result = 0;

        byte read;

        do
        {
            read = span[Position++];

            var value = read & 0b01111111;
            result |= value << 7 * numbersRead;

            numbersRead++;

            if (numbersRead > 5)
            {
                throw new InvalidOperationException("Variable integer is too big.");
            }
        } while ((read & 0b10000000) != 0);

        return result;
    }

    public string ReadVariableString()
    {
        var length = ReadVariableInteger();
        var buffer = span[Position..(Position += length)];
        return Encoding.UTF8.GetString(buffer);
    }
}