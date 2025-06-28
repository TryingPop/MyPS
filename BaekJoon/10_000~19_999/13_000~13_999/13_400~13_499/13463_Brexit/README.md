# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 너비 우선 탐색

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
A long time ago in a galaxy far, far away, there was a large interstellar trading union, consisting of many countries from all across the galaxy. Recently, one of the countries decided to leave the union. As a result, other countries are thinking about leaving too, as their participation in the union is no longer beneficial when their main trading partners are gone.<br/>
You are a concerned citizen of country X, and you want to find out whether your country will remain in the union or not. You have crafted a list of all pairs of countries that are trading partners of one another. If at least half of the trading partners of any given country Y leave the union, country Y will soon follow. Given this information, you now intend to determine whether your home country will leave the union.<br/>


## 입력
The input starts with one line containing four space separated integers C, P, X, and L. These denote the total number of countries (2 ≤ C ≤ 200 000), the number of trading partnerships (1 ≤ P ≤ 300 000), the number of your home country (1 ≤ X ≤ C) and finally the number of the first country to leave, setting in motion a chain reaction with potentially disastrous consequences (1 ≤ L ≤ C).<br/>
This is followed by P lines, each containing two space separated integers Ai and Bi satisfying 1 ≤ Ai < Bi ≤ C. Such a line denotes a trade partnership between countries Ai and Bi . No pair of countries is listed more than once.<br/>
Initially, every country has at least one trading partner in the union.<br/>


## 출력
For each test case, output one line containing either “leave” or “stay”, denoting whether you home country leaves or stays in the union.<br/>


## 예제 입력
5 5 1 1<br/>
3 4<br/>
1 2<br/>
2 3<br/>
1 3<br/>
2 5<br/>


## 예제 출력
leave<br/>


## 풀이
자신과 교역하는 국가 중 절반 이상이 탈퇴하면 자신이 탈퇴한다.<br/>
그래서 교역하는 국가의 수와 끊어진 교역 수를 기록해 절반이 넘으면 해당 노드를 큐에 넣으면 BFS 탐색을 했다.<br/>
그러면 많아야 노드를 1번 탐색하고, 간선은 2번 탐색한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/13463