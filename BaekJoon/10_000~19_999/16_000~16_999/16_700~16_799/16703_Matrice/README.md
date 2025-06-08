# C#

## 난이도 : 플레티넘 5

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Agent Sue Thomas and her son are looking for trinities in a grid. The word trinity is a neologism referring to a particular triangular (as the morpheme “tri” suggests) shape composed of cells in the grid.<br/>
Each trinity is a result of taking a square-shaped area of the cells and removing all cells that lie either above or below one of the two diagonals of the area. The diagonal may be either the main diagonal (southeast-northwest direction) or the main antidiagonal (southwest-northeast direction). A valid trinity consists of at least three grid cells and all its cells contain the same character.<br/>


## 입력
The first input line contains two numbers N and M (1 ≤ N, M ≤ 1000), describing the number of rows and columns in the grid, respectively. Each of next N lines contains M characters, whose ASCII codes are between 33 and 126, inclusively.<br/>


## 출력
Output the number of different valid trinities in the input grid.<br/>


## 예제 입력
5 6<br/>
\#girls<br/>
\#\#areb<br/>
\#\.\#est<br/>
\#\.\.\#\!\!<br/>
\#\#\#\#\#\!<br/>


## 예제 출력
7<br/>


## 풀이
예제를 보자.<br/>

|#|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|#|#|a|r|e|b|
|#|.|#|e|s|t|
|#|.|.|#|!|!|
|#|#|#|#|#|!|


가능한 삼각형은 다음과 같다.<br/>

|O|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|O|O|a|r|e|b|
|#|.|#|e|s|t|
|O|.|.|O|!|!|
|O|O|#|O|O|!|


|#|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|O|O|a|r|e|b|
|O|.|#|e|s|t|
|#|.|.|O|!|!|
|#|#|O|O|#|!|


|#|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|#|#|a|r|e|b|
|#|O|#|e|s|t|
|#|O|O|#|O|O|
|#|#|#|#|#|O|


이렇게 총 3개, 2개, 2개이다.<br/>
삼각형을 이루는 모든 원소가 같아야 한다.<br/>


가능한 삼각형의 형태를 보면 다음과 같이 4개이다.<br/>


|O|X||X|O||O|O||O|O|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|O|O||O|O||O|X||X|O|


이를 왼쪽부터 각각 ↘, ↙, ↗, ↖ 화살표로 표현한다.<br/>
이렇게 화살표를 선언한건 선분은 대각선을 뜻하고 화살표 방향은 가로가 2개인 곳을 나타낸다.<br/>
↘ 화살표의 경우 대각선이 ＼형태이고 아래쪽에 2개가 있다는 뜻이다.<br/>


↘형태의 삼각형부터 알아보자.<br/>
가장 아래 꼭짓점을 기준으로 구분한다. 만약 가장 아래 점이 여러개인 경우 가장 오른쪽에 있는 점을 기준으로 한다.<br/>
그러면 ↘형태에서는 다음 A 위치가 기준이 된다.<br/>

|O|||
|:---:|:---:|:---:|
|O|O||
|O|O|A|


왼쪽 위에서부터 가장 큰 삼각형의 크기를 기록해간다.<br/>
가장 큰 삼각형의 크기를 알면 해당 꼭짓점으로 하는 삼각형은 크기 - 1개 존재함을 알 수 있다.<br/>


왼쪽 끝과 위쪽 끝 가장자리에 있는 것은 1임이 자명하다.<br/>
예제로 크기롤 보면 다음과 같다.<br/>


|#|g|i|r|l|s||1|1|1|1|1|1|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|#|#|a|r|e|b||1||||||
|#|.|#|e|s|t||1||||||
|#|.|.|#|!|!||1||||||
|#|#|#|#|#|!||1||||||


이제 이외의 점은 왼쪽 위 대각선의 점과, 왼쪽 점과 같은지 먼저 확인해야 한다.<br/>
같은 경우 왼쪽 위 대각선의 점과, 왼쪽 점 중 작은 값 + 1임을 알 수 있다.<br/>
즉, 현재점 A에서 비교하는 점은 B가 된다.<br/>


|O|||
|:---:|:---:|:---:|
|O|B||
|O|B|A|


이렇게 예제를 채워가면 다음과 같다.<br/>

|#|g|i|r|l|s||1|1|1|1|1|1|
|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|#|#|a|r|e|b||1|2|1|1|1|1|
|#|.|#|e|s|t||1|1|1|1|1|1|
|#|.|.|#|!|!||1|1|2|1|1|1|
|#|#|#|#|#|!||1|2|1|1|2|1|


2이상에 대해 1을 빼줘서 삼각형의 갯수를 세어주면 된다.<br/>
해당 방법으로 ↘ 화살표인 삼각형의 갯수를 O(NM)에 모두 찾을 수 있다.<br/>


↙, ↗, ↖인 경우도 비슷하게 찾아주면 된다.<br/>
↙의 경우 현재지점 A에서 왼쪽과 위쪽 지점이 비교할 점 B가 된다.<br/>
↗의 경우 현재지점 A에서 위쪽과 오른쪽 위 지점이 B가 된다.<br/>
↖의 경우 현재지점 A에서 위쪽과 왼쪽 위 지점이 B가 된다.<br/>


이렇게 찾아 누적하면 정답이 된다.<br/>


## 고민했던 부분
문제를 잘못 해석해 위 예제에서 다음과 같은 경우도 가능한 줄 알았다.<br/>

|#|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|#|#|a|r|e|b|
|#|.|#|e|s|t|
|#|.|.|#|!|!|
|#|#|#|#|#|!|


|O|g|i|r|l|s|
|:---:|:---:|:---:|:---:|:---:|:---:|
|O|O|a|r|e|b|
|O|.|O|e|s|t|
|O|.|.|O|!|!|
|O|O|O|O|O|!|


만약 해당 경우가 가능하다면 O(NM * min(N, M))의 방법밖에 떠올리지 못했다.<br/>


방법은 다음과 같다.<br/>
누적합으로 왼쪽 방향으로 같은 것의 갯수를 left에 누적해간다.<br/>
비슷하게 오른쪽, 위쪽, 아래쪽 누적합을 각각 right, up, down 배열에 저장한다.<br/>


↘ 화살표인 경우 각 점 A에대해 ↘방향으로 진행하는데 현재지점 A의 up만큼만 확인한다.<br/>
그리고 ↘방향의 점 B에 대해 B의 right 값을 비교하면서 A와 B의 택시 거리의 절반이 B의 right 값보다 크거나 같은 경우 삼각형이 됨을 알 수 있다.<br/>
이렇게 가능한 삼각형을 찾아가면 된다.<br/>


비슷하게 ↙, ↗, ↖인 경우도 찾아주면 된다.<br/>
다만 해당 풀이의 경우 O(NM * min(N, M))이고 N, M이 1000까지오므로 최악의 경우 10^9이므로 시간초과날 수 있다.<br/>
그리고 내부가 모두 같은 거만 세어야 하는 문제 조건과 맞지 않는다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/16703