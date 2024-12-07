# C#

## 난이도 : 플레티넘 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 2048 MB

## 문제
You are now in charge of two programming interns, and you must develop a large system. There are a number of tasks that need to be completed by the end of the summer. You know how long each intern will take to complete each task, in minutes.<br/>
Compute the minimum number of minutes it will take to complete all tasks for development of the system, assuming that the two interns are the only developers, that they work independently and concurrently, that they do not share tasks, and that the amount of time it takes an intern to complete all their tasks is the sum of the number of minutes it takes to do each task one after the other.<br/>


## 입력
The first line of input contains a single integer n (1 ≤ n ≤ 50), which is the number of tasks.<br/>
Each of the next n lines contains two integers a and b (1 ≤ a,b ≤ 10^5). Each line represents a single task, where a is the number of minutes it will take the first intern to complete the task, and b is the number of minutes it will take the second intern to complete the task.<br/>


## 출력
Output a single integer, which is the minimum number of minutes needed to complete the development project.<br/>

## 예제 입력
4<br/>
100 1<br/>
1 90<br/>
1 20<br/>
1 20<br/>

## 예제 출력
3<br/>


## 풀이
dp[i][j] = val를 i번째까지 물건을 선택했을 때 a가 j시간만큼 일하고 b가 val만큼 일한 것을 나타낸다.<br/>
최소 시간을 찾기에 val는 최소가 되게 담아야 한다.<br/>
그리고 dp 탐색 방법은 배낭 방법으로 탐색하면 된다.<br/>


최대 일하는 시간이 500만이고, 물건은 50개 있다. 그래서 약 2.5억 C#이 추가시간 받으면 풀만하다 여겨 배낭으로 시도했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/31949