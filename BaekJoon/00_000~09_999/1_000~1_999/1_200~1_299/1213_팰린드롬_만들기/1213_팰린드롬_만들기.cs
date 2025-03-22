using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 11
이름 : 배성훈
내용 : 팰린드롬 만들기
    문제번호 : 1213번

    문자열이 주어지면 팰린드롬으로 만들 수 있는지 확인하는 문제이다
    만들어지면, 알파벳 순으로 출력한다
    입력된 문자는 오로지 알파벳 대문자만 이루어져있다

    주된 아이디어는 다음과 같다
    각각의 알파벳을 카운트해서
    홀수가 많아야 1개인지 확인하면 된다

    홀수가 2개 이상이면 못만든다!

    팰린드롬 dp문제를 해결하기위해 먼저 푼 문제이다
*/

namespace BaekJoon.etc
{
    internal class etc_0017
    {

        static void Main17(string[] args)
        {

            // 상수? 비슷한 느낌의 변수 선언
            string IMPOSSIBLE = "I\'m Sorry Hansoo";
            int ALPHABET = 26;

            // 입력
            string str = Console.ReadLine();

            int[] alphabet = new int[ALPHABET];

            // 알파벳 카운팅
            for (int i = 0; i < str.Length; i++)
            {

                alphabet[str[i] - 'A']++;
            }

            // 정답 문자열 일부 만들기
            // 짝수부터 채워간다
            int curIdx = 0;
            int end = str.Length - 1;
            char[] result = new char[end + 1];
            for (char i = 'A'; i <= 'Z'; i++)
            {

                int idx = i - 'A';
                while (alphabet[idx] > 1)
                {

                    // 앞 뒤로 이어 붙인다
                    alphabet[idx] -= 2;
                    result[curIdx] = i;
                    result[end - curIdx] = i;

                    curIdx++;
                }
            }

            // 이제 홀수가 몇개인지 판별!
            bool chk = false;
            bool failed = false;
            for (int i = 0; i < ALPHABET; i++)
            {

                if (alphabet[i] == 1)
                {

                    if (!chk) 
                    { 
                        
                        // 1개 존재확인
                        chk = true;
                        result[curIdx] = (char)(i + 'A');
                    }
                    // 홀수 원소 2개 발견
                    else failed = true;
                }

                // 홀수 2개 판별했으므로 더 이상 탐색 X
                if (failed) break;
            }

            // 실패한 경우
            if (failed) Console.WriteLine(IMPOSSIBLE);
            else
            {

                // 발견한 경우
                for (int i = 0; i < result.Length; i++)
                {

                    Console.Write(result[i]);
                }
            }
        }
    }
}
