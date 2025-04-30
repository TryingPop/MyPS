using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 29
이름 : 배성훈
내용 : 순열 그래프의 연결성 판별
    문제번호 : 7982번

    애드 혹 문제다.
    아이디어는 단순하다.
    하나씩 읽는다.
    현재 좌표를 cur이라 하면 남은 좌표 중 cur보다 작은 노드들은 모두 cur과 이어진다.
    그래서 남은 최솟값과 최댓값을 매번 확인한다.
    
    만약 최솟값이 최댓값보다 커지는 순간 이는 하나의 연결이 됨을 알 수 있다.
    이 경우 연결됨을 끝지점만 기록해 놓는다.
    시작지점은 이전 연결된 끝지점 + 1임이 자명하기 때문이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1590
    {

        static void Main1590(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();

            bool[] visit = new bool[n + 2];
            int[] cnt = new int[n];

            int min = 1;
            int max = 0;
            int idx = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                visit[cur] = true;

                if (cur == min)
                {

                    // 최솟값 갱신
                    while (visit[min])
                        min++;
                }

                max = Math.Max(cur, max);
                if (max < min)
                {

                    // 연결 구간이 발견된 경우 끝 값만 기록
                    cnt[idx++] = max;
                }
            }

            sw.Write($"{idx}\n");
            int prev = 1;

            // 이중 포문이지만 두 포인터 알고리즘으로 실상 N만 읽는다.
            for (int i = 0; i < idx; i++)
            {

                sw.Write($"{cnt[i] - prev + 1} ");
                for (int j = prev; j <= cnt[i]; j++)
                {

                    sw.Write($"{j} ");
                }

                sw.Write('\n');
                prev = cnt[i] + 1;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
