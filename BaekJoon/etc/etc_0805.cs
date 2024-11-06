using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : 배고파(Hard)
    문제번호 : 28245번

    브루트포스, 비트마스킹 문제다
    가능한 범위 모두 탐색해도 3600번 정도 연산하기에 브루트포스로 했다
    그리고 이 중 차이가 최소인 경우를 찾아 저장하고 출력하니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0805
    {

        static void Main805(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;
            int x = 0, y = 0;
            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    long find = ReadLong();

                    FindXY(find);
                    sw.Write($"{x} {y}\n");
                }

                sr.Close();
                sw.Close();
            }

            void FindXY(long find)
            {

                long DIFF = 1_000_000_000_000_000_000;

                for (int i = 0; i < 61; i++)
                {

                    long f = 1L << i;
                    for (int j = 0; j <= i; j++)
                    {

                        long b = 1L << j;
                        long chk = Math.Abs(f + b - find);
                        
                        if (chk < DIFF)
                        {

                            DIFF = chk;
                            x = j;
                            y = i;
                        }
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

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

            long ReadLong()
            {

                int c;
                long ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }


#if other
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

var n = int.Parse(reader.ReadLine());

while (n-- > 0)
{
    var m = long.Parse(reader.ReadLine());

    var result = FindSumOf2Powers(m);
    writer.WriteLine(result[0] + " " + result[1]);
}

reader.Close();
writer.Close();

int[] FindSumOf2Powers(long m)
{
    // For 0 and 1, nearest sum of 2 powers is 1 + 1 = 2, (2^0 + 2^0)
    if (m <= 1)
        return new int[] { 0, 0 };
    
    // When x == y, you can't check with bit masking. (2^x = 2^(x-1) + 2^(x-1))
    int bitCount = CountBits(m);
    if (bitCount == 1)
    {
        var xy = MSBPosition(m) - 1;
        return new int[] { xy, xy };
    }

    // The m is sum of 2 powers.
    if (bitCount == 2) 
        return new int[] { LSBPosition(m), MSBPosition(m) };

    // If there're 3 or more bits, subtract or add until it has 1 or 2 bits.
    int msb = MSBPosition(m);
    
    long minDiff = long.MaxValue;
    int x = -1;
    int y = -1;

    if (msb < 63)
    {
        minDiff = (1L << (msb + 1)) - m;
        x = msb;
        y = msb;
    }

    var diff = m - (1L << msb);
    if (minDiff >= diff)
    {
        minDiff = diff;
        x = msb - 1;
        y = msb - 1;
    }

    for (int i = msb - 1; i >= 0; i--)
    {
        diff = Math.Abs(m - ((1L << msb) + (1L << i)));
        if (minDiff >= diff)
        {
            minDiff = diff;
            x = i;
            y = msb;
        }
    }

    return new int[] { x, y };
}

int CountBits(long value)
{
    long v = value;
    int count = 0;
    for (; v > 0; count++)
        v &= v - 1;

    return count;
}

int MSBPosition(long value) => Convert.ToString(value, 2).Length - 1;

int LSBPosition(long value)
{
    if (value == 0)
        return -1;

    var binary = Convert.ToString(value, 2);

    int lsbPos = 0;
    for (int i = binary.Length - 1; i >= 0; i--)
        if (binary[i] == '1')
        {
            lsbPos = binary.Length - 1 - i;
            break;
        }

    return lsbPos;
}
#elif other2
// #include <iostream>
using namespace std;
typedef long long ll;

int main() {
    ios_base::sync_with_stdio(false); cin.tie(nullptr);
    int n, x, y;
    ll m, k, l;
    cin >> n;
    for (int i=0; i<n; i++) {
        cin >> m;
        if (m == 1) m = 2;
        k = l = 1;
        x = y = 0;
        while (k < m) { k *= 2; y++; }
        y--; k /= 2;
        while (k + l <= m) { l *= 2; x++; }
        x--; l /= 2;
        if (m - (k + l) <= (k + 2*l) - m) cout << x << " " << y << "\n";
        else cout << x + 1 << " " << y << "\n";
    }
    return 0;
}
#endif
}
