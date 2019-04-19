using System;
using System.Collections.Generic;
using System.Text;

namespace Lightning.Core.ListResult
{
    //
    // 摘要:
    //     Implements Abp.Application.Services.Dto.IListResult`1.
    //
    // 类型参数:
    //   T:
    //     Type of the items in the Abp.Application.Services.Dto.ListResultDto`1.Items list
    public class ListResultDto<T> : IListResult<T>
    {
        //
        // 摘要:
        //     Creates a new Abp.Application.Services.Dto.ListResultDto`1 object.
        public ListResultDto() { }
        //
        // 摘要:
        //     Creates a new Abp.Application.Services.Dto.ListResultDto`1 object.
        //
        // 参数:
        //   items:
        //     List of items
        public ListResultDto(IReadOnlyList<T> items) { }

        //
        // 摘要:
        //     List of items.
        public IReadOnlyList<T> Items { get; set; }
    }
}
