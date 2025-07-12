# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Every day before Mirko goes to work, he connects to the Internet and reads mail from his boss that contains a list of Mirko's jobs for that day. Each job is defined by its starting time and duration.<br/>
Mirko's work shift lasts N minutes. When Mirko arrives to work, he starts processing his jobs. If there are more jobs he has to start working on at the same time, he can choose which one he will process and other jobs are will be processed by his co-workers. When he finishes one job, he read newspapers until next job starts. If Mirko is busy processing one job when another job starts, he will not process that other job not even after he finishes current one.<br/>
When more jobs are starting at the same time by choosing which job to process Mirko can increase amount of time he will be free to read newspapers.<br/>
Write a program that will help Mirko decide which jobs to process in order to maximize his free time.<br/>


## 입력
First line of the input file contains two integers: N and K, 1 ≤ N ≤ 10000, 1 ≤ K ≤ 10000. N represents how many minutes lasts Mirko's work shift. K represents number of jobs.<br/>
Each of the next K lines contains data about one job: integers P and T, meaning that job is starting in Pth minute and its duration is T minutes. 1 ≤ P ≤ N, 1 ≤ P+T-1 ≤ N.<br/>


## 출력
Write to the output file the maximum number of minutes Mirko can spend reading newspapers.<br/>


## 예제 입력
10 3<br/>
1 2<br/>
4 3<br/>
5 1<br/>

## 예제 출력
5<br/>


## 풀이
쉬는 시간 = 전체 시간 - 일하는 시간이므로 쉬는 시간의 최댓값은 일하는 시간의 최솟값과 같다.<br/>
그래서 일하는 시간의 최솟값을 찾았다.<br/>


쉬고 있을 때 일이 있으면 반드시 그 일을 해야 한다.<br/>
쉬는 시간 시작 시간에서 이후 가장 가까운 일을 찾을 수 있어야 한다.<br/>
일을 시작 시간 순서로 정렬한 뒤 이분탐색으로 찾는다면 log k에 찾을 수 있어 일을 시작 시간으로 정렬했다.<br/>


같은 시간에 일이 여러 개라면 원하는 일을 선택할 수 있다.<br/>
일하는 시간이 최소인 경우로 일을 선택 하고 시간을 저장한다면 그리디로 최소 시간을 찾을 수 있다.<br/>


최소 시간을 찾는데 dp의 방법을 이용했다.<br/>
dp[i] = val를 일을 앞에서 정렬했으므로 정렬된 i번째 일을 할 경우 일하는 최소 시간 val을 저장한다.<br/>


i번째 일을 하므로 i번째 일하는 시간을 t[i], 시작시간을 p[i]라 하자.<br/>
그리고 p[i] + t[i]이상이면서 가장 가까운 다음 일의 시간 next를 이분 탐색으로 찾는다.<br/>
그리고 next 시간인 일을 선택해야 하므로 최솟값 함수 Min에 대해 dp[i] = t[i] + Min(dp[j]), j는 p[j] = next인 j임을 알 수 있다.<br/>


초기 일을 시작 시간을 기준으로 정렬하므로 k log k번 연산을 한다.<br/>
그리고 일의 최소 시간을 찾는데 중복 방문을 허용하지 않으면 많아야 k개의 일을 확인한다.<br/>
그리고 매 경우 다음 일의 시간을 확인하기 위해 이분탐색을 하므로 log k의 연산을 한다.<br/>
그래서 최소 일을 찾는 연산은 k log k번 한다.<br/>
따라서 시간 복잡도는 O(k log k + k log k) = O(k log k)가 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/3263