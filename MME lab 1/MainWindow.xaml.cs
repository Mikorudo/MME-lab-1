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
			ChangeMatrix(null, null);
		}
		enum MatrixType
		{
			WinType,
			LoseType,
			ErrorType
		}
		int matrixWidth;
		int matrixHeight;
		MatrixType matrixType;
		//TextBlock[,] matrixInput;
		private void ChangeMatrix(object sender, RoutedEventArgs e)
		{
			try
			{
				matrixWidth = Convert.ToInt32(MatrixWidthInput.Text);
				matrixHeight = Convert.ToInt32(MatrixHeightInput.Text);

				#region ChangeInputMatrix
				#region CreateGrid
				Grid newMatrixGrid = new Grid();
				newMatrixGrid.Margin = new Thickness(5, 5, 5, 5);
				for (int i = 0; i < matrixWidth + 1; i++)
					newMatrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
				for (int i = 0; i < matrixHeight + 1; i++)
					newMatrixGrid.RowDefinitions.Add(new RowDefinition());
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
						newMatrixGrid.Children.Add(textBox);
					}
				}
				for (int i = 1; i <= matrixWidth; i++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBlock, i);
					Grid.SetRow(textBlock, 0);
					textBlock.Text = "B" + i;
					newMatrixGrid.Children.Add(textBlock);
				}
				for (int i = 1; i <= matrixHeight; i++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBlock, 0);
					Grid.SetRow(textBlock, i);
					textBlock.Text = "A" + i;
					newMatrixGrid.Children.Add(textBlock);
				}
				#endregion
				#region UpdateVisualGrid
				int index = matrixSP.Children.IndexOf(matrixGrid);
				matrixSP.Children.RemoveAt(index);
				matrixGrid = newMatrixGrid;
				matrixSP.Children.Insert(index, newMatrixGrid);
				#endregion
				#endregion
				#region ChangeInputParams
				#region CreateGrid
				Grid newParamsGrid = new Grid();
				newParamsGrid.Margin = new Thickness(5, 5, 5, 5);
				for (int i = 0; i < matrixWidth; i++)
					newParamsGrid.ColumnDefinitions.Add(new ColumnDefinition());
				for (int i = 0; i < 2; i++)
					newParamsGrid.RowDefinitions.Add(new RowDefinition());
				#endregion
				#region FillGrid
				for (int i = 0; i < matrixWidth; i++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBlock, i);
					Grid.SetRow(textBlock, 0);
					textBlock.Text = "p" + (i + 1);
					newParamsGrid.Children.Add(textBlock);

					TextBox textBox = new TextBox();
					textBox.TextAlignment = TextAlignment.Center;
					Grid.SetColumn(textBox, i);
					Grid.SetRow(textBox, 1);
					newParamsGrid.Children.Add(textBox);
				}
				#endregion
				#region ChangeGrid
				index = paramsSP.Children.IndexOf(pParamsGrid);
				paramsSP.Children.RemoveAt(index);
				pParamsGrid = newParamsGrid;
				paramsSP.Children.Insert(index, newParamsGrid);
				#endregion
				#endregion
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
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
