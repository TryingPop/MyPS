using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 숫자 카드 2
    문제번호 : 10816번

    왜 인덱스 에러가 났는지 모르겠다.
    int[] 로 푸는게 빠르다!
*/

namespace BaekJoon._21
{
    internal class _21_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            Dictionary<int, int> cards;

            {

                int cardNum = int.Parse(sr.ReadLine());

                int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);


                cards = new Dictionary<int, int>(cardNum);

                for (int i = 0; i < cardNum; i++)
                {

                    if (!cards.ContainsKey(inputs[i]))
                    {

                        cards[inputs[i]] = 1;
                    }
                    else
                    {

                        cards[inputs[i]]++;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            {

                int findNum = int.Parse(sr.ReadLine());

                int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                for (int i = 0; i < findNum; i++)
                {

                    if (cards.ContainsKey(inputs[i]))
                    {

                        sb.Append(cards[inputs[i]].ToString());
                    }
                    else
                    {

                        sb.Append("0");
                    }

                    if (i != findNum - 1)
                    {

                        sb.Append(" ");
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }

        }
    }

}