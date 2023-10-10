using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;

namespace CashFlowzBackend.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserViewModel, UserDto>();
            CreateMap<LoginResultViewModel, LoginResultDto>();
            CreateMap<BudgetViewModel, BudgetDto>();
            CreateMap<CategoryViewModel, CategoryDto>();
            CreateMap<IncomeViewModel, IncomeDto>();
            CreateMap<ExpenseViewModel, ExpenseDto>();
            CreateMap<TransactionViewModel, TransactionDto>();
        }
    }

}
