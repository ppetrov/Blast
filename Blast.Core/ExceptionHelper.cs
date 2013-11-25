using System;

namespace Blast.Core
{
	internal static class ExceptionHelper
	{
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw new ArgumentNullException(GetArgumentName(argument));
		}

		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(GetArgumentName(argument));
		}

		internal static string GetArgumentName(ExceptionArgument argument)
		{
			switch (argument)
			{
				case ExceptionArgument.Name:
					return @"name";
				case ExceptionArgument.Value:
					return @"value";
				case ExceptionArgument.Item:
					return @"item";
				case ExceptionArgument.Items:
					return @"items";
				case ExceptionArgument.Option:
					return @"option";
				case ExceptionArgument.Options:
					return @"options";
				case ExceptionArgument.Match:
					return @"match";
				case ExceptionArgument.Comparison:
					return @"comparison";
				default:
					return string.Empty;
			}
		}

		
	}

	internal enum ExceptionArgument
	{
		Name,
		Value,
		Item,
		Items,
		Option,
		Options,
		Match,
		Comparison
	}
}