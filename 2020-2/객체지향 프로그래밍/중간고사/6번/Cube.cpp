#include <iostream>
using namespace std;

class Rectangle {
private:
	int row, col; // 가로, 세로
public:
	Rectangle(int row, int col) {
		this->row = row;
		this->col = col;
	}
	int getArea() { return row * col; }
};

class Cube : public Rectangle
{
private:
	int volume;
public:
	Cube(int row, int col, int height) : Rectangle(row, col)
	{
		volume = row * col * height;
	}
	int getVolume() { return volume; }
};
int main()
{
	Cube box(10, 30, 20);
	cout << "부피 : " << box.getVolume() << endl;
	return 0;
}