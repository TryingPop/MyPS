using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 브실이의 입시전략
    문제번호 : 29723번

    자료구조, 정렬, 해시를 이용하는 문제이다
    그리디하게 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0203
    {

        static void Main203(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            Dictionary<string, int> score = new(info[0]);

            for (int i = 0; i < info[0]; i++)
            {

                // 과목 및 점수 입력
                string[] temp = sr.ReadLine().Split(' ');
                score[temp[0]] = int.Parse(temp[1]);
            }

            int ret = 0;
            for (int i = 0; i < info[2]; i++)
            {

                // 필수 과목 점수 계산
                string str = sr.ReadLine();
                ret += score[str];
                score[str] = -1;
            }

            sr.Close();

            int[] find = new int[info[0] - info[2]];
            int idx = 0;

            foreach(var item in score.Values)
            {

                // 필수 과목은 앞에서 계산해서 넘긴다
                if (item == -1) continue;
                // 필수과목이 아닌 경우 배열에 저장
                find[idx++] = item;
            }

            // 정렬
            Array.Sort(find);

            int min = ret;
            int max = ret;

            int last = find.Length - 1;
            for (int i = 0; i < info[1] - info[2]; i++)
            {

                // 최소값은 앞에서부터
                min += find[i];
                // 최대값은 뒤에서부터
                max += find[last - i];
            }

            Console.WriteLine($"{min} {max}");
        }
    }
}
