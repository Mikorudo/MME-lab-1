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
		enum MatrixType
		{
			WinType,
			LoseType,
			ErrorType
		}
		private int matrixWidth;
		private int matrixHeight;
		private MatrixType matrixType;
		//TextBlock[,] matrixInput;
		private void ChangeMatrixSize(object sender, RoutedEventArgs e)
		{
			try
			{
				matrixWidth = Convert.ToInt32(MatrixWidthInput.Text);
				matrixHeight = Convert.ToInt32(MatrixHeightInput.Text);

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
			for (int i = 1; i <= matrixHeight; i++)
			{
				for (int j = 1; j <= matrixWidth; j++)
				{
					TextBox textBox = new TextBox();
					textBox.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBox, j);
					Grid.SetRow(textBox, i);
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
			matrixType = MatrixType.ErrorType;
			switch (MatrixTypeInput.Text)
			{
				case "В":
				case "в":
					matrixType = MatrixType.WinType;
					break;
				case "П":
				case "п":
					matrixType = MatrixType.LoseType;
					break;
				default:
					matrixType = MatrixType.ErrorType;
					break;
			}
		}
	}
}
