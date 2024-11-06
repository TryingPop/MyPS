using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : 부등호
    문제번호 : 2529번

    브루트포스, 백트래킹 문제다
    그리디하게 접근해서 스택과 비슷하게풀어냈다

    아이디어는 다음과 같다
    큰 값을 찾을 때는 > 부등호가 나오면 현재 넣을 수 잇는 가장 큰 값을 왼쪽에 넣는다
    < 부등호가 나오면 일단은 < 가 연달아 나오는 최대 길이를 계산해야한다
    3개가 나왔다고 하면 ?1 < ?2 < ?3 < ?4 인데,
    ?4 에 넣을 수 있는 가장 큰 값, 이후에 ?3에 넣을 수 있는 가장 큰값 ... ?1에 넣을 수 있는 가장 큰값을 넣는다
    이렇게 진행하며 채운다
    그리고 마지막에 다 안들어가는 경우가 생기는데, 이는 마지막에 > 연산을 추가해 다 채워줘야한다
    이러한 아이디어는 스택의 아이디어와 같다
    최소값은 부등호를 바꿔 생각하고 작은 값부터 채워가면 된다

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0429
    {

        static void Main429(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int n = int.Parse(sr.ReadLine());

            int[] arr = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                int op = sr.Read();
                arr[i] = op;
                sr.Read();
            }

            sr.Close();

            arr[n] = '>';

            int chkIdx = 9;
            int curIdx = 0;
            int[] retMax = new int[n + 1];

            for (int i = 0; i <= n; i++)
            {

                if (arr[i] == '>')
                {

                    for (int j = i; j >= curIdx; j--)
                    {

                        retMax[j] = chkIdx--;
                    }

                    curIdx = i + 1;
                }
            }

            arr[n] = '<';
            curIdx = 0;
            chkIdx = 0;
            int[] retMin = new int[n + 1];

            for (int i = 0; i <= n; i++)
            {

                if (arr[i] == '<')
                {

                    for (int j = i; j >= curIdx; j--)
                    {

                        retMin[j] = chkIdx++;
                    }

                    curIdx = i + 1;
                }
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i <= n; i++)
                {

                    sw.Write(retMax[i]);
                }
                sw.Write('\n');

                for (int i = 0; i <= n; i++)
                {

                    sw.Write(retMin[i]);
                }
            }
        }
    }

#if other
using System.Text;

var k = int.Parse(Console.ReadLine()!);
var line = Console.ReadLine()!;
var isRightBigger = new bool[k];
for (int i = 0; i < k; i++)
    isRightBigger[i] = line[2 * i] == '<';

var chosen = new int[k + 1];
var visited = new bool[10];
var ret = new StringBuilder();
for (int i = 9; i >= 0; i--)
{
    chosen[0] = i;
    visited[i] = true;
    var success = ChooseMax(0);
    visited[i] = false;
    if (success)
        break;
}
foreach (var o in chosen)
    ret.Append(o);
ret.Append('\n');

for (int i = 0; i <= 9; i++)
{
    chosen[0] = i;
    visited[i] = true;
    var success = ChooseMin(0);
    visited[i] = false;
    if (success)
        break;
}
foreach (var o in chosen)
    ret.Append(o);
Console.Write(ret);

bool ChooseMax(int operIndex)
{
    if (operIndex == k)
        return true;

    var isRightBig = isRightBigger[operIndex];
    var cur = chosen[operIndex];
    var start = 9;
    if (!isRightBig)
        start = cur - 1;

    for (int i = start; i >= 0; i--)
    {
        if (visited[i]) continue;
        if (isRightBig)
        {
            if (cur > i)
                return false;
        }

        var nextIndex = operIndex + 1;
        chosen[nextIndex] = i;
        visited[i] = true;
        var ret = ChooseMax(nextIndex);
        visited[i] = false;
        if (ret) return true;
    }
    return false;
}

bool ChooseMin(int operIndex)
{
    if (operIndex == k)
        return true;

    var isRightBig = isRightBigger[operIndex];
    var cur = chosen[operIndex];
    for (int i = (isRightBig ? cur + 1 : 0); i <= 9; i++)
    {
        if (visited[i]) continue;
        if (!isRightBig)
        {
            if (cur < i)
                return false;
        }

        var nextIndex = operIndex + 1;
        chosen[nextIndex] = i;
        visited[i] = true;
        var ret = ChooseMin(nextIndex);
        visited[i] = false;
        if (ret) return true;
    }
    return false;
}
#elif other2

#endif
}
