﻿using AwosFramework.Generators.MongoDBUpdateGenerator;
using MongoDBSemesterProjekt.Database.Models;

namespace MongoDBSemesterProjekt.ApiModels.Requests
{
	[MongoDBUpdate(typeof(UserModel), MethodName = "ToUserAddGroup")]
	[MongoDBUpdate(typeof(UserModel), MethodName = "ToUserRemoveGroup")]
	public class ApiSetGroupRequest
	{
		[UpdateProperty(MethodName = "ToUserAddGroup", CollectionHandling = CollectionHandling.AddToSet)]
		[UpdateProperty(MethodName = "ToUserRemoveGroup", CollectionHandling = CollectionHandling.PullAll)]
		public string[] Groups { get; set; }
	}
}
