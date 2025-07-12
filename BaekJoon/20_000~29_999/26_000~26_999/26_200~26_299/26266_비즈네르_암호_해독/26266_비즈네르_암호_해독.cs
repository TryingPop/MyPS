using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 비즈네르 암호 해독
    문제번호 : 26266번

    2번 틀렸다 처음에는 로직문제인 줄 알았으나
    하나하나 확인하니 로직문제는 없었다

    그러면 특정 합에서 문제가 생기는 것이라 판단했고
    합이 26 즉, 0이되는 경우를 확인했다
    그러니 'Z' 처리가 되어야할 부분이 'A' - 1이라 이상한 답을 내어 틀렸다

    문제 아이디어는 다음과 같다
    우선 전체 암호를 찾는다

    그리고 주기를 찾아야한다
    따로 주기를 확인하는 아이디어가 안떠올라
    나눠떨어지는 값에 한해 주기를 일일히 확인하는 브루트 포스로 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0222
    {

        static void Main222(string[] args)
        {

            int ALPHABET = 26;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string str1 = sr.ReadLine();
            string str2 = sr.ReadLine();
            sr.Close();

            int len = str1.Length;
            int[] diff = new int[len];

            // 전체 암호구문 찾기
            for (int i = 0; i < len; i++)
            {

                diff[i] = str2[i] - str1[i];
                if (diff[i] < 0) diff[i] += ALPHABET;
            }


            int idx = len;
            int chk = 1 + len / 2;
            for (int i = 1; i <= chk; i++)
            {

                if (len % i != 0) continue;
                int calc = len / i;
                bool fail = false;

                // 반복 주기 찾기
                // 반복 길이가 i라 가정하고 안되는지 브루트포스로 확인
                for (int j = 0; j < i; j++)
                {

                    // 주기가 2라 판단하면 0번 인덱스에 대해 2, 4, 6 ,8, 10, ..., 2n
                    // 의 모든 원소와 0이 같은지 판별
                    // 같다면 이제
                    // 1번인덱스와 3, 5, 7, 9, ... , 2n - 1이 같은지 판별
                    // 같다면 주기라 처리
                    int cur = j;
                    for (int k = 1; k < calc; k++)
                    {

                        int other = j + i * k;
                        if (diff[cur] == diff[other]) continue;

                        fail = true;
                        break;
                    }

                    if (fail) break;
                }

                // 반복 안되면 다음 숫자로 올린다
                if (fail) continue;

                // 가장짧은 주기를 찾았으니 탈출
                idx = i;
                break;
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < idx; i++)
                {

                    char c = (char)(diff[i] - 1 + 'A');
                    if (c == 'A' - 1) c = 'Z';
                    sw.Write(c);
                }
            }
        }
    }
}
