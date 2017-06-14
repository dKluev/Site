namespace Specialist.Services.Order
{
    public interface ISpecialistExportService {
        void Export(decimal orderID, bool checkIfExists, string eduDocTypeTC);
    	void Export(Entities.Context.Order order, bool checkIfExists);

        void SetDVP(decimal sigId);
    }
}