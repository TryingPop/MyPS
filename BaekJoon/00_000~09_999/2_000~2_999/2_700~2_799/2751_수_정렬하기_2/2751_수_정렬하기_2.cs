using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 8
이름 : 배성훈
내용 : 수 정렬하기 2
    문제번호 : 2751번

    이전에 풀었던 문제이다
    블로그에 기록용으로 남기기 위해 병합정렬로 새로 풀어본다
*/

namespace BaekJoon.etc
{
    internal class etc_0006
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int[] arr = new int[len];

            for (int i = 0; i < len; i++)
            {

                int n = 0;
                int c;
                bool minus = false;
                while((c = sr.Read()) != -1 && c != '\n')
                {


                    if (c == '\r') continue;
                    else if (c == '-') 
                    { 
                        
                        minus = true;
                        continue;
                    }

                    n *= 10;
                    n += c - '0';
                }

                if (minus) arr[i] = -n;
                else arr[i] = n;
            }

            sr.Close();

            int[] dp = new int[len];

            MergeSort(arr, 0, len - 1, dp);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < len; i++)
                {

                    sw.Write(arr[i]);
                    sw.Write('\n');
                }
            }
        }

        static void MergeSort(int[] _arr, int _start, int _end, int[] _calc)
        {

            // 탈출
            if (_start == _end) return;

            // 분할
            int mid = (_start + _end) / 2;
            MergeSort(_arr, _start, mid, _calc);
            MergeSort(_arr, mid + 1, _end, _calc);

            // 정복
            // 투 포인트 알고리즘
            int left = _start;
            int right = mid + 1;
            int resultIdx = _start;

            while (true)
            {

                if (left > mid || right > _end) break;

                if (_arr[left] > _arr[right]) _calc[resultIdx++] = _arr[right++];
                else _calc[resultIdx++] = _arr[left++];
            }

            while(left <= mid)
            {

                _calc[resultIdx++] = _arr[left++];
            }

            while(right <= _end)
            {

                _calc[resultIdx++] = _arr[right++];
            }

            for (int i = _start; i <= _end; i++)
            {

                _arr[i] = _calc[i];
            }
        }
    }
}
