#include <iostream>
using namespace std;

class Shape {
private:
	char* sh_name; // 도형의 이름
public:
	Shape(char* name)
	{
		sh_name = new char[strlen(name) + 1];
		strcpy_s(sh_name, strlen(name) + 1, name);
	}
	virtual void Show() = 0;
};

class Circle : public Shape
{
private:
	float size;
	float radius;
public:
	Circle(char* name, float radius) : Shape(name)
	{
		this->radius = radius;
		size = radius * radius * 3.14f;
	}
	void Show()
	{
		cout << "형태 : 원" << endl;
		cout << "반지름 : " << radius << endl;
		cout << "면적 : " << size << endl;
		cout << endl;
	}
};

class Rectangle : public Shape
{
private:
	float width;
	float height;
	float size;
public:
	Rectangle(char* name, float width, float height) : Shape(name)
	{
		this->width = width;
		this->height = height;
		size = width * height;
	}
	void Show()
	{
		cout << "형태 : 직사각형" << endl;
		cout << "밑변과 높이의 길이 : " << width << "," << height << endl;
		cout << "면적 : " << size << endl;
		cout << endl;
	}
};

class Square : public Shape
{
private:
	float size;
	float length;
public:
	Square(char* name, float length) : Shape(name)
	{
		this->length = length;
		size = length * length;
	}
	void Show()
	{
		cout << "형태 : 정사각형" << endl;
		cout << "한 변의 길이 : " << length << endl;
		cout << "면적 : " << size << endl;
		cout << endl;
	}
};
int main() {
	Shape* pt[3] = { new Circle("원", 5.4), new Rectangle("직사각형", 3, 5), new Square("정사각형", 10) };
	for (int i = 0; i < 3; i++)
		pt[i]->Show();
	for (int i = 0; i < 3; i++)
		delete pt[i];
	return 0;
}