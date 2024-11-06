using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 23
이름 : 배성훈
내용 : Matrix Chain Multiplication
    문제번호 : 6604번

    자료 구조, 스택, 문자열, 파싱 문제다
    조건대로 구현하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1071
    {

        static void Main1071(string[] args)
        {

            string NO = "error\n";
            (int l, int r) LEFT = (-1, -1);
            StreamReader sr;
            StreamWriter sw;

            int n;
            Dictionary<int, (int l, int r)> dic;
            string str;
            (int l, int r)[] stk;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    long ret = GetRet();
                    if (ret < 0) sw.Write(NO);
                    else sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            long GetRet()
            {

                long ret = 0;
                int len = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == ')')
                    {

                        if (stk[len - 1] == LEFT) len--;
                        else
                        {

                            (int l, int r) right = stk[len - 1];
                            len -= 2;

                            if (len == 0 || stk[len - 1] == LEFT) stk[len++] = right;
                            else
                            {

                                (int l, int r) left = stk[len - 1];
                                if (left.r != right.l) return -1;
                                ret += (left.l * right.r * left.r);
                                stk[len - 1] = (left.l, right.r);
                            }
                        }
                    }
                    else if (str[i] == '(') stk[len++] = LEFT;
                    else
                    {

                        if (len > 0 && stk[len - 1] != LEFT)
                        {

                            (int l, int r) right = dic[str[i]];
                            (int l, int r) left = stk[len - 1];
                            if (left.r != right.l) return -1;
                            ret += (left.l * right.r * left.r);
                            stk[len - 1] = (left.l, right.r);
                        }
                        else stk[len++] = dic[str[i]];
                    }
                }

                return ret;
            }

            bool Input()
            {

                str = sr.ReadLine();
                return !string.IsNullOrEmpty(str);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());
                dic = new(n);

                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    dic[temp[0][0]] = (int.Parse(temp[1]), int.Parse(temp[2]));
                }

                stk = new (int l, int r)[100];
            }
        }
    }
}
