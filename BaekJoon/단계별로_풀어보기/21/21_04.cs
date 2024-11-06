using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 나는야 포켓몬 마스터 이다솜
    문제번호 : 1620번

    딕셔너리와 배열을 이용하는게 좋다
    딕셔너리에서 >> 값으로 키를 찾는 First를 이용하는 것은 시간이 오래 걸린다 
*/
namespace BaekJoon._21
{
    internal class _21_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            Dictionary<string, int> pocketInt = new Dictionary<string, int>(info[0]);
            string[] array = new string[info[0] + 1];
            for (int i = 1; i <= info[0]; i++)
            {
                string str = sr.ReadLine();

                pocketInt[str] = i;
                array[i] = str;
            }

            // Dictionary<int, string> pocketString = pocketInt.ToDictionary
            //     <KeyValuePair<string, int>, int, string>(x => x.Value, x => x.Key);
            
            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < info[1]; i++)
            {

                string input = sr.ReadLine();

                if (input[0] > '9')
                {

                    sb.AppendLine(pocketInt[input].ToString());
                }
                else
                {

                    sb.AppendLine(array[int.Parse(input)]);
                }
            }
            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
                sw.WriteLine(sb);

        }
    }
}
