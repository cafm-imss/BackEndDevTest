# Backend Developer Assessment Tasks

## Overview
This assessment evaluates your ability to work with Entity Framework Core, Web APIs, real-time communication, and Angular frontend development. You'll be converting a stored procedure-based system to a modern web application.

## Prerequisites
- SQL Server 2022
- Visual Studio 2022
- .NET 8.0 SDK
- Node.js & npm
- Angular CLI

## Getting Started

1. Clone this repository
2. Create a new branch named `candidate-[your-name]`
3. Restore the database provided in `db/cmms-test.bak`

## Tasks

### Task 1: Backend Development
**Duration: 1.5 hours**

#### Objectives
1. **Database First Approach**
   - Create a new solution with three projects:
     ```
     CAFM.API (ASP.NET Core Web API)
     CAFM.Core (Class Library)
     CAFM.Database (Class Library)
     ```
   - Use Entity Framework Core to scaffold existing tables:
     - WorkOrders
     - WorkOrderDetails
     - TaskPriorities
     - TaskStatues
     - Assets
     - Messages_System
   - Create and apply initial migration (comment out Up/Down content)

2. **Service Layer**
   - Convert `WorkOrdersSave` stored procedure to C# service
   - Implement:
     - Transaction management
     - Validation rules
     - Internal number generation
     - Multilingual support

3. **API Development**
   - Create WorkOrderController with endpoints:
     ```
     POST /api/workorder
     GET /api/workorder/{id}
     PUT /api/workorder/{id}/status
     ```
   - Document using Swagger

4. **Testing**
   - Provide Postman collection testing all endpoints

5. **Deliverable**
   - Commit with message: `feat: implement work order core functionality`

### Task 2: Real-time Updates
**Duration: 1 hours**

#### Objectives
1. **SignalR Integration**
   - Add SignalR Hub
   - Implement work order update notifications
   - Handle client subscriptions
       - Work order status changes
       - Assignment changes
       - Priority updates
       - New work orders
    - Handle client subscriptions by location/company

2. **Service Layer Updates**
   - Modify service to broadcast changes
   - Implement connection management
   - Handle errors appropriately

3. **Testing**
   - Update Postman collection with SignalR tests

4. **Deliverable**
   - Commit with message: `feat: add real-time notifications`

### Task 3: Angular Frontend
**Duration: 1 hours**

#### Objectives
1. **Angular Setup**
   ```bash
   ng new cafm-client
   cd cafm-client
   ng add @microsoft/signalr
   ```

2. **Features**
   - Work order list component with two-level expansion:
     ```
     WorkOrder #1001 - Emergency AC Repair
     ├── Asset: First Floor AC Unit
     └── Status History
         ├── New (2024-02-01 10:00)
         ├── In Progress (2024-02-01 11:30)
         └── Completed (2024-02-01 14:15)
     ```
   - Real-time updates via SignalR
   - Error handling
   - Loading states

3. **Deliverable**
   - Commit with message: `feat: add angular frontend`

## Evaluation Criteria
- Code organization and architecture
- Proper use of Entity Framework Core
- Error handling and validation
- Transaction management
- Real-time functionality implementation
- Frontend component design
- Code quality and best practices
- Git commit organization

## Submission Instructions
1. Push all changes to your branch
2. Create a pull request to main
3. Include in PR description:
   - Setup instructions
   - Any assumptions made
   - Known limitations
4. Attach Postman collection

## Notes
- Keep commits atomic and well-documented
- Follow best practices for each technology
- Ensure proper error handling throughout
- Maintain consistent code style
- Write clear documentation