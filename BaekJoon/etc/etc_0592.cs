using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 21
이름 : 배성훈
내용 : 마인크래프트
    문제번호 : 18111번

    구현, 브루트포스 문제다
    처음에는 층의 범위를 몰라 6400만 층인줄 알았다;
    이후 25만 * ? 에 ?에 logN ?들어갈만한 시간을 못찾아서 시간내 방법이 안떠올랐고, 
    문제를 다시 읽으니 층이 256층 제한이 걸려있었다

    그래서 처음은 그냥 0층부터 일일히 확인하는 식으로 했고(240ms)
    이후엔 최소층과 최대층 범위로 줄여 했다(192ms)
    그리고나서 꼭 순차적으로 벽을 쌓을 필요가 없다고 느껴 벽돌을 높이별로 모아서 높이를 찾았다(72ms)

    최소층, 최대층 범위만 확인하면 되는 이유는 쌓는데 시간이 걸리기에 최대층 보다 높게 쌓는건 최소 시간이 보장안된다
    마찬가지로 최소층 보다 낮게 쌓는것도 최소 시간이 보장안되기 때문에 최대층, 최소층 범위 안에서만 확인하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0592
    {

        static void Main592(string[] args)
        {

            StreamReader sr;
            int[] block;
            int row;
            int col;
            int remain;
            int min = 256;
            int max = 0;

            Solve();

            void Solve()
            {

                Input();
                Find();
            }

            void Find()
            {

                int ret1 = 200_000_000;
                int ret2 = 0;
                for (int i = min; i <= max; i++)
                {

                    int curR = remain;
                    int cur = 0;

                    for (int h = 0; h < 257; h++)
                    {

                        if (block[h] == 0) continue;

                        if (h < i)
                        {

                            cur += (i - h) * block[h];
                            curR -= (i - h) * block[h];
                        }
                        else
                        {

                            cur += 2 * (h - i) * block[h];
                            curR += (h - i) * block[h];
                        }
                    }

                    if (curR < 0) break;
                    if (cur > ret1) continue;
                    ret1 = cur;
                    ret2 = i;
                }

                Console.Write($"{ret1} {ret2}");
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
                row = ReadInt();
                col = ReadInt();

                remain = ReadInt();
                block = new int[257];
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = ReadInt();
                        block[cur]++;
                        min = cur < min ? cur : min;
                        max = max < cur ? cur : max;
                    }
                }
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
Span<int> f = stackalloc int[3];
var line = sr.ReadLine()!;
var next = 0;
var i = 0;
while (true)
{
    var space = line.IndexOf(' ', next + 1);
    if (space == -1)
        break;
    f[i++] = int.Parse(line.AsSpan(next, space - next));
    next = space + 1;
}
f[i] = int.Parse(line.AsSpan(next));

var area = f[0] * f[1];
Span<int> heights = stackalloc int[256 + 1];
while (f[0]-- > 0)
{
    next = 0;
    line = sr.ReadLine()!;
    while (true)
    {
        var space = line.IndexOf(' ', next + 1);
        if (space == -1)
            break;
        heights[int.Parse(line.AsSpan(next, space - next))]++;
        next = space + 1;
    }
    heights[int.Parse(line.AsSpan(next))]++;
}

int time = 0, inven = f[2], high = 256, low = 0;
while (true)
{
    while (true)
    {
        var blockNum = heights[high];
        if (blockNum == 0)
        {
            high--;
            continue;
        }
        if (blockNum == area)
        {
            Console.Write($"{time} {high}");
            return;
        }
        var voidSpace = area - blockNum;
        if (inven < voidSpace || 2 * blockNum < voidSpace)
        {
            inven += blockNum;
            heights[--high] += blockNum;
            time += 2 * blockNum;
        }
        else break;
    }


    while (true)
    {
        var blockNum = heights[low];
        if (blockNum == 0)
        {
            low++;
            continue;
        }
        if (blockNum == area)
        {
            Console.Write($"{time} {low}");
            return;
        }
        if (inven >= blockNum)
        {
            inven -= blockNum;
            heights[++low] += blockNum;
            time += blockNum;
        }
        else break;
    }
}
#elif other2
class Program
{
    static void Main(string[] args)
    {
        string[] sInputs = Console.ReadLine().Split();
        int n = Convert.ToInt32(sInputs[0]); // 세로 길이
        int m = Convert.ToInt32(sInputs[1]); // 가로 길이
        int b = Convert.ToInt32(sInputs[2]); // 초기 블럭 개수
        const int HEIGHT_MAX = 256; // 높이는 0~256
        const int HEIGHT_MIN = 0;

        int resultTime = -1; // 땅을 고르는데 걸린 시간
        int resultHeight = -1; // 최종 땅의 높이

        int[] geometry = new int[257];
        Array.Fill(geometry, 0);

        for (int i = 0; i < n; i++)
        {
            string[] lines = Console.ReadLine().Split();
            for (int j = 0; j < m; j++)
            {
                var value = Convert.ToInt32(lines[j]);
                geometry[value]++;
            }
        }

        for (int i = 256; 0 <= i; i--)
        {
            var needBlocks = 0;
            for (int j = 0; j < i; j++)
            {
                needBlocks += (i - j) * geometry[j];
            }

            var toDigBlocks = 0;
            for (int j = i + 1; j <= 256; j++)
            {
                toDigBlocks += (j - i) * geometry[j];
            }

            // 이미 있는 블록 + 파낼 블록 < 메워야하는 블록, 이라면 불가능.
            if (toDigBlocks + b < needBlocks)
                continue;

            var time = (needBlocks * 1) + (toDigBlocks * 2);
            var height = i;

            if (resultTime == -1 || time < resultTime)
            {
                resultHeight = height;
                resultTime = time;
            }
        }


        Console.WriteLine($"{resultTime} {resultHeight}");
        
    }
}
#elif other3
import sys
input=sys.stdin.readline
def sol():
    n,m,b=map(int,input().split())
    data=[0]*257
    for _ in range(n):
        for i in map(int,input().split()):
            data[i]+=1
    have=sum(i*data[i] for i in range(257))
    ans=(have*2,0)
    need=0
    t=data[0]
    nm=n*m
    for i in range(1,257):
        need+=t
        have-=nm-t
        t+=data[i]
        if have+b-need<0:
            break
        else:
            ans=min((have*2+need,-i),ans)
    print(ans[0],-ans[1])
sol()
#endif
}
