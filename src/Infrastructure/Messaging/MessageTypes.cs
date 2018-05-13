namespace Pitstop.Infrastructure.Messaging
{
    /// <summary>
    /// Complete list of message types in the system.
    /// </summary>
    public enum MessageTypes
    {
        // General Comamnds or Events
        Unknown,

        // Customer Commands and Events
        RegisterCustomer,

        CustomerRegistered,

        // Logistics Commands and Events
        RegisterTransport,
        UpdateTransport,
        RemoveTransport,

        TransportRegistered,
        TransportUpdated,
        TransportRemoved,

        // Vehicle Commands and Events
        RegisterVehicle,

        VehicleRegistered,

        // Workshop Commands and Events
        PlanMaintenanceJob,
        FinishMaintenanceJob,

        WorkshopPlanningCreated,
        MaintenanceJobPlanned,
        MaintenanceJobFinished,

        // Time Events
        DayHasPassed,
    }
}
