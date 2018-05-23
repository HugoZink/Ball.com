﻿namespace Pitstop.Infrastructure.Messaging
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
        SupportMessageSent,
        SendSupportMessage,

        // Logistics Commands and Events
        RegisterTransport,
        UpdateTransport,
        RemoveTransport,

        TransportRegistered,
        TransportUpdated,
        TransportRemoved,

        // Vehicle Commands and Events
        VehicleRegistered,
        WorkshopPlanningCreated,
        MaintenanceJobPlanned,
        MaintenanceJobFinished,

        RegisterVehicle,
        PlanMaintenanceJob,
        FinishMaintenanceJob,

        // Products Commands and Events
        AddProduct,
        UpdateProduct,

        NewProductAdded,
        ProductUpdated,

        // Order commands and events
        CreateOrder,
        UpdateOrder,
        DeleteOrder,
        PlaceOrder,
		NewProductAdded,
		ProductUpdated,
	    
	    // Shipping Events and Commands
	    OrderShipped,
	    ShipOrder,

		// Payment Commands and Events
		PayOrder,
		OrderPayed,

		// Warehouse Commands and Events
		RegisterPackage,

		PackageRegistered,

		// Order commands and events
		CreateOrder,
		UpdateOrder,
		DeleteOrder,
		PlaceOrder,

        OrderCreated,
        OrderUpdated,
        OrderDeleted,
        OrderPlaced,

		// Time Events
		DayHasPassed,
		DayHasBegun,
    }
}
