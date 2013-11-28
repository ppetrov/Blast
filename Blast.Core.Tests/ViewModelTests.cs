namespace Blast.Core.Tests
{
	public class ViewModelTests
	{
		 
	}

	public class TestViewModel : ViewModel
	{
		public string Name { get; set; }

		public TestViewModel()
		{
			this.Name = string.Empty;
		}
	}
}