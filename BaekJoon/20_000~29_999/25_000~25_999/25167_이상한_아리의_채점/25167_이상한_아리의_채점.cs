using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 이상한 아리의 채점
    문제번호 : 25167번

    정렬 문제 -> 사전식 이름 정렬 부분에서 오타로 많이 틀렸다;
        ret = name.CompareTo(other.name)이 아닌,
        name.CompareTo(other.name)만 해서 여러 번 틀렸다
    
    
    정렬을 원하는 형태로만 할 수 있고 해당 문제를 구현 할 수 있으면 쉽게 풀린다

    아이디어는 다음과 같다
    문제에 이름과 틀린 시간, 맞춘 시간을 기록하고 이 두 변수로 점수를 부여하고 점수로 정렬해야한다
    그리고 정렬 했을 때 누구의 것인지 알기위해서 사람을 가리키는 변수도 필요하다

    그래서 문제라는 새로운 구조체에
        틀린 시간, 정답 시간, 이름의 인덱스, 그리고 점수를 정의했다

    시간을 저장하는 단위로 분으로 잡았다
    시험 시간 외의 시간은 주어지지 않기에 따로 적합성은 체크 안했다
    그리고 조건에 맞게 한 번 맞췄다면 따로 채점을 안해야한다
    그래서 정답이 기록되어져 있다면 세팅되게 했다

    그리고 이를 바탕으로 점수를 도출하는데, 조건에 맞는 경우만 클리어 시간 - 틀린 시간으로 기록했다 
        -> 이경우 많아야 720점 못넘는다!
    그리고 조건에 맞춰 시도했으나 못맞춘 경우 9_999점, 이외 점수 취급 안해주면 10_000점 줬다
    다음으로 어차피 사람 이름 인덱스를 포함하고 있으니 점수에 오름차순으로 정렬되게 구조체를 설정했다

    그다음으로 사람도 새로운 구조체로 정의했다 랭킹 점수와 이름 인덱스를 저장한 자료구조다
    랭킹 점수가 같은 경우 사전식 이름 순으로 정렬되게 설정했다

    이외는 조건대로만 해주면 이상없다!    
*/

namespace BaekJoon.etc
{
    internal class etc_0066
    {

        static void Main66(string[] args)
        {

            string WRONG = "wrong";
            string SOLVE = "solve";
            
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            Problem[][] problem = new Problem[info[0] + 1][];
            Dictionary<string, int> nameToIdx = new(info[1]);
            Human[] humans = new Human[info[1]];
            {


                string[] temp = sr.ReadLine().Split(' ');

                for (int i = 0; i < info[1]; i++)
                {

                    nameToIdx.Add(temp[i], i);
                    humans[i].idx = i;
                    humans[i].name = temp[i];
                }
            }

            for (int i = 1; i <= info[0]; i++)
            {

                problem[i] = new Problem[info[1]];

                for (int j = 0; j < info[1]; j++)
                {

                    problem[i][j].Set(j);
                }
            }

            for (int i = 0; i < info[2]; i++)
            {

                // 입력값 기록
                string[] temp = sr.ReadLine().Split(' ');

                int p = int.Parse(temp[0]);
                int time = TimeToNumber(temp[1]);
                int idx = nameToIdx[temp[2]];
                bool isWrong = temp[3] == WRONG;

                problem[p][idx].RecordScore(time, isWrong);
            }

            sr.Close();

            for (int i = 1; i <= info[0]; i++)
            {

                // 점수 계산
                for (int j = 0; j < info[1]; j++)
                {

                    problem[i][j].CalcScore();
                }

                Array.Sort(problem[i]);

                int beforeScore = -1;
                int rank = 0;
                for (int j = 0; j < info[1]; j++)
                {

                    // 해당 문제 랭킹 점수 부여
                    int curScore = problem[i][j].score;
                    if (beforeScore != curScore)
                    {

                        rank += 1;
                        beforeScore = curScore;
                    }

                    
                    if (curScore == 10_000) humans[problem[i][j].idx].rank += info[1] + 1;
                    else if (curScore == 9_999) humans[problem[i][j].idx].rank += info[1];
                    else humans[problem[i][j].idx].rank += rank;
                }
            }

            // 랭킹 정렬해서 출력
            Array.Sort(humans);
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < humans.Length; i++)
                {

                    sw.WriteLine(humans[i].name);
                }
            }
        }

        struct Human : IComparable<Human>
        {

            public string name;
            public int idx;
            public int rank;

            public int CompareTo(Human other)
            {

                // 랭킹 점수 -> 사전식 이름 순 정렬!
                int ret = rank.CompareTo(other.rank);
                if (ret == 0) ret = name.CompareTo(other.name);
                return ret;
            }
        }

        struct Problem : IComparable<Problem>
        {

            private int wrongTime;
            private int clearTime;

            public int score;
            public int idx;

            public void Set(int _idx)
            {

                wrongTime = 2_000;
                clearTime = 2_000;

                score = 10_000;
                idx = _idx;
            }

            public void RecordScore(int score, bool isWrong)
            {

                
                if (isWrong && wrongTime == 2_000 && clearTime == 2_000)
                {

                    // 아직 맞추지 않았을 경우
                    // 틀린 시간 기록가능!
                    wrongTime = score;
                }
                else if (!isWrong && clearTime == 2_000)
                {

                    // 맞춘 시간은 기록이 안됐으면 기록가능!
                    clearTime = score;
                }
            }

            public void CalcScore()
            {

                // 틀린 기록 없으면 10_000점
                if (wrongTime == 2_000) return;
                // 시도만 했을 경우 9_999점
                else if (clearTime == 2_000)
                {

                    score--;
                    return;
                }

                // 적합한 경우
                score = clearTime - wrongTime;
            }

            public int CompareTo(Problem other)
            {

                // 점수순으로 정렬
                return score.CompareTo(other.score);
            }
        }

        static int TimeToNumber(string _time)
        {

            int hour = 0;
            int calc = 0;
            for (int i = 0; i < _time.Length; i++)
            {

                if (_time[i] == ':')
                {

                    calc = i;
                    break;
                }
                hour = hour * 10 + _time[i] - '0';
            }

            int min = 0;
            for (int i = calc + 1; i < _time.Length; i++)
            {

                min = min * 10 + _time[i] - '0';
            }

            int ret = 60 * hour + min;
            return ret;
        }
    }
}
