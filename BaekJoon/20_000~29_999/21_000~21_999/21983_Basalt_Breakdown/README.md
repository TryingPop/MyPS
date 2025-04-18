# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 수학
  - 기하학
  - 사칙연산

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
One of Iceland's most popular attractions is Svartifoss ("black waterfall"). Its name derives from the black hexagonal basalt columns that frame the waterfall on either side. Originally formed from cooling lava, centuries of erosion have shaped the columns into their characteristic shape.<br/>
A group of geologists at RU went on an excursion to Svartifoss. They took some probes and performed various measurements on the hexagonal rocks that have broken off the basalt walls.<br/>
Just as they return to RU, they realise that they have forgotten a crucial measurement. They have determined the area of the hexagonal face, but they did not write down what its perimeter was. Assuming that the face has the shape of a perfect regular hexagon, help the geologists compute the perimeter.<br/>


## 입력
The input consists of:<br/>

  - One line with an integer a (1 ≤ a ≤ 10^18), the area of the hexagonal rock face in square centimetres.<br/>


## 출력
Output the perimeter of the rock face in centimetres. Your answer should have an absolute or relative error of at most 10^-6.<br/>


## 예제 입력
50<br/>


## 예제 출력
26.32148026<br/>


## 풀이
정육각형의 넓이가 입력으로 주어진다.<br/>
정육면체의 한 변의 길이를 a라 하면 a를 변의 길이로하는 정삼각형 6개의 넓이 합이 정육면체의 넓이합과 같다.<br/>
그리고 피타고라스 정리를 이용해 a를 변의 길이로 하는 정삼각형의 넓이를 a로 표현하면 (√3 x a^2) / 2이다.<br/>
정육면체의 넓이 area는 해당 정삼각형을 6개 넓이와 같으므로 area = 3 x √3 x a^2임을 알 수 있다.<br/>
정육면체의 변의 길이 a에 대해 정육면체의 넓이 area로 표현하면 변의 길이는 양수이므로 a = √(area / (3 x √3))가 된다.<br/>
이제 둘레의 길이는 6a이므로 a를 찾고 6을 곱하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/21983