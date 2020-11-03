using System;

namespace CK
{
    // 전체 프로그램의 클래스 입니다.
    class Program
    {
        // 전체 게임 시스템을 관리하는 클래스의 변수입니다.
        static GameSystem gs = new GameSystem();
        // 프로그램의 코드를 담당하는 메인 함수입니다.
        static void Main(string[] args)
        {
            // 게임 시스템을 초기화 합니다.
            gs.Init();
            // 게임 시스템 반복문을 돌려 지속적으로 진행합니다.
            while(!GameSystem.GameOver) // 게임이 끝날 때 꺼줍니다.
            {
                gs.Update();
                gs.Render();
                gs.SceneCheck();
            }
        }
    }

    // 게임 전체적인 시스템을 관리할 클래스 입니다.
    class GameSystem
    {
        private SceneManager sm = new SceneManager();
        static public bool GameOver = false;
        // 게임 시스템관련된 변수 등을 초기화합니다.
        public void Init()
        {
            sm.SceneNumber = (int)SceneManager.TYPE.INTRO;
            sm.SetScene(SceneManager.TYPE.INTRO);
        }
        // 게임 시스템의 계산을 진행합니다.
        public void Update()
        {
            sm.scene.Update();
        }
        // 게임에서 보이는 텍스트들을 그려줍니다.
        public void Render()
        {
            Console.Clear();
            sm.scene.Render();
        }
        // 현재 씬이 무엇인지 판별하고 다르다면 바꿔줍니다
        public void SceneCheck()
        {
            sm.SceneChange();
        }
        // 게임 씬에 대한 추상 클래스입니다
        abstract class Scene
        {
            // 어떤 씬인지 판별할 숫자입니다.
            public int number;
             public abstract void Init(); // Scene을 초기화 합니다.
            public abstract void Update(); // Scene의 루프를 돕니다.
            public abstract void Render(); // Scene을 그려줍니다.
        }

        // 인터페이스를 사용하여 SceneManager 를 미리 구성합니다.
        interface SceneManagerBase
        {
            void SetScene(int whatscene);
            int SceneNumber
            {
                get;
                set;
            }
        }

        // 씬을 관리할 클래스를 선언합니다.
        class SceneManager
        {
            int s_number; // 씬의 넘버를 담아둘 변수입니다.
            public Scene scene; // 관리, 변경될 씬입니다. (사용자가 만들 수 있음)
            public int SceneNumber // get, set을 사용해서 s_number를 SceneNumber로 받아오거나 보낼 수 있다.
            {
                get
                {
                    return s_number;
                }
                set
                {
                    s_number = value;
                }
            }
            // 각 씬들을 준비해 놓습니다.
            private IntroScene i_scene = new IntroScene();
            private GameScene g_scene = new GameScene();
            private FireEnding f_scene = new FireEnding();
            private WaitEnding w_scene = new WaitEnding();
            private CoroEnding c_scene = new CoroEnding();
            // 씬의 이름을 열거형으로 둡니다.
            public enum TYPE
            {
                INTRO,
                GAME,
                FIREENDING,
                WAITENDING,
                COROENDING
            }

            // 씬을 바꿔주는 함수 입니다. 
            public void SceneChange()
            {
                if (scene.number != SceneNumber) // 예전의 씬 넘버와 다를 경우 씬을 변환 시켜줍니다.
                {
                    SetScene((TYPE)scene.number);
                }
            }
            // 씬을 설정하는 함수 입니다.
            public void SetScene(TYPE whatscene)
            {
                switch (whatscene) // 방금 전 선언 했던 열거형으로 값을 받아서 그에 맞는 씬을 설정해줍니다.
                {
                    case TYPE.INTRO:
                        scene = i_scene;
                        break;
                    case TYPE.GAME:
                        scene = g_scene;
                        break;
                    case TYPE.FIREENDING:
                        scene = f_scene;
                        break;
                    case TYPE.WAITENDING:
                        scene = w_scene;
                        break;
                    case TYPE.COROENDING:
                        scene = c_scene;
                        break;
                }
                Console.Clear(); // 화면을 비워줍니다.
                scene.Init(); // 설정한 후 씬을 초기화 시켜줍니다.
            }
        }

        // 게임을 실행 시 처음으로 나오는 씬인 인트로 씬입니다.
        class IntroScene : Scene
        {
            bool errorvalue = false; // 값을 제대로 입력했는 지 확인 하는 변수 입니다.
            // 추상 클래스에서 선언 했던 함수들을 오버라이드 해주어 사용합니다.
            public override void Init() // 씬을 초기화 해줍니다.
            {
                // 인트로를 보여주고 값이 정해지지 않았기 때문에 확인 변수를 false로 설정합니다.
                ShowIntro();
                errorvalue = false;
            }
            public override void Update() // 씬의 루프를 돕니다.
            {
                // 입력받는 값을 체크합니다.
                CheckNumber();
            }
            public override void Render() // 씬을 그려줍니다.
            {
                //인트로를 보여줍니다.
                ShowIntro();
            }

            // 값을 입력받아 체크하는 함수입니다.
            private void CheckNumber()
            {
                int num = int.Parse(Console.ReadLine()); // 입력받는 값을 정수형으로 바꿔줍니다.

                switch (num)
                {
                    case 1:
                        number = 1; // 다음 씬으로 숫자를 설정하여 씬이 변경되게 합니다.
                        break;
                    case 2:
                        GameSystem.GameOver = true; // 게임을 종료시키기위해서 게임오버변수를 true로 설정합니다.
                        break;
                    default:
                        errorvalue = true; // 값을 잘못입력 하였음을 알립니다.
                        break;
                }
            }

            // 인트로를 보여주는 함수입니다.
            private void ShowIntro()
            {
                Console.WriteLine("┌─────────────────────────────────────────────────┐");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                  나 방 무 찌 르 기              │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    1. 게임 시작                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    2. 게임 종료                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                숫자를 입력해 주세요             │");
                if (errorvalue)
                    Console.WriteLine("│                 다시 입력해 주세요              │");
                else
                    Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("└─────────────────────────────────────────────────┘");

            }
        }

        // 각 오브젝트들을 그릴 수 있게 하는 추상클래스입니다.
        abstract class RenderObject
        {
            protected char[,] render; // 그릴 무언가를 담을 다차원 배열입니다.
            protected int width, height, x, y; // 그릴 오브젝트의 너비, 높이, x, y값을 지정합니다.
            public abstract void SetRender(); // 그릴 무언가를 설정합니다.
            public void Render(ref char[,] screen) // 그려줄 판에 이 오브젝트를 그려줍니다.
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        screen[y + i, x + j] = render[i, j];
                    }
                }
            }

            public void SetPosition(int tx, int ty) // 그릴 좌표를 지정합니다.
            {
                x = tx;
                y = ty;
            }
        }

        // 나방을 그려주는 클래스입니다.
        class Moth : RenderObject
        {
            public override void SetRender() // 나방을 그릴 너비, 높이, 그릴 것을 설정합니다.
            {
                width = 3;
                height = 1;
                render = new char[1, 3]
                {
                    {'▶','i','◀' }
                };
            }
        }

        // 학생을 그려주는 클래스입니다.
        class Student : RenderObject
        {
            public override void SetRender() // 학생을 그릴 너비, 높이, 그릴 것을 설정합니다.
            {
                SetPosition(1, 1);
                width = 12;
                height = 11;
                render = new char[11, 12]
                {
                    { '┌', '─', '─', '─', '─', '┘','└','─','─','─','─','┐'},
                    { ' ', ' ', ' ', ' ', ' ', ' ',' ',' ',' ',' ',' ' , ' '},
                    {'└', '┳', '┳', '┘', ' ', ' ',' ',' ','└','┳','┳','┘' },
                    { '종', ' ', ' ', ' ', ' ', ' ',' ',' ',' ','나',' ' , ' '},
                    { ' ', ' ', ' ', ' ', ' ', ' ',' ',' ',' ',' ',' ' , ' '},
                    { ' ', '강', ' ', ' ', ' ', ' ',' ',' ',' ',' ','방' , ' '},
                    { ' ', ' ', ' ', ' ', '└', '─','─','┘',' ',' ',' ' , ' '},
                    { '원', '┌', '─', '─', '─', '─','─','┐',' ','싫',' ' , ' '},
                    { ' ', ' ', ' ', ' ', '─', '─','─',' ',' ',' ',' ' , ' '},
                    { ' ', '츄', ' ', ' ', ' ', ' ',' ',' ',' ',' ','어' , ' '},
                    { ' ', ' ', ' ', ' ', ' ', ' ',' ',' ',' ',' ',' ' , ' '},
                };
            }
        }

        // UI을 그려주는 클래스입니다.
        class UI : RenderObject
        {
            public override void SetRender() // UI을 그릴 너비, 높이, 그릴 것을 설정합니다.
            {
                SetPosition(1, 12);
                width = 30;
                height = 2;
                render = new char[2, 30]
                {
                    { ' ','[','청','강','대','생',']',' ',' ',' ',' ',' ', ' ', ' ', ' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','[','나','방','들',']' },
                    { ' ','1','.','불','태','우','기',' ',' ','2','.','그', '냥', '두', '기',' ',' ','3','.','c','o','r','o',' ','나','방','바','이','러','스' }
                };
            }

               
        }

        // 본 게임이라고 불릴 수 있는 게임 씬 클래스 입니다.
        class GameScene : Scene
        {
            char[,] screen = new char[15, 50]; // 화면을 그릴 다차원 배열을 선언합니다.
            RenderObject[] ro = new RenderObject[11]; // 그려질 오브젝트를 담는 배열을 지정합니다.
            Student student = new Student(); // 학생 하나를 선언합니다.
            Moth[] moth = new Moth[9]; // 나방 9개를 선언합니다.
            UI ui = new UI(); // ui 하나를 선언합니다.
            public override void Init() // 씬을 초기화 합니다.
            {
                // 학생, 나방, ui 등 각 오브젝트들을 배열에 담습니다.
                ro[0] = student;
                for (int i = 1; i < 10; i++)
                {
                    moth[i - 1] = new Moth();
                    ro[i] = moth[i - 1];
                }
                ro[10] = ui;

                // 나방의 위치를 설정해줍니다.
                moth[0].SetPosition(30, 1);
                moth[1].SetPosition(24, 2);
                moth[2].SetPosition(30, 3);
                moth[3].SetPosition(40, 5);
                moth[4].SetPosition(30, 6);
                moth[5].SetPosition(35, 7);
                moth[6].SetPosition(42, 9);
                moth[7].SetPosition(26, 10);
                moth[8].SetPosition(33, 2);

                // 씬을 그려줍니다.
                Render();
            }
            public override void Update() // 씬의 루프를 돕니다.
            {
                // 받은 값을 체크합니다.
                CheckNumber();
            }
            public override void Render() // 씬을 그려줍니다.
            {
                // 그려줄 판을 지정하고 그곳에 오브젝트를 올린 후 게임에 보여줍니다.
                SetScreen();
                ShowObject();
                ShowGame();
            }

            // 오브젝트들을 그려줍니다.
            private void ShowObject()
            {
                for(int i = 0; i < 11; i++)
                {
                    ro[i].SetRender();
                    ro[i].Render(ref screen);
                }
            }

            // 게임을 보여줍니다.
            private void ShowGame()
            {
                for (int i = 0; i < 15; i++)
                {
                    for(int j= 0; j < 50; j++)
                    {
                        Console.Write(screen[i, j]);
                    }
                    Console.WriteLine();
                }

            }

            // 입력받은 값을 체크해주는 함수 입니다.
            private void CheckNumber()
            {
                int num = int.Parse(Console.ReadLine()); // 값을 받아와서 정수값으로 변환해줍니다.

                // 각 해당하는 씬으로 이동합니다.
                switch (num)
                {
                    case 1:
                        number = 2; 
                        break;
                    case 2:
                        number = 3;
                        break;
                    case 3:
                        number = 4;
                        break;
                }
            }

            // 그려줄 판을 설정합니다.
            private void SetScreen()
            {
                screen[0, 0] = '┌';
                for (int j = 1; j < 50; j++)
                {
                    screen[0, j] = '─';
                }
                screen[0, 49] = '┐';
                for (int i = 1; i < 14; i++)
                {
                    screen[i, 0] = '│';
                    for (int j = 1; j < 50; j++)
                    {
                        screen[i, j] = ' ';
                    }
                }
                screen[1, 47] = '│';
                screen[2, 45] = '│';
                screen[3, 47] = '│';
                screen[4, 47] = '│';
                screen[5, 47] = '│';
                screen[6, 45] = '│';
                screen[7, 47] = '│';
                screen[8, 47] = '│';
                screen[9, 47] = '│';
                screen[10, 45] = '│';
                screen[11, 49] = '│';
                screen[12, 42] = '│';
                screen[13, 35] = '│';
                screen[14, 0] = '└';
                for (int j = 1; j < 50; j++)
                {
                    screen[14, j] = '─';
                }
                screen[14, 49] = '┘';
            }
        }

        // 불태우기 엔딩 씬 클래스입니다.
        class FireEnding : Scene
        {
            bool errorvalue = false; 
            public override void Init()
            {
                ShowEnding();
                errorvalue = false;
            }
            public override void Update()
            {
                CheckNumber();
            }
            public override void Render()
            {
                ShowEnding();
            }

            private void CheckNumber()
            {
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        number = 1;
                        break;
                    case 2:
                        GameSystem.GameOver = true;
                        break;
                    default:
                        errorvalue = true;
                        break;
                }
            }

            private void ShowEnding()
            {
                Console.WriteLine("┌─────────────────────────────────────────────────┐");
                Console.WriteLine("│                                 ▶i◀           │");
                Console.WriteLine("│       ▶i◀                                     │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│        나방이 불에 타버렸지만 살아남았습니다.   │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│    ▶i◀           1. 다시 시작                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    2. 게임 종료                 │");
                Console.WriteLine("│                                      ▶i◀      │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                숫자를 입력해 주세요             │");
                if (errorvalue)
                    Console.WriteLine("│                 다시 입력해 주세요              │");
                else
                    Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("└─────────────────────────────────────────────────┘");
            }
        }

        // 그냥두기 엔딩 씬 클래스입니다.
        class WaitEnding : Scene
        {
            bool errorvalue = false;
            public override void Init()
            {
                ShowEnding();
                errorvalue = false;
            }
            public override void Update()
            {
                CheckNumber();
            }
            public override void Render()
            {
                ShowEnding();
            }

            private void CheckNumber()
            {
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        number = 1;
                        break;
                    case 2:
                        GameSystem.GameOver = true;
                        break;
                    default:
                        errorvalue = true;
                        break;
                }
            }

            private void ShowEnding()
            {
                Console.WriteLine("┌─────────────────────────────────────────────────┐");
                Console.WriteLine("│                                 ▶i◀           │");
                Console.WriteLine("│       ▶i◀                                     │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│              나방이 더욱 증식 되었습니다.       │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│    ▶i◀           1. 다시 시작      ▶i◀      │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    2. 게임 종료                 │");
                Console.WriteLine("│         ▶i◀                        ▶i◀      │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                숫자를 입력해 주세요             │");
                if (errorvalue)
                    Console.WriteLine("│                 다시 입력해 주세요              │");
                else
                    Console.WriteLine("│          ▶i◀                                  │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("└─────────────────────────────────────────────────┘");
            }
        }

        // coro 나방 바이러스 엔딩 씬 클래스입니다.
        class CoroEnding : Scene
        {
            bool errorvalue = false;
            public override void Init()
            {
                ShowEnding();
                errorvalue = false;
            }
            public override void Update()
            {
                CheckNumber();
            }
            public override void Render()
            {
                ShowEnding();
            }

            private void CheckNumber()
            {
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        number = 1;
                        break;
                    case 2:
                        GameSystem.GameOver = true;
                        break;
                    default:
                        errorvalue = true;
                        break;
                }
            }

            private void ShowEnding()
            {
                Console.WriteLine("┌─────────────────────────────────────────────────┐");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│ 나방이 사회적 거리두기를 실천하여 사라졌습니다. │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    1. 다시 시작                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                    2. 게임 종료                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("│                숫자를 입력해 주세요             │");
                if (errorvalue)
                    Console.WriteLine("│                 다시 입력해 주세요              │");
                else
                    Console.WriteLine("│                                                 │");
                Console.WriteLine("│                                                 │");
                Console.WriteLine("└─────────────────────────────────────────────────┘");
            }
        }
    }





}
