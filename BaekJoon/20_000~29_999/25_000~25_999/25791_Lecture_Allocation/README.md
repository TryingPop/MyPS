# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
You are the coordinator for a competitive programming club. You need to hire some teachers to give lectures. There are a fixed number of lectures that need to be given this year. Additionally, there are a limited number of teachers that are willing to give lectures. Each teacher can teach up to three lectures, but not all the teachers need to teach a lecture, i.e., a teacher could teach 0, 1, 2, or 3 lectures. Each teacher charges a different amount depending on the number of lectures they give.<br/>
The money not spent will be used to fly the team to other contests, so you want to spend as little money as possible hiring enough teachers to give all the lectures.<br/>
Given the number of lectures to teach and how much each teacher charges for giving the lectures, determine the least amount of money necessary such that all the lectures will be taught.<br/>


## 입력
The first input line contains two integers, L and T (1 ≤ L ≤ 5000, L/3 ≤ T ≤ L), representing (respectively) the number of lectures and the number of teachers. Each of the following T input lines contains three integers, the i th of which contains ai1, ai2, and ai3 (0 < ai1 < ai2 < ai3 ≤ 100,000), representing (respectively) how much the i th teacher charges to give 1, 2, and 3 lectures.<br/>


## 출력
Print on a single line by itself a single positive integer: the least cost for paying the teachers to cover all L lectures. Assume that there are enough teachers to cover all the lectures.<br/>


## 예제 입력
4 3<br/>
8 10 20<br/>
10 20 30<br/>
11 17 25<br/>


## 예제 출력
27<br/>


## 힌트
For the Sample Input, the first teacher can give two lectures and the third teacher can give two lectures, so the total cost is 10 + 17 = 27.<br/>


## 풀이
강의 수는 많아야 5000이다.<br/>
그리고 강사 수는 5000이하이다.<br/>
그래서 dp[i] = val를 들은 강의 수 i에 가장 싼 가격 val를 배낭형식으로 찾아가도 5000 x 5000 x 3으로 유효하다 판단했다.<br/>


k번째 강사이고 j는 강의 신청 횟수에 대해 arr[k][j]는 k 강사의 j번 강의 들을 때 비용이라 하자.<br/>
그러면 dp[i]가 방문한 곳이면 다음과 같은 점화식이 나온다. dp[i + j] = min(dp[i] + arr[k][j], dp[i + j])<br/>


이렇게 배낭 형식으로 접근해 정답을 찾았다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/25791