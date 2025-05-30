﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwosFramework.Generators.MongoDBUpdateGenerator
{
	public static class Constants
	{
		public const string NameSpace = "AwosFramework.Generators.MongoDBUpdateGenerator";
		public const string ExtensionsNameSpace = "AwosFramework.Generators.MongoDBUpdateGenerator.Extensions";
		public const string ExtensionClassNameFormat = "{0}UpdateExtensions";

		public const string MarkerAttributeClassName = "MongoDBUpdateAttribute";
		public const string MarkerAttributeClassFullName = $"{NameSpace}.{MarkerAttributeClassName}";

		public const string MarkerAttribute_IgnoreUnmarkedProperties_PropertyName = "IgnoreUnmarkedProperties";
		public const bool MarkerAttribute_IgnoreUnmarkedProperties_DefaultValue = false;

		public const string MarkerAttribute_MethodName_PropertyName = "MethodName";
		public const string MarkerAttribute_MethodName_DefaultValue = "ToUpdate";

		public const string MarkerAttribute_UsePartialClass_PropertyName = "UsePartialClass";
		public const bool MarkerAttribute_UsePartialClass_DefaultValue = true;

		public const string MarkerAttribute_NestedProperty_PropertyName = "NestedProperty";
		public const string MarkerAttribute_NestedProperty_DefaultValue = null;


		public static readonly string MarkerAttributeClass =
		$$"""
		using System;

		namespace {{NameSpace}}
		{
			[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
			public class {{MarkerAttributeClassName}} : Attribute
			{
				public Type EntityType { get; set; }
				public bool {{MarkerAttribute_IgnoreUnmarkedProperties_PropertyName}} { get; set; } = {{(MarkerAttribute_IgnoreUnmarkedProperties_DefaultValue ? "true" : "false")}};
				public bool {{MarkerAttribute_UsePartialClass_PropertyName}} { get; set; } = {{(MarkerAttribute_UsePartialClass_DefaultValue ? "true" : "false")}};
				public string? {{MarkerAttribute_MethodName_PropertyName}} { get; set; } = {{(MarkerAttribute_MethodName_DefaultValue == null ? "null" : $"\"{MarkerAttribute_MethodName_DefaultValue}\"")}};
				public string? {{MarkerAttribute_NestedProperty_PropertyName}} { get; set; } = {{(MarkerAttribute_NestedProperty_DefaultValue == null ? "null" : $"\"{MarkerAttribute_NestedProperty_DefaultValue}\"")}};

				public {{MarkerAttributeClassName}}(Type entityType)
				{
					this.EntityType = entityType;
				}
			}
		}
		""";

		public const string UpdatePropertyAttributeClassName = "UpdatePropertyAttribute";
		public const string UpdatePropertyAttributeClassFullName = $"{NameSpace}.{UpdatePropertyAttributeClassName}";
		public const string UpdatePropertyAttribute_TargetPropertyName_PropertyName = "TargetPropertyName";
		public const string UpdatePropertyAttribute_TargetPropertyName_DefaultValue = null;
		public const string ModelParameterName = "apiModel";


		public const string UpdatePropertyAttribute_IgnoreNull_PropertyName = "IgnoreNull";
		public const bool UpdatePropertyAttribute_IgnoreNull_DefaultValue = true;

		public const string UpdatePropertyAttribute_IgnoreEmpty_PropertyName = "IgnoreEmpty";
		public const bool UpdatePropertyAttribute_IgnoreEmpty_DefaultValue = true;

		public const string UpdatePropertyAttribute_CollectionHandling_PropertyName = "CollectionHandling";
		internal const CollectionHandling UpdatePropertyAttribute_CollectionHandling_DefaultValue = CollectionHandling.Set;

		public const string UpdatePropertyAttribute_MethodName_PropertyName = "MethodName";
		public const string UpdatePropertyAttribute_MethodName_DefaultValue = MarkerAttribute_MethodName_DefaultValue;

		public const string UpdatePropertyAttribute_ApplyToAllMethods_PropertyName = "ApplyToAllMethods";
		public const bool UpdatePropertyAttribute_ApplyToAllMethods_DefaultValue = false;

		public const string UpdatePropertyAttribute_UseStringEmpty_PropertyName = "UseStringEmpty";
		public const bool UpdatePropertyAttribute_UseStringEmpty_DefaultValue = true;

		public const string UpdatePropertyAttribute_IsSourceArray_PropertyName = "IsSourceArray";
		public const bool UpdatePropertyAttribute_IsSourceArray_DefaultValue = false;

		public const string UpdatePropertyAttribute_AppendSourceExpression_PropertyName = "AppendSourceExpression";
		public const string UpdatePropertyAttribute_AppendSourceExpression_DefaultValue = null;

		public static readonly string UpdatePropertyAttributeClass =
		$$"""
		using System;

		namespace {{NameSpace}}
		{
			[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
			public class {{UpdatePropertyAttributeClassName}} : Attribute
			{
				public string? {{UpdatePropertyAttribute_TargetPropertyName_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_TargetPropertyName_DefaultValue == null ? "null" : $"\"{UpdatePropertyAttribute_TargetPropertyName_DefaultValue}\"")}};
				public string? {{UpdatePropertyAttribute_MethodName_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_MethodName_DefaultValue == null ? "null" : $"\"{UpdatePropertyAttribute_MethodName_DefaultValue}\"")}};
				public string? {{UpdatePropertyAttribute_AppendSourceExpression_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_AppendSourceExpression_DefaultValue == null ? "null" : $"\"{UpdatePropertyAttribute_AppendSourceExpression_DefaultValue}\"")}};
				public bool {{UpdatePropertyAttribute_IgnoreNull_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_IgnoreNull_DefaultValue ? "true" : "false")}};
				public bool {{UpdatePropertyAttribute_IgnoreEmpty_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_IgnoreEmpty_DefaultValue ? "true" : "false")}};
				public bool {{UpdatePropertyAttribute_ApplyToAllMethods_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_ApplyToAllMethods_DefaultValue ? "true" : "false")}};
				public bool {{UpdatePropertyAttribute_UseStringEmpty_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_UseStringEmpty_DefaultValue ? "true" : "false")}};	
				public bool {{UpdatePropertyAttribute_IsSourceArray_PropertyName}} { get; set; } = {{(UpdatePropertyAttribute_IsSourceArray_DefaultValue ? "true" : "false")}};
				public {{nameof(CollectionHandling)}} {{UpdatePropertyAttribute_CollectionHandling_PropertyName}} { get; set; } = {{nameof(CollectionHandling)}}.{{UpdatePropertyAttribute_CollectionHandling_DefaultValue}};				
			}
		}
		""";

		public const string UpdatePropertyIgnoreAttributeClassName = "UpdatePropertyIgnoreAttribute";
		public const string UpdatePropertyIgnoreAttributeClassFullName = $"{NameSpace}.{UpdatePropertyIgnoreAttributeClassName}";
		public const string UpdatePropertyIgnoreAttributeClass =
		$$"""
		using System;

		namespace {{NameSpace}}
		{
			[AttributeUsage(AttributeTargets.Property)]
			public class {{UpdatePropertyIgnoreAttributeClassName}} : Attribute
			{
			}
		}
		""";
		public static readonly DiagnosticDescriptor CollectionHandlingNotApplicable = new DiagnosticDescriptor(
			"UPD001",
			"Only applicable to Collections",
			$"{nameof(CollectionHandling)} is only applicable to properties with array or enumerable like type",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);


		public static readonly DiagnosticDescriptor IgnoreEmptyNotApplicable = new DiagnosticDescriptor(
			"UPD002",
			"Only applicable to Collections",
			$"{nameof(UpdateProperty.IgnoreEmpty)} is only applicable to properties with array or enumerable like type",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor MethodNameAlreadyExists = new DiagnosticDescriptor(
			"UPD003",
			"Method Name already exists",
			$"Method with the name {{0}} already exists in the class",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor CollectionHandlingNotSupported = new DiagnosticDescriptor(
			"UPD004",
			"CollectionHandling not supported",
			$"{{0}} is not supported for {UpdatePropertyAttribute_CollectionHandling_PropertyName}",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor UseStringEmptyNotApplicable = new DiagnosticDescriptor(
			"UPD005",
			"Only applicable to string properties",
			$"{UpdatePropertyAttribute_UseStringEmpty_PropertyName} is only applicable to string properties",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor PartialClassMissing = new DiagnosticDescriptor(
			"UPD006",
			"Not an partial class",
			$"{MarkerAttribute_UsePartialClass_PropertyName} was set but class {{0}} is not marked as partial",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor IgnoreNullNotApplicable = new DiagnosticDescriptor(
			"UPD007",
			"Not nullable",
			$"{UpdatePropertyAttribute_IgnoreNull_PropertyName} is only applicable to reference types or nullable value types",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor PropertyNotFound = new DiagnosticDescriptor(
			"UPD008",
			"Property not found",
			$"Property {{0}} not found in the target class {{1}}",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor PropertyNotEnumerable = new DiagnosticDescriptor(
			"UPD009",
			"Property not enumerable",
			$"Property {{0}} of {{1}} is not enumerable but the source property is",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static DiagnosticDescriptor IsSourceArrayNotApplicable = new DiagnosticDescriptor(
			"UPD010",
			"IsSourceArray not Applicable here",
			$"{UpdatePropertyAttribute_IsSourceArray_PropertyName} is only applicable when placed on class",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor TargetPropertyNameMissing = new DiagnosticDescriptor(
			"UPD011",
			$"{UpdatePropertyAttribute_TargetPropertyName_PropertyName} missing",
			$"{UpdatePropertyAttribute_TargetPropertyName_PropertyName} is missing in the attribute, it needs to be set if the attribute is added to a class",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor ClassNotFound = new DiagnosticDescriptor(
			"UPD012",
			"Class not found",
			$"Class {{0}} not found in AST",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);

		public static readonly DiagnosticDescriptor InvalidNestedProperty = new DiagnosticDescriptor(
			"UPD013",
			"Invalid Nested Property",
			$"Nested property {{0}} is not found in the target class {{1}}",
			"Usage",
			DiagnosticSeverity.Error,
			true
		);
	}
}
