using Specialist.Entities.Context.ViewModel;

namespace Specialist.Services.Order
{
    public interface IEditExamVMService {
        EditExamVM Get(decimal examID);
        void Update(EditExamVM model);
    }
}