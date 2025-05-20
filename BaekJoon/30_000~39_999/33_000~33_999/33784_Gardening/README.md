# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 기하학
  - 다각형의 넓이

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 2048 MB

## 문제
Bob has an incredibly huge garden with lots of grass and beautiful flowers, but since he started training his programming skills for the SKP, he does not have that much time to maintain it any more. To reduce the time spent maintaining his garden, Bob selected an area of his garden where he wants to place square stone tiles. He subdivided his garden into a n by n square grid (1 ≤ n ≤ 1000) such that one stone tile fits exactly into one grid cell. Therefore, each tile must be placed inside exactly one grid cell.<br/>
The area Bob wants to fill with tiles is given as a sequence of m points defining its perimeter. Each line segment between points p_i and p_{i+1} defines an edge of the area. Point p_0 is also connected to point p_{m-1}. In each cell within the defined perimeter, exactly one stone tile is placed. Bob now needs your help to count the number of stone tiles he needs to fill the entire designated area.<br/>


## 입력
The first line of the input consists of one integer m (4 ≤ m ≤ 1000): the number of points that define the perimeter of the selected area.<br/>
The following input consists of m distinct lines with two space-separated integers x_i and y_i (1 ≤ x_i, y_i ≤ 1000): The coordinates of point p_i are (x_i, y_i). The bottom left corner is defined as point (0, 0) and the top right corner is defined as point (n,n).<br/>


## 출력
One line with the number of square tiles required to fill the entire designated area.<br/>


## 예제 입력
12<br/>
5 1<br/>
5 6<br/>
10 6<br/>
10 7<br/>
1 7<br/>
1 14<br/>
14 14<br/>
14 9<br/>
18 9<br/>
18 3<br/>
12 3<br/>
12 1<br/>


## 예제 출력
160<br/>


## 풀이
문제에서 가장자리 점 pi는 서로 인접한 점끼리 이어져 있다고 한다.<br/>
그래서 만들어지는 도형은 정사각형 1개와 위상동형이다.<br/>


도형에 테두리를 만들면 내부와 외부로 구분된다.<br/>
테두리를 벽이라 생각하고 명백히 도형 외부의 점인 0, 0에서 BFS 탐색을 진행하면 모든 외부의 점들을 방문하게 될 것이다.<br/>
가장자리 점의 좌표가 1000이하인 자연수이므로 2배를 해도 2002 x 2002는 약 400만이므로 점들의 좌표를 배열로 관리할 수 있고 BFS 탐색이 유효하다.<br/>


이제 테두리를 잇는데 단순 정수좌표라 하면 사각형이 (0, 0), (1, 0), (1, 1), (0, 1)인 경우와 (2, 0), (2, 1), (3, 1), (3, 0) 정사각형이 합쳐진 경우를 보자.<br/>
그러면 넓이는 2인데, (0, 0), (0, 1), (1, 0), (1, 1), ..., (3, 0), (3, 1)에 표시될 것이다.<br/>


반면 (0, 0), (3, 0), (3, 1), (0, 1) 사각형인 경우도 보자.<br/>
그러면 넓이는 3인데, (0, 0), (0, 1), (1, 0), (1, 1), ..., (3, 0), (3, 1)에 표시될 것이다.<br/>


즉 명백히 구분안되는 케이스가 생긴다. 위 (예제)[#예제_입력] 역시 구분안된다.<br/>
이는 0.5 좌표를 확인하는 식으로 좌표를 2배해서 관리하면 구분지을 수 있다.<br/>


이제 테두리를 만드는게 일이된다.<br/>
이 역시 위상 동형의 성질을 이용하면 만들어지는 도형은 凹 와 동형이 된다.<br/>
가장자리 홀수 번째는 시작 지점으로 인접한 짝수번째는 끝으로 선분을 이으면 도형이 된다.<br/>
이렇게 좌표에 표시해 줘서 테두리를 이어줬다.<br/>


테두리를 이었다면 앞의 방법으로 0, 0에서 BFS 탐색을 이용해 외부 점을 방문처리한다.<br/>
이후 방문 안된 점에대해 넓이를 세어 정답을 찾았다.<br/>


다른 사람의 풀이를 보니 한붓긋기로 되어져 있으므로 신발끈 공식으로 간단하게 풀었다.<br/>
해당 방법이 확실히 좋아 보인다.<br/>


CHAT GPT도 이용해봤는데, 시간초과 오답을 내놓는다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/33784