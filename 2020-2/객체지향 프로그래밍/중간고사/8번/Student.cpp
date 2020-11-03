#include <iostream>
using namespace std;

class Student {
protected:
	string name;
	string no;
public:
	Student(string na, string n) : name(na), no(n)
	{}
	virtual void Show() = 0;
};

class Current_Student : public Student
{
private:
	static int count;
public:
	Current_Student(string na, string n) : Student(na,n)
	{
		count++;
	}
	void Show()
	{
		cout << "���л� : " << name << " " << no << endl;
	}
	static int GetCount() { return count; }
};

class Fresh_Student : public Student
{
private:
	static int count;
public:
	Fresh_Student(string na, string n) : Student(na, n)
	{
		count++;
	}
	void Show()
	{
		cout << "���л� : " << name << " " << no << endl;
	}
	static int GetCount() { return count; }
};

class Graduate_Student : public Student
{
private:
	static int count;
public:
	Graduate_Student(string na, string n) : Student(na, n)
	{
		count++;
	}
	void Show()
	{
		cout << "������ : " << name << " " << no << endl;
	}
	static int GetCount() { return count; }
};
int Fresh_Student::count = 0;
int Current_Student::count = 0;
int Graduate_Student::count = 0;


int main()
{
	Student* std1 = new Current_Student[3]{
		Current_Student("������", "20200301"),
		Current_Student("���ּ�", "20200302"),
		Current_Student("�嵷��", "20200303")
	};
	
	Student* std2 = new Graduate_Student[2]{
		Graduate_Student("������", "20200301"),
		Graduate_Student("���ּ�", "20200302")
	};
	Student* std3 = new Fresh_Student[2]{
		Fresh_Student("�嵷��", "20200301"),
		Fresh_Student("������", "20200304")
	};

	for (int i = 0; i < 3; i++)
		std1[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ���л���: " << Current_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	for (int i = 0; i < 2; i++)
		std2[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ���л���: " << Graduate_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	for (int i = 0; i < 2; i++)
		std3[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ���л���: " << Fresh_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	return 0;
}
