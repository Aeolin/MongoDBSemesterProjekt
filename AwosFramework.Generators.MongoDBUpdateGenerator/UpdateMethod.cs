﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwosFramework.Generators.MongoDBUpdateGenerator
{
	internal class UpdateMethod
	{
		public string TargetClassName { get; set; }
		public string MethodName { get; set; }
		public UpdateProperty[] Properties { get; set; }
		public bool UsePartialClass { get; set; }

		public UpdateMethod(string targetClass, string methodName, UpdateProperty[] properties,
			bool usePartialClass = Constants.MarkerAttribute_UsePartialClass_DefaultValue
		)
		{
			TargetClassName=targetClass;
			MethodName=methodName;
			Properties=properties;
			UsePartialClass =usePartialClass;
		}

	}
}
