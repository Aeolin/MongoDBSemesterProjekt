﻿using Microsoft.Extensions.Primitives;
using MongoDBSemesterProjekt.Database.Models;
using MongoDBSemesterProjekt.Services.TemplateRouter;
using MongoDBSemesterProjekt.Utils;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Frozen;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using YamlDotNet.Core;

namespace MongoDBSemesterProjekt.Services.Pagination
{
	public struct PaginationValues
	{
		public string? CursorNext { get; init; }
		public string? CursorPrevious { get; init; }
		public int Limit { get; init; }
		public bool Ascending { get; init; }
		public IEnumerable<string> Columns { get; init; }

		public const string LIMIT_KEY = "limit";
		public const string CURSOR_PREV_KEY = "cursorPrevious";
		public const string CURSOR_NEXT_KEY = "cursorNext";
		public const string ASCENDING_KEY = "asc";
		public const string COLUMNS_KEY = "orderBy";
		public static readonly FrozenSet<string> PAGINATION_VALUES = [LIMIT_KEY, CURSOR_PREV_KEY, CURSOR_NEXT_KEY, ASCENDING_KEY, COLUMNS_KEY];

		public PaginationValues(string? cursorNext, string? cursorPrev, int limit, bool ascending, IEnumerable<string> columns)
		{
			CursorNext = cursorNext;
			CursorPrevious = cursorPrev;
			Limit = limit;
			Ascending = ascending;
			Columns = columns;
		}

		public static PaginationValues FromRouteMatch(RouteMatch match)
		{
			var cursorNext = match.QueryValues.GetParsedValueOrDefault<string?>(CURSOR_NEXT_KEY, null);
			var cursorPrevious = match.QueryValues.GetParsedValueOrDefault<string?>(CURSOR_PREV_KEY, null);
			var limit = match.QueryValues.GetParsedValueOrDefault<int>(LIMIT_KEY, match.RouteTemplateModel.PaginationLimit);
			var ascending = match.QueryValues.GetParsedValueOrDefault<bool>(ASCENDING_KEY, true);
			var columns = match.QueryValues.GetParsedValueOrDefault<IEnumerable<string>>(COLUMNS_KEY, match.RouteTemplateModel.PaginationColumns ?? ["_id"]);
			return new PaginationValues(cursorNext, cursorPrevious, limit, ascending, columns);
		}
	}
}
