using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 14
이름 : 배성훈
내용 : 성인 게임
    문제번호 : 23256번

    다각형을 돌려가면서 만드는 문제이다
    1 -> 2로 오른쪽으로 이어붙일 수 있는 경우를 비교해보니,
    7가지 경우를 붙일 수 있는 모양과 3가지 경우를 붙일 수 있는 모양이 있었다
    이는 오른쪽 끝이 1칸짜리 블록인지, 2개짜리 블록인지에 따라 결정되었다!
    7가지 경우를 붙일 수 있는 모양을 A, 3가지 경우를 이어 붙일 수 있는 모양을 B라하자

    예를들어 ㅁ을 한개짜리 블록이라 하고 ㅇㅇ는 ㅁ이 두개 이어진거라 생각하자
           ㅁ
        ㅁ    ㅁ <- 해당 블록이 7개 경우를 붙일 수 있는 블록
           ㅁ
        한개 짜리 블록들로 구성된 경우는 A이다

           ㅁ
        ㅁ    ㅇ <- 해당 블록은 3개 경우를 이어 붙일 수 있는 블록
           ㅇ
        밑에 '/' 2개 블록이 붙어있는 경우 B이다

           ㅇ
        ㅁ    ㅇ <- 해당 블록은 3개 경우를 이어 붙일 수 있는 블록
           ㅁ
        위에 '\' 형태로 붙어 있다 B이다

    그래서 A의 개수와 B의 개수를 기록하며 나아간다
    A에서 A가 나올 수 있는건 3개이고, B가 나올 수 있는거 4개이다
    B에서 A가 나올 수 있는건 1개이고, B가 나올 수 있는건 2개이다
    
    그래서 
        dpA[i] = dpA[i - 1] * 3 + dpB[i - 1]
        dpB[i] = dpA[i - 1] * 4 + dpB[i - 1] * 2

    그리고 dpA[i] + dpB[i]는 i개에서 나올 수 있는 전체 경우다!
    해당 아이디어를 코드로 나타낸게 아래 코드다!
    시간은 80ms나왔다
*/

namespace BaekJoon.etc
{
    internal class etc_0035
    {

        static void Main35(string[] args)
        {

            int r = 1_000_000_007;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            // 10억 * 4 + @@@ 하기에
            // long 자료형으로 설정, 1_000_001은 최대 길이가 100만이므로 100만 + 1로 잡았다
            long[] dpA = new long[1_000_001];
            long[] dpB = new long[1_000_001];

            // 길이가 1일 때,
            dpB[1] = 4;
            dpA[1] = 3;

            // 기록된 길이
            int calc = 1;
            while(test-- > 0)
            {

                int len = ReadInt(sr);

                // 기록이 안되었을 경우 기록하면서 나아간다
                // 해당 길이가 기록되었다면 for문 안돌아간다
                for (int i = calc + 1; i <= len; i++)
                {

                    dpB[i] = dpB[i - 1] * 2 + dpA[i - 1] * 4;
                    dpA[i] = dpB[i - 1] + dpA[i - 1] * 3;

                    // 형식에 맞춰 10억 7로 나눈나머지를 저장!
                    dpB[i] %= r;
                    dpA[i] %= r;
                }

                // 기록된 길이 갱신 여부 확인
                calc = calc < len ? len : calc;

                // 결과 계산
                long ret = (dpB[len] + dpA[len]) % r;
                
                sw.WriteLine(ret);

            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != ' ' && c != -1 && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
