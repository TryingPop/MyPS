using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 복권
    문제번호 : 1359번

    수학, 브루트포스 문제다.
    서로 독립시행이므로 당첨 번호를 속에서 확률을 찾아도 된다.
    그래서 1 ~ m을 당첨번호로 잡았다.
    이후 n개 중 m개를 택하는 경우를 잡고 여기서 불가능한 경우를 찾아 빼서 확률을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1439
    {

        static void Main1439(string[] args)
        {

            int n, m, k;

            Input();

            GetRet();

            void GetRet()
            {

                int[] fac = new int[n + 1];
                fac[0] = 1;
                for (int i = 1; i <= n; i++)
                {

                    fac[i] = fac[i - 1] * i;
                }

                int sum = Combi(n, m);
                int pop = 0;

                for (int i = 0; i < k; i++)
                {

                    pop += Combi(m, i) * Combi(n - m, m - i);
                }

                decimal ret = sum - pop;
                ret /= sum;

                Console.Write(ret);

                int Combi(int _n, int _k)
                {

                    if (_k < 0 || _k > _n) return 0;
                    return fac[_n] / (fac[_n - _k] * fac[_k]);
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                k = int.Parse(temp[2]);
            }
        }
    }
}
