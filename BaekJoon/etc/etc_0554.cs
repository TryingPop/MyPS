using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 배열 합치기
    문제번호 : 11728번

    정렬, 두 포인터 문제다
    N log N + M log M < (N + M) log (N + M) 이지만 하나의 배열로 보고 풀었다
    두 포인터 쓰면 미미하게 성능 향상될지도 모르겠다

    그리고 중간에 flush로 writer를 한번씩 비워주니 메모리가 더 적게 들어간다
    2 ^ 15 ==> 약 3.2만번 마다 비웠는데 해당 경우에 20ms 만큼 빨라졌다
*/

namespace BaekJoon.etc
{
    internal class etc_0554
    {

        static void Main554(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 4);

            Solve();

            sr.Close();
            sw.Close();
            void Solve()
            {

                int n = ReadInt();
                int m = ReadInt();

                int[] arr = new int[n + m];
                for (int i = 0; i < n + m; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);

                int flush = 0;
                for (int i = 0; i < n + m; i++, flush++)
                {

                    sw.Write($"{arr[i]} ");
                    if ((flush & (1 << 16)) != 0) 
                    { 
                        
                        sw.Flush();
                        flush = 0;
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
namespace CSharpPlayboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InputOutput io = new InputOutput();
            int n = io.ReadInt();
            int m = io.ReadInt();

            int[] A = new int[n];
            int[] B = new int[m];

            for (int i = 0; i < n; i++)
                A[i] = io.ReadInt();

            for (int i = 0; i < m; i++)
                B[i] = io.ReadInt();

            int a = 0, b = 0;
            while (a < n && b < m)
            {
                if (A[a] > B[b])
                    io.WriteToBuffer_EDIT(B[b++]);
                else
                    io.WriteToBuffer_EDIT(A[a++]);
            }
            while (a < n)
                io.WriteToBuffer_EDIT(A[a++]);
            while (b < m)
                io.WriteToBuffer_EDIT(B[b++]);
            io.Dispose();

        }


        public class InputOutput : System.IDisposable
        {
            private System.IO.Stream _readStream, _writeStream;
            private int _readIdx, _bytesRead, _writeIdx, _inBuffSize, _outBuffSize;
            private readonly byte[] _inBuff, _outBuff;
            private readonly bool _bThrowErrorOnEof;

            public void SetBuffSize(int n)
            {
                _inBuffSize = _outBuffSize = n;
            }

            public InputOutput(bool throwEndOfInputsError = false)
            {
                _readStream = System.Console.OpenStandardInput();
                _writeStream = System.Console.OpenStandardOutput();
                _readIdx = _bytesRead = _writeIdx = 0;
                _inBuffSize = _outBuffSize = 1 << 22;
                _inBuff = new byte[_inBuffSize];
                _outBuff = new byte[_outBuffSize];
                _bThrowErrorOnEof = throwEndOfInputsError;
            }

            public int ReadInt()
            {
                byte readByte;
                while ((readByte = GetByte()) < '-') ;

                var neg = false;
                if (readByte == '-')
                {
                    neg = true;
                    readByte = GetByte();
                }
                var m = readByte - '0';
                while (true)
                {
                    readByte = GetByte();
                    if (readByte < '0') break;
                    m = m * 10 + (readByte - '0');
                }
                return neg ? -m : m;
            }


            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            private byte GetByte()
            {
                if (_readIdx >= _bytesRead)
                {
                    _readIdx = 0;
                    _bytesRead = _readStream.Read(_inBuff, 0, _inBuffSize);
                    if (_bytesRead >= 1) return _inBuff[_readIdx++];

                    if (_bThrowErrorOnEof) throw new System.Exception("End Of Input");
                    _inBuff[_bytesRead++] = 0;
                }
                return _inBuff[_readIdx++];
            }

            
            public void WriteToBuffer_EDIT(int c)
            {
                byte[] temp = new byte[10];
                int tempidx = 0;
                if (c < 0)
                {
                    if (_writeIdx == _outBuffSize) Flush(); _outBuff[_writeIdx++] = (byte)'-'; c = -c;
                }
                do
                {
                    temp[tempidx++] = (byte)((c % 10) + '0');
                    c /= 10;
                } while (c > 0);
                for (int i = tempidx - 1; i >= 0; i--)
                {
                    if (_writeIdx == _outBuffSize) Flush();
                    _outBuff[_writeIdx++] = temp[i];
                }
                if (_writeIdx == _outBuffSize)
                    Flush();
                _outBuff[_writeIdx++] = (byte)' ';
            }

            private void Flush()
            {
                _writeStream.Write(_outBuff, 0, _writeIdx);
                _writeStream.Flush();
                _writeIdx = 0;
            }

            public void Dispose()
            {
                Flush();
                _writeStream.Close();
                _readStream.Close();
            }
        }
    }
}

#endif
}
