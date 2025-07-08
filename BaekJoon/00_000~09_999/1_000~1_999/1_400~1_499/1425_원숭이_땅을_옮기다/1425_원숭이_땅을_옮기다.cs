using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 원숭이 땅을 옮기다
    문제번호 : 1425번

    이분 탐색 문제다
    원숭이의 수가 50마리 이하라, 원숭이 거리가 가능한 경우는 50 C 2 = 1225 경우다
    원숭이들의 전체 거리와, 높이의 최대 최소값까지는 최대거리를 찾기 위해 담아야함을 알았다
    땅의 높이따른 거리는 
        (두 원숭이의 거리) = |x차이| + |y차이| + 2 * |두 원숭이 중 땅과 높이차가 최소|
    여기서 차이는 두 원숭이의 차이를 의미한다
    x차이는 두 원숭이의 x값 차이, 그리고 |x|는 x의 절대값을 의미한다

    이러한 식 때문에 원숭이들 y좌표가 최대값이 되는거 아닌가? 추론을 했고
    두 번째 예제에서 막혔다

    즉, 최대값이 최소가 되는 경우 중에 원숭이 사이의 땅이 될 수도 있다
    여기서 힌트를 봤고 무엇을 기준으로 이분탐색을 할지 못찾았다
    
    결국 구글링을 했고 원숭이 거리의 최대값을 갖고 이분 탐색을 해야함을 알았다
    (참고한 사이트) https://boomrabbit.tistory.com/116  

    최대값을 갖고 이분탐색을 하는데, 방법은 다음과 같다
    해당 최대값이 가질 수 있는 값인지 판별해야한다
    유효하면 최대값을 줄이고, 유효 안하면 최대값을 올려야한다

    최대 거리 유효 판별은 두 가지로 이뤄진다
        1.
                (두 원숭이의 거리) = |x차이| + |y차이| + 2 * |두 원숭이 중 땅과 높이차가 최소|
            에서 원숭이 거리의 최소값은 |x차이| + |y차이|이고,
            가정한 최대값은 원숭이 거리의 최소값의 차이보다는 커야한다

        2.
            그리고 두번째 판정은 정수인 땅의 범위가 유효해야한다
                (두 원숭이의 거리) = |x차이| + |y차이| + 2 * |두 원숭이 중 땅과 높이차가 최소|
            거리 식 중에 |두 원숭이 중의 땅과 높이차가 최소| 에서 땅의 범위를 얻을 수 있다

            |x차이| + |y차이|는 정해진 값이므로 dis라하자
            최대 거리 >= (두 원숭이 사이의 거리) = dis + 2 * |두 원숭이 중 땅과 높이차가 최소| 이고,
            
            최대 거리 - dis >= 2 * |두 원숭이 중 땅과 높이차가 최소|
            땅의 위치를 h라하면
            => (최대 거리 - dis) / 2 >= Math.Min(|minY - h|, |maxY - h|)
            diff = (최대 거리 - dis) / 2라하면
            => diff >= Math.Min(|minY - h|, |maxY - h|)
            여기서 h <= minY 일때,
                diff >= Math.Min(minY - h, maxY - h) = minY - h
                => h >= minY - diff
            h >= maxY일 때는
                h <= diff + maxY를 얻는다
            그래서 minY - diff <= h <= maxY + diff
            해당 범위들이 전부 겹치는 정수 높이가 적어도 하나 존재하는지 확인해야한다
            이는 교집합하면서 판별한다

    이렇게 이분탐색을해서 정답을 제출하니 통과했다
    교집합 연산에서 부등호를 빼서 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0243
    {

        static void Main243(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            (int x, int y)[] monkeys = new (int x, int y)[n];

            int[] chks = new int[n];

            for (int i = 0; i < n; i++)
            {

                int x = ReadInt(sr);
                int y = ReadInt(sr);

                monkeys[i] = (x, y);

                chks[i] = y;
            }

            sr.Close();

            // 두 원숭이들의 거리차 -> 최대 40억이므로 long형으로 했다
            long[] dis = new long[(n * (n - 1)) / 2];
            long[] minY = new long[dis.Length];
            long[] maxY = new long[dis.Length];
            int idx = 0;
            for (int i = 0; i < n; i++)
            {

                for (int j = i + 1; j < n; j++)
                {

                    // 원숭이들의 거리차
                    dis[idx] = GetDis(monkeys[i], monkeys[j]);
                    long min = monkeys[i].y < monkeys[j].y ? monkeys[i].y : monkeys[j].y;
                    long max = monkeys[i].y < monkeys[j].y ? monkeys[j].y : monkeys[i].y;

                    minY[idx] = min;
                    maxY[idx] = max;
                    idx++;
                }
            }

            // 최대값을 갖고 이분 탐색을 한다
            long l = 0;
            // 40억보다 크기만 하면 된다
            long r = 5_000_000_000;
            while(l <= r)
            {

                long mid = (l + r) / 2;
                bool possible = true;

                long bot = -1_000_000_000;
                long top = 1_000_000_000;
                for (int i = 0; i < dis.Length; i++)
                {

                    // 최대값이 될 수 있는지 먼저 조사
                    long diff = mid - dis[i];
                    if (diff < 0)
                    {

                        possible = false;
                        break;
                    }

                    // 정수의 나눗셈으로 끝점을 정수값으로 맞춘다
                    diff /= 2;

                    long chkBot = minY[i] - diff;
                    long chkTop = maxY[i] + diff;

                    // 높이가 겹치지 않는지 확인
                    if (chkTop < bot || top < chkBot)
                    {

                        possible = false;
                        break;
                    }

                    // 높이가 겹치니
                    // 높이 유효 범위를 교집합해야한다
                    if (bot <= chkBot && chkBot <= top) bot = chkBot;
                    if (bot <= chkTop && chkTop <= top) top = chkTop;
                }

                if (possible) r = mid - 1;
                else l = mid + 1;
            }

            long ret = r + 1;
            Console.WriteLine(ret);
        }

        static long GetDis((int x, int y) _pos1, (int x, int y) _pos2)
        {

            // 원숭이간 거리 찾기
            long ret;

            // 여기서는 오버플로우 안난다
            // y차이도 많아야 +-20억 사이
            long h = _pos1.y - _pos2.y;
            h = h < 0 ? -h : h;

            // x차이 많아야 +-20억 사이
            long w = _pos1.x - _pos2.x;
            w = w < 0 ? -w : w;

            // 여기서 오버플로우날 수 있어서
            // long으로 w, h 설정했다
            ret = h + w;
            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
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
