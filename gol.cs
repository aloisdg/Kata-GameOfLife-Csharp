using System;
using System.Linq;
using System.Collections.Generic;

public class Cell
{
	char _content = 'x';
	
	public Cell(char content)
	{
		_content = content;
	}
	
	public void Die()
	{
		_content = '_';
	}
	
	public void Live()
	{
		_content = 'x';
	}
	
	public override string ToString() => _content.ToString();
}

public class Grid
{
	List<List<Cell>> _grid = new List<List<Cell>>();
	
	public Grid(IEnumerable<IEnumerable<Cell>> grid)
	{
		_grid = grid.Select(x => x.ToList()).ToList();
	}
	
	private void AddNewLine(int position, int length)
	{
		if (position % length == 0)
			_grid.Add(new List<Cell>());
	}
	
	private void AddNewCell(char content)
	{		
		_grid.Last().Add(new Cell(content));
	}
	
	public Grid(string grid, int length)
	{
		for (var i = 0; i < grid.Length; i++)
		{
			AddNewLine(i, length);
			AddNewCell(grid[i]);
		}
	}
	
	public override string ToString()
    {
        return string.Concat(_grid.Select(line => string.Concat(line)));
    }
	
	public string ToPrettyString()
    {
        return string.Join("\n", _grid.Select(line => string.Concat(line)));
    }
	
	public void Live()
	{
		var size = _grid.Count();
		for (var i = 0; i < size; i++)
		{
			var line = _grid[i];
			for (var j = 0; j < size; j++)
			{
				_grid[i][j].Die();	
			}
		}
	}
}

public class Program
{
	public static void Main()
	{
		CanACellLives();
		CanACellDies();
			
		CanPrint();
		ACellAloneShouldDie();
		TwoCellsShouldLive();
	}

	public static void CanACellLives()
	{
		var cell = new Cell('_');
		cell.Live();
		Console.WriteLine("x" == cell.ToString());
	}
	
	
	public static void CanACellDies()
	{
		var cell = new Cell('x');
		cell.Die();
		Console.WriteLine("_" == cell.ToString());
	}
	
	public static void CanPrint()
	{
		var grid = new Grid("____x____", 3);
		Console.WriteLine("____x____" == grid.ToString());
	}
	
		
	public static void ACellAloneShouldDie()
	{
		var grid = new Grid("____x____", 3);
		grid.Live();
		Console.WriteLine("_________" == grid.ToString());
	}
	
	public static void TwoCellsShouldLive()
	{
		var grid = new Grid("____x____", 3);
		grid.Live();
		Console.WriteLine("___xx____" == grid.ToString());
	}
}
