using System;
using System.Buffers;

namespace Pingo.Helpers;


public class GmlSequenceReader
{
    ReadOnlySequence<byte>.Enumerator _enumerator;
    SequencePosition _position;

    public GmlSequenceReader(ReadOnlySequence<byte> sequence)
    {
        _enumerator = sequence.GetEnumerator();
        _position = sequence.Start;
    }


    public bool TryReadExact(int length, out ReadOnlySequence<byte> sequence)
    {
        if (_enumerator.MoveNext() && _enumerator.Current.Length >= length)
        {
            sequence = new ReadOnlySequence<byte>(_enumerator.Current.Slice(0, length));
            _position = sequence.End; // update the position for future reads
            return true;
        }
        else
        {
            sequence = default;
            return false;
        }
    }
}
