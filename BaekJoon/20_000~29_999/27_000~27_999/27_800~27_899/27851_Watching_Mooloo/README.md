# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 그리디 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
Bessie likes to watch shows on Mooloo. Because Bessie is a busy cow, she has planned a schedule for the next N (1 ≤ N ≤ 10^5) days that she will watch Mooloo. Because Mooloo is a paid subscription service, she now needs to decide how to minimize the amount of money she needs to pay.<br/>
Mooloo has an interesting subscription system: it costs d + K(1 ≤ K ≤ 10^9) moonies to subscribe to Mooloo for d consecutive days. You can start a subscription at any time, and you can start a new subscription as many times as you desire if your current subscription expires. Given this, figure out the minimum amount of moonies Bessie needs to pay to fulfill her schedule.<br/>


## 입력
The first line contains integers N and K.<br/>
The second line contains N integers describing the days Bessie will watch Mooloo:<br/>
1 ≤ d_1 < d_2 < ... < d_N ≤ 10^14.<br/>


## 출력
Note that the large size of integers involved in this problem may require the use of 64-bit integer data types (e.g., a "long long" in C/C++).<br/>


## 예제 입력
2 3<br/>
1 10<br/>


## 예제 출력
8<br>


## 힌트
Bessie first buys a one-day subscription on day 1, spending d+K = 1+3 = 4 moonies. Bessie also buys a one-day subscription on day 10, spending d+K = 1+3 = 4 moonies. In total, Bessie spends 8 moonies.<br/>


## 풀이
그리디로 i번째 날짜에 구독료가 최소가되게 1번째부터 n번째까지 진행하는 것이 전체의 최소임이 보장된다.<br/>
그래서 해당 날짜 간격으로 비교하는데 간격이 구독료보다 비싼 경우 구독을 갱신하고, 아니면 연장하는식으로 진행하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/27851