using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 9단계 3번 문제
 * 
 * 재귀함수가 뭔가요?
 */

namespace BaekJoon._09
{
    internal class _09_03
    {
        static void Main3(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            SelfFunc(num);

        }

        // 타입이 불분명한건 그냥 void 하고 return; 하면 된다
        static void SelfFunc(int n, int m = 0)
        {
            if (n <= 0)
            {
                for (int i = 1; i <= m; i++)
                {
                    Console.Write("____");
                }
                Console.WriteLine("\"재귀함수가 뭔가요?\"");
                for (int i = 1; i <= m; i++)
                {
                    Console.Write("____");
                }
                Console.WriteLine("\"재귀함수는 자기 자신을 호출하는 함수라네\"");
                for (int i = 1; i <= m; i++)
                {
                    Console.Write("____");
                }
                Console.WriteLine("라고 답변하였지.");
                return;
            }
            LemmaFunc1(m);
            SelfFunc(n - 1, m+1);
            LemmaFunc2(m);
        }

        static void LemmaFunc1(int m)
        {
            if (m == 0)
            {
                Console.WriteLine("어느 한 컴퓨터공학과 학생이 유명한 교수님을 찾아가 물었다.");
            }
            for (int i = 1; i <= m; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("\"재귀함수가 뭔가요?\"");
            for (int i = 1; i <= m; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("\"잘 들어보게. 옛날옛날 한 산 꼭대기에 이세상 모든 지식을 통달한 선인이 있었어.");
            for (int i = 1; i <= m; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("마을 사람들은 모두 그 선인에게 수많은 질문을 했고, 모두 지혜롭게 대답해 주었지.");
            for (int i = 1; i <= m; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("그의 답은 대부분 옳았다고 하네. 그런데 어느 날, 그 선인에게 한 선비가 찾아와서 물었어.\"");
        }

        static void LemmaFunc2(int m)
        {
            for (int i = 1; i <=m; i++)
            {
                Console.Write("____");
            }
            Console.WriteLine("라고 답변하였지.");
        }
    }
}
