﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBSemesterProjekt.ApiModels;
using MongoDBSemesterProjekt.Authorization;
using MongoDBSemesterProjekt.Models;
using MongoDBSemesterProjekt.Utils;
using System.ComponentModel.DataAnnotations;
using AwosFramework.Generators.MongoDBUpdateGenerator.Extensions;

namespace MongoDBSemesterProjekt.Controllers
{
	[ApiController]
	[Route("api/v1/admin")]
	public class AdminController : HtmxBaseController
	{
		private static readonly FindOneAndUpdateOptions<GroupModel> ReturnUpdatedOptions = new FindOneAndUpdateOptions<GroupModel>
		{
			ReturnDocument = ReturnDocument.After
		};

		public AdminController(IMongoDatabase dataBase, IMapper mapper) : base(dataBase, mapper)
		{
		}

		[HttpGet("groups")]
		[ProducesResponseType<CursorResult<ApiGroup[], ObjectId>>(StatusCodes.Status200OK)]
		[Permission("admin/get-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> ListGroupsAsync([FromQuery][Range(1, 100)]int limit = 20, [FromQuery]ObjectId? cursor = null) 
		{
			cursor ??= ObjectId.Empty;
			var groups = await _db.GetCollection<GroupModel>(GroupModel.CollectionName)
				.Find(x => x.Id > cursor)
				.Limit(limit)
				.ToListAsync();
		
			ObjectId? lastObject = groups.Count == limit ? groups.Last().Id : null;
			return Ok(CursorResult.Create(lastObject, _mapper.Map<ApiGroup[]>(groups)));
		}

		[HttpPost("groups")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Permission("admin/create-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> CreateGroupAsync([FromBody] ApiGroup group)
		{
			var model = _mapper.Map<GroupModel>(group);
			await _db.GetCollection<GroupModel>(GroupModel.CollectionName).InsertOneAsync(model);
			return Ok(_mapper.Map<ApiGroup>(model));
		}

		[HttpDelete("groups/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/delete-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> DeleteGroupAsync([FromRoute] string slug)
		{
			var result = await _db.GetCollection<GroupModel>(GroupModel.CollectionName).DeleteOneAsync(x => x.Slug == slug);
			if (result.DeletedCount == 0)
				return NotFound();

			return Ok();
		}

		[HttpPut("groups/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/update-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> UpdateGroupAsync([FromRoute] string slug, [FromForm][FromBody] ApiGroupUpdateRequest update)
		{	
			var result = await _db.GetCollection<GroupModel>(GroupModel.CollectionName)
				.FindOneAndUpdateAsync(x => x.Slug == slug, update.ToUpdate(), ReturnUpdatedOptions);

			if (result == null)
				return NotFound();
			
			return Ok(_mapper.Map<ApiGroup>(result));
		}

		[HttpGet("groups/{slug}")]
		[ProducesResponseType<ApiGroup>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/get-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> GetGroupAsync([FromRoute] string slug)
		{
			var group = await _db.GetCollection<GroupModel>(GroupModel.CollectionName).Find(x => x.Slug == slug).FirstOrDefaultAsync();
			if (group == null)
				return NotFound();

			return Ok(_mapper.Map<ApiGroup>(group));
		}

		[HttpPut("groups/{slug}/permissions")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/update-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> UpdateGroupPermissionsAsync([FromRoute] string slug, [FromForm][FromBody] ApiGroupSetPermissionRequest permissions)
		{
			var result = await _db.GetCollection<GroupModel>(GroupModel.CollectionName)
				.FindOneAndUpdateAsync(x => x.Slug == slug, permissions.ToUpdateAddPermission(), ReturnUpdatedOptions);
			
			if (result == null)
				return NotFound();
			
			return Ok(_mapper.Map<ApiGroup>(result));
		}

		[HttpDelete("groups/{slug}/permissions")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/update-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> RemoveGroupPermissionsAsync([FromRoute] string slug, [FromForm][FromBody] ApiGroupSetPermissionRequest permissions)
		{
			var result = await _db.GetCollection<GroupModel>(GroupModel.CollectionName)
				.FindOneAndUpdateAsync(x => x.Slug == slug, permissions.ToUpdateRemovePermission(), ReturnUpdatedOptions);
			
			if (result == null)
				return NotFound();
			
			return Ok(_mapper.Map<ApiGroup>(result));
		}

		[HttpGet("groups/{slug}/permissions")]
		[ProducesResponseType<string[]>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/get-group", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> GetGroupPermissionsAsync([FromRoute] string slug)
		{
			var group = await _db.GetCollection<GroupModel>(GroupModel.CollectionName).Find(x => x.Slug == slug).FirstOrDefaultAsync();
			if (group == null)
				return NotFound();

			return Ok(group.Permissions);
		}

		[HttpGet("users")]
		[ProducesResponseType<CursorResult<ApiUser[], ObjectId>>(StatusCodes.Status200OK)]
		[Permission("admin/get-user", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> ListUsersAsync([FromQuery][Range(1, 100)] int limit = 20, [FromQuery] ObjectId? cursor = null)
		{
			cursor ??= ObjectId.Empty;
			var users = await _db.GetCollection<UserModel>(UserModel.CollectionName)
				.Find(x => x.Id > cursor)
				.Limit(limit)
				.ToListAsync();

			ObjectId? lastObject = users.Count == limit ? users.Last().Id : null;
			return Ok(CursorResult.Create(lastObject, _mapper.Map<ApiUser[]>(users)));
		}

		[HttpGet("users/{id}")]
		[ProducesResponseType<ApiUser>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/get-user", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> GetUserAsync([FromRoute] ObjectId id)
		{
			var user = await _db.GetCollection<UserModel>(UserModel.CollectionName).Find(x => x.Id == id).FirstOrDefaultAsync();
			if (user == null)
				return NotFound();
			
			return Ok(_mapper.Map<ApiUser>(user));
		}

		[HttpGet("users/{id}/groups")]
		[ProducesResponseType<ApiGroup[]>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Permission("admin/get-user", Constants.ADMIN_ROLE)]
		public async Task<IActionResult> GetUserGroupsAsync([FromRoute] ObjectId id)
		{
			var user = await _db.GetCollection<UserModel>(UserModel.CollectionName).Find(x => x.Id == id).FirstOrDefaultAsync();
			if (user == null)
				return NotFound();
			
			var groups = await _db.GetCollection<GroupModel>(GroupModel.CollectionName).Find(x => user.Groups.Contains(x.Slug)).ToListAsync();
			return Ok(_mapper.Map<ApiGroup[]>(groups));
		}
	}
}
