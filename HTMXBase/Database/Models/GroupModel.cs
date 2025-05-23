﻿using AwosFramework.Generators.MongoDBUpdateGenerator;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HTMXBase.Database.Models
{

	[MongoDBUpdate(typeof(GroupModel))]
	public class GroupModel : EntityBase
	{
		public const string CollectionName = "groups";

		[Index(IsUnique = true)]
		public required string Slug { get; set; }		
		public required string Name { get; set; }	
		public string? Description { get; set; }

		[UpdateProperty(CollectionHandling = CollectionHandling.AddToSet)]
		public IList<string> Permissions { get; set; } = new List<string>();
	}
}
