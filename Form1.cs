namespace WinFormsAppmaze2
{
    public partial class Form1 : Form
    {

        private int[,] maze;
        private int player1X, player1Y;
        private int player2X, player2Y;


        public Form1()
        {
            InitializeComponent();
            KeyDown += MazeForm_KeyDown;
            Paint += MazeForm_Paint;

            LoadMaze("S.txt");
        }

        private void LoadMaze(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                int rows = lines.Length;
                int cols = lines[0].Split(',').Length;

                maze = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    string[] values = lines[i].Split(',');

                    for (int j = 0; j < cols; j++)
                    {
                        if (values[j].Trim() == "E")
                        {
                            maze[i, j] = 2; 
                        }
                        else
                        {
                            maze[i, j] = int.Parse(values[j].Trim());

                            
                            if (maze[i, j] == 0)
                            {
                                player1X = i;
                                player1Y = j;
                            }
                            else if (maze[i, j] == 1)
                            {
                                player2X = i;
                                player2Y = j;
                            }
                        }
                    }
                }

                Refresh();
            }
            else
            {
                MessageBox.Show("Файл лабиринта не найден.");
                Close();
            }
        }

        private void MazeForm_KeyDown(object sender, KeyEventArgs e)
        {
            MovePlayer1(e.KeyCode);
            MovePlayer2(e.KeyCode);
            Refresh();
        }

        private void MovePlayer1(Keys key)
        {
            int deltaX = 0, deltaY = 0;

            switch (key)
            {
                case Keys.W:
                    deltaX = -1;
                    break;
                case Keys.S:
                    deltaX = 1;
                    break;
                case Keys.A:
                    deltaY = -1;
                    break;
                case Keys.D:
                    deltaY = 1;
                    break;
            }

            int newPlayerX = player1X + deltaX;
            int newPlayerY = player1Y + deltaY;

            if (IsWithinBounds(newPlayerX, newPlayerY) && maze[newPlayerX, newPlayerY] != 1)
            {
                player1X = newPlayerX;
                player1Y = newPlayerY;

                if (maze[player1X, player1Y] == 2)
                {
                    MessageBox.Show("Игрок 1 нашел выход!");
                    ShowFireworks();
                }
            }
        }

        private void ShowFireworks()
        {
            MessageBox.Show("Поздравляем! Салют!");
        }

        private void MovePlayer2(Keys key)
        {
            int deltaX = 0, deltaY = 0;

            switch (key)
            {
                case Keys.Up:
                    deltaX = -1;
                    break;
                case Keys.Down:
                    deltaX = 1;
                    break;
                case Keys.Left:
                    deltaY = -1;
                    break;
                case Keys.Right:
                    deltaY = 1;
                    break;
            }

            int newPlayerX = player2X + deltaX;
            int newPlayerY = player2Y + deltaY;

            if (IsWithinBounds(newPlayerX, newPlayerY) && maze[newPlayerX, newPlayerY] != 1)
            {
                player2X = newPlayerX;
                player2Y = newPlayerY;

                if (maze[player2X, player2Y] == 2)
                {
                    MessageBox.Show("Игрок 2 нашел выход!");
                    ShowFireworks();
                }
            }
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < maze.GetLength(0) && y >= 0 && y < maze.GetLength(1);
        }

        private void MazeForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int cellSize = 30;

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Brush brush = Brushes.White;

                    if (maze[i, j] == 1)
                        brush = Brushes.Black;
                    else if (maze[i, j] == 2)
                        brush = Brushes.Green;

                    g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);
                }
            }

           
            g.FillEllipse(Brushes.Red, player1Y * cellSize, player1X * cellSize, cellSize, cellSize);
            g.FillEllipse(Brushes.Blue, player2Y * cellSize, player2X * cellSize, cellSize, cellSize);
        }

    }
}
