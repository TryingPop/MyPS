# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 그리디 알고리즘
  - 정렬

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Your favorite football team is playing a charity tournament, which is part of a worldwide fundraising effort to help children with disabilities. As in a normal tournament, three points are awarded to the team winning a match, with no points to the losing team. If the game is drawn, each team receives one point.<br/>
Your team played N matches during the first phase of the tournament, which has just finished. Only some teams, the ones with more accumulated points, will advance to the second phase of the tournament. However, as the main objective of the tournament is to raise money, before the set of teams that will pass to the second phase is determined, each team is allowed to buy additional goals. These new goals count as normally scored goals, and may be used to alter the result of any of the matches the team played.<br/>
Your team’s budget is enough to buy up to G goals. Can you tell the maximum total number of points your team can get after buying the goals, supposing the other teams will not buy any goals?<br/>


## 입력
The first line contains two integers N (1 ≤ N ≤ 10^5) and G (0 ≤ G ≤ 10^6) representing respectively the number of matches your team played and the number of goals your team can buy. Each of the next N lines describes a match result with two integers S and R (0 ≤ S, R ≤ 100), indicating respectively the goals your team scored and received on that match before buying goals.<br/>


## 출력
Output a line with an integer representing the maximum total number of points your team can get after buying the goals.<br/>


## 예제 입력
2 1<br/>
1 1<br/>
1 1<br/>


## 예제 출력
4<br/>


## 풀이
이전 경기의 골이 다음 경기의 골에 영향을 안주고 반대 역시 마찬가지다.<br/>
또한 무승부 2개보다 승리 1개가 점수가 더 높으므로 무승부를 2개 늘리는거보다 승리 1개를 만드는게 좋다.<br/>
그리고 2골 차이로 이기던, 1골 차이로 이기던 3점으로 같으므로 1골 차이로 이기는게 좋다.<br/>
그리디로 득점차이로 정렬하고, 낮은거 부터 1점차로 이기게 만들면서 진행한다.<br/>
그렇게 승리하게 만들고, 더 이상 승리가 불가능한 경우 무승부로 최대한 만든다.<br/>
이후 얻은 점수를 계산하면 찾아야 하는 최대 점수이다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/9530