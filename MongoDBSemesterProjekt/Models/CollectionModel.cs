﻿using System.Text.Json;

namespace MongoDBSemesterProjekt.Models
{
	public class CollectionModel : EntityBase
	{
		public const string CollectionName = "collections";

		public required string Slug { get; set; }
		public required string Name { get; set; }

		public TimeSpan? CacheRetentionTime { get; set; }
		public required JsonDocument Schema { get; set; }
		public IList<TemplateModel> Templates { get; set; } = new List<TemplateModel>();
		public bool IsInbuilt { get; set; }
		public string? DefaultTemplate { get; set; }
		public string? QueryPermission { get; set; }
		public string? InsertPermission { get; set; }
		public string? ModifyPermission { get; set; }
		public string? DeletePermission { get; set; }
		public string? ComplexQueryPermission { get; set; }
	}
}
