using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 5학년은 다니기 싫어요
    문제번호 : 23028번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0064
    {

        static void Main64(string[] args)
        {

            string YES = "Nice";
            string NO = "Nae ga wae";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int cnt = 8 - ReadInt(sr);      // 남은 학기

            int major = ReadInt(sr);        // 현재 전공 점수
            int total = ReadInt(sr);        // 현재 전체 점수

            // 남은 학기
            while (cnt-- > 0)
            {

                // 전공 들을 수 있으면 전공으로 채운다!
                int n = 6;
                int addMajor = ReadInt(sr);
                n -= addMajor;
                major += 3 * addMajor;
                total += 3 * addMajor;

                // 전공 다차고 남은 자리는 교양으로 나머지를 채운다
                // 헤르미온느!
                int addTotal = ReadInt(sr);
                addTotal = addTotal < n ? addTotal : n;
                total += 3 * addTotal;
            }

            // 나머지는 결과에 영향안줘서 안읽는다
            sr.Close();

            // 결과 출력
            if (major >= 66 && total >= 130) Console.WriteLine(YES);
            else Console.WriteLine(NO);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
