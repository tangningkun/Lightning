using System.Collections.Generic;

namespace Lightning.Core.ListResult
{
    //
    // 摘要:
    //     This interface is defined to standardize to return a list of items to clients.
    //
    // 类型参数:
    //   T:
    //     Type of the items in the Abp.Application.Services.Dto.IListResult`1.Items list
    public interface IListResult<T>
    {
        //
        // 摘要:
        //     List of items.
        IReadOnlyList<T> Items { get; set; }
    }
}