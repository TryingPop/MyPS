using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 29
이름 : 배성훈
내용 : Multithreaded Program
    문제번호 : 24650번

    해 구성하기, 그리디 알고리즘, 해시 문제다.
    아이디어는 단순하다.
    뒤에서부터 일치하는 것을 먼저 실행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1848
    {

        static void Main1848(string[] args)
        {

            int n, sum;                         // Thread 개수
            Stack<(string key, int val)>[] func;// Thread가 할일들
            Dictionary<string, int> ret;        // 결과 변수들
            Stack<int> order;

            Input();

            GetRet();

            Output();

            void Output()
            {

                // 출력 함수
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                if (order.Count < sum)
                {

                    sw.Write("No");
                    return;
                }

                sw.Write("Yes\n");
                while (order.Count > 0)
                {

                    sw.Write($"{order.Pop()} ");
                }
            }

            void GetRet()
            {

                // 정답 찾기
                order = new(sum);
                bool flag = true;
                while (flag)
                {

                    flag = false;

                    // 결과와 일치하는게 있는지 확인
                    for (int i = 0; i < n; i++)
                    {

                        while (func[i].Count > 0)
                        {

                            var chk = func[i].Peek();
                            if (ret.ContainsKey(chk.key)
                                && ret[chk.key] == chk.val)
                            {

                                flag = true;
                                func[i].Pop();
                                order.Push(i + 1);
                                ret.Remove(chk.key);
                            }
                            else break;
                        }
                    }

                    // 결과가 없는 경우 빼기
                    for (int i = 0; i < n; i++)
                    {

                        while (func[i].Count > 0 && !ret.ContainsKey(func[i].Peek().key))
                        {

                            flag = true;
                            func[i].Pop();
                            order.Push(i + 1);
                        }
                    }
                }
            }

            void Input()
            {

                // 입력 함수
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());
                sum = 0;

                func = new Stack<(string key, int val)>[n];
                for (int i = 0; i < n; i++)
                {

                    int m = int.Parse(sr.ReadLine());
                    func[i] = new(m);

                    for (int j = 0; j < m; j++)
                    {

                        string[] temp = sr.ReadLine().Split('=');
                        func[i].Push((temp[0], int.Parse(temp[1])));
                    }

                    sum += m;
                }

                int k = int.Parse(sr.ReadLine());
                ret = new(k);

                for (int i = 0; i < k; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    ret[temp[0]] = int.Parse(temp[1]);
                }
            }
        }
    }
}
