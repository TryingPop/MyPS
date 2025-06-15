# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 브루트포스 알고리즘
  - 비트마스킹
  - 백트래킹

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Your local gym has b bars and p plates for barbells. In order to prepare a weight for lifting, you must choose a single bar, which has two sides. You then load each side with a (possibly empty) set of plates. For safety reasons, the plates on each side must balance; they must sum to the same weight. The combination of plates on either side might be different, but the total weight on either side must be the same. What weights are available for lifting?<br/>


## 입력
Each input will consist of a single test case. Note that your program may be run multiple times on different inputs. The first line of input contains two integers, b and p (1 ≤ b,p ≤ 14), representing the number of bars and plates. Then, there are b lines each containing a single integer x (1 ≤ x ≤ 108 ) which are the weights of the bars. After that, there are p lines each containing a single integer y (1 ≤ y ≤ 108 ) which are the weights of the plates.<br/>


## 출력
Output a sorted list of all possible lifting weights, one per line. There must be no duplicates.<br/>


## 예제 입력
2 5<br/>
100<br/>
110<br/>
5<br/>
5<br/>
1<br/>
4<br/>
6<br/>


## 예제 출력
100<br/>
110<br/>
112<br/>
120<br/>
122<br/>
130<br/>


## 풀이
플레이트를 양쪽에 놓거나 안놓는 경우 가능한 경우는 플레이트가 14개 이므로 3^14이다.<br/>
그래서 브루트포스로 플레이트를 양쪽에 놓으면서 무게가 같은 경우를 찾았다.<br/>


양쪽이 같은 경우는 왼쪽을 기준으로만 보면 가능한 경우는 2^14개 있다.<br/>
막대기가 14개 있으므로 가능한 무게는 14 x 2^14개가 가능하다.<br/>
그리고 중복을 제외하고 오름차순으로 출력해야 한다.<br/>


HashSet에 양쪽이 같은 경우 무게를 저장했다.<br/>
그러면 중복 확인은 HashsSet에서 해주기에 오름차순 정렬해서 원소들을 읽었다.<br/>
오름차순 정렬은 Linq의 OrderBy를 이용했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/13751