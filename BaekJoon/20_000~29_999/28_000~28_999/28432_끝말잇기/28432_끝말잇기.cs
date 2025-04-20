using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 끝말잇기
    문제번호 : 28432번

    문자열, 구현 문제다
    끝말잇기를 할 때, 중복되는 문자열이 없어야한다
    중복여부를 확인하기 위해 HashSet 자료구조를 이용했다
    ?는 하나만 들어오기에 다음과 같은 코드를 구현했다

    중복문제와 "?"만 입력되는 경우로 index에러로 2번 틀렸다
    이외는 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0286
    {

        static void Main286(string[] args)
        {

            string question = "?";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            int findIdx = -2;
            string before = "";
            string next = "";

            HashSet<string> set = new(n);
            for (int i = 0; i < n; i++)
            {

                string str = sr.ReadLine();

                if (str == question) findIdx = i;
                // ?을 제외한 문자열 저장
                else set.Add(str);
                // ? 다음 문자열 next에 저장
                if (i == findIdx + 1) next = str;
                // ? 바로 앞 문자열 before 저장
                else if (findIdx == -2) before = str;
            }

            int m = int.Parse(sr.ReadLine());

            string ret = null;
            for (int i = 0; i < m; i++)
            {

                string str = sr.ReadLine();
                // 중복된 문자열이면 넘긴다
                if (set.Contains(str)) continue;

                if (findIdx == 0)
                {

                    // 문자열 1개만 입력된 경우 여기로 온다
                    if (set.Count == 0 || str[^1] == next[0]) ret = str;
                }
                else if (findIdx == n - 1)
                {

                    if (str[0] == before[^1]) ret = str;
                }
                else
                {

                    if (str[0] == before[^1] && str[^1] == next[0]) ret = str;
                }

                // 결과 찾았다
                if (ret != null) break;
            }

            sr.Close();

            Console.Write(ret);
        }
    }
}
