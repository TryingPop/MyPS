using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 스터디 시간 정하기
    문제번호 : 23301번

    구현 브루트포스 문제다
    누적합 아이디어를 써서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0237
    {

        static void Main237(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int humans = ReadInt(sr);
            int studyTime = ReadInt(sr);

            // 누적합 아이디어 사용
            int[] time = new int[1_000 + 1];
            int[] sum = new int[1_000 + 1];

            for (int i = 0; i < humans; i++)
            {

                int cnt = ReadInt(sr);

                for (int j = 0; j < cnt; j++)
                {

                    int s = ReadInt(sr);
                    int e = ReadInt(sr);

                    time[s]++;
                    time[e]--;
                }
            }

            sr.Close();

            int cur = 0;
            for (int i = 0; i < 1_001; i++)
            {

                cur += time[i];
                sum[i] = cur;
            }

            cur = 0;
            for (int i = 0; i < studyTime; i++)
            {

                // 해당 시간에 만족도 조사
                // sum[i] : i ~ i + 1 시간의 만족도
                cur += sum[i];
            }

            int max = cur;
            int ret1 = 0;
            int ret2 = studyTime;
            for (int i = studyTime; i < 1_001; i++)
            {

                cur += sum[i];
                cur -= sum[i - studyTime];

                if (max < cur) 
                { 
                    
                    max = cur;
                    // 정답 형식에 맞춰 출력
                    ret1 = i - studyTime + 1;
                    ret2 = i + 1;
                }
            }

            Console.WriteLine($"{ret1} {ret2}");
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
}
