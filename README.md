# driveSync App

Welcome to the driveSync App! This .NET application is designed to facilitate cheaper rides for users by allowing them to share rides with other passengers and inventory items they plan to carry along the way. It includes several controllers and features to manage passengers, drivers, rides, inventory, and bookings.

## Structure and User Functionalities:

### Admin:
- Can add, view, edit, and delete passengers.
- Can add, view, edit, and delete drivers.
- Can view, edit, and delete bookings for both passengers and drivers.

### Passengers:
- Can view bookings.
- Can create bookings.
- Can delete bookings.
- Cannot update bookings; only Admin has authorization.

### Drivers:
- Can view bookings.
- Can delete bookings.
- Cannot update bookings; only Admin has authorization.
- Can add, update, delete, and view rides.

## Incorporated Feedback:
- Added well-documented comments for all MVC and data controllers.
- Properly differentiated controller methods for drivers, passengers, and admin.
- Utilized built-in login and enhanced it by integrating the account controller for login and authorization.

## Summary of Controllers:
- RideController
- BookingController
- PassengerController
- DriverController

Each controller has corresponding data controllers. Features include drivers managing rides, passengers creating and deleting bookings, and admins overseeing both passengers and drivers, with exclusive booking update rights.

## Usage

To run the driveSync App locally:

1. Clone this repository.
2. Open the solution file in Visual Studio.
3. Configure the database connection in the appropriate configuration file.
4. Build the solution.
5. Start the application.
6. Access the app through the specified URL in your browser.

## Contributors

1. **Fazhrul Sadip**
   - Models: `Passenger.cs`
   - Controllers: `PassengerController`, `PassengerDataController`
   - Views: `Passenger`

2. **Awotunde Abraham**
   - Models: `Driver.cs`, `Ride.cs`, `Booking.cs`, `Admin`
   - Controllers: `DriverController`, `DriverDataController`, `RideController`, `RideDataController`, `AdminController`, `AdminDataController`, `BookingController`, `BookingDataController`
   - Views: `Ride`, `Driver`, `Admin`, `Booking`
