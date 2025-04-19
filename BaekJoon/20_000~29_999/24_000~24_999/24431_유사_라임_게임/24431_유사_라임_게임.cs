using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 유사 라임 게임
    문제번호 : 24431번

    구현, 자료구조, 문자열, 정렬, 해시를 사용한 집합과 맵, 트리를 사용한 집합과 맵 문제다

    주된 아이디어는 다음과 같다
    문자열을 입력 받으면 원하는 접미사의 길이로 자른다
    그리고 해당 접미사의 개수를 센다
    세는데 딕셔너리 자료구조를 이용했다
    개수가 2가되면 바로 쌍으로 묶는다
    문자열을 다 확인했을 때 나온 쌍을 결과로 해서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0265
    {

        static void Main265(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            Dictionary<string, int> chk = new(500);

            while(test-- > 0)
            {

                int n = ReadInt(sr);
                int l = ReadInt(sr);
                int f = ReadInt(sr);

                string[] str = sr.ReadLine().Split(' ').Select(x => x.Substring(l - f)).ToArray();

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (chk.ContainsKey(str[i])) 
                    {

                        int val = chk[str[i]];

                        if (val == 1) 
                        { 

                            ret++;
                            val = -1;
                        }

                        val++;
                        chk[str[i]] = val;
                    }
                    else chk[str[i]] = 1;
                }

                chk.Clear();
                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
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
