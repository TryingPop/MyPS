using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 26
이름 : 배성훈
내용 : 카드 정렬하기
    문제번호 : 1715번

    그리디, 우선순위 큐 문제다
    우선순위큐를 써서 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0913
    {

        static void Main913(string[] args)
        {

            StreamReader sr;
            PriorityQueue<int, int> pq;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long ret = 0;

                while(pq.Count > 1)
                {

                    int a = pq.Dequeue();
                    int b = pq.Dequeue();

                    ret += a + b;
                    pq.Enqueue(a + b, a + b);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pq = new(n);
                for (int i = 0; i < n; i++)
                {

                    int add = ReadInt();
                    pq.Enqueue(add, add);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ConsoleApp2
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(Console.OpenStandardInput());
        static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            int[] numArr = new int[1001];
            long count = 0;
            for(int i = 0; i < N; i++)
            {
                numArr[int.Parse(sr.ReadLine())]++;
            }
            // 여기까지 입력

            // 기본적인 구성은
            // 10, 20, 40, 50이 있을때
            // numArr[10], numArr[20], numArr[40], numArr[50] = 1
            // numArr[30], numArr[40], numArr[50] = 1
            // numArr[50], numArr[70] = 1    
            // numArr[120] = 1;
            // for 문을 순차적으로 돌면서 값이 있으면 더해서 더한 값 인덱스 + 1
            // 단 1000까지만 입력이 들어오므로 해당 방식을 사용하면
            // 1000이상은 무조건 값이 순차적으로 들어감
            // 따라서 큐를 사용하여 값을 차곡차곡 누적해줌
            // 그리고 위와 같은 방식으로 두 개씩 빼서 더하고 끝에 누적
            Queue<int> q = new Queue<int>();
            int bNum = 0;
            for (int i = 0; i <= 1000; i++)
            {
                for(int j = 0; j < numArr[i]; j++)
                {
                    if(bNum == 0)
                        bNum = i;
                    else
                    {
                        count += bNum + i;
                        if (bNum + i <= 1000)
                            numArr[bNum + i]++;
                        else
                            q.Enqueue(bNum + i);
                        bNum = 0;
                    }
                }
            }

            // q에 값이 들어갔으면 아직 끝이 아닌데 합산 안하는 경우 방지
            if(q.Count >= 1 && bNum != 0)
            {
                int a = bNum;
                int b = q.Dequeue();
                count += a + b;
                q.Enqueue(a + b);
            }

            // 위에 설명했던대로 값 적은 것부터 하나씩 빼서 누적
            while(q.Count > 1)
            {
                int a = q.Dequeue();
                int b = q.Dequeue();
                count += a + b;
                q.Enqueue(a + b);
            }

            sw.WriteLine(count);
            sw.Flush();
            sr.Close();
            sw.Close();
        }
    }
}

#endif
}
