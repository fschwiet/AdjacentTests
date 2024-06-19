namespace Library.RFC3092;

public class Bar
{
	public int Divide(int a, int b) => a / b;
}

public class BarTests
{
	[Fact]
	public void CanDivide()
	{
		Assert.Equal(3, new Bar().Divide(6, 2));
	}
}