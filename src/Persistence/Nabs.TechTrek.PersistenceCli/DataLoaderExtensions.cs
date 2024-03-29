﻿namespace Nabs.TechTrek.PersistenceCli;

public static class DataLoaderExtensions
{

	public static void AddItemsFromResourceFile<TEntity>(this TechTrekDbContext dbContext, string resourceFileName, bool generateId = false)
		where TEntity : EntityBase<Guid>
	{
		var items = GetJson<TEntity>(x => x.EndsWith(resourceFileName));
		if (generateId)
		{
			foreach (var item in items)
			{
				item.Id = Guid.NewGuid();
			}
		}
		dbContext.AddRange(items);
	}

	public static void AddItemsFromResourceFile<TEntity>(this TechTrekDbContext dbContext, string resourceFileName)
		where TEntity : EntityBase<int>
	{
		var items = GetJson<TEntity>(x => x.EndsWith(resourceFileName));
		dbContext.AddRange(items);
	}

	private static List<TEntity> GetJson<TEntity>(Func<string, bool> predicate) where TEntity : class
	{
		var assembly = typeof(DataLoaderExtensions).Assembly;
		var resourceFileName = assembly.GetManifestResourceNames()
			.FirstOrDefault(predicate)
			?? throw new InvalidOperationException("Resource file not found.");

		var resourceStream = assembly.GetManifestResourceStream(resourceFileName);

		using var reader = new StreamReader(resourceStream!, Encoding.UTF8);
		var text = reader.ReadToEnd();

		var result = DefaultJsonSerializer.Deserialize<List<TEntity>>(text);
		return result!;
	}
}