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
		SendSupportMessage,
        RegisterVehicle,
        PlanMaintenanceJob,
        FinishMaintenanceJob,

        // Events
        DayHasPassed,
        CustomerRegistered,
		SupportMessageSent,
        VehicleRegistered,
        WorkshopPlanningCreated,
        MaintenanceJobPlanned,
        MaintenanceJobFinished
    }
}
