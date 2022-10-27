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
			public double? GetSaddlePoint()
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

			public void SavageCritera(TextBlock[] solutionOutput)
			{
				List<double> values = new List<double>();
				double[,] newMatrix = new double[array.GetLength(0), array.GetLength(1)];
				switch (type)
				{
					case MatrixType.WinType:
						for (int j = 0; j < array.GetLength(1); j++)
						{
							double maxColumn = double.MinValue;
							for (int i = 0; i < array.GetLength(0); i++)
							{
								if (maxColumn < array[i, j])
								{
									maxColumn = array[i, j];
								}
							}
							values.Add(maxColumn);
						}
						for (int j = 0; j < array.GetLength(1); j++)
						{
							for (int i = 0; i < array.GetLength(0); i++)
							{
								newMatrix[i, j] = Math.Round(values[i] - array[i, j], 1);
							}
						}
						break;
					case MatrixType.LoseType:
						for (int j = 0; j < array.GetLength(1); j++)
						{
							double minColumn = double.MaxValue;
							for (int i = 0; i < array.GetLength(0); i++)
							{
								if (minColumn > array[i, j])
								{
									minColumn = array[i, j];
								}
							}
							values.Add(minColumn);
						}
						for (int j = 0; j < array.GetLength(1); j++)
						{
							for (int i = 0; i < array.GetLength(0); i++)
							{
								newMatrix[i, j] = Math.Round(array[i, j] - values[j], 1);
							}
						}
						break;
					default:
						break;
				}
				values.Clear();
				for (int i = 0; i < newMatrix.GetLength(0); i++)
				{
					double maxRow = double.MinValue;
					for (int j = 0; j < newMatrix.GetLength(1); j++)
					{
						if (maxRow < newMatrix[i, j])
						{
							maxRow = newMatrix[i, j];
						}
					}
					values.Add(maxRow);
				}
				var minResult = values.Min(t => t);
				for (int i = 0; i < array.GetLength(0); i++)
				{
					solutionOutput[i].Text = values[i].ToString();
					if (values[i] == minResult)
						solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
					else
						solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
				}
			}
			public void LaplaceCritera(TextBlock[] solutionOutput) // Добавить результат
			{
				List<double> values = new List<double>();
				for (int i = 0; i < array.GetLength(0); i++)
				{
					double sum = 0;
					for (int j = 0; j < array.GetLength(1); j++)
					{
						sum += array[i, j];
					}
					values.Add(sum * (1.0 / array.GetLength(1)));
				}
				switch (type)
				{
					case MatrixType.WinType:
						double maxResult = values.Max(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == maxResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					case MatrixType.LoseType:
						double minResult = values.Min(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == minResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					default:
						break;
				}
			}

			public void WaldCriteria(TextBlock[] solutionOutput) //Вывод результата
			{
				List<double> values = new List<double>();
				switch (type)
				{
					case MatrixType.WinType:
						for (int i = 0; i < array.GetLength(0); i++)
						{
							double minRow = double.MaxValue;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
								{
									minRow = array[i, j];
								}
							}
							values.Add(minRow);
						}

						double maxResult = values.Max(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == maxResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					case MatrixType.LoseType:

						for (int i = 0; i < array.GetLength(0); i++)
						{
							double maxRow = double.MinValue;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (maxRow < array[i, j])
								{
									maxRow = array[i, j];
								}
							}
							values.Add(maxRow);
						}
						double minResult = values.Min(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == minResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					default:
						break;
				}
			}

			public void HurwitzCriteria(double alpha, TextBlock[] solutionOutput) //Вывод
			{
				List<double> values = new List<double>();
				double minRow, maxRow;
				switch (type)
				{
					case MatrixType.WinType:
						for (int i = 0; i < array.GetLength(0); i++)
						{
							minRow = int.MaxValue;
							maxRow = int.MinValue;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
									minRow = array[i, j];
								if (maxRow < array[i, j]) maxRow = array[i, j];
							}
							values.Add(alpha * maxRow + (1 - alpha) * minRow);
						}
						double maxResult = values.Max(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == maxResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					case MatrixType.LoseType:
						for (int i = 0; i < array.GetLength(0); i++)
						{
							minRow = int.MaxValue;
							maxRow = int.MinValue;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
									minRow = array[i, j];
								if (maxRow < array[i, j]) maxRow = array[i, j];
							}
							values.Add(alpha * minRow + (1 - alpha) * maxRow);
						}

						double minResult = values.Min(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == minResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					default:
						break;
				}
			}

			public void OptimismCritera(TextBlock[] solutionOutput)
			{
				List<double> values = new List<double>();
				switch (type)
				{
					case MatrixType.WinType:
						for (int i = 0; i < array.GetLength(0); i++)
						{
							double maxRow = double.MinValue;
							int index = 0;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (maxRow < array[i, j])
								{
									maxRow = array[i, j];
									index = j;
								}
							}
							values.Add(maxRow);
						}
						double maxResult = values.Max(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == maxResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					case MatrixType.LoseType:
						for (int i = 0; i < array.GetLength(0); i++)
						{
							double minRow = double.MaxValue;
							int index = 0;
							for (int j = 0; j < array.GetLength(1); j++)
							{
								if (minRow > array[i, j])
								{
									minRow = array[i, j];
									index = j;
								}
							}
							values.Add(minRow);
						}
						double minResult = values.Min(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == minResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					default:
						break;
				}
			}

			public void BayesCriteria(double[] probs, TextBlock[] solutionOutput)
			{
				if (array.GetLength(1) != probs.Length)
				{
					throw new Exception("Кол-во вероятностей и строк не совпадает!");
				}
				if (probs.Sum() != 1)
				{
					throw new Exception("Сумма вероятностей не равна 1!");
				}
				List<double> values = new List<double>();
				for (int i = 0; i < array.GetLength(0); i++)
				{
					double sum = 0;
					for (int j = 0; j < array.GetLength(1); j++)
					{
						sum += array[i, j] * probs[j];
					}
					values.Add(sum);
				}
				switch (type)
				{
					case MatrixType.WinType:
						var maxResult = values.Max(t=>t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == maxResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
						break;
					case MatrixType.LoseType:
						var minResult = values.Min(t => t);
						for (int i = 0; i < array.GetLength(0); i++)
						{
							solutionOutput[i].Text = values[i].ToString();
							if (values[i] == minResult)
								solutionOutput[i].Background = new SolidColorBrush(Colors.Green);
							else
								solutionOutput[i].Background = new SolidColorBrush(Colors.Red);
						}
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
		TextBlock[,] solutionOutput;

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
					array[i, j] = Convert.ToDouble(matrixInput[i, j].Text);
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
			//newGrid.Margin = new Thickness(5, 5, 5, 5);
			for (int i = 0; i < 7; i++)
				newGrid.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < 2 + matrixHeight; i++)
				newGrid.RowDefinitions.Add(new RowDefinition());
			#endregion
			#region 2
			solutionOutput = new TextBlock[7, matrixHeight];
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < matrixHeight; j++)
				{
					TextBlock textBlock = new TextBlock();
					solutionOutput[i, j] = textBlock;
					textBlock.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBlock, i);
					Grid.SetRow(textBlock, j);
					textBlock.Text = "текст";
					newGrid.Children.Add(textBlock);
				}
			}
			#endregion
			#region 3
			int index = SolutionSP.Children.IndexOf(SolutionGrid);
			SolutionSP.Children.RemoveAt(index);
			SolutionGrid = newGrid;
			SolutionSP.Children.Insert(index, newGrid);
			#endregion
		}
		private TextBlock[] GetColumn(TextBlock[,] matrix, int columnNumber)
		{
			return Enumerable.Range(0, matrixHeight).Select(x => solutionOutput[columnNumber, x]).ToArray();
		}
		private void GetSolution(object sender, RoutedEventArgs e)
		{
			try
			{
				double alpha = Convert.ToDouble(aParamsInput.Text);
				double[] probs = new double[matrixWidth];
				for (int i = 0; i < matrixWidth; i++)
					probs[i] = Convert.ToDouble(paramsInput[i].Text);
				double[,] array = GetMatrixValues();
				MatrixType matrixType = GetMatrixType();
				if (matrixType == MatrixType.ErrorType)
					throw new Exception("Ошибка в типе матрицы");
				Matrix matrix = new Matrix(array, matrixType);
				for (int i = 0; i < matrixHeight; i++)
					solutionOutput[0, i].Text = i.ToString();
				double? saddlePoint = matrix.GetSaddlePoint();
				if (saddlePoint is null)
					SaddlePoint.Text = "Нет";
				else
					SaddlePoint.Text = saddlePoint.ToString();
				matrix.WaldCriteria(GetColumn(solutionOutput, 1));
				matrix.HurwitzCriteria(alpha, GetColumn(solutionOutput, 2));
				matrix.LaplaceCritera(GetColumn(solutionOutput, 3));
				matrix.SavageCritera(GetColumn(solutionOutput, 4));
				matrix.OptimismCritera(GetColumn(solutionOutput, 5));
				matrix.BayesCriteria(probs, GetColumn(solutionOutput, 6));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
		}
	}
}
