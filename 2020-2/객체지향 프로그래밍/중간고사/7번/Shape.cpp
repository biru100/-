#include <iostream>
using namespace std;

class Shape {
private:
	char* sh_name; // ������ �̸�
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
		cout << "���� : ��" << endl;
		cout << "������ : " << radius << endl;
		cout << "���� : " << size << endl;
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
		cout << "���� : ���簢��" << endl;
		cout << "�غ��� ������ ���� : " << width << "," << height << endl;
		cout << "���� : " << size << endl;
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
		cout << "���� : ���簢��" << endl;
		cout << "�� ���� ���� : " << length << endl;
		cout << "���� : " << size << endl;
		cout << endl;
	}
};
int main() {
	Shape* pt[3] = { new Circle("��", 5.4), new Rectangle("���簢��", 3, 5), new Square("���簢��", 10) };
	for (int i = 0; i < 3; i++)
		pt[i]->Show();
	for (int i = 0; i < 3; i++)
		delete pt[i];
	return 0;
}