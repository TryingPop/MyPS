# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 구현

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
The game “Draw a forest picture” is quite popular among younger visitors of the amusement park. The number of players in the game is virtually unlimited and nearly everybody becomes a winner. The game is simple. At the beginning, a leader of the game describes briefly a picture of a forest which he or she had seen recently. Then the players are given some paper and crayons and they have to reproduce the image as best as they can. Everybody who hands in at least partial image of virtually any piece of any forest anywhere on Earth depicted in any style wins a small reward in the form of a chocolate or a fruit.<br/>
In this problem, you have to implement an imitation of the game. As you are much more experienced than the younger players, your drawing has to meet the specifications exactly. The image you have to reproduce depicts a glade — an open area within a woodland which is somewhat less populated by the trees than the surrounding thick forest. The image has to be printed in the “ascii-art” style.<br/>
An M × M image is represented by a canvas consisting of M rows, each row containing M characters. Each pixel in the image is represented by some printable ASCII character on the canvas. The coordinates of the pixels in the image correspond to the coordinates of the characters on the canvas. The coordinates of the pixels in the bottom left corner and in the top right corner of the image are (0, 0) and (M − 1, M − 1), respectively. The x-coordinate of the pixel in the bottom right corner of the image is M − 1.<br/>
Each pixel in the image depicts either grass or a part of a standing tree or a tree stump. A pixel depicting grass is represented by a single dot character (“.”, ASCII code 46) on the canvas. Standing trees and tree stumps are depicted by more pixels, their representation on the canvas follows.<br/>
A standing tree has a positive height S and it consists of four parts: The roots, the tree trunk, the branches, and the treetop. The roots are represented by three horizontally adjacent characters: underscore, vertical bar, underscore (“_|_”, ASCII codes 95, 124, 95). The tree trunk is represented by S vertically adjacent vertical bars (“|”, ASCII code 124) located immediately above the center of the tree roots. The branches consist of S left branches located immediately to the left of the tree trunk, and S right branches located immediately to the right of the tree trunk. Each branch is adjacent to the tree trunk. A left branch is represented by a single forward slash character (“/”, ASCII code 47), a right branch is represented by a single backslash character (“\”, ASCII code 92). A treetop is represented by a single caret character (“^”, ASCII code 94) located immediately above the topmost character of the tree trunk.<br/>
A tree stump consists of three horizontally adjacent pixels represented by characters underscore, lowercase letter “o”, underscore (“_o_”, ASCII codes 95, 111, 95).<br/>
Note that a standing tree or a tree stump may appear in the image only partially or may not appear in the image at all, depending on its coordinates. See the sample data below for additional illustration of this fact.<br/>


## 입력
There are more test cases. Each case starts with a line containing two integers M, N separated by space (1 ≤ M ≤ 100, 1 ≤ N ≤ 105). Next, there are N lines, each line contains a triple of integers S, X, Y , separated by spaces, which describe one standing tree or one tree stump. The values of X and Y represent the coordinates of the center of either tree roots or a tree stump. In case of S = 0 the triple describes a stump. In case of S > 0 the triple describes a standing tree with height S. It holds 0 ≤ S ≤ 9, −10^9 ≤ X, Y ≤ 10^9.<br/>
It is guaranteed that no parts of two different standing trees and/or tree stumps should be depicted by the same pixel.<br/>


## 출력
For each test case, print the canvas with the image of the glade. The top row of the canvas should be the first printed row of the image. The bottom row of the canvas should be the last printed row of the image. The printout should be decorated by a square border made of asterisk characters (“*”, ASCII code 42), the thickness of the border should be one pixel. The border should frame the canvas tightly, that is, there should be no spaces between the border and canvas neither horizontally nor vertically. Print one empty line after each case.<br/>


## 예제 입력
3 2<br/>
0 5 5<br/>
9 1 0<br/>
8 10<br/>
3 3 2<br/>
0 2 1<br/>
1 -1 -1<br/>
0 -1 2<br/>
3 0 6<br/>
6 4 7<br/>
0 7 4<br/>
3 8 -1<br/>
5 5 -5<br/>
9 2 -10<br/>


## 예제 출력
\*\*\*\*\*<br/>
\*\/\|\\\*<br/>
\*\/\|\\\*<br/>
\*\_\|\_\*<br/>
\*\*\*\*\*<br/>
<br/>
\*\*\*\*\*\*\*\*\*\*<br/>
\*\|\\\.\_\|\_\.\.\*<br/>
\*\|\_\.\^\.\.\.\.*<br/>
\*\.\.\/\|\\\.\.\.\*<br/>
\*\.\.\/\|\\\.\_\o\*<br/>
\*\.\.\/\|\\\.\.\.\*<br/>
\*\_\.\_\|\_\.\.\/\*<br/>
\*\.\_\o\_\.\^\.\/\*<br/>
\*\\\.\^\.\/\|\\\/\*<br/>
\*\*\*\*\*\*\*\*\*\*<br/>


## 풀이
맵의 크기는 100 x 100이다.<br/>
나무는 많아야 가로 3칸을 차지한다.<br/>
그래서 각 나무에 대해 많아야 300개의 칸을 그린다.<br/>


나무는 10만개까지 들어온다.<br/>
각 경우 300을 곱해도 3000만이다.<br/>
각 케이스에대해서는 그림판에 나무를 그리면서 진행하는 것이 유효하다.<br/>
다른 방법도 안떠오르고 해서 그림판에 나무를 그리면서 진행을 했다.<br/>


먼저 그루터기의 경우 세로 길이가 1이고 가로가 3으로 일정하다.<br/>
그래서 3군대의 점이 유효한지 판별하며 그리기를 시도했다.<br/>


나무를 그리는 것은 그림판 범위에 나무로 변형을 시킨다.<br/>
이는 왼쪽 끝, 오른쪽 끝, 아래 끝, 위쪽 끝으로 판별 가능하다.<br/>


왼쪽 끝이 m이상인 나무는 그림판 오른쪽에 있고, 범위를 벗어난다.<br/>
오른쪽 끝은 0 미만인 경우 범위를 벗어난다.<br/>
아래쪽 끝은 m 이상인 경우 범위를 벗어난다.<br/>
위쪽 끝은 0 미만인 경우 범위를 벗어난다.<br/>


이렇게 비교하면 겹치는 구간이 하나도 없는 곳을 찾는 것과 같다.<br/>


이제 겹치는 곳이 있는 경우에 한해 그리기를 시도한다.<br/>
먼저 아래와 위쪽의 범위를 그림판에 오게 맞춘다.<br/>


나무의 맨 아래와 맨 위의 그리는 법은 특수하다.<br/>
그래서 그림판에 들어오는 아래 끝과 기존 나무의 아래끝을 비교한다.<br/>
같은 경우 나무의 아래로 그린다.<br/>
비슷하게 위도 판별해 그린다.<br/>
그리고 나서 남은 중앙 부분을 그렸다.<br/>


이렇게 제출하니 이상없이 통과한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/14981