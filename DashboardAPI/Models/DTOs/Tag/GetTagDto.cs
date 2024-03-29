﻿namespace DashboardAPI.Models.DTOs.Tag
{
    /// <summary>
    /// GET Dto type of <see cref="Tag"/>.
    /// </summary>
    public class GetTagDto : ADto, ITagDto
    {
        public string Name { get; set; }
    }
}
