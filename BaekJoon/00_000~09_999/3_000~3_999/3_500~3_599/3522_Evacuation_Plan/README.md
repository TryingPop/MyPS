# C#

## 난이도 : 골드 2

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 정렬
  - 역추적

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Flatland government is building a new highway that will be used to transport weapons from its main weapon plant to the frontline in order to support the undergoing military operation against its neighbor country Edgeland. Highway is a straight line and there are n construction teams working at some points on it.<br/>
During last days the threat of a nuclear attack from Edgeland has significantly increased. Therefore the construction office has decided to develop an evacuation plan for the construction teams in case of a nuclear attack. There are m shelters located near the constructed highway. This evacuation plan must assign each team to a shelter that it should use in case of an attack.<br/>
Each shelter entrance must be securely locked from the inside to prevent any damage to the shelter itself. So, for each shelter there must be some team that goes to this shelter in case of an attack. The office must also supply fuel to each team, so that it can drive to its assigned shelter in case of an attack. The amount of fuel that is needed is proportional to the distance from the team’s location to the assigned shelter. To minimize evacuation costs, the office would like to create a plan that minimizes the total fuel needed.<br/>
Your task is to help them develop such a plan<br/>


## 입력
The first line of the input file contains n — the number of construction teams (1 ≤ n ≤ 4000). The second line contains n integer numbers — the locations of the teams. Each team’s location is a positive integer not exceeding 109, all team locations are different.<br/>
The third line of the input file contains m — the number of shelters (1 ≤ m ≤ n). The fourth line contains m integer numbers — the locations of the shelters. Each shelter’s location is a positive integer not exceeding 109, all shelter locations are different.<br/>
The amount of fuel that needs to be supplied to a team at location x that goes to a shelter at location y is equal to |x − y|.<br/>


## 출력
The first line of the output file must contain z — the total amount of fuel needed. The second line must contain n integer numbers: for each team output the number of the shelter that it should be assigned to. Shelters are numbered from 1 to m in the order they are listed in the input file.<br/>


## 예제 입력
3<br/>
1 2 3<br/>
2<br/>
2 10<br/>


## 예제 출력
8<br/>
1 1 2<br/>


## 풀이
각 방공호에 최소 1명씩 있으면서 모두가 방공호에 들어가는 가장 짧은 거리의 비용으로 맞춰야 한다.<br/>


n개의 방공호와 n명의 사람이 있다고 생각하자.<br/>
그러면 방공호의 위치와 사람의 위치로 각각 오름차순 정렬한다.<br/>
방공호에 최소 1명이 들어가는 가장 값싼 경우는 정렬된 i번째 사람과 정렬된 i번째 방공호임을 그리디로 알 수 있다.<br/>


그래서 사람의 위치와 방공호의 위치로 각각 정렬한다.<br/>
정답은 사람의 기존 인덱스 순으로 기존 방공호의 번호를 입력해야 하기에 인덱스 번호도 함께 저장한다.<br/>


이제 j ≤ i에 대해 dp[i][j] = val를 정렬된 i번째사람까지 선택했고 사람과 정렬된 j번째 방공호까지 최소 1명씩 채웠을 때 가장 작은 비용이되게 val를 설정했다.<br/>
dis[i][j]를 정렬된 i번째 사람이 정렬된 j번째 방공호의 거리라 하자.<br/>
그러면 dp[i][j] = Math.Min(dp[i - 1][j - 1], dp[i - 1][j]) + dis[i][j]점화식을 얻는다.<br/>


이렇게 dp의 값을 채워갔다.<br/>
그러면 dp[n][m]의 값이 가장 작은 비용이 된다.<br/>


이제 각 사람이 방공호로 가는 것은 dp의 역추적으로 찾았다.<br/>
점화식을 보면 i = n, j = m에서 시작한다.<br/>
i번째 사람은 j번째 방공호로 간다.<br/>


그리고 다음 i는 i - 1이 된다.<br/>
반면 j는 점화식으로부터 다음과 같이 찾는다.<br/>


dp[i - 1][j - 1] > dp[i - 1][j]인 경우 j 는 j가 된다..<br/>
이외의 dp[i - 1][j - 1] ≤ dp[i - 1][j]인 경우 j는 j - 1이 된다.<br/>


이렇게 모든 정렬된 i = 1, 2, ..., n에 대해 정렬된 j번 방공호로 가는 경우를 dp에 저장한다.<br/>
기존 사람의 인덱스에 맞게 기존 방공호를 찾아주면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/3522