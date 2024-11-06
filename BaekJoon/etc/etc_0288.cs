using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 19
이름 : 배성훈
내용 : 단풍잎 이야기
    문제번호 : 16457번

    브루트포스, 백트래킹 문제다
    가능한 키의 경우를 모두 찾고, 만족하는 퀘스트를 찾는 식으로했다
    탐색 방법은 DFS를 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0288
    {

        static void Main288(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);
            int k = ReadInt(sr);

            int[][] q = new int[m][];
            // 퀘스트에 필요한 키 입력
            for (int i = 0; i < m; i++)
            {

                q[i] = new int[k];
                for (int j = 0; j < k; j++)
                {

                    q[i][j] = ReadInt(sr);
                }
            }

            sr.Close();

            // 키 누름 여부용 불 배열
            bool[] calc = new bool[2 * n + 1];

            int ret = DFS(q, calc, 0, n, 1);
            Console.WriteLine(ret);
        }

        static int DFS(int[][] _q, bool[] _calc, int _depth, int _maxDepth, int _before)
        {

            int ret = 0;
            if (_depth == _maxDepth)
            {

                // 깨는 퀘스트 수 확인
                for (int i = 0; i < _q.Length; i++)
                {

                    ret++;
                    for (int j = 0; j < _q[0].Length; j++)
                    {

                        if (_calc[_q[i][j]]) continue;
                        ret--;
                        break;
                    }
                }

                return ret;
            }

            for (int i = _before; i < _calc.Length; i++)
            {

                if (_calc[i]) continue;
                _calc[i] = true;

                // 최고 수만 반환!
                int calc = DFS(_q, _calc, _depth + 1, _maxDepth, i + 1);
                ret = calc < ret ? ret : calc;
                _calc[i] = false;
            }

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var keyCount = input[0];
            var questCount = input[1];
            
            if(input[2] > input[0]){
                Console.WriteLine(0);
                return;
            }

            List<int> keys = new List<int>(keyCount * 2);
            List<List<int>> info = new List<List<int>>();
        
            for (int i = 0; i < questCount; i++) {
                var quest = Console.ReadLine().Split().Select(int.Parse).ToList();
                quest.ForEach(v => {
                    if (!keys.Contains(v)) keys.Add(v);
                });
                info.Add(quest);
            }

            int[] selectedIndexes = new int[keyCount];
            int cursor = keyCount - 1;
            
            for (int i = 0; i < keyCount; i++) {
                selectedIndexes[i] = i;
            }

            int ret = 0;
            UpdateBySelected();

            while (cursor >= 0) {
                UpdateBySelected();
                MoveNextCase();
            }
        
            Console.WriteLine(ret);

            void UpdateBySelected() {
                int clearedQuest = 0;
                int[] selected = selectedIndexes.Select(idx => keys[Math.Min(keys.Count - 1, idx)]).ToArray();
                foreach (var curInfo in info) {
                    bool success = true;
                    foreach (var required in curInfo) {
                        if (!selected.Contains(required)) {
                            success = false;
                            break;
                        }
                    }

                    if (success) clearedQuest++;
                }

                if (ret < clearedQuest) ret = clearedQuest;
            }

            void MoveNextCase() {
                for (int i = keyCount - 1, k = 1; i >= cursor; i--, k++) {
                    if (selectedIndexes[i] + k < keys.Count) {
                        selectedIndexes[i]++;
                        for (int j = i + 1; j < keyCount; j++) {
                            if(j > 0)selectedIndexes[j] = selectedIndexes[j - 1] + 1;
                        }
                        return;
                    }
                }

                cursor--;
                if (cursor < 0) return;

                selectedIndexes[cursor]++;
                
                for (int i = cursor + 1; i < selectedIndexes.Length; i++) {
                    selectedIndexes[i] = selectedIndexes[i - 1] + 1;
                }
            }
        }
    }
}
#endif
}
