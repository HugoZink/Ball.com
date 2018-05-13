namespace Pitstop.Infrastructure.Messaging
{
    /// <summary>
    /// Complete list of message types in the system.
    /// </summary>
    public enum MessageTypes
    {
        // General
        Unknown, 

        // Commands
        RegisterCustomer,
        RegisterVehicle,
        AddProduct,
		PlanMaintenanceJob,
        FinishMaintenanceJob,

        // Events
        DayHasPassed,
        CustomerRegistered,
        VehicleRegistered,
        NewProductAdded,
		WorkshopPlanningCreated,
        MaintenanceJobPlanned,
        MaintenanceJobFinished
    }
}
