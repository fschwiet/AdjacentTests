namespace Library.RFC3092;

public class FooTests
{
	[Fact]
	public void GetSame_works()
	{
		Assert.Equal(246, new Foo().Multiply(123, 2));
	}
}