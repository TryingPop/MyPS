/*
날짜 : 2024. 2. 25
이름 : 배성훈
내용 : 치킨 TOP N
    문제번호 : 11582번

    마지막에 주어지는 사람 변수를 정복 몇 번째 과정으로 잘못 해석해서 1번 틀렸다
    분할정복 정렬이 어떻게 이루어지는지 묻는 문제다

    따로 정렬을 정의 하는게 아닌 내장 Array.Sort메서드를 이용해 풀었다
    전부 진행할 필요가 없기에 해당 사람 수에 맞는 개수를 묶어서 정렬 했다
    400ms 걸리는거 보면 2^20개 예제가 있는거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0095
    {

        static void Main95(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[] arr = new int[len];

            for (int i = 0; i < len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            int human = ReadInt(sr);
            sr.Close();

            int sortLen = len / human;
            int[] sortArr = new int[sortLen];

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < human; i++)
            {

                // 값 갱신
                for (int j = 0; j < sortLen; j++)
                {

                    sortArr[j] = arr[j + sortLen * i];
                }

                // 정렬
                Array.Sort(sortArr);
                for (int j = 0; j < sortLen; j++)
                {

                    sw.Write(sortArr[j]);
                    sw.Write(' ');
                }
            }
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
