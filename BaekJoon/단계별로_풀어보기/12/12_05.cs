using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 8
이름 : 배성훈
내용 : 영단어 암기는 괴로워
    문제번호 : 20920번

    조건에 맞춰 정렬하기
    나오는 빈도수, 길이가 길수록, 사전에 나오는 순서대로 우선순위를 두어 문자열 정렬

    10만개 이상의 데이터 관리가 필요하므로 입출력 버퍼 관리가 중요하다!

    정렬은 Sort이용 방법과 Linq 문법을 이용한 Orderby가 있다

    입출력 버퍼 관리를 하기위해 StreamReader, StreamWriter를 이용하고,
    빠른 정답 출력을 위해 StringBuilder 이용
*/

namespace BaekJoon._12
{
    internal class _12_05
    {
        
        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] chk = Array.ConvertAll(sr.ReadLine().Split(' '), input => int.Parse(input));
            int length = chk[0];
            int min = chk[1];

            List<string> words = new List<string>();
            Dictionary<string, int> counts = new Dictionary<string, int>();

            for (int i = 0; i < length; i++)
            {

                string word = sr.ReadLine();

                if (word.Length < min)
                {

                    continue;
                }

                if (counts.TryAdd(word, 0))
                {

                    words.Add(word);
                }
                else
                {

                    counts[word] += 1;
                }

                /*
                if (counts.ContainsKey(word))
                {

                    counts[word] += 1;
                }
                else
                {

                    words.Add(word);
                    counts[word] = 1;
                }
                */
            }
            sr.Close();

            // 정렬
            words.Sort((x, y) =>
            {

                int rs1 = -counts[x].CompareTo(counts[y]);
                int rs2 = -(x.Length).CompareTo(y.Length);
                int rs3 = string.Compare(x, y);

                return rs1 != 0 ?
                rs1 : rs2 != 0 ?
                rs2 : rs3;
            });

            // words = words.OrderByDescending(word => counts[word]).
            //     ThenByDescending(word => word.Length).
            //     ThenBy(word => word).
            //     ToList();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Count; i++) 
            {

                sb.AppendLine(words[i]);
            }

            // 출력 버퍼 관리 -> 속도 향상용
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            Console.WriteLine(sb);
            sw.Flush();
            sw.Close();

            /*
            string[] inputs = new string[length];
            int[] counts = new int[length];

            for (int i = 0; i < length; i++)
            {

                inputs[i] = Console.ReadLine();

                // 길이 확인
                if (inputs[i].Length < min)
                {
                    inputs[i] = null;
                    i--;
                    length--;

                    continue;
                }

                // 같은 문자 보유 중인지 확인
                for (int j = 0; j < i; j++)
                {

                    if (inputs[j] == inputs[i])
                    {

                        inputs[i] = null;
                        i--;
                        length--;

                        counts[j]++;
                        break;
                    }
                }
            }

            // 정렬된 인덱스
            int[] result = new int[length];

            for (int i = 0; i < length; i++)
            {

                bool change = false;

                for (int j = 0; j < i; j++)
                {

                    // 빈도수 확인
                    if (counts[i] > counts[result[j]])
                    {

                        // 빈도수가 큰 경우
                        // 한 칸씩 뒤로 미뤄서 재정렬
                        for (int k = i - 1; k >= j; k--)
                        {

                            result[k + 1] = result[k]; 
                        }

                        result[j] = i;
                        change = true;
                        break;
                    }

                    if (counts[i] == counts[result[j]])
                    {

                        // 빈도수가 같은 경우

                        // 문자열 길이 확인
                        if (inputs[i].Length > inputs[result[j]].Length)
                        {

                            // 길이가 긴 경우
                            // 빈도수가 큰 경우
                            for (int k = i - 1; k >= j; k--)
                            {

                                result[k + 1] = result[k];
                            }

                            result[j] = i;
                            change = true;
                            break;
                        }
                        else if (inputs[i].Length == inputs[result[j]].Length)
                        {

                            // 길이가 같은경우

                            // 알파벳 순서 확인
                            for (int k = 0; k < inputs[i].Length; k++)
                            {

                                // 사전으로 먼저 배열 되었는지 확인
                                // 소문자만 있어서 따로 변환 안했다
                                if (inputs[i][k] > inputs[result[j]][k])
                                {

                                    // 
                                    for (int l = i - 1; l >= j; l--)
                                    {

                                        result[l + 1] = result[l];
                                    }

                                    result[j] = i;
                                    change = true;
                                    break;
                                }
                            }

                            if (change)
                            {

                                break;
                            }
                        }
                    }
                }

                if (!change)
                {

                    result[i] = i;
                }
            }

            for (int i = 0; i < length; i++)
            {

                Console.WriteLine(inputs[result[i]]);
            }
            */
        }
    }
}
