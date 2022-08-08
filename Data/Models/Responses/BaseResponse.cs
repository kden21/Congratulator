﻿using Congratulator.Data.Models.Enums;

namespace Congratulator.Data.Models.Responses
{
	public class BaseResponse<T> : IBaseResponse<T>
	{
		public string? Description { get; set; }
		public StatusCode StatusCode { get; set; }
		public T? Data { get; set; }
	}
	public interface IBaseResponse<T>
	{
		T? Data { get; set; }
	}
}
