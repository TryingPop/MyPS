# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그래프 이론
  - 최소 스패닝 트리

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
#### 가희는 도시 시뮬레이션 게임을 하고 있습니다. 이 게임은 나의 도시와 다른 도시들을 연합하여, 나의 도시를 키우는 게임입니다. 가희의 도시에 사는 사람들은 철도만 이용하여 이동합니다. 건설된 철도 노선들을 적절히 이용하여 가희의 도시에서 도시 a로 이동하지 못하면, 사람들은 도시 a와 교류를 하지 못하게 되고, 가희의 도시는 도시 a와 연합할 수 없습니다.
#### 가희는 월드에 있는 도시 n-1개와 가희의 도시를 연합하여 세력을 확장하려고 합니다. 이 게임은 철도 노선 Q개를 구매할 수 있습니다. 가희는 이 철도 노선들을 적절하게 구매하여 총 건설 비용을 최소로 하려고 합니다. 그러면서 가희의 도시와 n-1개의 도시들을 빠르게 연합하려고 합니다. 가희가 건설할 수 있는 철도 노선들에 대한 정보가 주어졌을 때, 총 건설 비용과 언제 n-1개의 도시들과 가희의 도시가 연합하는지 구해주세요. 목표를 달성하는 것이 불가능하다면 첫 줄에 -1을 출력해 주세요.

## 입력
#### 첫 번째 줄에 n과 Q가 공백으로 구분되어 주어집니다. 월드에 1번 도시부터 n번 도시까지 있음을 의미하며, 가희의 도시는 1번 도시입니다. 또한 건설할 수 있는 노선은 Q개가 있음을 의미합니다.
#### 다음 Q개의 줄에 건설할 수 있는 철도 노선의 정보가 아래와 같이 주어집니다.
####  from to cost time
#### 이는 월드에 있는 두 도시, 
from번 도시에서 
to번 도시를 경유하는 도시 없이, 양방향으로 연결하는 철도를 비용 
cost를 들여 시각 
time에 지을 수 있음을 의미합니다. ( 1 ≤ from ≤ n, 1 ≤ to ≤ n, from ≠ to ) 철도 노선들은 구매하는 즉시 지어지며, 같은 시각에 여러 철도 노선을 건설할 수 있습니다.

## 출력
#### 가희의 도시와 n-1개의 도시가 연합을 하는 시점과 총 건설 비용을 공백으로 구분하여 출력해 주세요. 만약, n-1개의 도시와 가희의 도시가 연합할 수 없다면, 첫 줄에 -1을 출력해 주세요.

## 제한
  - 2 ≤ n ≤ 2 x 10^5
  - 2 ≤ Q ≤ 2 x 10^5
  - 1 ≤ time ≤ 10^9
  - 1 ≤ cost ≤ 10^9

## 예제 입력
4 5<br/>
1 4 1 5<br/>
2 3 1 1000000000<br/>
1 4 1 13<br/>
3 2 1 117<br/>
2 4 1 10<br/>

## 예제 출력
117 3<br/>

## 힌트
#### 3개의 도시와 연합하기 위해서, 아래와 같이 철도 노선을 건설하면 됩니다.
  - 시각 5에 도시 1과 4를 연결하는 철도 노선을 비용 1을 들여 건설합니다.
  - 시각 10에 도시 2와 4를 연결하는 철도 노선을 비용 1을 들여 건설합니다.
  - 시각 117에 도시 3과 2를 연결하는 철도 노선을 비용 1을 들여 건설합니다.
#### 이 때, 시각 117에 총 건설 비용 3을 들여, 3개의 도시와 연합할 수 있습니다. 시각 117에 건설하지 않고, 시각 1,000,000,000에 도시 2와 3을 연결하는 철도 노선을 건설하는 경우 총 건설 비용은 3입니다. 하지만 시각 117에 같은 비용을 들여 목표를 달성하는 방법이 있으므로 답이 아닙니다.

## 문제 링크
https://www.acmicpc.net/problem/30797