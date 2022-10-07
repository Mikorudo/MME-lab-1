using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MME_lab_1
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ChangeMatrixSize(null, null);
		}

		private class Matrix
		{
			public double[,] array;
			public MatrixType type;
			public Matrix(double[,] array, MatrixType type)
			{
				this.array = array;
				this.type = type;
			}
			private double? GetSaddlePoint()
			{
				for (int i = 0; i < array.GetLength(0); i++)
				{
					for (int j = 0; j < array.GetLength(1); j++)
					{
						if (IsMinInRow(i, j) && IsMaxInCol(i, j))
							return array[i, j];
					}
				}
				return null;
			}
			private bool IsMaxInCol(int i, int j)
			{
				for (int k = 0; k < array.GetLength(0); k++)
				{
					if (array[k, j] > array[i, j])
						return false;
				}
				return true;
			}
			private bool IsMinInRow(int i, int j)
			{
				for (int k = 0; k < array.GetLength(1); k++)
				{
					if (array[i, k] < array[i, j])
						return false;
				}
				return true;
			}
			private void LaplaceCritera() // Добавить результат
			{
				List<(double, int, int)> values = new List<(double, int, int)>();
				int i, j;
				for (i = 0; i < array.GetLength(0); i++)
				{
					double sum = 0;
					for (j = 0; j < array.GetLength(1); j++)
					{
						sum += array[i, j];
					}
					values.Add((sum * (1.0 / array.GetLength(1)), i, j));
				}
				switch (type)
				{
					case MatrixType.WinType:
						double maxReuslt = values.Max(t => t.Item1);
						//var maxResult = values.OrderByDescending(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Лапласа: Решение: {maxResult.Item2 + 1}, значение {maxResult.Item1}");
						break;
					case MatrixType.LoseType:
						double minReulst = values.Min(t => t.Item1);
						//var minResult = values.OrderBy(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Лапласа: Решение: {minResult.Item2 + 1}, значение {minResult.Item1}");
						break;
					default:
						break;
				}
			}

			private void WaldCriteria() //Вывод результата
			{
				List<(double, int, int)> values = new List<(double, int, int)>();
				int i, j;
				switch (type)
				{
					case MatrixType.WinType:
						for (i = 0; i < array.GetLength(0); i++)
						{
							double minRow = double.MaxValue;
							int index = 0;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
								{
									minRow = array[i, j];
									index = j;
								}
							}
							values.Add((minRow, i, index));
						}
						var maxResult = values.Max(t => t.Item1);	//.OrderByDescending(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Вальда:  Решение: {maxResult.Item2 + 1}, значение {maxResult.Item1}");
						break;
					case MatrixType.LoseType:

						for (i = 0; i < array.GetLength(0); i++)
						{
							double maxRow = double.MinValue;
							int index = 0;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (maxRow < array[i, j])
								{
									maxRow = array[i, j];
									index = j;
								}
							}
							values.Add((maxRow, i, j));
						}
						var minResult = values.Min(t => t.Item1);	//OrderBy(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Вальда: Решение: {minResult.Item2 + 1}, значение {minResult.Item1}");
						break;
					default:
						break;
				}
			}

			private void HurwitzCriteria(double alpha) //Вывод
			{
				List<(double, int, int)> values = new List<(double, int, int)>();
				int i, j;
				double minRow, maxRow;
				switch (type)
				{
					case MatrixType.WinType:
						for (i = 0; i < array.GetLength(0); i++)
						{
							minRow = int.MaxValue;
							maxRow = int.MinValue;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
									minRow = array[i, j];
								if (maxRow < array[i, j]) maxRow = array[i, j];
							}
							values.Add((alpha * maxRow + (1 - alpha) * minRow, i, j));
						}
						var maxResult = values.Max(t => t.Item1); //.OrderByDescending(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Гурвица:  Решение: {maxResult.Item2 + 1} значение {maxResult.Item1}");
						break;
					case MatrixType.LoseType:
						for (i = 0; i < array.GetLength(0); i++)
						{
							minRow = int.MaxValue;
							maxRow = int.MinValue;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
									minRow = array[i, j];
								if (maxRow < array[i, j]) maxRow = array[i, j];
							}
							values.Add((alpha * minRow + (1 - alpha) * maxRow, i, j));
						}
						var minResult = values.Min(t => t.Item1);//.OrderBy(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию Гурвица:  Решение: {minResult.Item2 + 1} значение {minResult.Item1}");
						break;
					default:
						break;
				}
			}

			private void OptimismCritera()
			{
				List<(double, int, int)> values = new List<(double, int, int)>();
				int i, j;
				switch (type)
				{
					case MatrixType.WinType:
						for (i = 0; i < array.GetLength(0); i++)
						{
							double maxRow = double.MinValue;
							int index = 0;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (maxRow < array[i, j])
								{
									maxRow = array[i, j];
									index = j;
								}
							}
							values.Add((maxRow, i, j));
						}
						var maxResult = values.OrderByDescending(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию оптимизма: Решение: {maxResult.Item2 + 1}, значение {maxResult.Item1}");
						break;
					case MatrixType.LoseType:
						for (i = 0; i < array.GetLength(0); i++)
						{
							double minRow = double.MaxValue;
							int index = 0;
							for (j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
								{
									minRow = array[i, j];
									index = j;
								}
							}
							values.Add((minRow, i, index));
						}
						var minResult = values.OrderBy(t => t.Item1).ToList().First();
						//Console.WriteLine($"По критерию оптимизма:  Решение: {minResult.Item2 + 1}, значение {minResult.Item1}");
						break;
					default:
						break;
				}
			}

			private void BayesCriteria(double[] probs)
			{
				if (array.GetLength(0) != probs.Length)
				{
					throw new Exception("Кол-во вероятностей и строк не совпадает!");
				}
				if (probs.Sum() != 1)
				{
					throw new Exception("Сумма вероятностей не равна 1!");
				}
				List<(double, int, int)> values = new List<(double, int, int)>();
				int i, j;
				for (i = 0; i < array.GetLength(0); i++)
				{
					double sum = 0;
					for (j = 0; j < array.GetLength(1); j++)
					{
						sum += array[i, j] * probs[j];
					}
					values.Add((sum, i, j));
				}
				switch (type)
				{
					case MatrixType.WinType:
						var maxResult = values.OrderByDescending(t => t.Item1).ToList().First();
						//Console.WriteLine($"По байесовскому методу: Решение: {maxResult.Item2 + 1}, значение {maxResult.Item1}");
						break;
					case MatrixType.LoseType:
						var minResult = values.OrderBy(t => t.Item1).ToList().First();
						//Console.WriteLine($"По байесовскому методу: Решение: {minResult.Item2 + 1}, значение {minResult.Item1}");
						break;
					default:
						break;
				}
			}
		}

		enum MatrixType
		{
			WinType,
			LoseType,
			ErrorType
		}
		private int matrixWidth;
		private int matrixHeight;
		TextBox[,] matrixInput;
		TextBox[] paramsInput;

		private void ChangeMatrixSize(object sender, RoutedEventArgs e)
		{
			try
			{
				matrixWidth = Convert.ToInt32(MatrixWidthInput.Text);
				matrixHeight = Convert.ToInt32(MatrixHeightInput.Text);
				if (matrixWidth < 1 || matrixHeight < 1)
					throw new Exception("Недостаточный размер матрицы");
				matrixInput = new TextBox[matrixHeight, matrixWidth];
				paramsInput = new TextBox[matrixWidth];
				

				ChangeInputMatrix();
				ChangeInputParams();
				ChangeSolutionGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
		}
		private MatrixType GetMatrixType()
		{
			string str = MatrixTypeInput.Text;
			if (str == "П" || str == "п")
				return MatrixType.LoseType;
			else if (str == "В" || str == "в")
				return MatrixType.WinType;
			else
				return MatrixType.ErrorType;
		}
		private double[,] GetMatrixValues()
		{
			double[,] array = new double[matrixHeight, matrixWidth];
			for (int i = 0; i < matrixHeight; i++)
				for (int j = 0; j < matrixWidth; j++)
					array[i, j] = Convert.ToDouble(matrixInput[i, j]);
			return array;
		}
		private void ChangeInputMatrix()
		{
			#region CreateGrid
			Grid newGrid = new Grid();
			newGrid.Margin = new Thickness(5, 5, 5, 5);
			for (int i = 0; i < matrixWidth + 1; i++)
				newGrid.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < matrixHeight + 1; i++)
				newGrid.RowDefinitions.Add(new RowDefinition());
			#endregion
			#region FillGrid
			for (int i = 0; i < matrixHeight; i++)
			{
				for (int j = 0; j < matrixWidth; j++)
				{
					TextBox textBox = new TextBox();
					matrixInput[i, j] = textBox;
					textBox.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBox, j + 1);
					Grid.SetRow(textBox, i + 1);
					newGrid.Children.Add(textBox);
				}
			}
			for (int i = 1; i <= matrixWidth; i++)
			{
				TextBlock textBlock = new TextBlock();
				textBlock.TextAlignment = TextAlignment.Center;
				textBlock.Margin = new Thickness(0, 0, 0, 2);
				Grid.SetColumn(textBlock, i);
				Grid.SetRow(textBlock, 0);
				textBlock.Text = "B" + i;
				newGrid.Children.Add(textBlock);
			}
			for (int i = 1; i <= matrixHeight; i++)
			{
				TextBlock textBlock = new TextBlock();
				textBlock.TextAlignment = TextAlignment.Right;
				textBlock.Margin = new Thickness(0, 0, 5, 0);
				Grid.SetColumn(textBlock, 0);
				Grid.SetRow(textBlock, i);
				textBlock.Text = "A" + i;
				newGrid.Children.Add(textBlock);
			}
			#endregion
			#region UpdateVisualGrid
			int index = matrixSP.Children.IndexOf(matrixGrid);
			matrixSP.Children.RemoveAt(index);
			matrixGrid = newGrid;
			matrixSP.Children.Insert(index, newGrid);
			#endregion
		}

		private void ChangeInputParams()
		{
			#region CreateGrid
			Grid newGrid = new Grid();
			newGrid.Margin = new Thickness(5, 5, 5, 5);
			for (int i = 0; i < matrixWidth; i++)
				newGrid.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < 2; i++)
				newGrid.RowDefinitions.Add(new RowDefinition());
			#endregion
			#region FillGrid
			for (int i = 0; i < matrixWidth; i++)
			{
				TextBlock textBlock = new TextBlock();
				textBlock.TextAlignment = TextAlignment.Center;
				Grid.SetColumn(textBlock, i);
				Grid.SetRow(textBlock, 0);
				textBlock.Text = "p" + (i + 1);
				newGrid.Children.Add(textBlock);

				TextBox textBox = new TextBox();
				paramsInput[i] = textBox;
				textBox.TextAlignment = TextAlignment.Center;
				Grid.SetColumn(textBox, i);
				Grid.SetRow(textBox, 1);
				newGrid.Children.Add(textBox);
			}
			#endregion
			#region UpdateVisualGrid
			int index = paramsSP.Children.IndexOf(pParamsGrid);
			paramsSP.Children.RemoveAt(index);
			pParamsGrid = newGrid;
			paramsSP.Children.Insert(index, newGrid);
			#endregion
		}

		private void ChangeSolutionGrid()
		{
			#region 1
			Grid newGrid = new Grid();
			newGrid.Margin = new Thickness(5, 5, 5, 5);
			for (int i = 0; i < 5; i++)
				newGrid.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < 2 + matrixHeight; i++)
				newGrid.RowDefinitions.Add(new RowDefinition());
			#endregion
			#region 2

			TextBlock textBlock1 = new TextBlock();
			textBlock1.TextAlignment = TextAlignment.Center;
			textBlock1.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(textBlock1, 0);
			Grid.SetRow(textBlock1, 0);
			textBlock1.Text = "Седловая точка";
			newGrid.Children.Add(textBlock1);

			TextBlock textBlock2 = new TextBlock();
			textBlock2.TextAlignment = TextAlignment.Center;
			textBlock2.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(textBlock2, 1);
			Grid.SetRow(textBlock2, 0);
			Grid.SetColumnSpan(textBlock2, 3);
			textBlock2.Text = "Решение по критерию";
			newGrid.Children.Add(textBlock2);

			TextBlock textBlock3 = new TextBlock();
			textBlock3.TextAlignment = TextAlignment.Center;
			textBlock3.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(textBlock3, 4);
			Grid.SetRow(textBlock3, 0);
			textBlock3.Text = "Байесовский подход";
			newGrid.Children.Add(textBlock3);

			TextBlock textBlock4 = new TextBlock();
			textBlock4.TextAlignment = TextAlignment.Center;
			Grid.SetColumn(textBlock4, 1);
			Grid.SetRow(textBlock4, 1);
			textBlock4.Text = "Гурвица";
			newGrid.Children.Add(textBlock4);

			TextBlock textBlock5 = new TextBlock();
			textBlock5.TextAlignment = TextAlignment.Center;
			Grid.SetColumn(textBlock5, 2);
			Grid.SetRow(textBlock5, 1);
			textBlock5.Text = "Вальда";
			newGrid.Children.Add(textBlock5);

			TextBlock textBlock6 = new TextBlock();
			textBlock6.TextAlignment = TextAlignment.Center;
			Grid.SetColumn(textBlock6, 3);
			Grid.SetRow(textBlock6, 1);
			textBlock6.Text = "Лапласа";
			newGrid.Children.Add(textBlock6);

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < matrixHeight; j++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBlock, i);
					Grid.SetRow(textBlock, j + 2);
					textBlock.Text = "текст";
					newGrid.Children.Add(textBlock);
				}
			}
			#endregion
			#region 3
			int index = matrixSP.Children.IndexOf(SolutionGrid);
			matrixSP.Children.RemoveAt(index);
			SolutionGrid = newGrid;
			matrixSP.Children.Insert(index, newGrid);
			#endregion
		}

		private void GetSolution(object sender, RoutedEventArgs e)
		{
			try
			{
				double[,] array = GetMatrixValues();
				MatrixType matrixType = GetMatrixType();
				if (matrixType == MatrixType.ErrorType)
					throw new Exception("Ошибка в типе матрицы");
				Matrix matrix = new Matrix(array, matrixType);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
		}
	}
}
