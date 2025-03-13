﻿using AutoMapper;
using AwosFramework.Generators.MongoDBUpdateGenerator;
using MongoDBSemesterProjekt.Database.Models;

namespace MongoDBSemesterProjekt.Api.Models
{
	[AutoMap(typeof(TemplateModel))]
	[MongoDBUpdate(typeof(CollectionModel), NestedProperty = "Templates[$]", IgnoreUnmarkedProperties = true)]
	public class ApiTemplate
	{
		public string Slug { get; set; }

		[UpdateProperty]
		public bool SingleItem { get; set; }

		[UpdateProperty]
		public string? Template { get; set; }

		[UpdateProperty]
		public bool Disabled { get; set; }
	}
}
