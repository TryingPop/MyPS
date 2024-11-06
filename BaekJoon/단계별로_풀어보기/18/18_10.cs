using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 좌표 압축
    문제번호 : 18870번
*/

namespace BaekJoon._18
{
    internal class _18_10
    {

        static void Main10(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            int len = int.Parse(Console.ReadLine());

            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
#if false

            Dictionary<int, int> order = new Dictionary<int, int>(100_000);
            int pre = int.MinValue;
            int count = 0;
            foreach (var item in nums.OrderBy(x => x))
            {

                if (item > pre)
                {

                    pre = item;
                    order[item] = count++;
                }
            }

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            for (int i = 0; i < len; i++)
            {

                sw.Write($"{order[nums[i]]} ");
            }

            sw.Write('\n');
#else

            // 다른 사람 풀이
            (int input, int idx)[] chk = new (int input, int idx)[len];

            // 입력값, 인덱스 값을 튜플로 저장한다
            for (int i = 0; i < len; i++)
            {

                chk[i] = (nums[i], i);
            }

            // input > idx 순서로 오름차순 정렬
            Array.Sort(chk);
            
            
            int[] result = new int[len];
            int newIndex = 0;

            // input에 따라 인덱스가 정렬 되어져 있다
            
            // 처음껀 어차피 0이니 인덱스를 안찾아도 된다
            // 그래서 i = 1부터 시작해도 이상 없다
            // 만약 추가하고 싶거나 초기 값이 1이라면 for문 위에 
            // result[chk[i].idx] = newIndex;
            // 를 넣어주면 된다
            for (int i = 1; i < len; i++)
            {

                // input 값이 다른 경우 즉, 순위가 올라가는 경우
                if (chk[i - 1].input != chk[i].input)
                {

                    // 순위 값 추가
                    newIndex++;
                }
                
                // 순위 값을 인덱스에 넣는다
                result[chk[i].idx] = newIndex;
            }

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            sw.Write(string.Join(' ', result));
#endif
            sw.Close();

        }
    }
}
