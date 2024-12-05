using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : 수 나누기 게임
    문제번호 : 27172번

    에라토스테네스의 체 문제다.
    단순히 두 수를 나누는지 비교하면 n이 10만이므로 10만^2이라 시간 초과날 수 있다.
    그래서 다른 방법을 고민했고, 범위가 100만인걸 확인해 공약수를 찾는게 아닌
    배수를 이용하는 에라토스테네스의 체 아이디어를 이용했다.
    그러면 커봐야 n log n 의 시간이 걸린다. 여기서 n은 입력되는 값의 범위다.
*/
namespace BaekJoon.etc
{
    internal class etc_1154
    {

        static void Main1154(string[] args)
        {

            int n;
            int[] arr;
            int[] idx;
            int[] score;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                score = new int[n];
                for (int i = 0; i < n; i++)
                {

                    idx[arr[i]] = i;
                }

                int max = idx.Length;
                for (int i = 1; i < max; i++)
                {

                    if (idx[i] == -1) continue;

                    for (int j = i << 1; j < max; j += i)
                    {

                        if (idx[j] == -1) continue;
                        score[idx[i]]++;
                        score[idx[j]]--;
                    }
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < n; i++)
                    {

                        sw.Write($"{score[i]} ");
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];
                int max = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    max = Math.Max(max, cur);
                }

                idx = new int[max + 1];
                Array.Fill(idx, -1);

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }

#if other
// #nullable disable

using System;
using System.IO;
using System.Runtime.CompilerServices;
public sealed class FastIO : IDisposable
{
    private Byte[] _readBuffer;
    private int _readIndex;
    private int _readLength;
    private Stream _readStream;

    private Byte[] _writeBuffer;
    private int _writeIndex;
    private Stream _writeStream;

    public FastIO()
    {
        _readBuffer = new Byte[65536];
        _readStream = Console.OpenStandardInput(_readBuffer.Length);
        _readLength = _readStream.Read(_readBuffer, 0, _readBuffer.Length);

        _writeBuffer = new Byte[65536];
        _writeStream = Console.OpenStandardOutput(_writeBuffer.Length);
    }

    private static bool IsWhitespace(int ch)
    {
        return ch == '\r' || ch == '\n' || ch == ' ';
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte ReadByte()
    {
        // End of stream
        if (_readLength == 0)
            return 0;

        var val = _readBuffer[_readIndex++];
        if (_readIndex == _readLength)
        {
            _readIndex = 0;
            _readLength = _readStream.Read(_readBuffer, 0, _readBuffer.Length);
        }

        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteByte(byte b)
    {
        _writeBuffer[_writeIndex++] = b;

        if (_writeIndex == _writeBuffer.Length)
        {
            _writeStream.Write(_writeBuffer, 0, _writeIndex);
            _writeIndex = 0;
        }
    }

    public int ReadPositiveInt()
    {
        var v = ReadByte();
        while (IsWhitespace(v))
            v = ReadByte();

        var abs = v - '0';

        while (true)
        {
            v = ReadByte();

            if (v == 0 || IsWhitespace(v))
                break;
            else
                abs = abs * 10 + (v - '0');
        }

        return abs;
    }

    public void WriteInt(int value)
    {
        if (value == 0)
        {
            WriteByte((byte)'0');
            WriteByte((byte)' ');
            return;
        }
        if (value < 0)
        {
            WriteByte((byte)'-');
            WriteInt(-value);
            return;
        }

        var divisor = 1;
        var valcopy = value;

        while (valcopy > 0)
        {
            valcopy /= 10;
            divisor *= 10;
        }

        divisor /= 10;

        while (divisor != 0)
        {
            var digit = (value / divisor) % 10;
            WriteByte((byte)('0' + digit));
            divisor /= 10;
        }

        WriteByte((byte)' ');
    }

    public void Dispose()
    {
        _writeStream.Write(_writeBuffer, 0, _writeIndex);
    }
}

public static class Program
{
    public static void Main()
    {
        //using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        //using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);
        using var io = new FastIO();

        var n = io.ReadPositiveInt();
        var map = new int[1_000_001];
        var x = new int[n];
        var sc = new int[n];

        for (var idx = 0; idx < n; idx++)
        {
            var val = io.ReadPositiveInt();

            x[idx] = val;
            map[val] = 1 + idx;
        }

        for (var idx = 0; idx < n; idx++)
        {
            var val = x[idx];

            for (var target = 2 * val; target < map.Length; target += val)
                if (map[target] != 0)
                {
                    sc[map[target] - 1]--;
                    sc[idx]++;
                }
        }

        foreach (var v in sc)
        {
            io.WriteInt(v);
        }
    }
}

#endif
}
