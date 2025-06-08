using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 자리수로 나누기
    문제번호 : 1490번

    수학, 브루트포스 문제다
    for문 범위를 잘못 기입해 한 번 틀렸다

    경우의 수가 많아야 9!만큼 확인하기에 뒤에 숫자를 이어붙여가며 
    n에 자릿수 숫자로 나눠 떨어지는지 확인하는 브루트포스로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0405
    {

        static void Main405(string[] args)
        {

            string str = Console.ReadLine();

            bool[] chk = new bool[10];
            for (int i = 0; i < str.Length; i++)
            {

                chk[str[i] - '0'] = true;
            }

            long num = long.Parse(str);

            Queue<long> q = new Queue<long>(100_000);

            q.Enqueue(num);

            long ret = 0;
            while(q.Count > 0)
            {

                long node = q.Dequeue();

                bool find = true;
                for (int i = 1; i < 10; i++)
                {

                    if (!chk[i]) continue;
                    if (node % i != 0)
                    {

                        find = false;
                        break;
                    }
                }

                if (find)
                {

                    ret = node;
                    q.Clear();
                    break;
                }

                node *= 10;
                
                for (int i = 0; i < 10; i++)
                {

                    q.Enqueue(node + i);
                }
            }

            Console.WriteLine(ret);
        }
    }
}
