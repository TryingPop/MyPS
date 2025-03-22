using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 11
이름 : 배성훈
내용 : 팰린드롬 만들기
    문제번호 : 1254

    뒤에만 끼워 넣는 경우의 팰린드롬 만드는 방법
    앞뒤 끼워넣을 수 있는 줄 알고 고민했다;

    기존 문자열에서 맨 끝 원소를 포함하는 가장 큰 팰린드롬 수를 찾아내면 된다
    그래서 앞에서부터 팰린드롬인지 조사한다
    그리고 팰린드롬이 안되면 해당 문자를 뒤에 추가하여 만들었다고 생각하고 없앤다
*/

namespace BaekJoon.etc
{
    internal class etc_0016
    {

        static void Main16(string[] args)
        {

            string str = Console.ReadLine();
            
            // 추가할 개수
            int cut = 0;

            for (int i = 0; i < str.Length; i++)
            {

                // 팰린드롬 체크
                bool success = true;
                int endIdx = str.Length - 1;
                for (int j = i; j < str.Length; j++)
                {

                    // 팰린드롬이 안된다
                    if (str[j] != str[endIdx--])
                    {

                        success = false;
                        break;
                    }
                }

                // 팰린드롬이다
                if (success)
                {

                    // 추가한 개수를 적는다
                    cut = i;
                    break;
                }
            }

            Console.WriteLine(str.Length + cut);
        }
    }
}
