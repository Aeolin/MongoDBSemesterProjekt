﻿using AutoMapper;
using AwosFramework.Generators.MongoDBUpdateGenerator;
using MongoDB.Bson;
using MongoDBSemesterProjekt.Database.Models;

namespace MongoDBSemesterProjekt.Api.Models
{
	[AutoMap(typeof(FieldMatchModel))]
	[MongoDBUpdate(typeof(RouteTemplateModel), NestedProperty = "Fields[$]", MethodName = "ToUpdate")]
	public class ApiFieldMatchModel
	{
		public string ParameterName { get; set; }
		
		[UpdateProperty(MethodName = "ToUpdate")]
		public string DocumentFieldName { get; set; }
		
		[UpdateProperty(MethodName = "ToUpdate")]
		public MatchKind MatchKind { get; set; }
		
		[UpdateProperty(MethodName = "ToUpdate")]
		public BsonType BsonType { get; set; }
		
		[UpdateProperty(MethodName = "ToUpdate")]
		public bool IsOptional { get; set; }
		
		[UpdateProperty(MethodName = "ToUpdate")]
		public bool IsNullable { get; set; }
	}
}
