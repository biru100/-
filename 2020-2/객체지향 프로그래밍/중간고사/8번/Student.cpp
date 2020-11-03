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
		cout << "ÀçÇÐ»ý : " << name << " " << no << endl;
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
		cout << "ÀçÇÐ»ý : " << name << " " << no << endl;
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
		cout << "Á¹¾÷»ý : " << name << " " << no << endl;
	}
	static int GetCount() { return count; }
};
int Fresh_Student::count = 0;
int Current_Student::count = 0;
int Graduate_Student::count = 0;


int main()
{
	Student* std1 = new Current_Student[3]{
		Current_Student("ÀÌÀ¯¸®", "20200301"),
		Current_Student("±èÁÖ¼º", "20200302"),
		Current_Student("Àåµ·°Ç", "20200303")
	};
	
	Student* std2 = new Graduate_Student[2]{
		Graduate_Student("ÀÌÀ¯¸®", "20200301"),
		Graduate_Student("±èÁÖ¼º", "20200302")
	};
	Student* std3 = new Fresh_Student[2]{
		Fresh_Student("Àåµ·°Ç", "20200301"),
		Fresh_Student("¹ÚÀººó", "20200304")
	};

	for (int i = 0; i < 3; i++)
		std1[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ÃÑÇÐ»ý¼ö: " << Current_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	for (int i = 0; i < 2; i++)
		std2[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ÃÑÇÐ»ý¼ö: " << Graduate_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	for (int i = 0; i < 2; i++)
		std3[i].Show();
	cout << "------------------------------------" << endl;
	cout << "              ÃÑÇÐ»ý¼ö: " << Fresh_Student::GetCount() << endl;
	cout << "------------------------------------" << endl;
	return 0;
}
