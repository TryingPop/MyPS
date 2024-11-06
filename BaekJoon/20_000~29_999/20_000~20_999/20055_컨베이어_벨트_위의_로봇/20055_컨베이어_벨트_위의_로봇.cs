using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 컨베이어 벨트 위의 로봇
    문제번호 : 20055번

    시물레이션 구현 능력이다
    그냥 조건에 맞게 순서대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0102
    {

        static void Main102(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int maxLen = 2 * len;
            int broke = ReadInt(sr);

            (int val, bool robot)[] belt = new (int val, bool robot)[maxLen];

            for (int i = 0; i < maxLen; i++)
            {

                belt[i] = (ReadInt(sr), false);
            }
            sr.Close();

            // 인덱스라 1, N 에 각각 -1했다
            int zeroPtr = 0;
            int nPtr = len - 1;

            // 현재 단계
            int step = 0;
            // 종료된 발판?
            int ret = 0;
            while (true)
            {

                step++;

                // 벨트 이동
                // 전체를 이동시킬 수 없기에 시작지점과 N포인트를 이동시킨다
                zeroPtr = zeroPtr == 0 ? maxLen - 1 : zeroPtr - 1;
                nPtr = nPtr == 0 ? maxLen - 1 : nPtr - 1;

                // 벨트 이동 후 nPtr의 로봇은 꺼낸다!
                belt[nPtr].robot = false;

                // 로봇 이동 부분!
                // 역순으로 조사한다!
                for (int i = 1; i < len; i++)
                {

                    // 현재 idx, 다음 idx 찾는다
                    int curIdx = nPtr - i;
                    if (curIdx < 0) curIdx += maxLen;
                    int nextIdx = curIdx + 1;
                    if (nextIdx >= maxLen) nextIdx -= maxLen;

                    // 현재 벨트에 로봇이 있는지 확인
                    if (belt[curIdx].robot)
                    {

                        // 다음 벨트로 이동가능한지 조사
                        if (belt[nextIdx].val > 0 && !belt[nextIdx].robot)
                        {

                            belt[curIdx].robot = false;
                            belt[nextIdx].robot = nextIdx != nPtr;
                            belt[nextIdx].val--;
                            // val을 깎기에 종료되었는지 조사!
                            if (belt[nextIdx].val == 0) ret++;
                        }
                    }
                }

                // zeroPtr에 로봇놓기
                // 놓을 때 1감소한다!
                // 그런데 못놓는지 확인
                if (belt[zeroPtr].val > 0)
                {

                    belt[zeroPtr].val--;
                    belt[zeroPtr].robot = true;
                    if (belt[zeroPtr].val == 0) ret++;
                }

                if (ret >= broke) break;
            }

            Console.WriteLine(step);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
public class Program
{
    internal record struct Belt(int Durability, bool RobotExists);

    private static void Main(string[] args)
    {
        int n, k, beltLength;
        Belt[] belt;
        using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
        {
            beltLength = 2 * (n = ScanInt(sr));
            k = ScanInt(sr);
            belt = new Belt[beltLength];
            for (int i = 0; i < beltLength; i++)
                belt[i] = new(ScanInt(sr), false);
        }

        var robotPoses = new List<int>();
        var newRobotPos = new List<int>();
        var loadPos = 0;
        var unloadPos = n - 1;
        var unabledBeltCount = 0;
        var phase = 0;
        do
        {
            phase++;
            unloadPos = (unloadPos - 1 + beltLength) % beltLength;
            loadPos = (loadPos - 1 + beltLength) % beltLength;

            foreach (var r in robotPoses)
            {
                if (r == unloadPos)
                {
                    belt[r].RobotExists = false;
                    continue;
                }

                var nextPos = (r + 1) % beltLength;
                (var dur, var robotExists) = belt[nextPos];
                if (dur > 0 && !robotExists)
                {
                    belt[r].RobotExists = false;
                    if (--belt[nextPos].Durability == 0)
                        unabledBeltCount++;
                    if (nextPos != unloadPos)
                    {
                        newRobotPos.Add(nextPos);
                        belt[nextPos].RobotExists = true;
                    }
                }
                else newRobotPos.Add(r);
            }

            ref var loadBelt = ref belt[loadPos];
            if (loadBelt.Durability > 0 && !loadBelt.RobotExists)
            {
                loadBelt.RobotExists = true;
                newRobotPos.Add(loadPos);
                if (--loadBelt.Durability == 0)
                    unabledBeltCount++;
            }
            (robotPoses, newRobotPos) = (newRobotPos, robotPoses);
            newRobotPos.Clear();
        } while (unabledBeltCount < k);
        Console.Write(phase);
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#elif other2
using var reader = new StreamReader(Console.OpenStandardInput());
int[] Read() => Array.ConvertAll(reader.ReadLine().Split(), int.Parse);

var read = Read();
int N = read[0], K = read[1];
var belt = Read();
var robots = new int[N];

int count = 0;
for (int c = 1; ; c++)
{
    int last = belt[^1];
    for (int i = belt.Length - 1; i > 0; i--)
        belt[i] = belt[i - 1];
    belt[0] = last;

    for (int i = N - 1; i > 0; i--)
        robots[i] = robots[i - 1];
    robots[N - 1] = robots[0] = 0;

    for (int i = N - 2; i > 0; i--)
        if (robots[i] == 1 && robots[i + 1] == 0 && belt[i + 1] > 0)
        {
            if (--belt[i + 1] == 0)
                count++;
            robots[i + 1] = 1;
            robots[i] = 0;
        }

    if (belt[0] > 0)
    {
        robots[0] = 1;
        if (--belt[0] == 0)
            count++;
    }

    if (count >= K)
    {
        Console.WriteLine(c);
        return;
    }
}
#endif
}
