using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 23
이름 : 배성훈
내용 : 수 정렬하기 2
    문제번호 : 2751번

    정렬 문제로 연습한 문제다;
*/

namespace BaekJoon.etc
{
    internal class etc_0772
    {

        static void Main772(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int len;

            Solve();

            void Solve()
            {

                Input();

                HeapSort();

                Output();
            }

            void HeapSort()
            {

                for (int i = 0; i < len; i++)
                {

                    Up(i);
                }

                Swap(0, len - 1);

                for (int i = len - 2; i >= 1; i--)
                {

                    Down(i);
                    Swap(0, i);
                }
            }

            void Up(int _idx)
            {

                while(_idx > 0)
                {

                    int parent = (_idx - 1) / 2;

                    if (arr[_idx].CompareTo(arr[parent]) <= 0) return;

                    Swap(_idx, parent);
                    _idx = parent;
                }
            }

            void Swap(int _idx1, int _idx2)
            {

                int temp = arr[_idx1];
                arr[_idx1] = arr[_idx2];
                arr[_idx2] = temp;
            }

            void Down(int _len)
            {

                int idx = 0;
                while (idx < _len)
                {

                    int l = 2 * idx + 1;
                    int r = 2 * idx + 2;

                    if (l <= _len)
                    {

                        if (r <= _len)
                        {

                            int child;
                            if (arr[l].CompareTo(arr[r]) > 0) child = l;
                            else child = r;

                            if (arr[idx].CompareTo(arr[child]) < 0)
                            {

                                Swap(idx, child);
                                idx = child;
                                continue;
                            }
                        }
                        else if (arr[idx].CompareTo(arr[l]) < 0)
                        {

                            Swap(idx, l);
                            idx = l;
                            continue;
                        }
                    }

                    return;
                }
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{arr[i]}\n");
                }
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                len = ReadInt();
                arr = new int[len];
                for (int i = 0; i < len; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
