using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Supplier;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface ISupplierService
    {
        IDataResult<Supplier> GetById(int supplierId);
        IDataResult<List<Supplier>> GetList();
        IDataResult<int> Add(SupplierForAddDto supplierForAddDto);
        IResult Update(SupplierForUpdateDto supplierForUpdateDto);
        IResult Delete(int supplierId);
    }
}
